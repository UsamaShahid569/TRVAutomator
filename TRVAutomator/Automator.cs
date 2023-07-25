using IronXL;
using Microsoft.Playwright;
using Microsoft.VisualBasic.Devices;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TRVAutomator;

public class Automator
{
    public string FileName { get; set; }

    public Automator() { }
    public void Automate(Form1 form1)
    {
        FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        var book = new XSSFWorkbook(fs);

        try
        {
            var sheet = book.GetSheet("Target Relevance Verification");


            if(sheet == null)
            {
                MessageBox.Show("Sheet is Empty");
                return;
            }
            RowIndex.Keyword = ProcessIndex(sheet, 3);
            RowIndex.HR = ProcessIndex(sheet, 7);
            RowIndex.SR = ProcessIndex(sheet, 9);
            RowIndex.BR = ProcessIndex(sheet, 11);
            RowIndex.IR = ProcessIndex(sheet, 13);

            Task task = Task.Run(() => ProcessKeyword(sheet));
            task.Wait();

        }
        finally
        {
            fs.Close();
            using(FileStream stream = new FileStream("D:\\projects\\Abe\\TRVAutomator\\Resources\\outfile.xlsx", FileMode.Create, FileAccess.Write))
            {
                book.Write(stream);
            }

            // Close the workbook
            book.Close();
        }

    }

    public async Task ProcessKeyword(ISheet workSheet)
    {
        try
        {
            using var playwright = await Playwright.CreateAsync();

            var row = workSheet.GetRow(2);
            var url = row.GetCell(1).StringCellValue;
            var browserInstance = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browserInstance.NewPageAsync();

            if(url == null)
            {
                MessageBox.Show("Please add the Marketplace");
                return;
            }
            await page.GotoAsync("https://" + url);
            await ClickAsync(Selectors.LocationPopover, page);
            await page.Locator(Selectors.ZipCode).TypeAsync("10001");
            await page.GetByText("Apply").PressAsync("Enter");

            Task applyEnter = PressEnter("Submit", page);
            await Task.Run(() => applyEnter);
            Task continueEnter = PressEnter("Submit", page);
            await Task.Run(() => continueEnter);
            await page.ReloadAsync();


            for(var i = RowIndex.Keyword - 1; i >= 3; i--)
            {
                var keyWord = workSheet.GetRow(i).GetCell(3).StringCellValue?.Trim();
                await page.FillAsync(Selectors.SearchBox, string.Empty);
                await page.Locator(Selectors.SearchBox).TypeAsync(keyWord);
                await page.Keyboard.PressAsync("Enter");
                var option = await Task.Run(() => SelectRelevency(keyWord));
                var (cellNum, rowNum) = GetColumn(option);
                workSheet.GetRow(rowNum).GetCell(cellNum).SetCellValue(keyWord);
                workSheet.GetRow(i).GetCell(3).SetCellValue(string.Empty);
            }
        }
        catch(Exception ex)
        {

            throw;
        }

    }

    private string SelectRelevency(string keyWord)
    {
        var optionForm = new OptionFrom();
        optionForm.Keyword = keyWord;
        optionForm.ShowDialog();
        return optionForm.SelectedOption;
    }

    private (int , int) GetColumn(string option) => option switch
    {
        "Hyper Relevant" => (7, RowIndex.HR++),
        "Semi Relevant" => (9, RowIndex.SR++),
        "Broad Relevant" => (11, RowIndex.BR++),
        "Irrelevant" => (13, RowIndex.IR++),
    };

    private async Task ClickAsync(string locator, IPage page, int maxRetries = 3)
    {
        var element = await page.QuerySelectorAsync(locator);

        if(element is not null)
        {
            await page.ClickAsync(locator);
            return;
        }
        else
        {
            if(maxRetries > 0)
            {
                await page.ReloadAsync();
                await ClickAsync(locator, page, maxRetries - 1);
            }
            else
            {
                MessageBox.Show("Location is not getting selected");
            }

        }
    }

    private async Task PressEnter(string type, IPage page)
    {
        Selectors.InputSelector = type;
        var elementSelector = Selectors.InputSelector;
        var element = await page.QuerySelectorAsync(elementSelector);
        if(element != null)
        {
            // If the element exists, simulate pressing the "Enter" key on it
            await element.PressAsync("Enter");
        }
    }

    private int ProcessIndex(ISheet workSheet, int cellNum)
    {
        int count = workSheet.LastRowNum;
        for(int i = 2;  i < count; i++)
        {
            var row = workSheet.GetRow(i);
            var cell = row.GetCell(cellNum).StringCellValue;
            if(cell is null || String.IsNullOrEmpty(cell))
            {
                return i;
            }
        }
        return 2;
    }


    public static class Selectors
    {
        public static string ZipCode { get; set; } = "#GLUXZipUpdateInput";
        public static string LocationPopover { get; set; } = "a[id =\"nav-global-location-popover-link\"]";
        public static int KeyWordColumnIndex { get; set; } = 3;
        public static string SearchBox { get; set; } = "#twotabsearchtextbox";

        private static string InputSelectorF;
        public static string InputSelector
        {
            get
            {
                return InputSelectorF;
            }
            set
            {
                InputSelectorF = $"input[type='{value}']";
            }
        }
    }
}

public static class RowIndex
{
    public static int Keyword { get; set; }
    public static int HR { get; set; }
    public static int SR { get; set; }
    public static int BR { get; set; }
    public static int IR { get; set; }
}

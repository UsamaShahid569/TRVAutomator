using IronXL;
using Microsoft.Playwright;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
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
        var workBook = WorkBook.Load(FileName);

        try
        {
            var sheet = workBook.WorkSheets.FirstOrDefault();

            if(sheet == null)
            {
                MessageBox.Show("Sheet is Empty");
                return;
            }
            ColumnIndex.KeywordIndex = ProcessIndex(sheet, "D");
            ColumnIndex.HR = ProcessIndex(sheet, "H");
            ColumnIndex.SR = ProcessIndex(sheet, "J");
            ColumnIndex.BR = ProcessIndex(sheet, "L");
            ColumnIndex.IR = ProcessIndex(sheet, "N");

            Task task = Task.Run(() => ProcessKeyword(sheet));
            task.Wait();
            workBook.Save();

        }
        finally
        {
            workBook.Save();
        }

    }

    public async Task ProcessKeyword(WorkSheet workSheet)
    {
        try
        {
            using var playwright = await Playwright.CreateAsync();
            var browserInstance = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browserInstance.NewPageAsync();
            var url = workSheet["B3"]?.ToString();

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


            for(var i = ColumnIndex.KeywordIndex - 1; i >= 3; i--)
            {
                var keyWord = workSheet[$"D{i}"]?.ToString();
                await page.FillAsync(Selectors.SearchBox, string.Empty);
                await page.Locator(Selectors.SearchBox).TypeAsync(keyWord);
                await page.Keyboard.PressAsync("Enter");
                var option = await Task.Run(() => SelectRelevency(keyWord));
                var (column, cell) = GetColumn(option);
                workSheet[$"{column}{cell}"].Value = keyWord;
                workSheet[$"D{i}"].Value = string.Empty;
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

    private (string , int) GetColumn(string option) => option switch
    {
        "Hyper Relevant" => ("H", ColumnIndex.HR++),
        "Semi Relevant" => ("J", ColumnIndex.SR++),
        "Irrelevant" => ("N", ColumnIndex.IR++),
        "Broad Relevant" => ("L", ColumnIndex.BR++),
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

    private int ProcessIndex(WorkSheet workSheet, string column)
    {
        int count = workSheet.GetColumn(column).Count;
        for(int i = 3;  i < count; i++)
        {
            string cell = workSheet[$"{column}{i}"]?.ToString();
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

public static class ColumnIndex
{
    public static int KeywordIndex { get; set; }
    public static int HR { get; set; }
    public static int SR { get; set; }
    public static int BR { get; set; }
    public static int IR { get; set; }
}

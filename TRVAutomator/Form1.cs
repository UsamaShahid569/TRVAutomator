using IronXL;

namespace TRVAutomator
{
    public partial class Form1 : Form
    {
        public Automator _automator { get; set; }
        public Form1(Automator automator)
        {
            InitializeComponent();
            _automator = automator;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new()
            {
                Filter = "Excel files (*.xlsx)|*.xlsx"
            };
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                if(openFile.FileName == null)
                {
                    MessageBox.Show("Selected file is null");
                    return;
                }

                _automator.FileName = openFile.FileName;
            }
        }

        private void StartVerifiyingBtn_Click(object sender, EventArgs e)
        {
            _automator.Automate(this);
        }
    }
}
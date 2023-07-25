using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRVAutomator
{
    public partial class OptionFrom : Form
    {
        public OptionFrom()
        {
            InitializeComponent();
        }

        public string SelectedOption { get; private set; }

        public string Keyword
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedOption = button1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedOption = button2.Text;
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SelectedOption = button3.Text;
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SelectedOption = button4.Text;
            this.Close();
        }


    }
}

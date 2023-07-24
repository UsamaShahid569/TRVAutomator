using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SizeF = System.Drawing.SizeF;

namespace TRVAutomator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SelectFileBtn = new Button();
            StartVerifiying = new Button();
            SuspendLayout();
            // 
            // SelectFileBtn
            // 
            SelectFileBtn.Location = new Point(1, -2);
            SelectFileBtn.Name = "SelectFileBtn";
            SelectFileBtn.Size = new Size(583, 68);
            SelectFileBtn.TabIndex = 0;
            SelectFileBtn.Text = "Select File";
            SelectFileBtn.UseVisualStyleBackColor = true;
            SelectFileBtn.Click += btnSelectFile_Click;
            // 
            // StartVerifiying
            // 
            StartVerifiying.Location = new Point(1, 72);
            StartVerifiying.Name = "StartVerifiying";
            StartVerifiying.Size = new Size(583, 80);
            StartVerifiying.TabIndex = 1;
            StartVerifiying.Text = "Start Verifiying";
            StartVerifiying.UseVisualStyleBackColor = true;
            StartVerifiying.Click += StartVerifiyingBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 158);
            Controls.Add(StartVerifiying);
            Controls.Add(SelectFileBtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button SelectFileBtn;
        private Button StartVerifiying;
    }
}
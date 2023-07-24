namespace TRVAutomator
{
    partial class OptionFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(-7, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(585, 27);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(25, 45);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(249, 29);
            button1.TabIndex = 1;
            button1.Text = "Hyper Relevant";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(289, 45);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(259, 29);
            button2.TabIndex = 2;
            button2.Text = "Semi Relevant";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(25, 89);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(249, 29);
            button3.TabIndex = 3;
            button3.Text = "Irrelevant";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(289, 89);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(259, 29);
            button4.TabIndex = 4;
            button4.Text = "Broad Relevant";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // OptionFrom
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(578, 247);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "OptionFrom";
            Text = "OptionFrom";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
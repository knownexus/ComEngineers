namespace ComEngineers
{
    partial class Form1
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
            if (disposing && (components != null))
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
            Upload_Button = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // Upload_Button
            // 
            Upload_Button.Location = new Point(471, 200);
            Upload_Button.Margin = new Padding(4, 5, 4, 5);
            Upload_Button.Name = "Upload_Button";
            Upload_Button.Size = new Size(100, 35);
            Upload_Button.TabIndex = 0;
            Upload_Button.Text = "Upload File";
            Upload_Button.UseVisualStyleBackColor = true;
            Upload_Button.Click += Upload_Button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(471, 118);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 692);
            Controls.Add(label1);
            Controls.Add(Upload_Button);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Upload_Button;
        private System.Windows.Forms.Label label1;
    }
}


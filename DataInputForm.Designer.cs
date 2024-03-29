using EntityFrame.API.Commands;

namespace ComEngineers
{
    partial class DataInputForm
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
            RequestLatest = new Button();
            SessionId_Label = new Label();
            sessionListBox = new ComboBox();
            loadSessionButton = new Button();
            DisplayDataButton = new Button();
            SuspendLayout();
            // 
            // Upload_Button
            // 
            Upload_Button.Location = new Point(133, 149);
            Upload_Button.Margin = new Padding(4, 5, 4, 5);
            Upload_Button.Name = "Upload_Button";
            Upload_Button.Size = new Size(100, 44);
            Upload_Button.TabIndex = 0;
            Upload_Button.Text = "Upload File";
            Upload_Button.UseVisualStyleBackColor = true;
            Upload_Button.Click += Upload_Button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 77);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 1;
            label1.Text = "Session ID";
            // 
            // RequestLatest
            // 
            RequestLatest.Location = new Point(321, 149);
            RequestLatest.Margin = new Padding(4, 5, 4, 5);
            RequestLatest.Name = "RequestLatest";
            RequestLatest.Size = new Size(185, 44);
            RequestLatest.TabIndex = 2;
            RequestLatest.Text = "Request Latest Session";
            RequestLatest.UseVisualStyleBackColor = true;
            RequestLatest.Click += RequestLatest_Click;
            // 
            // SessionId_Label
            // 
            SessionId_Label.AutoSize = true;
            SessionId_Label.Location = new Point(186, 77);
            SessionId_Label.Margin = new Padding(4, 0, 4, 0);
            SessionId_Label.Name = "SessionId_Label";
            SessionId_Label.Size = new Size(0, 20);
            SessionId_Label.TabIndex = 3;
            // 
            // sessionListBox
            // 
            sessionListBox.FormattingEnabled = true;
            sessionListBox.Location = new Point(133, 253);
            sessionListBox.Name = "sessionListBox";
            sessionListBox.Size = new Size(151, 28);
            sessionListBox.TabIndex = 4;
            sessionListBox.Text = "Session Ids";
            sessionListBox.SelectedIndexChanged += SessionListBoxSelectedIndexChanged;
            // 
            // loadSessionButton
            // 
            loadSessionButton.Location = new Point(406, 253);
            loadSessionButton.Margin = new Padding(4, 5, 4, 5);
            loadSessionButton.Name = "loadSessionButton";
            loadSessionButton.Size = new Size(100, 28);
            loadSessionButton.TabIndex = 5;
            loadSessionButton.Text = "Load Sesion";
            loadSessionButton.UseVisualStyleBackColor = true;
            loadSessionButton.Click += LoadSessionButton_Click;
            // 
            // DisplayDataButton
            // 
            DisplayDataButton.Location = new Point(225, 335);
            DisplayDataButton.Name = "DisplayDataButton";
            DisplayDataButton.Size = new Size(150, 35);
            DisplayDataButton.TabIndex = 6;
            DisplayDataButton.Text = "Display Data";
            DisplayDataButton.UseVisualStyleBackColor = true;
            DisplayDataButton.Click += this.DisplayDataButton_Click;
            // 
            // DataInputForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 429);
            Controls.Add(DisplayDataButton);
            Controls.Add(loadSessionButton);
            Controls.Add(sessionListBox);
            Controls.Add(SessionId_Label);
            Controls.Add(RequestLatest);
            Controls.Add(label1);
            Controls.Add(Upload_Button);
            Margin = new Padding(4, 5, 4, 5);
            Name = "DataInputForm";
            Text = "Data Input Form";
            Load += DataInputForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Upload_Button;
        private System.Windows.Forms.Label label1;
        private Button RequestLatest;
        private Label SessionId_Label;
        private ComboBox sessionListBox;
        private Button loadSessionButton;
        private Button DisplayDataButton;
    }
}


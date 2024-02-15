namespace ComEngineers
{
    partial class DataDisplayForm
    {
        /// <summary>
        /// Required designer variable.d
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
            ReturnToDataInputButton = new Button();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            UpdateChart = new Button();
            SessionBox1 = new ComboBox();
            SessionBox2 = new ComboBox();
            MetricBox1 = new ComboBox();
            MetricBox2 = new ComboBox();
            ClearButton1 = new Button();
            ClearButton2 = new Button();
            SuspendLayout();
            // 
            // ReturnToDataInputButton
            // 
            ReturnToDataInputButton.Location = new Point(22, 387);
            ReturnToDataInputButton.Name = "ReturnToDataInputButton";
            ReturnToDataInputButton.Size = new Size(159, 34);
            ReturnToDataInputButton.TabIndex = 0;
            ReturnToDataInputButton.Text = "Return To Data Input";
            ReturnToDataInputButton.UseVisualStyleBackColor = true;
            ReturnToDataInputButton.Click += ReturnToDataInputButton_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Location = new Point(195, 0);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(593, 447);
            formsPlot1.TabIndex = 1;
            formsPlot1.Load += formsPlot1_Load;
            // 
            // UpdateChart
            // 
            UpdateChart.Location = new Point(22, 326);
            UpdateChart.Name = "UpdateChart";
            UpdateChart.Size = new Size(159, 29);
            UpdateChart.TabIndex = 2;
            UpdateChart.Text = "Update Chart";
            UpdateChart.UseVisualStyleBackColor = true;
            UpdateChart.Click += UpdateChart_Click;
            // 
            // SessionBox1
            // 
            SessionBox1.Location = new Point(30, 50);
            SessionBox1.Name = "SessionBox1";
            SessionBox1.Size = new Size(151, 28);
            SessionBox1.TabIndex = 3;
            SessionBox1.Text = "Session ID";
            SessionBox1.SelectedIndexChanged += SessionBox1_SelectedIndexChanged;
            // 
            // SessionBox2
            // 
            SessionBox2.FormattingEnabled = true;
            SessionBox2.Location = new Point(30, 199);
            SessionBox2.Name = "SessionBox2";
            SessionBox2.Size = new Size(151, 28);
            SessionBox2.TabIndex = 4;
            SessionBox2.Text = "Session ID";
            SessionBox2.SelectedIndexChanged += SessionBox2_SelectedIndexChanged;
            // 
            // MetricBox1
            // 
            MetricBox1.FormattingEnabled = true;
            MetricBox1.Location = new Point(30, 84);
            MetricBox1.Name = "MetricBox1";
            MetricBox1.Size = new Size(151, 28);
            MetricBox1.TabIndex = 5;
            MetricBox1.Text = "Metric";
            MetricBox1.Visible = false;
            MetricBox1.SelectedIndexChanged += MetricBox1_SelectedIndexChanged_1;
            // 
            // MetricBox2
            // 
            MetricBox2.FormattingEnabled = true;
            MetricBox2.Location = new Point(30, 233);
            MetricBox2.Name = "MetricBox2";
            MetricBox2.Size = new Size(151, 28);
            MetricBox2.TabIndex = 6;
            MetricBox2.Text = "Metric";
            MetricBox2.Visible = false;
            MetricBox2.SelectedIndexChanged += MetricBox2_SelectedIndexChanged;
            // 
            // ClearButton1
            // 
            ClearButton1.Location = new Point(53, 118);
            ClearButton1.Name = "ClearButton1";
            ClearButton1.Size = new Size(94, 29);
            ClearButton1.TabIndex = 7;
            ClearButton1.Text = "Clear";
            ClearButton1.UseVisualStyleBackColor = true;
            ClearButton1.Click += ClearButton1_Click;
            // 
            // ClearButton2
            // 
            ClearButton2.Location = new Point(53, 267);
            ClearButton2.Name = "ClearButton2";
            ClearButton2.Size = new Size(94, 29);
            ClearButton2.TabIndex = 8;
            ClearButton2.Text = "Clear";
            ClearButton2.UseVisualStyleBackColor = true;
            ClearButton2.Click += ClearButton2_Click;
            // 
            // DataDisplayForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ClearButton2);
            Controls.Add(ClearButton1);
            Controls.Add(MetricBox2);
            Controls.Add(MetricBox1);
            Controls.Add(SessionBox2);
            Controls.Add(SessionBox1);
            Controls.Add(UpdateChart);
            Controls.Add(formsPlot1);
            Controls.Add(ReturnToDataInputButton);
            Name = "DataDisplayForm";
            Text = "Data Display Form";
            Load += DataDisplayForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button ReturnToDataInputButton;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Button UpdateChart;
        private ComboBox SessionBox1;
        private ComboBox SessionBox2;
        private ComboBox MetricBox1;
        private ComboBox MetricBox2;
        private Button ClearButton1;
        private Button ClearButton2;
    }
}
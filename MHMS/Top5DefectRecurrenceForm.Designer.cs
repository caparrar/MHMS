namespace MHMS
{
    partial class Top5DefectRecurrenceForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.Top5DefectChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Top5DefectChart)).BeginInit();
            this.SuspendLayout();
            // 
            // Top5DefectChart
            // 
            this.Top5DefectChart.BorderlineColor = System.Drawing.Color.Silver;
            this.Top5DefectChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.Top5DefectChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Top5DefectChart.Legends.Add(legend1);
            this.Top5DefectChart.Location = new System.Drawing.Point(329, 40);
            this.Top5DefectChart.Name = "Top5DefectChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Defect";
            this.Top5DefectChart.Series.Add(series1);
            this.Top5DefectChart.Size = new System.Drawing.Size(439, 375);
            this.Top5DefectChart.TabIndex = 0;
            this.Top5DefectChart.Text = "chart1";
            title1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Top 5 Defect Recurrence";
            this.Top5DefectChart.Titles.Add(title1);
            // 
            // Top5DefectRecurrenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1108, 460);
            this.Controls.Add(this.Top5DefectChart);
            this.Name = "Top5DefectRecurrenceForm";
            this.Text = "Top 5 Defect Recurrence";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Top5DefectChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Top5DefectChart;
    }
}
namespace MHMS.Forms
{
    partial class TargetSettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FiscalYearDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TargetSettingDataGrid = new System.Windows.Forms.DataGridView();
            this.LabelHeader = new System.Windows.Forms.Panel();
            this.TitleCategory = new System.Windows.Forms.Label();
            this.HistoryDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSettingDataGrid)).BeginInit();
            this.LabelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.UploadButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.FiscalYearDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(1, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(319, 35);
            this.panel3.TabIndex = 8;
            // 
            // FiscalYearDropdown
            // 
            this.FiscalYearDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FiscalYearDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FiscalYearDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiscalYearDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiscalYearDropdown.FormattingEnabled = true;
            this.FiscalYearDropdown.Location = new System.Drawing.Point(82, 2);
            this.FiscalYearDropdown.Name = "FiscalYearDropdown";
            this.FiscalYearDropdown.Size = new System.Drawing.Size(232, 28);
            this.FiscalYearDropdown.TabIndex = 2;
            this.FiscalYearDropdown.TextChanged += new System.EventHandler(this.FiscalYearDropdown_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel4.CausesValidation = false;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(76, 33);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fiscal Year";
            // 
            // UploadButton
            // 
            this.UploadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(217)))), ((int)(((byte)(167)))));
            this.UploadButton.FlatAppearance.BorderSize = 0;
            this.UploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.UploadButton.Location = new System.Drawing.Point(797, 1);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(144, 35);
            this.UploadButton.TabIndex = 7;
            this.UploadButton.Text = "UPLOAD";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ChooseFileButton);
            this.panel2.Controls.Add(this.FilePath);
            this.panel2.Location = new System.Drawing.Point(332, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(459, 35);
            this.panel2.TabIndex = 6;
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.ChooseFileButton.FlatAppearance.BorderSize = 0;
            this.ChooseFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseFileButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseFileButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChooseFileButton.Location = new System.Drawing.Point(-1, -2);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(91, 35);
            this.ChooseFileButton.TabIndex = 2;
            this.ChooseFileButton.Text = "Choose File";
            this.ChooseFileButton.UseVisualStyleBackColor = false;
            this.ChooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // FilePath
            // 
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePath.Location = new System.Drawing.Point(96, 7);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(358, 18);
            this.FilePath.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.CategoryDropdown);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(10, 11);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(319, 35);
            this.panel5.TabIndex = 5;
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CategoryDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Items.AddRange(new object[] {
            "Efficiency",
            "MH Loss Rate",
            "Parts Loss Rate",
            "COPQ Manpower Rate"});
            this.CategoryDropdown.Location = new System.Drawing.Point(83, 2);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(231, 28);
            this.CategoryDropdown.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel6.CausesValidation = false;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(77, 33);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Category";
            // 
            // TargetSettingDataGrid
            // 
            this.TargetSettingDataGrid.AllowUserToAddRows = false;
            this.TargetSettingDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.TargetSettingDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TargetSettingDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TargetSettingDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TargetSettingDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetSettingDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TargetSettingDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetSettingDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.TargetSettingDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.TargetSettingDataGrid.Location = new System.Drawing.Point(10, 110);
            this.TargetSettingDataGrid.Name = "TargetSettingDataGrid";
            this.TargetSettingDataGrid.ReadOnly = true;
            this.TargetSettingDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetSettingDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TargetSettingDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            this.TargetSettingDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.TargetSettingDataGrid.RowTemplate.Height = 42;
            this.TargetSettingDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TargetSettingDataGrid.Size = new System.Drawing.Size(941, 210);
            this.TargetSettingDataGrid.TabIndex = 6;
            this.TargetSettingDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TargetSettingDataGrid_CellFormatting);
            this.TargetSettingDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TargetSettingDataGrid_CellMouseClick);
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.LabelHeader.Controls.Add(this.TitleCategory);
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Location = new System.Drawing.Point(10, 320);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(941, 42);
            this.LabelHeader.TabIndex = 7;
            // 
            // TitleCategory
            // 
            this.TitleCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleCategory.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TitleCategory.Location = new System.Drawing.Point(0, 0);
            this.TitleCategory.Name = "TitleCategory";
            this.TitleCategory.Size = new System.Drawing.Size(941, 42);
            this.TitleCategory.TabIndex = 0;
            this.TitleCategory.Text = "HISTORY TABLE";
            this.TitleCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HistoryDataGrid
            // 
            this.HistoryDataGrid.AllowUserToAddRows = false;
            this.HistoryDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.HistoryDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.HistoryDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HistoryDataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.HistoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.HistoryDataGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HistoryDataGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.HistoryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryDataGrid.Location = new System.Drawing.Point(10, 362);
            this.HistoryDataGrid.Name = "HistoryDataGrid";
            this.HistoryDataGrid.ReadOnly = true;
            this.HistoryDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.HistoryDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            this.HistoryDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.HistoryDataGrid.RowTemplate.Height = 42;
            this.HistoryDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HistoryDataGrid.Size = new System.Drawing.Size(941, 88);
            this.HistoryDataGrid.TabIndex = 8;
            this.HistoryDataGrid.Visible = false;
            // 
            // TargetSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(961, 460);
            this.Controls.Add(this.HistoryDataGrid);
            this.Controls.Add(this.LabelHeader);
            this.Controls.Add(this.TargetSettingDataGrid);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Name = "TargetSettingForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Target Setting";
            this.Load += new System.EventHandler(this.TargetSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSettingDataGrid)).EndInit();
            this.LabelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox FiscalYearDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.DataGridView TargetSettingDataGrid;
        private System.Windows.Forms.Panel LabelHeader;
        private System.Windows.Forms.DataGridView HistoryDataGrid;
        private System.Windows.Forms.Label TitleCategory;
    }
}
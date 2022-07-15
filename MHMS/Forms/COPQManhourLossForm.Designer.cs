namespace MHMS.Forms
{
    partial class COPQManhourLossForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.TypeDropdown = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.Type = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CategoryDropdown = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.FromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SectionDropdown = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ViewGraph = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.ExportButton = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.AddCOPQButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GenerateButton);
            this.panel1.Controls.Add(this.UpdateButton);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 82);
            this.panel1.TabIndex = 0;
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(217)))), ((int)(((byte)(167)))));
            this.GenerateButton.FlatAppearance.BorderSize = 0;
            this.GenerateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.GenerateButton.Location = new System.Drawing.Point(791, 42);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(150, 35);
            this.GenerateButton.TabIndex = 20;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = false;
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(189)))), ((int)(((byte)(31)))));
            this.UpdateButton.FlatAppearance.BorderSize = 0;
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.UpdateButton.Location = new System.Drawing.Point(635, 42);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(150, 35);
            this.UpdateButton.TabIndex = 19;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = false;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.TypeDropdown);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Location = new System.Drawing.Point(0, 42);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(305, 35);
            this.panel10.TabIndex = 17;
            // 
            // TypeDropdown
            // 
            this.TypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TypeDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TypeDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeDropdown.FormattingEnabled = true;
            this.TypeDropdown.Items.AddRange(new object[] {
            "For Approval",
            "Approved",
            "Rejected",
            "Cancelled"});
            this.TypeDropdown.Location = new System.Drawing.Point(82, 4);
            this.TypeDropdown.Name = "TypeDropdown";
            this.TypeDropdown.Size = new System.Drawing.Size(218, 25);
            this.TypeDropdown.TabIndex = 2;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel11.CausesValidation = false;
            this.panel11.Controls.Add(this.Type);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(76, 33);
            this.panel11.TabIndex = 0;
            this.panel11.Paint += new System.Windows.Forms.PaintEventHandler(this.panel11_Paint);
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Type.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Type.Location = new System.Drawing.Point(19, 7);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(38, 19);
            this.Type.TabIndex = 0;
            this.Type.Text = "Type";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.ToDateTimePicker);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(634, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(307, 35);
            this.panel8.TabIndex = 18;
            // 
            // ToDateTimePicker
            // 
            this.ToDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDateTimePicker.Location = new System.Drawing.Point(81, 5);
            this.ToDateTimePicker.Name = "ToDateTimePicker";
            this.ToDateTimePicker.Size = new System.Drawing.Size(220, 23);
            this.ToDateTimePicker.TabIndex = 2;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel9.CausesValidation = false;
            this.panel9.Controls.Add(this.label4);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(76, 33);
            this.panel9.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(9, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Date To";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CategoryDropdown);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Location = new System.Drawing.Point(316, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 35);
            this.panel2.TabIndex = 16;
            // 
            // CategoryDropdown
            // 
            this.CategoryDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CategoryDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CategoryDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryDropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryDropdown.FormattingEnabled = true;
            this.CategoryDropdown.Items.AddRange(new object[] {
            "For Approval",
            "Approved",
            "Rejected",
            "Cancelled"});
            this.CategoryDropdown.Location = new System.Drawing.Point(82, 4);
            this.CategoryDropdown.Name = "CategoryDropdown";
            this.CategoryDropdown.Size = new System.Drawing.Size(218, 25);
            this.CategoryDropdown.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel5.CausesValidation = false;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(76, 33);
            this.panel5.TabIndex = 0;
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
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.FromDateTimePicker);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(326, 11);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(308, 35);
            this.panel6.TabIndex = 17;
            // 
            // FromDateTimePicker
            // 
            this.FromDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDateTimePicker.Location = new System.Drawing.Point(81, 5);
            this.FromDateTimePicker.Name = "FromDateTimePicker";
            this.FromDateTimePicker.Size = new System.Drawing.Size(220, 23);
            this.FromDateTimePicker.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel7.CausesValidation = false;
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(76, 33);
            this.panel7.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(1, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Date From";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.SectionDropdown);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(305, 35);
            this.panel3.TabIndex = 15;
            // 
            // SectionDropdown
            // 
            this.SectionDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionDropdown.FormattingEnabled = true;
            this.SectionDropdown.Items.AddRange(new object[] {
            "COPQ",
            "ST",
            "WC/CC"});
            this.SectionDropdown.Location = new System.Drawing.Point(82, 5);
            this.SectionDropdown.Name = "SectionDropdown";
            this.SectionDropdown.Size = new System.Drawing.Size(218, 23);
            this.SectionDropdown.TabIndex = 2;
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
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(210)))), ((int)(((byte)(216)))));
            this.panel12.Controls.Add(this.ViewGraph);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(10, 92);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(8);
            this.panel12.Size = new System.Drawing.Size(941, 64);
            this.panel12.TabIndex = 18;
            // 
            // ViewGraph
            // 
            this.ViewGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.ViewGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewGraph.FlatAppearance.BorderSize = 0;
            this.ViewGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewGraph.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewGraph.ForeColor = System.Drawing.Color.White;
            this.ViewGraph.Location = new System.Drawing.Point(8, 8);
            this.ViewGraph.Name = "ViewGraph";
            this.ViewGraph.Size = new System.Drawing.Size(925, 48);
            this.ViewGraph.TabIndex = 17;
            this.ViewGraph.Text = "VIEW GRAPH";
            this.ViewGraph.UseVisualStyleBackColor = false;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.SystemColors.Window;
            this.panel13.Controls.Add(this.ExportButton);
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Controls.Add(this.AddCOPQButton);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(10, 156);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(941, 47);
            this.panel13.TabIndex = 19;
            // 
            // ExportButton
            // 
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(217)))), ((int)(((byte)(167)))));
            this.ExportButton.FlatAppearance.BorderSize = 0;
            this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.ExportButton.Location = new System.Drawing.Point(160, 6);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(154, 35);
            this.ExportButton.TabIndex = 20;
            this.ExportButton.Text = "EXPORT";
            this.ExportButton.UseVisualStyleBackColor = false;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(569, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(372, 47);
            this.panel14.TabIndex = 19;
            // 
            // panel15
            // 
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.SearchBox);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Location = new System.Drawing.Point(53, 6);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(319, 35);
            this.panel15.TabIndex = 18;
            // 
            // SearchBox
            // 
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBox.Location = new System.Drawing.Point(77, 7);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(236, 18);
            this.SearchBox.TabIndex = 1;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.panel16.CausesValidation = false;
            this.panel16.Controls.Add(this.label5);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(72, 33);
            this.panel16.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(11, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Search";
            // 
            // AddCOPQButton
            // 
            this.AddCOPQButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(189)))), ((int)(((byte)(31)))));
            this.AddCOPQButton.FlatAppearance.BorderSize = 0;
            this.AddCOPQButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCOPQButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCOPQButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.AddCOPQButton.Location = new System.Drawing.Point(0, 6);
            this.AddCOPQButton.Name = "AddCOPQButton";
            this.AddCOPQButton.Size = new System.Drawing.Size(154, 35);
            this.AddCOPQButton.TabIndex = 17;
            this.AddCOPQButton.Text = "ADD COPQ";
            this.AddCOPQButton.UseVisualStyleBackColor = false;
            this.AddCOPQButton.Click += new System.EventHandler(this.AddCOPQButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 203);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(941, 247);
            this.dataGridView1.TabIndex = 20;
            // 
            // COPQManhourLossForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(961, 460);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "COPQManhourLossForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "COPQ Manhour Loss";
            this.Load += new System.EventHandler(this.COPQManhourLossForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ComboBox TypeDropdown;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label Type;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DateTimePicker ToDateTimePicker;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox CategoryDropdown;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DateTimePicker FromDateTimePicker;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox SectionDropdown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button ViewGraph;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AddCOPQButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ExportButton;
    }
}
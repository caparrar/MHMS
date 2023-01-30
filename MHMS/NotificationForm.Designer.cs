namespace MHMS
{
    partial class NotificationForm
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NotificationTopPanel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NotificationDataGridView = new System.Windows.Forms.DataGridView();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(35)))), ((int)(((byte)(53)))));
            this.TopPanel.Controls.Add(this.CloseButton);
            this.TopPanel.Controls.Add(this.NotificationTopPanel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(773, 48);
            this.TopPanel.TabIndex = 2;
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::MHMS.Properties.Resources.delete;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(719, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(54, 48);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NotificationTopPanel
            // 
            this.NotificationTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationTopPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotificationTopPanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NotificationTopPanel.Location = new System.Drawing.Point(0, 0);
            this.NotificationTopPanel.Name = "NotificationTopPanel";
            this.NotificationTopPanel.Size = new System.Drawing.Size(773, 48);
            this.NotificationTopPanel.TabIndex = 0;
            this.NotificationTopPanel.Text = "NOTIFICATION";
            this.NotificationTopPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NotificationTopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotificationTopPanel_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(210)))), ((int)(((byte)(216)))));
            this.panel1.Controls.Add(this.NotificationDataGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(773, 414);
            this.panel1.TabIndex = 2;
            // 
            // NotificationDataGridView
            // 
            this.NotificationDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.NotificationDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NotificationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NotificationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationDataGridView.Location = new System.Drawing.Point(10, 10);
            this.NotificationDataGridView.Name = "NotificationDataGridView";
            this.NotificationDataGridView.Size = new System.Drawing.Size(753, 394);
            this.NotificationDataGridView.TabIndex = 0;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "NotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification Form";
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotificationDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label NotificationTopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView NotificationDataGridView;
    }
}
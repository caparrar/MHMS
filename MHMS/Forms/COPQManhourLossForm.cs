using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MHMS.Forms
{
    public partial class COPQManhourLossForm : Form
    {

        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public COPQManhourLossForm()
        {
            InitializeComponent();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void AddCOPQButton_Click(object sender, EventArgs e)
        {

            //ExportMHData();

            if (LoginForm.UserSection == "Tape Cassette")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\TAPE.xlsx");
            }
            else if (LoginForm.UserSection == "Ink Cartridge")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\INK.xlsx");
            }
            else if (LoginForm.UserSection == "Production Engineering")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (LoginForm.UserSection == "Ink Head")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (LoginForm.UserSection == "Molding Production")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\MOLD.xlsx");
            }
            else if (LoginForm.UserSection == "PCBA")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\PCBA.xlsx");
            }
            else if (LoginForm.UserSection == "Printer")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\PRT.xlsx");
            }
            else if (LoginForm.UserSection == "P-Touch")
            {
                MessageBox.Show("The template is preparing to open!", "Please wait a seconds...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\COPQ Data Sheet Format\P-TOUCH R01.xlsx");
            }

            //MessageBox.Show("Not yet ready to use, Ongoing development!", "ONGOING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }

        private void DateTo()
        {
            DateTime datenow = DateTime.Now;
            ToDateTimePicker.Value = datenow;
        }// <---- end

        //====================================================================================================================>>>>>>>>>>>>
        public static string SelectedSection;
        private void COPQManhourLossForm_Load(object sender, EventArgs e)
        {
            MHLossDataGridView.EnableHeadersVisualStyles = false;
            MHLossDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            MHLossDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Hide Row header in datagrid
            MHLossDataGridView.RowHeadersVisible = false;

            DateFrom(); // Call out the function for Date From and show when the form is loaded

            DateTo();

            // load section list from db to combobox


            if (LoginForm.isSingleSectionAccess == true)
            {
                if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
                {
                    //LoadSection();
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = LoginForm.UserSection;
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = "";
                }
                else
                {
                    SectionDropdown.Text = LoginForm.UserSection;
                    SectionDropdown.Enabled = false;
                }

                SectionMenuForm.isMultiSectionAccess = false;
            }

            if (SectionMenuForm.isMultiSectionAccess == true)
            {
                if (SectionMenuForm.UserSection == "BPS" || SectionMenuForm.UserSection == "Production Engineering")
                {
                    //LoadSection();
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = LoginForm.UserSection;
                }
                else if (LoginForm.UserSection == "Quality Innovation")
                {
                    SelectedSection = SectionDropdown.Text;
                    SectionDropdown.Text = "";
                }
                else
                {
                    SectionDropdown.Text = SectionMenuForm.UserSection;
                    SectionDropdown.Enabled = false;
                }
                
                LoginForm.isSingleSectionAccess = false;
            }

            

            //LoadMHLossData(); // Load all MH loss data to datagrid view

            /* FormatHeaderText();*/ // Format datagridview column header text

            //SelectMHDataBaseOnDropdownEntries();

            HideMonthlyStandardMHButton();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void HideMonthlyStandardMHButton()
        {
            if (LoginForm.UserSection == "Production Engineering")
            {
                StandardMHButton.Visible = true;
            }
            else if (LoginForm.UserSection == "BPS")
            {
                StandardMHButton.Visible = true;
            }
            else
            {
                StandardMHButton.Visible = false;
            }
        }

        //==================================================================================================================>>>>>>>>>>>>

        //Load Section in combobox
        public void LoadSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllProdSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            //SectionDropdown.ValueMember = "";
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Dashboard.SectionText == "BIPH-BPS")
            {
                UpdateDataButton.Enabled = true;
                UpdateMHLoss2 UpdateData = new UpdateMHLoss2();
                UpdateData.ShowDialog();
                //UpdateDataButton.BackColor = Color.FromArgb(21, 35, 53);
            }
            else if (Dashboard.SectionText == "BIPH-Production Engineering")
            {
                UpdateDataButton.Enabled = true;
                UpdateMHLoss2 UpdateData = new UpdateMHLoss2();
                UpdateData.ShowDialog();
                //UpdateDataButton.BackColor = Color.FromArgb(21, 35, 53);
            }
            else
            {
                //UpdateDataButton.Enabled = false;
                MessageBox.Show("You are not allowed to update data!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DisableSectionDropdown()
        {
            SectionDropdown.Enabled = false;
            SectionDropdown.Text = Dashboard.SectionText.Replace("BIPH-", "");
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LoadMHLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
            {
                SelectMHLossData();

                //// -> SQL query to select MH loss data
                //SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                //SelectMHLossData.CommandType = CommandType.StoredProcedure;
                //SelectMHLossData.Parameters.AddWithValue("@Procedure", "ViewAllMHLossData");
                //SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                //SelectMHLossData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //MHLossDataGridView.DataSource = dt;
                //con.Close();
            }
            else
            {
                DisableSectionDropdown();

                // -> SQL query to select MH loss data
                SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                SelectMHLossData.CommandType = CommandType.StoredProcedure;
                SelectMHLossData.Parameters.AddWithValue("@Procedure", "ViewMHLossDataBySection");
                SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                SelectMHLossData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MHLossDataGridView.DataSource = dt;
                con.Close();
            }

           
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchMHLossData();
            }
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void FormatHeaderText()
        {
            MHLossDataGridView.Columns["DateEncountered"].HeaderText = "Date Encountered";
            MHLossDataGridView.Columns["Section"].HeaderText = "Section";
            MHLossDataGridView.Columns["CostCenter"].HeaderText = "Cost Center";
            MHLossDataGridView.Columns["ModelName"].HeaderText = "Model Name";
            MHLossDataGridView.Columns["LossFactor"].HeaderText = "Loss Factor";
            MHLossDataGridView.Columns["ResponsibleSection"].HeaderText = "Responsible Section";
            MHLossDataGridView.Columns["LineStopDetail"].HeaderText = "Reason (Line Stop Detail)";
            MHLossDataGridView.Columns["StopTime"].HeaderText = "Stop Time";
            MHLossDataGridView.Columns["DirectMP"].HeaderText = "Direct MP";
            MHLossDataGridView.Columns["SemiDirectMP"].HeaderText = "Semi-Direct MP";
            MHLossDataGridView.Columns["LossManhour"].HeaderText = "Loss Manhour";
            MHLossDataGridView.Columns["Reason"].HeaderText = "Reason";
            MHLossDataGridView.Columns["TypeOfLoss"].HeaderText = "Type of Loss";
            //MHLossDataGridView.Columns["COPQAmount"].HeaderText = "COPQ Amount";
            MHLossDataGridView.Columns["DateIssued"].HeaderText = "Date Issued";
            MHLossDataGridView.Columns["Cause"].HeaderText = "Cause";
            MHLossDataGridView.Columns["Countermeasure"].HeaderText = "Countermeasure (if accepted) / Reason (if rejected)";
            MHLossDataGridView.Columns["ApplyingStatus"].HeaderText = "Applying Status";
            MHLossDataGridView.Columns["ReceivingStatus"].HeaderText = "Receiving Status";
            MHLossDataGridView.Columns["QIConfirmation"].HeaderText = "QI Confirmation";
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SearchMHLossData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select MH loss data
            SqlCommand SearchMHLossData = new SqlCommand("SP_SearchMHLossData", con);
            SearchMHLossData.CommandType = CommandType.StoredProcedure;
            SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
            SearchMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MHLossDataGridView.DataSource = dt;
            con.Close();


            /* FormatHeaderText();*/ // Format header text
            //SearchBox.Clear(); // Clear text box

        }

        //==================================================================================================================>>>>>>>>>>>>

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportMHData();
        }

        private void copyAlltoClipboardsss()
        {

            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            MHLossDataGridView.SelectAll();
            //Copy to clipboard
            MHLossDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = MHLossDataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportMHData()
        {
            string pathsss = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "") + @"\Desktop\COPQ_Exported_Data";
            System.IO.Directory.CreateDirectory(pathsss);

            copyAlltoClipboardsss();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            // xlWorkSheet.Cells[3, "XL"].Cells.NumberFormat = "@";
            CR.Select();
            xlWorkSheet.Cells.NumberFormat = "@";
            //string DateNowVal = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            //string folderPath = "C:\\Users\\manalojo\\Desktop\\Export\\";
            //    xlWorkBook.SaveAs(folderPath + "ViewExport_ " + DateNowVal + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            //false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlWorkSheet.PasteSpecial(CR, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true);
            xlWorkSheet.Columns.AutoFit();

            MessageBox.Show("Exported successfully", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //try
            //{
            //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
            //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            //    app.Visible = true;
            //    worksheet = workbook.Sheets["Sheet1"];
            //    worksheet = workbook.ActiveSheet;
            //    worksheet.Name = "MH Loss Details";

            //    try
            //    {
            //        for (int i = 0; i < MHLossDataGridView.Columns.Count; i++)
            //        {
            //            worksheet.Cells[1, i + 1] = MHLossDataGridView.Columns[i].HeaderText;
            //        }

            //        for (int i = 0; i < MHLossDataGridView.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < MHLossDataGridView.Columns.Count; j++)
            //            {
            //                if (MHLossDataGridView.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, j + 1] = MHLossDataGridView.Rows[i].Cells[j].Value.ToString();
            //                }
            //                else
            //                {
            //                    worksheet.Cells[i + 2, j + 1] = "";
            //                }
            //            }
            //        }

            //        //Getting the location and file name of the excel to save from user. 
            //        SaveFileDialog saveDialog = new SaveFileDialog();
            //        saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            //        saveDialog.FilterIndex = 2;

            //        if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            workbook.SaveAs(saveDialog.FileName);
            //            MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

            //    finally
            //    {
            //        app.Quit();
            //        workbook = null;
            //        worksheet = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
        //==================================================================================================================>>>>>>>>>>>>

        private void SelectMHDataBaseOnDropdownEntries()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.UserSection == "BPS" || LoginForm.UserSection == "Production Engineering")
            {
                SelectMHLossData();

                //if (DropdownEntriesValue.Text == "All")
                //{

                //    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                //    SelectMHData.CommandType = CommandType.StoredProcedure;
                //    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                //    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectAll");
                //    SelectMHData.Parameters.AddWithValue("@Value", "");
                //    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    MHLossDataGridView.DataSource = dt;
                //    con.Close();
                //}
                //else
                //{
                    
                //    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                //    SelectMHData.CommandType = CommandType.StoredProcedure;
                //    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                //    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectBasedOnEntriesValue");
                //    SelectMHData.Parameters.AddWithValue("@Value", DropdownEntriesValue.Text);
                //    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                //    DataTable dt = new DataTable();
                //    sda.Fill(dt);
                //    MHLossDataGridView.DataSource = dt;
                //    con.Close();
                //}
            }
            else
            {
                if (DropdownEntriesValue.Text == "All")
                {
                    
                    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                    SelectMHData.CommandType = CommandType.StoredProcedure;
                    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectMHLossDataBySection");
                    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectAll");
                    SelectMHData.Parameters.AddWithValue("@Value", "");
                    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectMHData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
                else
                {
                  
                    SqlCommand SelectMHData = new SqlCommand("SP_SelectMHDataBaseOnDropdownEntries", con);
                    SelectMHData.CommandType = CommandType.StoredProcedure;
                    SelectMHData.Parameters.AddWithValue("@Procedure", "SelectMHLossDataBySection");
                    SelectMHData.Parameters.AddWithValue("@SubProcedure", "SelectBasedOnEntriesValue");
                    SelectMHData.Parameters.AddWithValue("@Value", DropdownEntriesValue.Text);
                    SelectMHData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
                    SelectMHData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
            }
           
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void DropdownEntriesValue_TextChanged(object sender, EventArgs e)
        {
            if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type.", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TypeDropdown.Focus();
            }
            else if (DropdownEntriesValue.Text == "")
            {
                //No Action
            }
            else
            {
                //SelectMHDataBaseOnDropdownEntries();
                FilterDataBySelectedRangeOfDate();
                DropdownEntriesValue.ForeColor = Color.FromArgb(21, 35, 53);
            }
            
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select section!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SectionDropdown.Focus();
            }
            else if (TypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TypeDropdown.Focus();
            }
            else
            {
                if (TypeDropdown.Text == "Receiving")
                {
                    LossRatePanel.Height = 150;
                    LossRateDataGrid.Visible = true;
                    ViewGraphButton.Visible = false;
                    LossRateDataGrid.Dock = DockStyle.Fill;

                    LossRateDropdownList.Visible = true;

                    SelectLossRateData(); //Show loss rate data

                    //Hide Column in loss rate data grid view
                    //LossRateDataGrid.Columns["ID"].Visible = false;
                    //LossRateDataGrid.Columns["Fiscal Year"].Visible = false;
                    //LossRateDataGrid.Columns["UploadBy"].Visible = false;
                    //LossRateDataGrid.Columns["UploadDate"].Visible = false;

                    //Change column header back color
                    LossRateDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(189, 225, 255);
                    LossRateDataGrid.Columns["Target"].HeaderCell.Style.BackColor = Color.FromArgb(242, 213, 157);
                    LossRateDataGrid.Columns["Actual"].HeaderCell.Style.BackColor = Color.FromArgb(87, 119, 255);
                    LossRateDataGrid.Columns["Actual"].HeaderCell.Style.ForeColor = Color.White;
                    //LossRateDataGrid.Columns["Yearly Target"].HeaderCell.Style.BackColor = Color.FromArgb(242, 213, 157);
                    //LossRateDataGrid.Columns["Yearly Actual"].HeaderCell.Style.BackColor = Color.FromArgb(87, 119, 255);
                    //LossRateDataGrid.Columns["Yearly Actual"].HeaderCell.Style.ForeColor = Color.White;
                    //LossRateDataGrid.Columns["Q1"].HeaderCell.Style.BackColor = Color.Orange;
                    //LossRateDataGrid.Columns["Q2"].HeaderCell.Style.BackColor = Color.Orange;
                    //LossRateDataGrid.Columns["Q3"].HeaderCell.Style.BackColor = Color.Orange;
                    //LossRateDataGrid.Columns["Q4"].HeaderCell.Style.BackColor = Color.Orange;
                    LossRateDataGrid.EnableHeadersVisualStyles = false;

                   
                }
                else
                {
                    ViewGraphButton.Visible = true;
                    LossRatePanel.Height = MinimumSize.Height;
                    LossRateDataGrid.Visible = false;
                    ViewGraphButton.Visible = true;

                    LossRateDropdownList.Visible = false;
                }

                SelectedSection = SectionDropdown.Text;

                FilterDataBySelectedRangeOfDate();

            }
            
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void SelectLossRateData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            
                SqlCommand SelectLossRateData = new SqlCommand("SP_SelectLossRateData", con);
                SelectLossRateData.CommandType = CommandType.StoredProcedure;
                SelectLossRateData.Parameters.AddWithValue("@DopdownItem", LossRateDropdownList.Text);
                SelectLossRateData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectLossRateData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LossRateDataGrid.DataSource = dt;
                con.Close();
         
           
        }


        //==================================================================================================================>>>>>>>>>>>>

        private void FilterDataBySelectedRangeOfDate()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (DropdownEntriesValue.Text == "All")
            {
                if (TypeDropdown.Text == "Applying")
                {
                    // -> SQL query to select MH data base on selected entries
                    SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                    SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateAll");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
                else if (TypeDropdown.Text == "Receiving")
                {
                    // -> SQL query to select MH data base on selected entries
                    SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                    SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateAll");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-",""));
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", "");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
            }
            else
            {
                if (TypeDropdown.Text == "Applying")
                {
                    // -> SQL query to select MH data base on selected entries
                    SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                    SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateRange");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", SectionDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", Convert.ToInt32(DropdownEntriesValue.Text));
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
                else if (TypeDropdown.Text == "Receiving")
                {
                    // -> SQL query to select MH data base on selected entries
                    SqlCommand SelectMHDataBaseOnSelectedDetails = new SqlCommand("SP_SelectMHDataByDate", con);
                    SelectMHDataBaseOnSelectedDetails.CommandType = CommandType.StoredProcedure;
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Procedure", "FilterBySectionAndDateRange");
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Entries", Convert.ToInt32(DropdownEntriesValue.Text));
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                    SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                    //SelectMHDataBaseOnSelectedDetails.Parameters.AddWithValue("@Type", TypeDropdown.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(SelectMHDataBaseOnSelectedDetails);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MHLossDataGridView.DataSource = dt;
                    con.Close();
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SectionDropdown_TextChanged(object sender, EventArgs e)
        {
            //SelectMHLossData();

            //// Check Connection status -> Open connection if the connection is closed
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //if (SectionDropdown.Text == "Ink Cartridge")
            //{
            //    // -> SQL query to select Ink cartridge MH loss data
            //    SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
            //    SelectMHLossData.CommandType = CommandType.StoredProcedure;
            //    SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInCartridgeMHLoss");
            //    SelectMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            //    SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    MHLossDataGridView.DataSource = dt;
            //    con.Close();
            //}
            //else if (SectionDropdown.Text == "Ink Head")
            //{
            //    // -> SQL query to select Ink Head MH loss data
            //    SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
            //    SelectMHLossData.CommandType = CommandType.StoredProcedure;
            //    SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInCartridgeMHLoss");
            //    SelectMHLossData.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            //    SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
            //    SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    MHLossDataGridView.DataSource = dt;
            //    con.Close();
            //}

        }

        private void SelectMHLossData()
        {
            // Check Connection status -> Open connection if the current connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }

                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select tape cassette parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink head parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select molding parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select PCBA parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Printer parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Production Engineering")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Production Engineering");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Production Engineering parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Production Engineering");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select P-touch parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Engineering")
            {
                if (SectionDropdown.Text == "Ink Cartridge")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Cartridge");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }

                }
                else if (SectionDropdown.Text == "Tape Cassette")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select tape cassette parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Tape Cassette");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Ink Head")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select ink head parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectInkHeadMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Ink Head");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Molding Production")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select molding parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectMoldingMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Molding");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "PCBA")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select PCBA parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPCBAMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "PCBA");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Printer")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Printer parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPrinterMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Printer");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "Production Engineering")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Production Engineering");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select Production Engineering parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "Production Engineering");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }


                    //GetTotalAdjustedAmount();
                    //FormatHeaderText();
                }
                else if (SectionDropdown.Text == "P-Touch")
                {
                    if (DropdownEntriesValue.Text == "All")
                    {
                        // -> SQL query to select ink cartridge parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectAllMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", "");
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                    else
                    {
                        // -> SQL query to select P-touch parts loss data
                        SqlCommand SelectMHLossData = new SqlCommand("SP_SelectMHLossData", con);
                        SelectMHLossData.CommandType = CommandType.StoredProcedure;
                        SelectMHLossData.Parameters.AddWithValue("@Procedure", "SelectPTouchMHLossData");
                        SelectMHLossData.Parameters.AddWithValue("@Entries", DropdownEntriesValue.Text);
                        SelectMHLossData.Parameters.AddWithValue("@Section", "P-Touch");
                        SelectMHLossData.Parameters.AddWithValue("@DateFrom", FromDateTimePicker.Value.ToString());
                        SelectMHLossData.Parameters.AddWithValue("@DateTo", ToDateTimePicker.Value.ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MHLossDataGridView.DataSource = dt;
                        con.Close();
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MHLossDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in MHLossDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMHLoss2 sample = new UpdateMHLoss2();
            sample.ShowDialog();
        }

        private void LossRateButton_Click(object sender, EventArgs e)
        {
            MonthlyStandardManHour StandardMH = new MonthlyStandardManHour();
            StandardMH.ShowDialog();
        }

        public static bool HaveNewUploadedData = false;
        private void RefreshDatagridTimer_Tick(object sender, EventArgs e)
        {
            if (HaveNewUploadedData == true)
            {
                LoadMHLossData();

                HaveNewUploadedData = false;
            }
        }

        private void ViewGraphButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi " + LoginForm.FirstName + "! Sorry, The tableau report visualization is currently under construction. Please try again another time!");
        }

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FilterDataBySelectedRangeOfDate();
        }

        private void LossRateDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            LossRateDataGrid.Columns["Responsible Section"].Width = 150;

            foreach (DataGridViewColumn column in LossRateDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void LossRateDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectLossRateData(); //Show loss rate data
        }

        private void ApprovedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ApprovedCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Approved")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(87, 222, 155);
                    }
                }
                    
            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Approved")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void ForApprovalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ForApprovalCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "For Approval")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(222, 217, 87);
                    }
                }

            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "For Approval")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void RejectedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RejectedCheckBox.Checked == true)
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Rejected")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(222, 87, 109);
                    }
                }

            }
            else
            {
                foreach (DataGridViewRow row in MHLossDataGridView.Rows)
                {
                    if (row.Cells["Over All Status"].Value.ToString() == "Rejected")
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }




        //==================================================================================================================>>>>>>>>>>>>
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class UpdateDataForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public UpdateDataForm()
        {
            InitializeComponent();
        }

        private void DataUpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

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
            //SectionDropdown.ValueMember = "Section";
        }

        private void InsertGMMMSData()
        {
            int intRow = 0;

            foreach (DataGridViewRow row in UploadDatagrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {
                    intRow = intRow + 1;

                    // -> SQL query to insert Section to Approver setting
                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    
                    if ((SectionDropdown.Text == "Ink Cartridge") && (DataTypeDropdown.Text == "GMMS"))
                    {
                        SqlCommand InsertUsers = new SqlCommand("SP_InsertGMMSData", con);
                        InsertUsers.CommandType = CommandType.StoredProcedure;
                        InsertUsers.Parameters.AddWithValue("@Procedure", "GMMS_InkCartridge");
                        InsertUsers.Parameters.AddWithValue("@OrderNo", row.Cells["Order No#"].Value);
                        InsertUsers.Parameters.AddWithValue("@MaterialCode", row.Cells["Material Code"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@MaterialText", row.Cells["Material Text"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value);
                        InsertUsers.Parameters.AddWithValue("@Amount", row.Cells["Amount"].Value);
                        InsertUsers.Parameters.AddWithValue("@MoveReason", row.Cells["MoveReason"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@CreateDate", row.Cells["CreateDate"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@DetailsReason", row.Cells["Details Reason"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@ConfirmResult", row.Cells["Confirm Result"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Remark", row.Cells["Remark"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@ChargedObject", row.Cells["Charged object"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Creator", row.Cells["Creator"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Department", row.Cells["Dept"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("MM/dd/yyyy")); 
                        InsertUsers.Parameters.AddWithValue("@AdjustedAmount", row.Cells["ADJ# AMOUNT"].Value.ToString());
                        InsertUsers.ExecuteNonQuery();
                        con.Close();
                    }
                    else if ((SectionDropdown.Text == "Production Engineering") && (DataTypeDropdown.Text == "GMMS"))
                    {
                        SqlCommand InsertUsers = new SqlCommand("SP_InsertGMMSData", con);
                        InsertUsers.CommandType = CommandType.StoredProcedure;
                        InsertUsers.Parameters.AddWithValue("@Procedure", "GMMS_InkCartridge");
                        InsertUsers.Parameters.AddWithValue("@OrderNo", row.Cells["Order No#"].Value);
                        InsertUsers.Parameters.AddWithValue("@MaterialCode", row.Cells["Material Code"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@MaterialText", row.Cells["Material Text"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value);
                        InsertUsers.Parameters.AddWithValue("@Amount", row.Cells["Amount"].Value);
                        InsertUsers.Parameters.AddWithValue("@MoveReason", row.Cells["MoveReason"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@CreateDate", row.Cells["CreateDate"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@DetailsReason", row.Cells["Details Reason"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@ConfirmResult", row.Cells["Confirm Result"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Remark", row.Cells["Remark"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@ChargedObject", row.Cells["Charged object"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Creator", row.Cells["Creator"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@Department", row.Cells["Dept"].Value.ToString());
                        InsertUsers.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("MM/dd/yyyy"));
                        InsertUsers.Parameters.AddWithValue("@AdjustedAmount", row.Cells["ADJ# AMOUNT"].Value.ToString());
                        InsertUsers.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            MessageBox.Show("GMMS data was successfully updated", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SectionDropdown.Text = "";
            DataTypeDropdown.Text = "";
            FilePath.Clear();
            UploadDatagrid.DataSource = null;
            
        }

        private void UpdateDataForm_Load(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;

            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {
                filePath = file.FileName;//get the path of the file
                fileExt = Path.GetExtension(filePath);//get the file extension
                FilePath.Text = filePath + fileExt;

                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
                {

                    try
                    {

                        //if (ReadingFile.Text != "Progress: 100%")
                        //{
                        //    ReadingFileLabel.Visible = true;
                        //}
                        
                        //if (ReadingFile.Text == "Progress: 100%")
                        //{
                        //    ReadingFileLabel.Text = "";
                        //}

                        

                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);
                        UploadDatagrid.Visible = true;
                        UploadDatagrid.DataSource = dtExcel;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [GMMS$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }// end ReadExcel

        private void UploadUserButton_Click(object sender, EventArgs e)
        {
            if (SectionDropdown.Text == "")
            {
                MessageBox.Show("Please select the type of data that you want to upload!", "Reminders");
            }
            else if (DataTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select the type of data that you want to upload!", "Reminders");
            }
            else if (FilePath.Text == "")
            {
                MessageBox.Show("Please choose the file that you want to upload", "Reminders");
            }
            else
            {
                //InsertGMMMSData(); // insert GMMS Data

                try
                {
                    InsertGMMMSData(); // insert GMMS Data

                    //Show progress bar
                    progressBar1.Visible = true;
                    ReadingFile.Visible = true;
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 800 milliseconds.  
                Thread.Sleep(100);
                // Report progress.  
                backgroundWorker1.ReportProgress(i);
            }
        }

        string percentage = "";

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar   
            progressBar1.Value = e.ProgressPercentage;
            // Set the text.  
            percentage = e.ProgressPercentage.ToString();
            ReadingFile.Text = "Progress: " + percentage + "%";

            
        }
    }
}

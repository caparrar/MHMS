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
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class TargetSettingForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;

        public TargetSettingForm()
        {
            InitializeComponent();
        }

        private void TargetSettingForm_Load(object sender, EventArgs e)
        {
            AddYears(); // Load the list of fiscal year in combobox
            GetSectionApproverSetting(); // load all data from target setting table

            // Prevent the data grid column to sort
            foreach (DataGridViewColumn column in TargetSettingDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Prevent the data grid column to sort
            foreach (DataGridViewColumn column in HistoryDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void AddYears()
        {

            var currentYear = DateTime.Today.Year;
            for (int i = 5; i >= 0; i--)
            {
                // Now just add an entry that's the current year minus the counter
                FiscalYearDropdown.Items.Add((currentYear - i).ToString());
            }
        }

        private void GetSectionApproverSetting()
        {
            // -> SQL query to select Section Approver setting
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand SelectTargetSetting = new SqlCommand("SP_SelectTargetSetting", con);
            SelectTargetSetting.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectTargetSetting);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TargetSettingDataGrid.DataSource = dt;
            con.Close();
        }

        private void TargetSettingDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(71, 208, 197);
                e.CellStyle.ForeColor = Color.White;
            }
            //Set specific width on datagrid column
            TargetSettingDataGrid.Columns["History"].Width = 150;
            TargetSettingDataGrid.Columns["Last Update"].Width = 220;
        }

        // function for getting all data in Efficiency Files table
        private void GetEfficiencyFilesHistory()
        {
            // -> SQL query to select/get Efficiency Files
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectEfficiencyFiles = new SqlCommand("SP_SelectEfficiencyFiles", con);
                SelectEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectEfficiencyFilesBaseOnDate = new SqlCommand("SP_SelectEfficiencyFilesBaseOnDate", con);
                SelectEfficiencyFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectEfficiencyFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectEfficiencyFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        // function for getting all data in MH loss rate files table
        private void GetMHLossRateFilesHistory()
        {
            // -> SQL query to select/get MH loss rate files
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectMHLossRateFiles = new SqlCommand("SP_SelectMHLossRateFiles", con);
                SelectMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectMHLossRateFilesBaseOnDate = new SqlCommand("SP_SelectMHLossRateFilesBaseOnDate", con);
                SelectMHLossRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectMHLossRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectMHLossRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
           

        }

        // function for getting all data in Parts loss rate files table
        private void GetPartsLossRateFilesHistory()
        {
            // -> SQL query to select/get Parts loss rate files
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectPartsLossRateFiles = new SqlCommand("SP_SelectPartsLossRateFiles", con);
                SelectPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {

                SqlCommand SelectPartsLossRateFilesBaseOnDate = new SqlCommand("SP_SelectPartsLossRateFilesBaseOnDate", con);
                SelectPartsLossRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectPartsLossRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectPartsLossRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            
        }

        // function for getting all data in COPQ Manpower rate files table
        private void GetCOPQManpowerRateFilesHistory()
        {
            // -> SQL query to select/get COPQ Manpower rate files
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (FiscalYearDropdown.Text == "")
            {
                SqlCommand SelectCOPQManpowerRateFiles = new SqlCommand("SP_SelectCOPQManpowerRateFiles", con);
                SelectCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQManpowerRateFiles);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
            else if (FiscalYearDropdown.Text != "" || FiscalYearDropdown.Text != null)
            {
                SqlCommand SelectCOPQManpowerRateFilesBaseOnDate = new SqlCommand("SP_SelectCOPQManpowerRateFilesBaseOnDate", con);
                SelectCOPQManpowerRateFilesBaseOnDate.CommandType = CommandType.StoredProcedure;
                SelectCOPQManpowerRateFilesBaseOnDate.Parameters.AddWithValue("@UpdateDate", FiscalYearDropdown.Text);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQManpowerRateFilesBaseOnDate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                HistoryDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void TargetSettingDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (TargetSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "HISTORY")
                {
                    if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Efficiency")
                    {
                        HistoryDataGrid.Visible = true;
                        GetEfficiencyFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "MH Loss Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        GetMHLossRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "Parts Loss Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        GetPartsLossRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";
                    }
                    else if (TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() == "COPQ Manpower Rate")
                    {
                        HistoryDataGrid.Visible = true;
                        GetCOPQManpowerRateFilesHistory(); // Call out the function to execute
                        TitleCategory.Text = TargetSettingDataGrid.Rows[e.RowIndex].Cells["Category"].Value.ToString() + " History";
                    }
                }
                else 
                {}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string fileName = string.Empty;
        string fileNameWithExt = string.Empty;
        string fileExt = string.Empty;

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            

            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {

                //string path = Path.Combine(SaveDirectory);
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //path = path + fileName;
                //File.Copy(file.FileName, path);

                filePath = file.FileName;//get the path of the file
                fileName = Path.GetFileNameWithoutExtension(filePath); // get the file name without extension
                fileNameWithExt = Path.GetFileName(filePath);
                fileExt = Path.GetExtension(filePath);//get the file extension
                FilePath.Text = filePath;

                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0 || fileExt.CompareTo(".xlsm") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        HistoryDataGrid.Visible = true;
                        HistoryDataGrid.DataSource = dtExcel;
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
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con);//here we read data from sheet1
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }// end ReadExcel



        //private void SaveFileToFolder()
        //{
        //    string SaveDirectory = @"\\apbiphwb02\Release\COPQ Files\Target History\";

        //    if (!Directory.Exists(SaveDirectory))
        //    {
        //        Directory.CreateDirectory(SaveDirectory);
        //    }

        //    if (File.Exists(SaveDirectory + fileNameWithExt))
        //    {
        //        NewFileName = fileName + " rev" + (count++) + fileExt; // Create new filename if already exist
        //        string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
        //        File.Copy(FilePath.Text, newFileSavePath, true);
        //        MessageBox.Show("File already exist in directory folder, please rename your file!");
        //    }
        //    else
        //    {
        //        string fileSavePath = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder to filename
        //        File.Copy(FilePath.Text, fileSavePath, true);
        //    }

        //}

        // ---> Insert the revised file name
        private void InsertRevisedFiles()
        {
            // -> SQL query to insert files in Efficiency files table
            SqlConnection con = new SqlConnection(MHMS_Conn);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (CategoryDropdown.Text == "Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertEfficiencyFiles", con);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Efficiency File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "MH Loss Rate")
            {
                SqlCommand InsertMHLossRateFiles = new SqlCommand("SP_InsertMHLossRateFiles", con);
                InsertMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertMHLossRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertMHLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("MH Loss File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "Parts Loss Rate")
            {
                SqlCommand InsertPartsLossRateFiles = new SqlCommand("SP_InsertPartsLossRateFiles", con);
                InsertPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertPartsLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Parts Loss File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CategoryDropdown.Text == "COPQ Manpower Rate")
            {
                SqlCommand InsertCOPQManpowerRateFiles = new SqlCommand("SP_InsertCOPQManpowerRateFiles", con);
                InsertCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FileName", NewFileName);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertCOPQManpowerRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("COPQ Manpower File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ---> Insert the new files
        private void InsertFiles()
        {
            // -> SQL query to insert files in Efficiency files table
            SqlConnection con = new SqlConnection(MHMS_Conn);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (CategoryDropdown.Text == "Efficiency")
            {
                SqlCommand InsertEfficiencyFiles = new SqlCommand("SP_InsertEfficiencyFiles", con);
                InsertEfficiencyFiles.CommandType = CommandType.StoredProcedure;
                InsertEfficiencyFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertEfficiencyFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertEfficiencyFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Efficiency File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "MH Loss Rate")
            {
                SqlCommand InsertMHLossRateFiles = new SqlCommand("SP_InsertMHLossRateFiles", con);
                InsertMHLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertMHLossRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertMHLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertMHLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("MH Loss File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CategoryDropdown.Text == "Parts Loss Rate")
            {
                SqlCommand InsertPartsLossRateFiles = new SqlCommand("SP_InsertPartsLossRateFiles", con);
                InsertPartsLossRateFiles.CommandType = CommandType.StoredProcedure;
                InsertPartsLossRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertPartsLossRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertPartsLossRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Parts Loss File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CategoryDropdown.Text == "COPQ Manpower Rate")
            {
                SqlCommand InsertCOPQManpowerRateFiles = new SqlCommand("SP_InsertCOPQManpowerRateFiles", con);
                InsertCOPQManpowerRateFiles.CommandType = CommandType.StoredProcedure;
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@FileName", fileNameWithExt);
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateDate", DateTime.Now.ToString());
                InsertCOPQManpowerRateFiles.Parameters.AddWithValue("@UpdateBy", LoginForm.FirstName + " " + LoginForm.LastName);
                InsertCOPQManpowerRateFiles.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("COPQ Manpower File Uploaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ---> Generate Code
        private void GenerateCode()
        {
            int maxNum = 10000;
            int minNum = 1000;
            Random random = new Random();
            int randomNum = random.Next(minNum, maxNum);
        }

        int count = 2;
        string NewFileName;

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("File not found! Please select file to upload!");
            }
            else if (CategoryDropdown.Text == "")
            {
                MessageBox.Show("Please select category");
            }
            else
            {
                try
                {
                    int maxNum = 100000000;
                    int minNum = 1000;
                    Random random = new Random();
                    int randomNum = random.Next(minNum, maxNum);

                    TitleCategory.Text = CategoryDropdown.Text; // Category title will change depends on selected item in category lists

                    string SaveDirectory = @"\\apbiphwb02\Release\COPQ Files\Target History\";

                    if (!Directory.Exists(SaveDirectory))
                    {
                        Directory.CreateDirectory(SaveDirectory);
                    }

                    if (File.Exists(SaveDirectory + fileNameWithExt)) // if the file to upload is already exists this function will rename the file and insert the new file to the database
                    {
                        /*NewFileName = fileName + " rev" + (count++) + fileExt;*/ // Create new filename if already exist
                        NewFileName = fileName + randomNum + fileExt; // Create new filename if already exist
                        string newFileSavePath = Path.Combine(SaveDirectory, NewFileName); // combine the path of new folder to filename
                        File.Copy(FilePath.Text, newFileSavePath, true);

                        InsertRevisedFiles(); // ---> insert the revised files

                        //MessageBox.Show("File already exist in directory folder, please rename your file!");
                    }
                    else
                    {
                        string fileSavePath = Path.Combine(SaveDirectory, fileNameWithExt); // combine the path of new folder to filename
                        File.Copy(FilePath.Text, fileSavePath, true);

                        InsertFiles(); // Insert the not existis file or new file
                    }

                    //------------------------------------------------------------------------------------------------------------------//

                    int intRow = 0;

                    foreach (DataGridViewRow row in HistoryDataGrid.Rows)
                    {
                        // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                        if (!row.IsNewRow)
                        {
                            intRow = intRow + 1;

                            // Type code here -------

                        }
                    }

                    FilePath.Clear();
                    CategoryDropdown.Text = "";
                    HistoryDataGrid.DataSource = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // ---> Filter the data based on fiscal year when the year was change
        private void FiscalYearDropdown_TextChanged(object sender, EventArgs e)
        {
            if (TitleCategory.Text == "Efficiency History")
            {
                GetEfficiencyFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "MH Loss Rate History")
            {
                GetMHLossRateFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "Parts Loss Rate History")
            {
                GetPartsLossRateFilesHistory(); // Call out the function to execute
            }
            else if (TitleCategory.Text == "COPQ Manpower Rate History")
            {
                GetCOPQManpowerRateFilesHistory(); // Call out the function to execute
            }
        }
    }
}

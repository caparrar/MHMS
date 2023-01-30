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
using System.Windows.Forms;

namespace MHMS
{
    public partial class UpdateMHLossData : Form
    {

        //Connection String
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public UpdateMHLossData()
        {
            InitializeComponent();
        }

        private void UpdateDataTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateAndTimeLabel.Text = dateTime.ToString("dddd , MMM dd yyyy, hh : mm : ss");
        }

        private void BrowseButton_Click(object sender, EventArgs e)
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
                        DataTable dtExcel = new DataTable();
                    dtExcel = ReadExcel(filePath, fileExt);
                    MHLossUploadDatagrid.Visible = true;
                    MHLossUploadDatagrid.DataSource = dtExcel;

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
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [MH Loss$]", con);//here we read data from specified sheet name
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }// end ReadExcel

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("Please select the file to upload!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    InsertMHLossData(); // Insert MH Loss Data
                    InsertMHLossDataToApprovalForm(); // Insert MH loss data to approval form
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void InsertMHLossData()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }



            //int intRow = 0;

            foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {
                    //intRow = intRow + 1;

                    // -> SQL statement to insert MH loss data
                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand InsertMHLossData = new SqlCommand("SP_InsertMHLossData", con);
                    InsertMHLossData.CommandType = CommandType.StoredProcedure;
                    InsertMHLossData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@Date", row.Cells["Date"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@Plant", row.Cells["Plant"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@WorkCenter", row.Cells["Work center"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@Day_Night", row.Cells["Day/Night"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@CostCenter", row.Cells["Cost center/Model"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@ModelName", row.Cells["Cost center/Model name"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@ItemCode", row.Cells["Item code"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@ItemText", row.Cells["Item text"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@StopTime", row.Cells["Stop time(min)"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@LineStopDetail", row.Cells["Line stop content detail"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@LossFactor", row.Cells["Loss factor(EN)"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@DirectEmployee", row.Cells["Direct employee"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@SemiDirectEmployee", row.Cells["Semi-direct employee"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@LossManhour", row.Cells["Loss man-hour"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("MM/dd/yyyy"));
                    InsertMHLossData.ExecuteNonQuery();
                    con.Close();
                }
            }

            //MessageBox.Show("MH Loss data updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void InsertMHLossDataToApprovalForm()
        {
            //int intRow = 0;

            foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
            {
                // FUNCTION FOR CHECKING IF ALREADY EXIST IN DB.
                if (!row.IsNewRow)
                {
                    //intRow = intRow + 1;

                    // -> SQL statement to insert GMMS data
                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand InsertMHLossData = new SqlCommand("SP_InsertMHLossDataToApprovalForm", con);
                    InsertMHLossData.CommandType = CommandType.StoredProcedure;
                    InsertMHLossData.Parameters.AddWithValue("@ReferenceNo", row.Cells["Section"].Value.ToString() + "_" + DateTime.Now.ToString("yyyymmdd"));
                    InsertMHLossData.Parameters.AddWithValue("@Section", row.Cells["Section"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@CostCenter", row.Cells["Cost center/Model"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@Date", row.Cells["Date"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@ResponsibleSection", "");
                    InsertMHLossData.Parameters.AddWithValue("@LineStopDetail", row.Cells["Line stop content detail"].Value.ToString());
                    InsertMHLossData.Parameters.AddWithValue("@PartCode", "");
                    InsertMHLossData.Parameters.AddWithValue("@DirectEmployee", row.Cells["Direct employee"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@SemiDirectEmployee", row.Cells["Semi-direct employee"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@StopTime", row.Cells["Stop time(min)"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@LossManhour", row.Cells["Loss man-hour"].Value);
                    InsertMHLossData.Parameters.AddWithValue("@COPQAmount", "");
                    InsertMHLossData.ExecuteNonQuery();
                    con.Close();
                }
            }

            MessageBox.Show("MH Loss data updated successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //===================================================================================================================================

        private void SelectMHLossLastUpdated()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectMHLossLastUpdated = new SqlCommand("SP_SelectMHLossLastUpdated", con);
            SelectMHLossLastUpdated.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(SelectMHLossLastUpdated);
            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlDataReader reader = SelectMHLossLastUpdated.ExecuteReader();

            while (reader.Read())
            {
                MHLossLastUpdateLabel.Text = reader["Upload Date"].ToString();
            }
        }

        //===================================================================================================================================

        private void UpdateMHLossData_Load(object sender, EventArgs e)
        {
            SelectMHLossLastUpdated(); // Display MH loss last updated everytime the form was loaded
        }

        //===================================================================================================================================
    }
}

using ExcelDataReader;
using MHMS.Class;
using MHMS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace MHMS
{
    public partial class UpdateMHLoss2 : Form
    {
        // Connection string
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public UpdateMHLoss2()
        {
            InitializeComponent();
        }

        //Table collection
        DataTableCollection tableCollection;

        //=================================================================================================================>>>>>>>>>>>>>

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath.Text = openFileDialog.FileName;
                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                ExcelSheetDropdownList.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    ExcelSheetDropdownList.Items.Add(table.TableName);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please close the Excel File!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FilePath.Text = "";
                    }
                }
            }
        }

        //===================================================================================================================>>>>>>>>>>>>>

        int addOne = 0;
        private void ExcelSheetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[ExcelSheetDropdownList.SelectedItem.ToString()];

            if (dt != null)
            {
                List<MHData_Class> list = new List<MHData_Class>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    MHData_Class obj = new MHData_Class();

                    obj.ReferenceNo = dt.Rows[i]["Section"].ToString() + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-" + (addOne += 1);
                    obj.Section = dt.Rows[i]["Section"].ToString();
                    obj.DateEncountered = dt.Rows[i]["Date"].ToString();
                    obj.Plant = dt.Rows[i]["Plant"].ToString(); // no need to display
                    obj.WorkCenter = dt.Rows[i]["Work center"].ToString(); // no need to display  100.83
                    obj.Day_Night = dt.Rows[i]["Day/Night"].ToString(); // no need to display
                    obj.CostCenter = dt.Rows[i]["Cost center/Model"].ToString();
                    obj.ModelName = dt.Rows[i]["Cost center/Model name"].ToString();
                    obj.ItemCode = dt.Rows[i]["Item code"].ToString(); // display as part code
                    obj.ItemText = dt.Rows[i]["Item text"].ToString(); // no need to display
                    obj.StopTime = dt.Rows[i]["Stop time(min)"].ToString();
                    obj.LineStopDetail = dt.Rows[i]["Line stop content detail"].ToString();
                    obj.LossFactor = dt.Rows[i]["Loss factor(EN)"].ToString();
                    obj.DirectMP = dt.Rows[i]["Direct employee"].ToString();
                    obj.SemiDirectMP = dt.Rows[i]["Semi-direct employee"].ToString();
                    obj.LossManhour = dt.Rows[i]["Loss man-hour"].ToString();
                    obj.LossMH_ForCOPQAmount = dt.Rows[i]["Loss man-hour"].ToString();
                    obj.ApplyingApprovalStatus = "For Approval by COPQ PIC";
                    obj.ReceivingApprovalStatus = "---";
                    obj.OverAllStatus = "For Approval";
                    obj.QIConfirmation = "---";
                    obj.UploadDate = DateTime.Now.ToString("MM/dd/yyyy");

                    list.Add(obj);

                }

                manhourLossData2BindingSource2.DataSource = list;
            }
        }

        //===================================================================================================================>>>>>>>>>>>>>

        private void UpdateMHLoss2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mH_Management_SystemDataSet3.ManhourLossData2' table. You can move, or remove it, as needed.
            //this.manhourLossData2TableAdapter2.Fill(this.mH_Management_SystemDataSet3.ManhourLossData2);
            // TODO: This line of code loads data into the 'mH_Management_SystemDataSet2.ManhourLossData2' table. You can move, or remove it, as needed.
            //this.manhourLossData2TableAdapter1.Fill(this.mH_Management_SystemDataSet2.ManhourLossData2);
            // TODO: This line of code loads data into the 'mH_Management_SystemDataSet1.ManhourLossData2' table. You can move, or remove it, as needed.
            //this.manhourLossData2TableAdapter.Fill(this.mH_Management_SystemDataSet1.ManhourLossData2);
            //// TODO: This line of code loads data into the 'mH_Management_SystemDataSet.ManhourLossData' table. You can move, or remove it, as needed.
            //this.manhourLossDataTableAdapter.Fill(this.mH_Management_SystemDataSet.ManhourLossData);

            SelectMHLossLastUpdated(); //--> Show last update date from DB to label
        }

        //===================================================================================================================>>>>>>>>>>>>>

        //private void InsertManhourLossData_TEST()
        //{
        //    DapperPlusManager.Entity<MHData_Class>().Table("ManhourLossData_TEST");
        //    List<MHData_Class> MHLossData_TEST = manhourLossData2BindingSource2.DataSource as List<MHData_Class>;
        //    if (MHLossData_TEST != null)
        //    {
        //        using (IDbConnection db = new SqlConnection("Server=apbiph1131;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
        //        //using (IDbConnection db = con)
        //        {
        //            db.BulkInsert(MHLossData_TEST);
        //        }
        //    }

        //    MessageBox.Show("MH Data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    //Clear fields after upload
        //    FilePath.Clear();
        //    ExcelSheetDropdownList.Text = "";
        //    MHLossUploadDatagrid.DataSource = null;
        //    this.Close();
        //}

        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertManhourLossData()
        {
            DapperPlusManager.Entity<MHData_Class>().Table("ManhourLossData2");
            List<MHData_Class> MHLossData = manhourLossData2BindingSource2.DataSource as List<MHData_Class>;
            if (MHLossData != null)
            {
                using (IDbConnection db = new SqlConnection("Server=apbiph1131;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                //using (IDbConnection db = con)
                {
                    db.BulkInsert(MHLossData);
                }
            }

            MessageBox.Show("MH Data inserted successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Clear fields after upload
            FilePath.Clear();
            ExcelSheetDropdownList.Text = "";
            MHLossUploadDatagrid.DataSource = null;
            this.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void InsertRequestForApprovalData()
        {
            DapperPlusManager.Entity<MHData_Class>().Table("COPQApprovalData");
            List<MHData_Class> COPQApprovalData = manhourLossData2BindingSource2.DataSource as List<MHData_Class>;
            if (COPQApprovalData != null)
            {
                using (IDbConnection db2 = new SqlConnection("Server=apbiph1131;Database=MH_Management_System;User Id=MH_User;Password=P@ssw0rd;"))
                //using (IDbConnection db2 = con)
                {
                    db2.BulkInsert(COPQApprovalData);
                }
            }
        }


       

        private void RemovedDeletedMHLoss()
        {
            try
            {

                foreach (DataGridViewRow row in MHLossUploadDatagrid.Rows)
                {
                    // FUNCTION FOR CHECKING IF MH LOSS ALREADY EXIST IN DB.

                    SqlConnection con = new SqlConnection(MHMS_Conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand SelectMHLoss = new SqlCommand("SP_SelectMHLoss", con);
                    SelectMHLoss.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = SelectMHLoss.ExecuteReader();
                    reader.Read();


                    if (reader.HasRows == true)
                    {

                        //MessageBox.Show("Have existing data!");
                        //Update the status of existing data in COPQ Aprroval table
                        SqlCommand UpdateMHLossStatusInCOPQApproval = new SqlCommand("SP_UpdateMHLossStatusInCOPQApproval", con);
                        UpdateMHLossStatusInCOPQApproval.CommandType = CommandType.StoredProcedure;
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@DateEncountered", row.Cells[0].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@Section", row.Cells[1].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@CostCenter", row.Cells[2].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@LineStopDetail", row.Cells[6].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@StopTime", row.Cells[7].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@DirectMP", row.Cells[8].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@SemiDirectMP", row.Cells[9].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.Parameters.AddWithValue("@LossManhour", row.Cells[10].Value.ToString());
                        UpdateMHLossStatusInCOPQApproval.ExecuteNonQuery();
                        reader.Close();

                        ////Update the status of existing data in Manhour loss data table
                        //sdr.Close();
                        //SqlCommand UpdateMHLossStatusInManhourLoss = new SqlCommand("SP_UpdateMHLossStatusInManhourLoss", con);
                        //UpdateMHLossStatusInManhourLoss.CommandType = CommandType.StoredProcedure;
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@DateEncountered", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@Section", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@CostCenter", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@ModelName", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@LineStopDetail", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@StopTime", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@DirectMP", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@SemiDirectMP", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.Parameters.AddWithValue("@LossManhour", SqlDbType.NVarChar);
                        //UpdateMHLossStatusInManhourLoss.ExecuteNonQuery();
                    }

                    con.Close();
                }

                MessageBox.Show("Some of MH loss data deleted successfully.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilePath.Clear();
                ExcelSheetDropdownList.Items.Clear();
                DataTypeDropdownList.Items.Clear();
                MHLossUploadDatagrid.DataSource = null;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (FilePath.Text == "")
            {
                MessageBox.Show("Please select File!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FilePath.Select();
            }
            else if (DataTypeDropdownList.Text == "")
            {
                MessageBox.Show("Please select data type!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ExcelSheetDropdownList.Text == "")
            {
                MessageBox.Show("Please select Sheet!", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ExcelSheetDropdownList.Select();
            }
            else
            {
                if (DataTypeDropdownList.Text == "Additional")
                {
                    try
                    {
                        //InsertManhourLossData_TEST();
                        InsertManhourLossData();
                        InsertRequestForApprovalData();

                        //Trigger to update
                        COPQManhourLossForm.HaveNewUploadedData = true;

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else if (DataTypeDropdownList.Text == "Deleted")
                {
                    DialogResult dialogResult = MessageBox.Show("If you click 'Yes' the data will be automatically cancelled", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            //Delete data code here ...
                            RemovedDeletedMHLoss();

                            //Trigger to update
                            COPQManhourLossForm.HaveNewUploadedData = true;

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else { }
                }
            }
        }

        //===================================================================================================================>>>>>>>>>>>>>

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
                MHLossLastUpdateLabel.Text = reader["UploadDate"].ToString();
            }
        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void UpdateDataTimer_Tick(object sender, EventArgs e)
        {

        }

        //==================================================================================================================>>>>>>>>>>>>>

        private void ExportButton_Click(object sender, EventArgs e)
        {
            //Export all data from last upload
        }

       



        //==================================================================================================================>>>>>>>>>>>>>
    }
}

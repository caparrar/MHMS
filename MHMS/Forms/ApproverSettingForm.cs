using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class ApproverSettingForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;

        public ApproverSettingForm()
        {
            InitializeComponent();
        }

        private void GetSectionApproverSetting()
        {
            // -> SQL query to select Section Approver setting
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand SelectSectionApproverSetting = new SqlCommand("SP_SelectSectionApproverSetting", con);
            SelectSectionApproverSetting.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(SelectSectionApproverSetting);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ApproverSettingDataGrid.DataSource = dt;
            con.Close();
        }

        private void ApproverSettingForm_Load(object sender, EventArgs e)
        {
            GetSectionApproverSetting();

            //Align to middle and center the cell content of datagridview
            ApproverSettingDataGrid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in ApproverSettingDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // --> Change cell backcolor in datagrid
        private void ApproverSettingDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex==2 && e.Value != null)
            {
                //string status = "ACTIVE";
                //if (status == "ACTIVE")
                //{
                    e.CellStyle.BackColor = Color.FromArgb(60, 179, 141);
                    e.CellStyle.ForeColor = Color.White;
                //}
            }
            else if (e.ColumnIndex==1 && e.Value != null)
            {
                e.CellStyle.BackColor = Color.FromArgb(210, 52, 74);
                e.CellStyle.ForeColor = Color.White;
            }

            //Custom datagrid font
            ApproverSettingDataGrid.Columns["Section"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            ApproverSettingDataGrid.Columns["User Setting"].DefaultCellStyle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            ApproverSettingDataGrid.Columns["Status"].DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            //Custom datagrid header cell backcolor
            //ApproverSettingDataGrid.Columns["Section"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);
            //ApproverSettingDataGrid.Columns["Setting"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);
            //ApproverSettingDataGrid.Columns["Status"].HeaderCell.Style.BackColor = Color.FromArgb(21, 35, 53);

            //Set specific width on datagrid column
            ApproverSettingDataGrid.Columns["User Setting"].Width = 150;
            ApproverSettingDataGrid.Columns["Status"].Width = 150;

        }

        // --> Function for adding section in database
        private void AddSection()
        {
            if (Section.Text == "")
            {
                MessageBox.Show("Please type the section name that you want to add.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // -> SQL query to insert Section to Approver setting
                SqlConnection con = new SqlConnection(MHMS_Conn);
                if (con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand AddSectionToApproverSetting = new SqlCommand("SP_InsertSectionToApproverSetting", con);
                AddSectionToApproverSetting.CommandType = CommandType.StoredProcedure;
                AddSectionToApproverSetting.Parameters.AddWithValue("@Section", Section.Text);
                AddSectionToApproverSetting.Parameters.AddWithValue("@Setting", "⚙️");
                AddSectionToApproverSetting.Parameters.AddWithValue("@Status", "ACTIVE");
                AddSectionToApproverSetting.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Section Save Successfuly!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        } // --> End of Function

        private void SaveSectionButton_Click(object sender, EventArgs e)
        {
            AddSection();
            GetSectionApproverSetting();
        }

        public static string userSection = "";

        private void ApproverSettingDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "⚙️")
                {
                    //                if (ApproverSettingDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString() == DashboardForm.sections.Replace("BIPH-", "") && DashboardForm.AccessType == "USER")
                    //                {
                    //                    userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //                    UserSetting userSettings = new UserSetting();
                    //                    userSettings.ShowDialog();
                    //                }
                    if (Dashboard.sections.Replace("BIPH-", "") == "BPS")
                    {
                        userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        UserSetting userSettings = new UserSetting();
                        userSettings.ShowDialog();
                    }
                    else if (Dashboard.sections.Replace("BIPH-", "") == "Production Engineering")
                    {
                        userSection = ApproverSettingDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        UserSetting userSettings = new UserSetting();
                        userSettings.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to access this setting!");
                    }
                }
                else
                {
                    //MessageBox.Show("You click the wrong button hehe");
                }
            }
            catch
            {}
            
        }

        // ---> Search function
        private void SearchSection()
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand SearchSection = new SqlCommand("SP_SearchSection", con);
            SearchSection.CommandType = CommandType.StoredProcedure;
            SearchSection.Parameters.AddWithValue("@Section", SearchBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(SearchSection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ApproverSettingDataGrid.DataSource = dt;
            con.Close();
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchSection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var uploadTemplate = new UploadTemplateForm();
            uploadTemplate.ShowDialog();
        }

        private void DownloadTemplateButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\PE_COPQ System\03_System Requirement\Template.xlsx");
        }
    }
}

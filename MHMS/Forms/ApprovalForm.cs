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

namespace MHMS.Forms
{
    public partial class ApprovalForm : Form
    {

        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public ApprovalForm()
        {
            InitializeComponent();
        }

        private void AddRole()
        {
            if (LoginForm.COPQPIC == "✔️" && LoginForm.ProcessInCharge == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                RoleDropDown.Items.Add("SPV");

                //
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.COPQPIC == "✔️" && LoginForm.ProcessInCharge == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                //
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.COPQPIC == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                RoleDropDown.Items.Add("SPV");

                //
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.ProcessInCharge == "✔️" && LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                RoleDropDown.Items.Add("SPV");

                //
                RoleDropDown.Text = "COPQ Process In-Charge";
            }
            else if (LoginForm.COPQPIC == "✔️")
            {
                RoleDropDown.Items.Add("COPQ PIC");
                //
                RoleDropDown.Text = "COPQ PIC";
            }
            else if (LoginForm.ProcessInCharge == "✔️")
            {
                RoleDropDown.Items.Add("COPQ Process In-Charge");
                //
                RoleDropDown.Text = "COPQ Process In-Charge";
            }
            else if (LoginForm.SectionSPV == "✔️")
            {
                RoleDropDown.Items.Add("SPV");
                //
                RoleDropDown.Text = "SPV";
            }
            else if (LoginForm.SectionMGR == "✔️")
            {
                RoleDropDown.Items.Add("MGR");
                //
                RoleDropDown.Text = "MGR";
            }
        }
        //===================================================================================================================>>>>>>>>>>>>

        string FullName;
        private void ApprovalForm_Load(object sender, EventArgs e)
        {
            FullName = LoginForm.FirstName + " " + LoginForm.LastName;

            AddRole(); //add items in combobox role

            //Set backcolor and fore color to column header
            ApprovalDataGrid.EnableHeadersVisualStyles = true;
            ApprovalDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(86, 119, 157);
            ApprovalDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Hide row header
            ApprovalDataGrid.RowHeadersVisible = false;

            //This is admin function to show radio button for filtering request for approval
            ShowRadioButtonForAdmin();

           
            //Add checked box column
            AddCheckedBoxColumn();

            /*FormatHeaderText();*/ // Format Column Header Text
            if (DashboardForm2.COPQAcceptanceButtonIsClicked == true)
            {
                CategoryDropdown.Text = "COPQ";
            }
            //else if (DashboardForm2.STButtonIsClicked == true)
            //{
            //    CategoryDropdown.Text = "ST";
            //}
            //else if (DashboardForm2.WCCCButtonIsClicked == true)
            //{
            //    CategoryDropdown.Text = "WC/CC";
            //}


            //LoadApplyingApprovalData(); // Display all request for approval

            //UI 
            if (LoginForm.UserSection == "Quality Innovation")
            {
                TypeofApprovalDropdown.Text = "QI Confirmation";
                TypeofApprovalDropdown.Enabled = false;
                RoleDropDown.Text = "QI";
                RoleDropDown.Enabled = false;
                StatusDropdown.Text = "For QI Confirmation";
                //StatusDropdown.Enabled = false;

                StatusDropdown.Items.Add("For QI Confirmation"); // Add "For QI Confirmation" item in dropdown list when QI user logged in
                StatusDropdown.Items.Remove("Cancelled"); // Removed "Cancelled" item in  dropdown list when QI user logged in

                AcceptButton.Text = "CONFIRM";

                ExcludeCheckBox.Visible = true; // show checkbox
            }

            ////Checked as default to exclude EE when for was loaded
            //ExcludeCheckBox.Checked = true;

        }

        //===================================================================================================================>>>>>>>>>>>>

        private void AddCheckedBoxColumn()
        {
            // Add checkbox column in datagrid
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Select";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.FillWeight = 50; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            ApprovalDataGrid.Columns.Add(checkColumn);
            checkColumn.DisplayIndex = 0;
            checkColumn.Frozen = true;
            // <<----------
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void SelectApprovalCount()
        {
            ApprovalCount.Visible = true;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectApprovalCount = new SqlCommand("SP_SelectApprovalCount", con);
            SelectApprovalCount.CommandType = CommandType.StoredProcedure;
            SelectApprovalCount.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            SelectApprovalCount.Parameters.AddWithValue("@Role", RoleDropDown.Text);
            SelectApprovalCount.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            SelectApprovalCount.Parameters.AddWithValue("@Status", StatusDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalCount);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                SqlDataReader reader = SelectApprovalCount.ExecuteReader();
                while (reader.Read())
                {
                    ApprovalCount.Text = reader["ApprovalCount"].ToString() + " For Approval";
                   
                }
            }

            con.Close();
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void ShowRadioButtonForAdmin()
        {
            if (LoginForm.UserSection == "Production Engineering" || SectionMenuForm.UserSection == "Production Engineering")
            {
                /*This is for future update if nedeed -- uncomment the code below once it's needed*/

                //MySectionRadioBtn.Visible = true;
                //AllSectionRadioBtn.Visible = true;
            }
            else
            {
                //MySectionRadioBtn.Visible = false;
                //AllSectionRadioBtn.Visible = false;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FormatHeaderText()
        {
            ApprovalDataGrid.Columns["ReferenceNo"].HeaderText = "Reference No.";
            ApprovalDataGrid.Columns["DateEncountered"].HeaderText = "Date Encountered";
            ApprovalDataGrid.Columns["MHLossType"].HeaderText = "MH Loss Type";
            ApprovalDataGrid.Columns["Section"].HeaderText = "Section";
            ApprovalDataGrid.Columns["CostCenter"].HeaderText = "Cost Center";
            ApprovalDataGrid.Columns["ResponsibleSection"].HeaderText = "Responsible Section";
            ApprovalDataGrid.Columns["LineStopDetail"].HeaderText = "Rease (Line Stop Detail)";
            ApprovalDataGrid.Columns["StopTime"].HeaderText = "Stop Time";
            ApprovalDataGrid.Columns["DirectMP"].HeaderText = "Direct MP";
            ApprovalDataGrid.Columns["SemiDirectMP"].HeaderText = "Semi-Direct MP";
            ApprovalDataGrid.Columns["LossManhour"].HeaderText = "Loss Manhour";
            ApprovalDataGrid.Columns["Reason"].HeaderText = "Reason";
            ApprovalDataGrid.Columns["COPQAmount(USD)"].HeaderText = "COPQ Amount";
            ApprovalDataGrid.Columns["Cause"].HeaderText = "Cause";
            ApprovalDataGrid.Columns["ReasonOfRejection"].HeaderText = "Reason of Rejection";
            ApprovalDataGrid.Columns["Countermeasure"].HeaderText = "Countermeasure (if accepted) / Reason (if rejected)";
            ApprovalDataGrid.Columns["ApplyingApprovalStatus"].HeaderText = "Applying Approval Status";
            ApprovalDataGrid.Columns["ReceivingApprovalStatus"].HeaderText = "Receiving Approval Status";
            ApprovalDataGrid.Columns["QIConfirmation"].HeaderText = "QI Confirmation";
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Select all for approval data where section column is equal to user section
        private void LoadApplyingApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SPV approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

            }
            else if (Dashboard.SectionText == "BIPH-Production Engineering")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SPV approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {

                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {

                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {

                    }
                    // -> SQL query to select approval data for MGR approval
                    SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                    SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                    SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                    SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();
                }
            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Logistics Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Incoming Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Information Technology")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Material Purchasing 2")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Assurance")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Innovation")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Supplier Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApplyingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }

        }

        //====================================================================================================================>>>>>>>>>>>>

        //Select all for approval data where responsible section is equal to user section
        private void LoadReceivingApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Dashboard.SectionText == "BIPH-BPS")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectBPSApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Engineering")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionEngineeringApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Cartridge")
            {

                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkCartridgeApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-PCBA")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPCBAApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Ink Head")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInkHeadApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Molding Production")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMoldingApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Printer")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPrinterApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-P-Touch")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectPTouchApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }
            }
            else if (Dashboard.SectionText == "BIPH-Tape Cassette")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectTapeCassetteApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Logistics Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectLogisticsControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Incoming Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectIncomingQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Information Technology")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectInformationTechnologyApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Material Purchasing 2")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }


                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectMaterialPurchasing2ApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Production Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectProductionControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Assurance")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityAssuranceApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Quality Innovation")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectQualityInnovationApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
            }
            else if (Dashboard.SectionText == "BIPH-Supplier Quality Control")
            {
                if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ PIC")
                    {
                        // -> SQL query to select approval data for COPQ PIC approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                {
                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select approval data for COPQ process in-charge approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }

                if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                {
                    if (RoleDropDown.Text == "SPV")
                    {
                        // -> SQL query to select approval data for SVP approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }

                }

                if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                {
                    if (RoleDropDown.Text == "MGR")
                    {
                        // -> SQL query to select approval data for MGR approval
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectSupplierQualityControlApprovalData");
                        SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ApprovalDataGrid.DataSource = dt;
                        con.Close();
                    }
                }
                else if (Dashboard.SectionText == "BIPH-Development Engineering")
                {
                    if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ PIC")
                        {
                            // -> SQL query to select approval data for COPQ PIC approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "COPQPIC");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }

                    if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ Process In-Charge")
                        {
                            // -> SQL query to select approval data for COPQ process in-charge approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "COPQProcessIncharge");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }

                    if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                    {
                        if (RoleDropDown.Text == "SPV")
                        {
                            // -> SQL query to select approval data for SVP approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "SPV");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }

                    }

                    if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                    {
                        if (RoleDropDown.Text == "MGR")
                        {
                            // -> SQL query to select approval data for MGR approval
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectReceivingApprovalData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectDevelopmentEngineeringApprovalData");
                            SelectApprovalData.Parameters.AddWithValue("@Position", "MGR");
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            ApprovalDataGrid.DataSource = dt;
                            con.Close();
                        }
                    }
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Check each checkbox
        private void SelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAllChkBox.Checked == true)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            }
            else if (SelectAllChkBox.Checked == false)
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchRequestForApprovalData();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SearchRequestForApprovalData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (TypeofApprovalDropdown.Text == "QI Confirmation")
            {
                if (ExcludeCheckBox.Checked == true)
                {
                    SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                    SearchMHLossData.CommandType = CommandType.StoredProcedure;
                    SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Role", "");
                    SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "true");
                    SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();
                }
                else
                {
                    SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                    SearchMHLossData.CommandType = CommandType.StoredProcedure;
                    SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@Role", "");
                    SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                    SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "");
                    SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();
                }
                

                //ExcludeEEData();
            }
            else
            {
                SqlCommand SearchMHLossData = new SqlCommand("SP_SearchRequestForApproval", con);
                SearchMHLossData.CommandType = CommandType.StoredProcedure;
                SearchMHLossData.Parameters.AddWithValue("@Search", SearchBox.Text);
                SearchMHLossData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                SearchMHLossData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                SearchMHLossData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                SearchMHLossData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                SearchMHLossData.Parameters.AddWithValue("@ExcludeEE", "");
                SqlDataAdapter sda = new SqlDataAdapter(SearchMHLossData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
           
           

            /*FormatHeaderText();*/ // Format header text
        }

        //===================================================================================================================>>>>>>>>>>>>
        private void copyAlltoClipboardsss()
        {
            //dgvComponentList.SelectAll();
            //DataObject dataObj = dgvComponentList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            ApprovalDataGrid.SelectAll();

            //Copy to clipboard
            ApprovalDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = ApprovalDataGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExportButton_Click(object sender, EventArgs e)
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

            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
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
            //    worksheet.Name = "Approval Data";

            //    try
            //    {
            //        for (int i = 0; i < ApprovalDataGrid.Columns.Count; i++)
            //        {
            //            worksheet.Cells[1, i + 1] = ApprovalDataGrid.Columns[i].HeaderText;
            //        }

            //        for (int i = 0; i < ApprovalDataGrid.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < ApprovalDataGrid.Columns.Count; j++)
            //            {
            //                if (ApprovalDataGrid.Rows[i].Cells[j].Value != null)
            //                {
            //                    worksheet.Cells[i + 2, j + 1] = ApprovalDataGrid.Rows[i].Cells[j].Value.ToString();
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

        //====================================================================================================================>>>>>>>>>>

        public static string SelectedRowReferenceNo;
        public static string SelectedLineStopDetail;
        public static string ApprovalType;

        private void RejectButton_Click(object sender, EventArgs e)
        {

            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("Please select the item you want to reject!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (selectedRows.Count > 1)
            {
                MessageBox.Show("Cannot process multiple selected data, Please select one item to reject request!", "Reminders!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                {
                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                    {
                        //SelectedRowReferenceNo = row.Cells["Reference No."].Value.ToString();
                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        PartCode = row.Cells["Part Code"].Value.ToString();
                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                        ApprovalType = TypeofApprovalDropdown.Text;

                        if (MessageBox.Show("Are you sure do you want to reject item with line stop details of " + "'" + SelectedLineStopDetail + "'?", "Reminders", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            RejectionForm rejectionForm = new RejectionForm();
                            rejectionForm.ShowDialog();
                        }
                    }
                }

                GenerateMHData();
            }

            //RejectionForm rejectionForm = new RejectionForm();
            //rejectionForm.ShowDialog();

            //if (LoginForm.COPQPIC == "✔️")
            //{
            //    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
            //    {
            //        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //        {
            //            SelectedRowReferenceNo = row.Cells["Reference No."].Value.ToString();

            //            if (MessageBox.Show("Are you sure do you want to reject item with reference no. of " + ApprovalForm.SelectedRowReferenceNo, "Status", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //            {
            //                RejectionForm rejectionForm = new RejectionForm();
            //                rejectionForm.ShowDialog();
            //            }
            //        }
            //    }
            //}


            //if (LoginForm.SectionSPV == "✔️")
            //{
            //    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
            //    {
            //        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //        {
            //            SelectedRowReferenceNo = row.Cells["Reference No."].Value.ToString();

            //            if (MessageBox.Show("Are you sure do you want to reject item with reference no. of " + ApprovalForm.SelectedRowReferenceNo, "Status", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //            {
            //                RejectionForm rejectionForm = new RejectionForm();
            //                rejectionForm.ShowDialog();
            //            }
            //        }
            //    }
            //}


            //if (LoginForm.SectionMGR == "✔️")
            //{
            //    foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
            //    {
            //        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
            //        {
            //            SelectedRowReferenceNo = row.Cells["Reference No."].Value.ToString();

            //            if (MessageBox.Show("Are you sure do you want to reject item with reference no. of " + ApprovalForm.SelectedRowReferenceNo, "Status", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //            {
            //                RejectionForm rejectionForm = new RejectionForm();
            //                rejectionForm.ShowDialog();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //if (COPQPICRadioButton.Checked)
            //    //{
            //    //    MessageBox.Show("You are not authorized.");
            //    //}
            //    //else if (COPQPICRadioButton.Checked)
            //    //{
            //    //    MessageBox.Show("You are not authorized.");
            //    //}
            //    //else if (MGRRadioButton.Checked)
            //    //{
            //    //    MessageBox.Show("You are not authorized.");
            //    //}

            //}
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void ApprovalDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewColumn column in ApprovalDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (ApprovalDataGrid.Rows[e.RowIndex].Cells["Reason of Rejection"].Value.ToString() != "")
            {
                DataGridViewRow row = ApprovalDataGrid.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 142, 150);
            }
        }

        //===================================================================================================================>>>>>>>>>>>>


        public static bool ContinueButtonIsClicked = false;
        public static bool ApproveButtonIsClicked = false;
        public static bool AcceptButtonIsClicked = false;
        private void FrefreshDatagridTimer_Tick(object sender, EventArgs e)
        {
            if (ContinueButtonIsClicked == true)
            {
                GenerateMHData();

                ContinueButtonIsClicked = false;
            }

            if (ApproveButtonIsClicked == true)
            {
                GenerateMHData();

                ApproveButtonIsClicked = false;
            }

            if (AcceptButtonIsClicked == true)
            {
                GenerateMHData();

                AcceptButtonIsClicked = false;
            }
        }

        //=================================================================================================================>>>>>>>>>>>>

        private void GenerateMHData()
        {
            if (CategoryDropdown.Text == "COPQ")
            {
                if (StatusDropdown.Text == "For Approval")
                {
                    // Check Connection status -> Open connection if the connection is closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if (RoleDropDown.Text == "COPQ Process In-Charge")
                    {
                        // -> SQL query to select process in-charge pic
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //Select all process in-charge user
                        SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
                        SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
                        SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter da = new SqlDataAdapter(SelectProcessInchargeUser);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        //if process in-charge count is greater than 1 select the full name of user 
                        if (dt.Rows.Count > 1)
                        {
                            //show all data where the receiving status is for approval by copq process in-charge and fullname is equal to user login
                            // -> SQL query to select approval data based on status
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectCOPQProcessInChargeData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                            SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SelectApprovalData.Parameters.AddWithValue("@FullName", FullName);
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dataTable = new DataTable();
                            sda.Fill(dataTable);
                            ApprovalDataGrid.DataSource = dataTable;
                            con.Close();
                        }
                        else if (dt.Rows.Count == 1)
                        {
                            //if process in-charge count is equal to 1
                            //show all data where the receiving status is for approval by copq process in-charge
                            // ->SQL query to select approval data based on status
                            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                            SelectApprovalData.CommandType = CommandType.StoredProcedure;
                            SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                            SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                            SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                            DataTable dataTable = new DataTable();
                            sda.Fill(dataTable);
                            ApprovalDataGrid.DataSource = dataTable;
                            con.Close();
                        }
                    }
                    else
                    {
                         //-> SQL query to select approval data based on status
                        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                        SelectApprovalData.CommandType = CommandType.StoredProcedure;
                        SelectApprovalData.Parameters.AddWithValue("@Status", "For Approval");
                        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                        SelectApprovalData.Parameters.AddWithValue("@Role", RoleDropDown.Text);
                        SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        ApprovalDataGrid.DataSource = dataTable;
                        con.Close();

                    }

                    ApprovalDataGrid.Columns["Select"].Visible = true;

                    SelectApprovalCount();

                }
                else if (StatusDropdown.Text == "Approved")
                {
                    // Check Connection status -> Open connection if the connection is closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // -> SQL query to select approval data based on status
                    SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                    SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    SelectApprovalData.Parameters.AddWithValue("@Status", "Approved");
                    SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SelectApprovalData.Parameters.AddWithValue("@Role", ""); //no need
                    SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();

                    //Hide check box
                    ApprovalDataGrid.Columns["Select"].Visible = false;

                    ApprovalCount.Visible = false; // hide for approval count
                }
                else if (StatusDropdown.Text == "Rejected")
                {
                    // Check Connection status -> Open connection if the connection is closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // -> SQL query to select approval data based on status
                    SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                    SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    SelectApprovalData.Parameters.AddWithValue("@Status", "Rejected");
                    SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SelectApprovalData.Parameters.AddWithValue("@Role", ""); //no need
                    SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();

                    //Hide check box
                    ApprovalDataGrid.Columns["Select"].Visible = false;

                    ApprovalCount.Visible = false; // hide for approval count
                }
                else if (StatusDropdown.Text == "Cancelled")
                {
                    // Check Connection status -> Open connection if the connection is closed
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // -> SQL query to select approval data based on status
                    SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                    SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    SelectApprovalData.Parameters.AddWithValue("@Status", "Cancelled");
                    SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                    SelectApprovalData.Parameters.AddWithValue("@Role", ""); //no need
                    SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ApprovalDataGrid.DataSource = dt;
                    con.Close();

                    //Hide check box
                    ApprovalDataGrid.Columns["Select"].Visible = false;
                    ApprovalCount.Visible = false; // hide for approval count
                }
                else if (TypeofApprovalDropdown.Text == "QI Confirmation")
                {
                    // Check Connection status -> Open connection if the connection is closed
                    //if (con.State == ConnectionState.Closed)
                    //{
                    //    con.Open();
                    //}
                    
                
                    //SqlCommand SelectApprovalData = new SqlCommand("SP_SelectFilteredMHData", con);
                    //SelectApprovalData.CommandType = CommandType.StoredProcedure;
                    //SelectApprovalData.Parameters.AddWithValue("@Status", StatusDropdown.Text);
                    //SelectApprovalData.Parameters.AddWithValue("@Type", "QI Confirmation");
                    //SelectApprovalData.Parameters.AddWithValue("@Role", "");
                    //SelectApprovalData.Parameters.AddWithValue("@Section", Dashboard.SectionText.Replace("BIPH-", ""));
                    //SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                    //ApprovalDataGrid.DataSource = dt;
                    //con.Close();

                    ExcludeEEData();
                    ApprovalCount.Visible = false; // hide for approval count
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            GenerateMHData();

            //if (CategoryDropdown.Text == "COPQ")
            //{
            //    if (StatusDropdown.Text == "For Approval")
            //    {
            //        if (LoginForm.COPQPIC == "✔️")
            //        {
            //            // Check Connection status -> Open connection if the connection is closed
            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            // -> SQL query to select approval data based on status
            //            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectForApproval");
            //            SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //            SelectApprovalData.Parameters.AddWithValue("@ApproverType", "COPQPIC");
            //            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            ApprovalDataGrid.DataSource = dt;
            //            con.Close();

            //            EnabledRadioButtonForApprover();
            //        }
            //        else if (LoginForm.ProcessInCharge == "✔️")
            //        {
            //            // Check Connection status -> Open connection if the connection is closed
            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            // -> SQL query to select approval data based on status
            //            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectForApproval");
            //            SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //            SelectApprovalData.Parameters.AddWithValue("@ApproverType", "COPQProcessIncharge");
            //            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            ApprovalDataGrid.DataSource = dt;
            //            con.Close();

            //            EnabledRadioButtonForApprover();
            //        }
            //        else if (LoginForm.SectionSPV == "✔️")
            //        {

            //            // Check Connection status -> Open connection if the connection is closed
            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            // -> SQL query to select approval data based on status
            //            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectForApproval");
            //            SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //            SelectApprovalData.Parameters.AddWithValue("@ApproverType", "SPV");
            //            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            ApprovalDataGrid.DataSource = dt;
            //            con.Close();

            //            EnabledRadioButtonForApprover();

            //        }
            //        else if (LoginForm.SectionMGR == "✔️")
            //        {
            //            // Check Connection status -> Open connection if the connection is closed
            //            if (con.State == ConnectionState.Closed)
            //            {
            //                con.Open();
            //            }

            //            // -> SQL query to select approval data based on status
            //            SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //            SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //            SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectForApproval");
            //            SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //            SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //            SelectApprovalData.Parameters.AddWithValue("@ApproverType", "MGR");
            //            SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            ApprovalDataGrid.DataSource = dt;
            //            con.Close();

            //            EnabledRadioButtonForApprover();
            //        }

            //    }
            //    else if (StatusDropdown.Text == "Approved")
            //    {

            //        // Check Connection status -> Open connection if the connection is closed
            //        if (con.State == ConnectionState.Closed)
            //        {
            //            con.Open();
            //        }

            //        // -> SQL query to select approval data based on status
            //        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //        SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectApproved");
            //        SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //        SelectApprovalData.Parameters.AddWithValue("@ApproverType", "");
            //        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        ApprovalDataGrid.DataSource = dt;
            //        con.Close();

            //        EnabledRadioButtonForApprover();

            //    }
            //    else if (StatusDropdown.Text == "Rejected")
            //    {
            //        // Check Connection status -> Open connection if the connection is closed
            //        if (con.State == ConnectionState.Closed)
            //        {
            //            con.Open();
            //        }

            //        // -> SQL query to select approval data based on status
            //        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //        SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectRejected");
            //        SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //        SelectApprovalData.Parameters.AddWithValue("@ApproverType", "");
            //        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        ApprovalDataGrid.DataSource = dt;
            //        con.Close();

            //        EnabledRadioButtonForApprover();
            //    }
            //    else if (StatusDropdown.Text == "Cancelled")
            //    {
            //        // Check Connection status -> Open connection if the connection is closed
            //        if (con.State == ConnectionState.Closed)
            //        {
            //            con.Open();
            //        }

            //        // -> SQL query to select approval data based on status
            //        SqlCommand SelectApprovalData = new SqlCommand("SP_SelectApprovalDataBasedOnStatus", con);
            //        SelectApprovalData.CommandType = CommandType.StoredProcedure;
            //        SelectApprovalData.Parameters.AddWithValue("@Procedure", "SelectCancelled");
            //        SelectApprovalData.Parameters.AddWithValue("@Section", LoginForm.UserSection);
            //        SelectApprovalData.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
            //        SelectApprovalData.Parameters.AddWithValue("@ApproverType", "");
            //        SqlDataAdapter sda = new SqlDataAdapter(SelectApprovalData);
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        ApprovalDataGrid.DataSource = dt;
            //        con.Close();

            //        EnabledRadioButtonForApprover();
            //    }
            //}
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FilterApprovalStatus()
        {

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TypeofApprovalDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            //FilterApprovalDataBasedOnSelectedIndex();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FilterApprovalDataBasedOnSelectedIndex()
        {
            if (TypeofApprovalDropdown.Text == "Applying")
            {
                LoadApplyingApprovalData();
            }
            else if (TypeofApprovalDropdown.Text == "Receiving")
            {
                LoadReceivingApprovalData();
            }
        }

        public static string SelectedReferenceNo;
        public static string LineStopDetail;
        public static string PartCode;
        public static string COPQAmount;
        public static string DateEncountered;

        private void ApprovalDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ApprovalDataGrid.Columns[e.ColumnIndex].Name == "Select")
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)ApprovalDataGrid.Rows[e.RowIndex].Cells["Select"];

                if ((bool)checkCell.Value == true)
                {
                    int i = ApprovalDataGrid.CurrentRow.Index;
                    LineStopDetail = ApprovalDataGrid.Rows[e.RowIndex].Cells["Line Stop Detail"].Value.ToString();

                    if ((bool)checkCell.Value == false)
                    {
                        checkCell.EditingCellValueChanged = false;
                    }
                }
            }
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in ApprovalDataGrid.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["Select"].Value) == true
                                                  select row).ToList();

            if (selectedRows.Count < 1 || selectedRows.Count == 0)
            {
                MessageBox.Show("Please select data you want to approve!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (TypeofApprovalDropdown.Text == "Applying")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ PIC")
                        {
                            foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                            {
                                DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                PartCode = row.Cells["Part Code"].Value.ToString();
                                ApprovalType = TypeofApprovalDropdown.Text;

                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    COPQConfirmationForm copqConfirmationForm = new COPQConfirmationForm();
                                    copqConfirmationForm.ShowDialog();
                                }
                            }

                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                            //LoadApplyingApprovalData();
                        }
                    }
                    else if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                    {
                        if (RoleDropDown.Text == "SPV")
                        {
                            foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                            {
                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;

                                    // -> SQL query to update approval status
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                                    UpdateApprovalStatus.ExecuteNonQuery();
                                    con.Close();

                                }
                            }

                            //AcceptButtonIsClicked = true;
                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                            //LoadApplyingApprovalData();
                        }
                    }
                    else if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                    {
                        if (RoleDropDown.Text == "MGR")
                        {
                            foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                            {
                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;

                                    // -> SQL query to update approval status
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                                    UpdateApprovalStatus.ExecuteNonQuery();
                                    con.Close();
                                }
                            }

                            //AcceptButtonIsClicked = true;
                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                            //LoadApplyingApprovalData();
                        }
                    }
                }

                if (TypeofApprovalDropdown.Text == "Receiving")
                {
                    if (LoginForm.COPQPIC == "✔️" || SectionMenuForm.COPQPIC == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ PIC")
                        {
                            // -> SQL query to select process in-charge pic
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand SelectProcessInchargeUser = new SqlCommand("SP_SelectProcessInchargeUsers", con);
                            SelectProcessInchargeUser.CommandType = CommandType.StoredProcedure;
                            SelectProcessInchargeUser.Parameters.AddWithValue("@UserSection", Dashboard.SectionText.Replace("BIPH-", ""));
                            SqlDataAdapter da = new SqlDataAdapter(SelectProcessInchargeUser);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 1)
                            {
                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {

                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        PartCode = row.Cells["Part Code"].Value.ToString();
                                        ApprovalType = TypeofApprovalDropdown.Text;

                                        //Show Process in-charge form
                                        ProcessInchargeForm processInChanrge = new ProcessInchargeForm();
                                        processInChanrge.ShowDialog();
                                    }
                                }

                                GenerateMHData();
                                SelectAllChkBox.Checked = false;
                            }

                            if (dt.Rows.Count == 1)
                            {
                                foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                                {
                                    if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                    {
                                        DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                        LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                        PartCode = row.Cells["Part Code"].Value.ToString();
                                        ApprovalType = TypeofApprovalDropdown.Text;

                                        // -> SQL query to update approval status
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }

                                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by COPQ Process In-Charge");
                                        UpdateApprovalStatus.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }

                                //AcceptButtonIsClicked = true;
                                MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //LoadReceivingApprovalData();
                                GenerateMHData();
                                SelectAllChkBox.Checked = false;
                            }
                        }
                    }

                    if (LoginForm.ProcessInCharge == "✔️" || SectionMenuForm.ProcessInCharge == "✔️")
                    {
                        if (RoleDropDown.Text == "COPQ Process In-Charge")
                        {
                            foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                            {
                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;
                                    COPQAmount = row.Cells["COPQ Amount"].Value.ToString();

                                    // -> SQL query to update approval status
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    if (Convert.ToDecimal(COPQAmount) >= 100)
                                    {
                                        ProcessInChargeConfirmationForm processInChargeConfirmationForm = new ProcessInChargeConfirmationForm();
                                        processInChargeConfirmationForm.ShowDialog();


                                    }
                                    else
                                    {
                                        SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                        UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                        UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                        UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                                        UpdateApprovalStatus.ExecuteNonQuery();
                                        con.Close();

                                    }
                                }
                            }

                            //AcceptButtonIsClicked = true;
                            MessageBox.Show("Approved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //LoadReceivingApprovalData();
                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                        }
                    }

                    if (LoginForm.SectionSPV == "✔️" || SectionMenuForm.SectionSPV == "✔️")
                    {
                        if (RoleDropDown.Text == "SPV")
                        {
                            foreach (DataGridViewRow row in ApprovalDataGrid.Rows)
                            {
                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;

                                    // -> SQL query to update approval status
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionSPV");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by MGR");
                                    UpdateApprovalStatus.ExecuteNonQuery();
                                    con.Close();


                                }
                            }

                            //AcceptButtonIsClicked = true;
                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LoadReceivingApprovalData();
                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                        }
                    }

                    if (LoginForm.SectionMGR == "✔️" || SectionMenuForm.SectionMGR == "✔️")
                    {
                        if (RoleDropDown.Text == "MGR")
                        {
                            foreach (DataGridViewRow row in selectedRows)
                            {
                                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                                {
                                    DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                                    LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                                    PartCode = row.Cells["Part Code"].Value.ToString();
                                    ApprovalType = TypeofApprovalDropdown.Text;

                                    // -> SQL query to update approval status
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                                    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionMGR");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                                    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                                    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Approved");
                                    UpdateApprovalStatus.ExecuteNonQuery();
                                    con.Close();

                                }
                            }

                            //AcceptButtonIsClicked = true;
                            MessageBox.Show("Approved Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LoadReceivingApprovalData();
                            GenerateMHData();
                            SelectAllChkBox.Checked = false;
                        }
                    }
                }
                else if (TypeofApprovalDropdown.Text == "QI Confirmation")
                {
                    //Update QI Confirmation to Confirmed by Username, Date of approval
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                        {
                            DateEncountered = row.Cells["Date Encountered"].Value.ToString();
                            LineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                            SelectedLineStopDetail = row.Cells["Line Stop Detail"].Value.ToString();
                            PartCode = row.Cells["Part Code"].Value.ToString();
                            ApprovalType = TypeofApprovalDropdown.Text;

                            // -> SQL query to update approval status
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateQIConfirmationStatus", con);
                            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                            UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail);
                            UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", PartCode);
                            UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", DateEncountered);
                            UpdateApprovalStatus.Parameters.AddWithValue("@Type", TypeofApprovalDropdown.Text);
                            UpdateApprovalStatus.Parameters.AddWithValue("@ConfirmedBy", "Confirmed by: " + LoginForm.FirstName + " " + LoginForm.LastName + ", " + DateTime.Now.ToString());
                            UpdateApprovalStatus.ExecuteNonQuery();
                            con.Close();

                        }
                    }

                    //AcceptButtonIsClicked = true;
                    MessageBox.Show("Confirmed Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //LoadReceivingApprovalData();
                    ExcludeEEData();

                    //GenerateMHData();
                    SelectAllChkBox.Checked = false;
                   
                }
            }
        }


        private void ExcludeEEData()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (ExcludeCheckBox.Checked == true)
            {
                //Exclude EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "ExcludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
            else
            {
                //Include EE data
                SqlCommand SelectForQIConfirmationData = new SqlCommand("SP_SelectForQIConfirmationData", con);
                SelectForQIConfirmationData.CommandType = CommandType.StoredProcedure;
                SelectForQIConfirmationData.Parameters.AddWithValue("@procedure", "IncludeEEData");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Status", "For Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Type", "QI Confirmation");
                SelectForQIConfirmationData.Parameters.AddWithValue("@Role", "QI");
                SqlDataAdapter sda = new SqlDataAdapter(SelectForQIConfirmationData);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ApprovalDataGrid.DataSource = dt;
                con.Close();
            }
        }

        private void ExcludeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ExcludeEEData();
        }

        private void RoleDropDown_TextChanged(object sender, EventArgs e)
        {
            if (RoleDropDown.Text == "COPQ Process In-Charge")
            {
                TypeofApprovalDropdown.Text = "Receiving";
            }
            else
            {}
        }

        //====================================================================================================================>>>>>>>
    }
}
    


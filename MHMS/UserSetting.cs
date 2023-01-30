using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class UserSetting : Form
    {
        // Connection string
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public UserSetting()
        {
            InitializeComponent();
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TopPanel_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // <---------------------------

        //====================================================================================================================>>>>>>>>>>>>

        private void UserSetting_Load(object sender, EventArgs e)
        {
            FirstName.Focus();

            Section.Text = Forms.ApproverSettingForm.userSection;

            LoadAllUsers(); // ---> Calling function to load all users

            // ---> Disable Sort of MH PIC Data Grid View
            foreach (DataGridViewColumn column in UsersDataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LoadAllUsers()
        {
            // -> SQL query to select User Account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand LoadUsersPIC = new SqlCommand("SP_LoadUsersPIC", con);
            LoadUsersPIC.CommandType = CommandType.StoredProcedure;
            LoadUsersPIC.Parameters.AddWithValue("@Procedure", "SelectUserAccount");
            LoadUsersPIC.Parameters.AddWithValue("@Section", Section.Text.Replace("BIPH-", ""));
            SqlDataAdapter sda = new SqlDataAdapter(LoadUsersPIC);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UsersDataGrid.DataSource = dt;
            con.Close();
        }

        //====================================================================================================================>>>>>>>>>>>>

        string MHPIC_Value = "";
        string COPQPIC_Value = "";
        string PCPIC_Value = "";
        string Supervisor_Value = "";
        string Manager_Value = "";
        string GeneralManager_Value = "";
        string BILSupport_Value = "";
        string COPQProcessInCharge_Value = "";

        // --> Function for adding section in database
        private void AddNewUser()
        {
            //Regular expression for email validation
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(Email.Text.Trim());
            // <------

            if (ADID.Text == "" || Email.Text == "" || FirstName.Text == "" || LastName.Text == "")
            {
                MessageBox.Show("Please fill-in all fields.");
            }
            else if (MHPICCheckBox.Checked == false && COPQPICCheckBox.Checked == false && PCPICCheckBox.Checked == false && SupervisorCheckBox.Checked == false && ManagerCheckBox.Checked == false && GeneralMangerCheckBox.Checked == false && BILSupportCheckBox.Checked == false && COPQProcessInChargeCheckBox.Checked == false)
            {
                MessageBox.Show("User type is required. Please select user type atleast one.");
            }
            else if (!isValid)
            {
                MessageBox.Show("Invalid email address.");
            }
            else
            {
                if (MHPICCheckBox.Checked)
                {
                    MHPIC_Value = "✔️";
                }
                else
                {
                    MHPIC_Value = "";
                }

                if (COPQPICCheckBox.Checked)
                {
                    COPQPIC_Value = "✔️";
                }
                else
                {
                    COPQPIC_Value = "";
                }

                if (PCPICCheckBox.Checked)
                {
                    PCPIC_Value = "✔️";
                }
                else
                {
                    PCPIC_Value = "";
                }

                if (SupervisorCheckBox.Checked)
                {
                    Supervisor_Value = "✔️";
                }
                else
                {
                    Supervisor_Value = "";
                }

                if (ManagerCheckBox.Checked)
                {
                    Manager_Value = "✔️";
                }
                else
                {
                    Manager_Value = "";
                }

                if (GeneralMangerCheckBox.Checked)
                {
                    GeneralManager_Value = "✔️";
                }
                else
                {
                    GeneralManager_Value = "";
                }

                if (BILSupportCheckBox.Checked)
                {
                    BILSupport_Value = "✔️";
                }
                else
                {
                    BILSupport_Value = "";
                }

                if (COPQProcessInChargeCheckBox.Checked)
                {
                    COPQProcessInCharge_Value = "✔️";
                }
                else
                {
                    COPQProcessInCharge_Value = "";
                }

               
                // -> SQL query to insert user account
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand InsertUSer = new SqlCommand("SP_InsertUser", con);
                InsertUSer.CommandType = CommandType.StoredProcedure;
                InsertUSer.Parameters.AddWithValue("@FirstName", FirstName.Text);
                InsertUSer.Parameters.AddWithValue("@LastName", LastName.Text);
                InsertUSer.Parameters.AddWithValue("@ADID", ADID.Text);
                InsertUSer.Parameters.AddWithValue("@Email", Email.Text);
                InsertUSer.Parameters.AddWithValue("@Password", ADID.Text);
                InsertUSer.Parameters.AddWithValue("@Section", Section.Text);
                InsertUSer.Parameters.AddWithValue("@AccountType", "USER");
                InsertUSer.Parameters.AddWithValue("@Status", "ACTIVE");
                InsertUSer.Parameters.AddWithValue("@MHPIC", MHPIC_Value);
                InsertUSer.Parameters.AddWithValue("@COPQPIC", COPQPIC_Value);
                InsertUSer.Parameters.AddWithValue("@PCPIC", PCPIC_Value);
                InsertUSer.Parameters.AddWithValue("@Supervisor", Supervisor_Value);
                InsertUSer.Parameters.AddWithValue("@Manager", Manager_Value);
                InsertUSer.Parameters.AddWithValue("@GeneralManager", GeneralManager_Value);
                InsertUSer.Parameters.AddWithValue("@BILSupport", BILSupport_Value);
                InsertUSer.Parameters.AddWithValue("@COPQProcessInCharge", COPQProcessInCharge_Value);
                InsertUSer.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString());
                InsertUSer.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("User Successfuly Added!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ---> Clear inputed data in textbox
                ADID.Clear();
                Email.Clear();
                FirstName.Clear();
                LastName.Clear();
            }

        } // --> End of Function

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Unchecked all selected checkboxes
        private void UncheckedSelectedUserType()
        {
            MHPICCheckBox.Checked = false;
            COPQPICCheckBox.Checked = false;
            PCPICCheckBox.Checked = false;
            SupervisorCheckBox.Checked = false;
            ManagerCheckBox.Checked = false;
            GeneralMangerCheckBox.Checked = false;
            BILSupportCheckBox.Checked = false;
            COPQProcessInChargeCheckBox.Checked = false;
        }// <---

        //====================================================================================================================>>>>>>>>>>>>
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            AddNewUser();
            UncheckedSelectedUserType();
            LoadAllUsers();
        }

        //====================================================================================================================>>>>>>>>>>>>

        //initialized variable use to store data from datagrid
        string ID = "";
        string First_Name = "";
        string Last_Name = "";

        private void UsersDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = UsersDataGrid.Rows[e.RowIndex].Cells["ADID"].Value.ToString();
            First_Name = UsersDataGrid.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
            Last_Name = UsersDataGrid.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Delete user function
        private void DeleteUser()
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure do you want to delete user {First_Name} {Last_Name}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                // -> SQL query to delete Section to user setting
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand InsertUSer = new SqlCommand("SP_DeleteUser", con);
                InsertUSer.CommandType = CommandType.StoredProcedure;
                InsertUSer.Parameters.AddWithValue("@ADID", ID);
                InsertUSer.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("User Successfuly Deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { }


        } // --> End of Function

        //====================================================================================================================>>>>>>>>>>>>

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            DeleteUser();
            LoadAllUsers();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            Email.Text = (FirstName.Text).ToLower().Replace(" ", "") + "." + (LastName.Text).ToLower().Replace(" ", "") + "@brother-biph.com.ph";
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            Email.Text = (FirstName.Text).ToLower().Replace(" ", "") + "." + (LastName.Text).ToLower().Replace(" ", "") + "@brother-biph.com.ph";
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //====================================================================================================================>>>>>>>>>>>>

    }
}

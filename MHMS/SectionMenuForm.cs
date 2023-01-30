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

namespace MHMS
{
    public partial class SectionMenuForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public SectionMenuForm()
        {
            InitializeComponent();
        }

        private void SectionMenuForm_Load(object sender, EventArgs e)
        {
            LoadUserSection();
            SectionDropdownList.Text = "Please select section";
        }

        //======================================================================================>>>>>>>>>>>>>>>>>>>>>

        private void LoadUserSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAccount", con);
            SelectUserAccount.CommandType = CommandType.StoredProcedure;
            SelectUserAccount.Parameters.AddWithValue("@Procedure", "SelectUserSection");
            SelectUserAccount.Parameters.AddWithValue("@ADID", LoginForm.UserADID);
            SelectUserAccount.Parameters.AddWithValue("@Password", "");
            SelectUserAccount.Parameters.AddWithValue("@Section", "");
            SqlDataAdapter sda = new SqlDataAdapter(SelectUserAccount);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SelectUserAccount.ExecuteNonQuery();
            con.Close();

            SectionDropdownList.DataSource = ds.Tables[0];
            SectionDropdownList.DisplayMember = ds.Tables[0].Columns[0].ToString();
        }

        //======================================================================================>>>>>>>>>>>>>>>>>>>>>

        //======================================================================================>>>>>>>>>>>>>>>>>>>>>

        // Variable use for handling data from database
        public static string UserADID = "";
        public static string UserPassword = "";
        public static string UserSection = "";
        public static string FirstName = "";
        public static string LastName = "";
        public static string EmailAddress = "";
        public static string SectionName = "";
        public static string AccountType = "";
        public static string COPQPIC = "";
        public static string SectionSPV = "";
        public static string SectionMGR = "";
        public static string ProcessInCharge = "";


        public static bool isMultiSectionAccess;

        private void SignInButton_Click(object sender, EventArgs e)
        {
            // -> SQL query to select User Account
            SqlConnection con = new SqlConnection(MHMS_Conn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAccount", con);
            SelectUserAccount.CommandType = CommandType.StoredProcedure;
            SelectUserAccount.Parameters.AddWithValue("@Procedure", "SelectUserAccount2");
            SelectUserAccount.Parameters.AddWithValue("@ADID", LoginForm.UserADID);
            SelectUserAccount.Parameters.AddWithValue("@Password", LoginForm.UserPassword);
            SelectUserAccount.Parameters.AddWithValue("@Section", SectionDropdownList.Text);
            SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                SqlDataReader reader = SelectUserAccount.ExecuteReader();
                if (reader.Read())
                {
                    UserADID = reader["ADID"].ToString();
                    UserSection = reader["Section"].ToString();
                    FirstName = reader["First Name"].ToString();
                    LastName = reader["Last Name"].ToString();
                    EmailAddress = reader["Email"].ToString();
                    SectionName = "BIPH-" + reader["Section"].ToString();
                    AccountType = reader["Account Type"].ToString();

                    COPQPIC = reader["COPQ PIC"].ToString();
                    ProcessInCharge = reader["COPQ Process In-charge"].ToString();
                    SectionSPV = reader["Supervisor"].ToString();
                    SectionMGR = reader["Manager"].ToString();

                    isMultiSectionAccess = true;

                    // -> this code is use to Redirect  from this form to main form
                    this.Hide();
                    Dashboard formDasboard = new Dashboard();
                    formDasboard.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Incorrect ADID or password", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        //======================================================================================>>>>>>>>>>>>>>>>>>>>>

    }
}

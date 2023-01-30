using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class LoginForm : Form
    {
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

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
        //public static string QIConfirmation = "";

        //====================================================================================================================>>>>>>>>>>>>
        public static bool isSingleSectionAccess;
        private void LoginUser()
        {
            //-> textbox validation
            if (ADID.Text == "" && Password.Text == "")
            {
                MessageBox.Show("Please type your ADID and Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ADID.Text == "")
            {
                MessageBox.Show("Please type your ADID", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Password.Text == "")
            {
                MessageBox.Show("Please type your Password", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // -> SQL query to select User Account
                SqlConnection con = new SqlConnection(MHMS_Conn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand SelectUserAccount = new SqlCommand("SP_SelectUserAccount", con);
                SelectUserAccount.CommandType = CommandType.StoredProcedure;
                SelectUserAccount.Parameters.AddWithValue("@Procedure", "SelectUserAcount");
                SelectUserAccount.Parameters.AddWithValue("@ADID", ADID.Text);
                SelectUserAccount.Parameters.AddWithValue("@Password", Password.Text);
                SelectUserAccount.Parameters.AddWithValue("@Section", "");
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

                        isSingleSectionAccess = true;

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
                else if (dt.Rows.Count > 1)
                {
                    
                    SqlDataReader reader = SelectUserAccount.ExecuteReader();

                    if (reader.Read())
                    {
                        UserADID = reader["ADID"].ToString();
                        UserPassword = Password.Text;
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

                        this.Hide();
                        SectionMenuForm sectionMenuForm = new SectionMenuForm();
                        sectionMenuForm.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    // -> this Message show when the user account is not existing in database
                    MessageBox.Show("User not found!", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }  //***** End of login fuction *****

        //===================================================================================================================>>>>>>>>>>>>

        // ---> Hide the character inputed in password box
        private void HidePasswordEyeButton_Click(object sender, EventArgs e)
        {
            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordEyeButton.BringToFront();
            HidePasswordEyeButton.SendToBack();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Show the character inputed in password box
        private void ShowPasswordEyeButton_Click(object sender, EventArgs e)
        {
            if (Password.PasswordChar == '•')
            {
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }

            ShowPasswordEyeButton.SendToBack();
            HidePasswordEyeButton.BringToFront();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Login user when click the sign in button
        private void SignInButton_Click(object sender, EventArgs e)
        {
            LoginUser(); // call out the login user function
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Show change password form when click the forgot password button
        private void ForgotPassword_Click(object sender, EventArgs e)
        {
            Hide();
            ChangePassword changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Focus to password text box when hit enter key
        private void ADID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Password.Focus();
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        // ---> Login user when hit the enter key
        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                LoginUser(); // call out the login user function
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ADID.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("AP\\", "");

        }

        private void SignInButton_MouseEnter(object sender, EventArgs e)
        {
            SignInButton.BackColor = Color.FromArgb(211, 240, 254);
            SignInButton.ForeColor = Color.FromArgb(21, 35, 53);
        }

        private void SignInButton_MouseLeave(object sender, EventArgs e)
        {
            SignInButton.BackColor = Color.FromArgb(21, 35, 53);
            SignInButton.ForeColor = Color.FromArgb(211, 240, 254);
        }


        //====================================================================================================================>>>>>>>>>>>>
    }
}

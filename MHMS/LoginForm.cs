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
    public partial class LoginForm : Form
    {
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

        // Variable use for handling data from database
        public static string UserSection = "";
        public static string FirstName = "";
        public static string LastName = "";
        public static string EmailAddress = "";
        public static string SectionName = "";
        public static string AccessType = "";

        private void LoginUser()
        {
            //-> textbox validation
            if (ADID.Text == "" && Password.Text == "")
            {
                MessageBox.Show("Please type your ADID and Password", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ADID.Text == "")
            {
                MessageBox.Show("Please type your ADID", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Password.Text == "")
            {
                MessageBox.Show("Please type your Password", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                SelectUserAccount.Parameters.AddWithValue("@ADID", ADID.Text);
                SelectUserAccount.Parameters.AddWithValue("@Password", Password.Text);
                SqlDataAdapter da = new SqlDataAdapter(SelectUserAccount);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    // -> SQL query to select User Account
                    //SqlCommand SelectUserAccount2 = new SqlCommand("SP_SelectUserAccount", con);
                    //SelectUserAccount2.CommandType = CommandType.StoredProcedure;
                    //SelectUserAccount2.Parameters.AddWithValue("@ADID", ADID.Text);
                    //SelectUserAccount2.Parameters.AddWithValue("@Password", ADID.Text);

                    SqlDataReader reader = SelectUserAccount.ExecuteReader();
                    if (reader.Read())
                    {

                        UserSection = reader["Section"].ToString();
                        FirstName = reader["First Name"].ToString();
                        LastName = reader["Last Name"].ToString();
                        EmailAddress = reader["Email"].ToString();
                        SectionName = "BIPH-" + reader["Section"].ToString();
                        AccessType = reader["Account Type"].ToString();

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
                else
                {
                    // -> this Message show when the user account is not existing in database
                    MessageBox.Show("Incorrect ADID or password", "Access Denied", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }  //***** End of login fuction *****

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

       
        // ---> Login user when click the sign in button
        private void SignInButton_Click(object sender, EventArgs e)
        {
            LoginUser(); // call out the login user function
        }

        // ---> Show change password form when click the forgot password button
        private void ForgotPassword_Click(object sender, EventArgs e)
        {
            Hide();
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();
        }


        // ---> Focus to password text box when hit enter key
        private void ADID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Password.Focus();
            }
        }

        // ---> Login user when hit the enter key
        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                LoginUser(); // call out the login user function
            }
        }
    }
}

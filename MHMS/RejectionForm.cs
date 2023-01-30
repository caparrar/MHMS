using ExcelDataReader;
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
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class RejectionForm : Form
    {
        // Connection string
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        ////Table collection
        //DataTableCollection tableCollection;

        public RejectionForm()
        {
            InitializeComponent();
        }

        //=======================================================================================================================>>>>>>>>>>>>

        private void RejectionForm_Load(object sender, EventArgs e)
        {
            LineStopDetailTextBox.Text = ApprovalForm.LineStopDetail;
            LineStopDetailTextBox.ReadOnly = true;

            LoadResponsibleSection();

            SectionDropdown.Text = "Section (Optional)";
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
            LoadSection.Parameters.AddWithValue("@Procedure", "SelectAllSections");
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end

        //=======================================================================================================================>>>>>>>>>>>>>>>>>>

        private void SectionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReasonTextBox.Focus();
        }

        //=======================================================================================================================>>>>>>>>>>>>

        //Load Section in combobox
        public void LoadResponsibleSection()
        {
            // Check Connection status -> Open the connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select User Account
            SqlCommand LoadSection = new SqlCommand("SP_LoadResponsibleSection", con);
            LoadSection.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(LoadSection);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LoadSection.ExecuteNonQuery();
            con.Close();

            SectionDropdown.DataSource = ds.Tables[0];
            SectionDropdown.DisplayMember = ds.Tables[0].Columns[0].ToString();
            SectionDropdown.ValueMember = "Section";
        }// <---- end

        //=======================================================================================================================>>>>>>>>>>>>

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (ReasonTextBox.Text == "")
            {
                MessageBox.Show("Please type the reason of rejection!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UpdateApprovalStatus();

                //ApprovalForm.ContinueButtonIsClicked = true;

                this.Close();
            }
            
        }

        //=======================================================================================================================>>>>>>>>>>>>
        
        private void UpdateReasonOfRejection()
        {

            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
            //UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
            //UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            ////UpdateReasonOfRejection.Parameters.AddWithValue("@ResponsibleSection", SectionDropdown.Text);

            if (SectionDropdown.Text == "Section (Optional)" || SectionDropdown.Text == "")
            {
                SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
                UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
                UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
                UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                UpdateReasonOfRejection.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateReasonOfRejection.Parameters.AddWithValue("@ReasonOfRejection", ReasonTextBox.Text);
                UpdateReasonOfRejection.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateReasonOfRejection", con);
                UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
                UpdateReasonOfRejection.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
                UpdateReasonOfRejection.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                UpdateReasonOfRejection.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateReasonOfRejection.Parameters.AddWithValue("@ReasonOfRejection", "Transfer to " + SectionDropdown.Text + " - " + ReasonTextBox.Text);
                UpdateReasonOfRejection.ExecuteNonQuery();
                con.Close();

                //Send notification email to responsible section 
                SendEmail();
            }
            

            MessageBox.Show("Item with line stop detail of " + ApprovalForm.LineStopDetail + " was rejected successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        //===================================================================================================================>>>>>>>>>>>>

        private void UpdateResponsibleSection()
        {
            //// -> SQL query to insert user account
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}

            //SqlCommand UpdateReasonOfRejection = new SqlCommand("SP_UpdateResponsibleSection", con);
            //UpdateReasonOfRejection.CommandType = CommandType.StoredProcedure;
            //UpdateReasonOfRejection.Parameters.AddWithValue("@ReferenceNo", LineStopDetailTextBox.Text);
            //UpdateReasonOfRejection.Parameters.AddWithValue("@ResponsibleSection", SectionDropdown.Text);
            //UpdateReasonOfRejection.ExecuteNonQuery();
            //con.Close();

            //this.Close();
        }

        //==================================================================================================================>>>>>>>>>>>>

        private void UpdateApprovalStatus()
        {

            //For Revision 09/30/2022 ****************
            //if (LoginForm.COPQPIC == "✔️")
            //{

            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "Rejected");
            UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
            UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            UpdateApprovalStatus.ExecuteNonQuery();
            con.Close();

            UpdateReasonOfRejection();
               
            //}

            //if (LoginForm.ProcessInCharge == "✔️")
            //{
            //    // -> SQL query to insert user account
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "Rejected");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //    UpdateApprovalStatus.ExecuteNonQuery();
            //    con.Close();

            //    UpdateReasonOfRejection();

            //}

            //if (LoginForm.SectionSPV == "✔️")
            //{
            //    // -> SQL query to insert user account
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "Rejected");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //    UpdateApprovalStatus.ExecuteNonQuery();
            //    con.Close();

            //    UpdateReasonOfRejection();

            //}

            //if (LoginForm.SectionMGR == "✔️")
            //{
            //    // -> SQL query to insert user account
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
            //    UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "Rejected");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetailTextBox.Text);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Reason", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
            //    UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
            //    UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "Rejected by " + LoginForm.FirstName + " " + LoginForm.LastName + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
            //    UpdateApprovalStatus.ExecuteNonQuery();
            //    con.Close();

            //    UpdateReasonOfRejection();

            //}


            //MessageBox.Show("Item with reference no. of " + ApprovalForm.SelectedRowReferenceNo + " was rejected successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AttachedFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog AttachFile = new OpenFileDialog();
                AttachFile.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html; *.jpg)|*.pdf; *.docx; *.xlsx; *.html; *.jpg";

                if (AttachFile.ShowDialog() == DialogResult.OK)
                {
                    FileName.Text = AttachFile.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void SendEmail()
        {
            //Email header.
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.Append("<h2>[TEST ONLY] Manhour Management System (MHMS)</h2>");
            builder.Append("<br>" + DateTime.Now);
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Good day!");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("This is to inform you that " + LoginForm.UserSection + " section have transfered MH data in your section.");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("Please see the details below.");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("<br><b><font color=red>This is automatic generated email, Do not reply!</b><br></font>");
            builder.Append("<br>");
            builder.Append("<br>");
            builder.Append("<br>Thank you!").AppendLine();
            innerString = builder.ToString();

            EmailNotif(); // ---> Call out the email notification function
        }

        string EmailTo; // ---> Declared string.
        string innerString; // ---> Declared string.
        string Section;
        Attachment attach; //use to attach file in email

        private void EmailNotif()
        {
            //    Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            //bool isValid = regex.IsMatch(Email.Text.Trim());

            if (FileName.Text != "")
            {
                attach = new Attachment(FileName.Text);
            }

            SqlCommand LoadUsersPIC = new SqlCommand("SP_LoadUsersPIC", con);
            LoadUsersPIC.CommandType = CommandType.StoredProcedure;
            LoadUsersPIC.Parameters.AddWithValue("@Procedure", "SelectUserEmailForRejection");
            LoadUsersPIC.Parameters.AddWithValue("@Section", SectionDropdown.Text);
            SqlDataAdapter sda = new SqlDataAdapter(LoadUsersPIC);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                Section = row["Section"].ToString();

                EmailTo = row["Email"].ToString();

                //Email structure.
                MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailTo);
                mail.Bcc.Add(new MailAddress("charlotte.robles@brother-biph.com.ph"));
                mail.Bcc.Add(new MailAddress("donnalie.balba@brother-biph.com.ph"));
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "10.113.10.1";

                if (attach != null)
                {
                    mail.Attachments.Add(attach);
                }

                mail.Subject = "[MHMS] - Notification";

                mail.Body = innerString;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }

            MessageBox.Show("Email sent!");
        }

        private void SectionDropdown_DropDown(object sender, EventArgs e)
        {
            LoadSection();
        }

        private void SectionDropdown_DropDownClosed(object sender, EventArgs e)
        {
            SectionDropdown.Text = "Section (Optional)";
        }

        //==================================================================================================================>>>>>>>>>>>>
    }
}

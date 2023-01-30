using MHMS.Forms;
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
    public partial class COPQConfirmationForm : Form
    {

        // Connection string
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public COPQConfirmationForm()
        {
            InitializeComponent();
        }

        private void COPQConfirmationForm_Load(object sender, EventArgs e)
        {
            LineStopDetail.Text = ApprovalForm.LineStopDetail;
            LineStopDetail.ReadOnly = true;
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (MHLossTypeDropdown.Text == "")
            {
                MessageBox.Show("Please select MH loss type!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ReasonTextBox.Text == "")
            {
                MessageBox.Show("Please type the reason!", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MHLossTypeDropdown.Text == "No COPQ Needed (Delay)")
                {
                    UpdateCOPQAmountIfNoCOPQNeeded();
                    UpdateApprovalStatus();
                }
                else
                {
                    UpdateApprovalStatus();
                }

                this.Close();
            }
        }

        private void UpdateCOPQAmountIfNoCOPQNeeded()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open(); 
            }

            SqlCommand UpdateCOPQAmount = new SqlCommand("SP_UpdateCOPQAmountIfNoCOPQNeeded", con);
            UpdateCOPQAmount.CommandType = CommandType.StoredProcedure;
            //UpdateCOPQAmount.Parameters.AddWithValue("@MHLossType", MHLossTypeDropdown.Text);
            UpdateCOPQAmount.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
            UpdateCOPQAmount.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            UpdateCOPQAmount.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateCOPQAmount.ExecuteNonQuery();
            con.Close();
        }

        private void UpdateApprovalStatus()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (LoginForm.COPQPIC == "✔️")
            {
               
                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQPIC");
                UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", ReasonTextBox.Text);
                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", MHLossTypeDropdown.Text);
                UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
                UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateApprovalStatus.ExecuteNonQuery();
                con.Close();

                //UpdateMHLossType();
                //UpdateReason();

                MHLossTypeDropdown.Text = "";
                ReasonTextBox.Text = "";

                MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //private void UpdateMHLossType()
        //{
        //    // -> SQL query to insert user account
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlCommand UpdateMHLossType = new SqlCommand("SP_UpdateMHLossType", con);
        //    UpdateMHLossType.CommandType = CommandType.StoredProcedure;
        //    UpdateMHLossType.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
        //    UpdateMHLossType.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
        //    UpdateMHLossType.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
        //    UpdateMHLossType.Parameters.AddWithValue("@MHLossType", MHLossTypeDropdown.Text);
        //    UpdateMHLossType.ExecuteNonQuery();
        //    con.Close();
        //}

        //private void UpdateReason()
        //{
        //    // -> SQL query to insert user account
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlCommand UpdateReason = new SqlCommand("SP_UpdateReason", con);
        //    UpdateReason.CommandType = CommandType.StoredProcedure;
        //    UpdateReason.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
        //    UpdateReason.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
        //    UpdateReason.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
        //    UpdateReason.Parameters.AddWithValue("@Reason", ReasonTextBox.Text);
        //    UpdateReason.ExecuteNonQuery();
        //    con.Close();

        //}

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

       
    }
}

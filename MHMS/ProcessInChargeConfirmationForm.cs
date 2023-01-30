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
    public partial class ProcessInChargeConfirmationForm : Form
    {
        // Connection string
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;
        //static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS2"].ConnectionString;

        // SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public ProcessInChargeConfirmationForm()
        {
            InitializeComponent();
        }

        private void ProcessInChargeConfirmationForm_Load(object sender, EventArgs e)
        {
            LineStopDetail.Text = ApprovalForm.LineStopDetail;
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (CauseTextBox.Text == "")
            {
                MessageBox.Show("Please type the cause.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CountermeasureTextBox.Text == "")
            {
                MessageBox.Show("Please type the countermeasure.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    UpdateApprovalStatus();
                }
                    catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void UpdateApprovalStatus()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            if (LoginForm.ProcessInCharge == "✔️")
            {
                SqlCommand UpdateApprovalStatus = new SqlCommand("SP_UpdateApprovalStatus", con);
                UpdateApprovalStatus.CommandType = CommandType.StoredProcedure;
                UpdateApprovalStatus.Parameters.AddWithValue("@Procedure", "ApprovedBySectionCOPQProcessInCharge");
                UpdateApprovalStatus.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
                UpdateApprovalStatus.Parameters.AddWithValue("@Reason", ""); 
                UpdateApprovalStatus.Parameters.AddWithValue("@MHLossType", "");
                UpdateApprovalStatus.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
                UpdateApprovalStatus.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
                UpdateApprovalStatus.Parameters.AddWithValue("@Type", ApprovalForm.ApprovalType);
                UpdateApprovalStatus.Parameters.AddWithValue("@NextApprover", "For Approval by SPV");
                UpdateApprovalStatus.ExecuteNonQuery();
                con.Close();

                UpdateCauseAndCountemeasure();

                MessageBox.Show("Approved Successfully!", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //ApprovalForm.ApproveButtonIsClicked = true;
            this.Close();
        }

        private void UpdateCauseAndCountemeasure()
        {
            // -> SQL query to insert user account
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand UpdateCauseAndCountemeasure = new SqlCommand("SP_UpdateCauseAndCountemeasure", con);
            UpdateCauseAndCountemeasure.CommandType = CommandType.StoredProcedure;
            UpdateCauseAndCountemeasure.Parameters.AddWithValue("@LineStopDetail", LineStopDetail.Text);
            UpdateCauseAndCountemeasure.Parameters.AddWithValue("@PartCode", ApprovalForm.PartCode);
            UpdateCauseAndCountemeasure.Parameters.AddWithValue("@DateEncountered", ApprovalForm.DateEncountered);
            UpdateCauseAndCountemeasure.Parameters.AddWithValue("@Cause", CauseTextBox.Text);
            UpdateCauseAndCountemeasure.Parameters.AddWithValue("@Countermeasure", CountermeasureTextBox.Text);
            UpdateCauseAndCountemeasure.ExecuteNonQuery();
            con.Close();
        }

    }
}

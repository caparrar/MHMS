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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class NotificationForm : Form
    {
        //Connection String
        static string MHMS_Conn = ConfigurationManager.ConnectionStrings["MHMS.Properties.Settings.MHMS"].ConnectionString;

        //SQL Connection
        SqlConnection con = new SqlConnection(MHMS_Conn);

        public NotificationForm()
        {
            InitializeComponent();
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            
        }

      

        //================================================================================================================>>>>>>>>>>>>>>

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //================================================================================================================>>>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void NotificationTopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // <---------------------------

        //================================================================================================================>>>>>>>>>>>>>>

        private void SelectForApprovalRequest()
        {
            // Check Connection status -> Open connection if the connection is closed
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // -> SQL query to select Section Approver setting
            SqlCommand LoadNotification = new SqlCommand("SP_SelectAllForApprovalRequest", con);
            LoadNotification.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(LoadNotification);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            NotificationDataGridView.DataSource = dt;
            con.Close();

        }

       
        
    }
}

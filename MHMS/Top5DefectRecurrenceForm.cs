using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class Top5DefectRecurrenceForm : Form
    {
        public Top5DefectRecurrenceForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void Top5DefectRecurrence()
        //{
        //    SqlConnection connns = new SqlConnection(ConfigurationManager.ConnectionStrings["DBPATH"].ConnectionString);
        //    connns.Open();

        //    string sql = "SELECT COUNT(BI_Satellite_Office) FROM Employee_Summary where BI_GENDER = '" + filters + "' and  BI_Satellite_Office = 'Satellite 4' and BI_Gender = 'Male' and IE_Result = 'PASSED'  and (BI_Date between CONVERT(datetime,'" + dateTimePicker25.Value + "') AND CONVERT(datetime,'" + dateTimePicker26.Value + "'))";
        //    SqlCommand cmd = new SqlCommand(sql, connns);
        //    object count = cmd.ExecuteScalar();
        //    if (count != null) satellite4 = count.ToString();
        //    connns.Close();
        //}
    }
}

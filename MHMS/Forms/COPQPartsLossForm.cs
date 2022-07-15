using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class COPQPartsLossForm : Form
    {
        public COPQPartsLossForm()
        {
            InitializeComponent();
        }

        private void COPQPartsLossForm_Load(object sender, EventArgs e)
        {
            DateFrom(); // Call out the function for Date From and show when the form is loaded
        }

        // ---> Get the day 1 of the current month
        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }
        
        private void ViewGraph_Click(object sender, EventArgs e)
        {
            Process.Start("https://charts.livegap.com/app.php?lan=en&gallery=bar");
        }

        private void UpdateDataButton_Click(object sender, EventArgs e)
        {
            UpdateDataForm UpdateData = new UpdateDataForm();
            UpdateData.ShowDialog();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS.Forms
{
    public partial class COPQManhourLossForm : Form
    {
        public COPQManhourLossForm()
        {
            InitializeComponent();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddCOPQButton_Click(object sender, EventArgs e)
        {
            AddCOPQForm addCOPQ = new AddCOPQForm();
            addCOPQ.ShowDialog();
        }

        private void DateFrom()
        {
            DateTime now = DateTime.Now;
            FromDateTimePicker.Value = new DateTime(now.Year, now.Month, 1);
        }

        private void COPQManhourLossForm_Load(object sender, EventArgs e)
        {
            DateFrom(); // Call out the function for Date From and show when the form is loaded
        }
    }
}

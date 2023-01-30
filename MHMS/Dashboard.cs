using MHMS.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHMS
{
    public partial class Dashboard : Form
    {
        //Fields
        private Button currentBtn;
        /*private Panel leftBorderBtn;*/ // --> this is use to border color of side bar button
        private Form currentChildForm; // --> initialized variable

        private bool ReportisCollapsed; // --> this is use as variable for report dropdown button
        private bool SettingIsCollapsed; // --> this is use as variable for setting dropdown button
        private bool ApplicationIsCollapsed; // --> this is use as variable for setting dropdown button

        public Dashboard()
        {
            InitializeComponent();

            //leftBorderBtn = new Panel();
            //leftBorderBtn.Size = new Size(7, 43);
            //SideBarPanel.Controls.Add(leftBorderBtn);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; //use to maximize form base on screen size od computer
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(198, 46, 74);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(150, 46, 198);
            public static Color color5 = Color.FromArgb(171, 198, 46);
            public static Color color6 = Color.FromArgb(46, 198, 196);
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Methods
        private void ActivateButton(Object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(46, 198, 196);
                currentBtn.ForeColor = Color.White;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Disable button higlighted
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(4, 41, 56);
                currentBtn.ForeColor = Color.FromArgb(213, 241, 252);
                //currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                //currentBtn.IconColor = Color.FromArgb(213, 241, 252);
                //currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                //currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //Open only form
                currentChildForm.Close();
            }

            currentChildForm = childForm;

            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(childForm);
            MainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            TitleChildForm.Text = childForm.Text;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ReportButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            ReportDropdownTimer.Start();
            Icon.Image = ReportButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ReportDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (ReportisCollapsed)
            {
                ReportButtonPanel.Height += 10;
                if (ReportButtonPanel.Size == ReportButtonPanel.MaximumSize)
                {
                    ReportDropdownTimer.Stop();
                    ReportisCollapsed = false;
                }
            }
            else
            {
                ReportButtonPanel.Height -= 10;
                if (ReportButtonPanel.Size == ReportButtonPanel.MinimumSize)
                {
                    ReportDropdownTimer.Stop();
                    ReportisCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            SettingDropdownTimer.Start();
            Icon.Image = SettingsButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void SettingDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (SettingIsCollapsed)
            {
                SettingButtonPanel.Height += 10;
                if (SettingButtonPanel.Size == SettingButtonPanel.MaximumSize)
                {
                    SettingDropdownTimer.Stop();
                    SettingIsCollapsed = false;
                }
            }
            else
            {
                SettingButtonPanel.Height -= 10;
                if (SettingButtonPanel.Size == SettingButtonPanel.MinimumSize)
                {
                    SettingDropdownTimer.Stop();
                    SettingIsCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new Forms.DashboardForm());
            OpenChildForm(new Forms.DashboardForm2());
            Icon.Image = DashboardButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApplicationFormButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new Forms.ApplicationForm());
            Icon.Image = ApplicationFormButton.Image;
            ApplicationDropdownTimer.Start();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApplicationDropdownTimer_Tick(object sender, EventArgs e)
        {
            if (ApplicationIsCollapsed)
            {
                ApplicationFormPanel.Height += 10;
                if (ApplicationFormPanel.Size == ApplicationFormPanel.MaximumSize)
                {
                    ApplicationDropdownTimer.Stop();
                    ApplicationIsCollapsed = false;
                }
            }
            else
            {
                ApplicationFormPanel.Height -= 10;
                if (ApplicationFormPanel.Size == ApplicationFormPanel.MinimumSize)
                {
                    ApplicationDropdownTimer.Stop();
                    ApplicationIsCollapsed = true;
                }
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ManpowerForecastButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ManpowerForecastForm());
            Icon.Image = ManpowerForecastButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DPRButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.DPRForm());
            Icon.Image = DPRButton.Image;
            ApprovalButton.BackColor = Color.Transparent;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void COPQPartLossButton_Click(object sender, EventArgs e)
        {
            string UserSection = SectionLabel.Text.Replace("BIPH-", "");

            ActivateButton(sender, RGBColors.color6);
            Icon.Image = COPQPartLossButton.Image;

            if (UserSection == "BPS" || UserSection == "Tape Cassette" || UserSection == "Ink Cartridge" || UserSection == "Ink Head" || UserSection == "Production Engineering" || UserSection == "Printer" || UserSection == "P-Touch" || UserSection == "Molding Production" || UserSection == "PCBA" || UserSection == "QI")
            {
                OpenChildForm(new Forms.COPQPartsLossForm());
                
            }
            else
            {
                MessageBox.Show("Sorry, you are not allowed to access this module", "Reminders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            

        }

        //====================================================================================================================>>>>>>>>>>>>

        private void COPQManhourLossButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.COPQManhourLossForm());
            Icon.Image = COPQManhourLossButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void EfficiencyButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.EfficiencyForm());
            Icon.Image = EfficiencyButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApproverSettingButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApproverSettingForm());
            Icon.Image = ApproverSettingButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TargetSettingButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.TargetSettingForm());
            Icon.Image = TargetSettingButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure do you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void MinimizedButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //====================================================================================================================>>>>>>>>>>>>

        //Drag Form ------------------>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void TopBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void HeaderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }// <---------------------------

        //====================================================================================================================>>>>>>>>>>>>

        private void Logo_Click(object sender, EventArgs e)
        {
            if (TitleChildForm.Text != "Home")
            {
                currentChildForm.Close();
                Reset();
            }
            else
            {
                //Stay in current form
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void Reset()
        {
            DisableButton();
            //leftBorderBtn.Visible = false;
            TitleChildForm.Text = "Home";
            //Icon.Image = MHMS.Properties.Resources.house_32;
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string SectionText;

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //Change date format
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            key.SetValue("sShortDate", "MM/dd/yyyy");

            //String Region = System.Globalization.RegionInfo.CurrentRegion.DisplayName;
            //String Language = System.Globalization.CultureInfo.CurrentUICulture.EnglishName;
            //MessageBox.Show(Region, Language);
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-PH");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-PH");

            //RegistryKey keys = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            //string[] lang = { "en-PH" };
            //key.SetValue("PreferredUILanguagesPending", lang, RegistryValueKind.MultiString);



            ////Change regional format
            //RegistryKey keys = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            //string[] lang = { "en-PH" };
            //keys.SetValue("LocaleName", lang, RegistryValueKind.MultiString);


            if (LoginForm.isSingleSectionAccess == true)
            {
                SectionLabel.Text = LoginForm.SectionName;
                UserSection.Text = LoginForm.UserSection;
                SectionText = SectionLabel.Text;
                SectionMenuForm.isMultiSectionAccess = false;
            }

            if (SectionMenuForm.isMultiSectionAccess == true)
            {
                SectionLabel.Text = SectionMenuForm.SectionName;
                UserSection.Text = SectionMenuForm.UserSection;
                SectionText = SectionLabel.Text;
                LoginForm.isSingleSectionAccess = false;
            }

            UserName.Text = "Welcome " + LoginForm.FirstName + "!";
            UserLoginName.Text = LoginForm.FirstName + " " + LoginForm.LastName;
            AccountTypeLabel.Text = LoginForm.AccountType;

           

            if (AccountTypeLabel.Text == "ADMIN")
            {
                SettingsButton.Enabled = true;
                SettingsButton.BackColor = Color.FromArgb(21, 35, 53);
            }
            else if (AccountTypeLabel.Text == "ADMIN")
            {
                SettingsButton.Enabled = true;
                SettingsButton.BackColor = Color.FromArgb(21, 35, 53);
            }
            else
            {
                SettingButtonPanel.Visible = false;
            }

            // ---> Get Fullname of user
            InitialNameButton.Text = (LoginForm.FirstName.Substring(0, 1) + LoginForm.LastName.Substring(0, 1)).ToUpper();
            UserPicture.Text = (LoginForm.FirstName.Substring(0, 1) + LoginForm.LastName.Substring(0, 1)).ToUpper();

            //set version info
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.SystemVersion.Text = String.Format(this.SystemVersion.Text, version.Major, version.Minor, version.Build, version.Revision);
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void DateAndTime_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.DateTimeLabel.Text = dateTime.ToString("dddd , MMM dd, yyyy hh : mm : ss tt");
        }

        //====================================================================================================================>>>>>>>>>>>>

        public static string sections = "";

        private void SectionLabel_TextChanged(object sender, EventArgs e)
        {
            sections = SectionLabel.Text;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void InitialNameButton_Click(object sender, EventArgs e)
        {
            if (ChangePasswordPanel.Visible == false)
            {
                ChangePasswordPanel.Visible = true;
            }
            else
            {
                ChangePasswordPanel.Visible = false;
            }
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ChangePasswordPanel.Visible = false;
            var changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApplicationButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApplicationForm());
            Icon.Image = ApplicationButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void ApprovalButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.ApprovalForm());
            Icon.Image = ApprovalButton.Image;
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void NotificationBellButton_Click(object sender, EventArgs e)
        {
            NotificationForm Notif = new NotificationForm();
            Notif.ShowDialog();
        }

        //====================================================================================================================>>>>>>>>>>>>

        private void TruobleChecksheetButton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\System Trouble Checksheet.xlsx");
        }

        //====================================================================================================================>>>>>>>>>>>>
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure do you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }

        public static bool COPQAcceptanceIsClicked = false;
        public static bool STIsClicked = false;
        public static bool WCCCIsClicked = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (COPQAcceptanceIsClicked == true)
            {
                ApplicationDropdownTimer.Start();
                ApprovalButton.BackColor = Color.FromArgb(46, 198, 196);
                DashboardButton.BackColor = Color.Transparent;
                //OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApplicationFormButton.Image;
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApprovalForm());
                Icon.Image = ApprovalButton.Image;

                COPQAcceptanceIsClicked = false;
            }

            if (STIsClicked == true)
            {
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApprovalButton.Image;

                STIsClicked = false;
            }


            if (WCCCIsClicked == true)
            {
                //ActivateButton(sender, RGBColors.color6);
                OpenChildForm(new Forms.ApplicationForm());
                Icon.Image = ApprovalButton.Image;

                WCCCIsClicked = false;
            }
        }

        private void SwitchAccountButton_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void UpdateSystemButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to update and restart the system?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
                Process.Start(@"\\apbiphsh04\B1_BIPHCommon\19_BPS\02_Application\FY2022\MHMS\\setup.exe");
            }
           
        }

        //====================================================================================================================>>>>>>>>>>>>
    }
}

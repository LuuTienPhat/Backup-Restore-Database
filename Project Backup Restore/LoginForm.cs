using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup_Restore
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
            lbLoginName.Visible = lbServerName.Visible = lbPassword.Visible = false;
            Centering();
        }

        public void Centering()
        {
            lbTitle.Left = (this.ClientSize.Width - lbTitle.Width) / 2;
            panelControl1.Left = (this.ClientSize.Width - panelControl1.Width) / 2;
            panelControl2.Left = (this.ClientSize.Width - panelControl2.Width) / 2;
            panelControl3.Left = (this.ClientSize.Width - panelControl3.Width) / 2;
            panelControl4.Left = (this.ClientSize.Width - panelControl4.Width) / 2;
        }

        //btnConnect
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isInputValid() == true)
            {
                Program.SERVER_NAME = cbxServerName.Text.Trim();
                Program.LOGIN = txtLoginName.Text.Trim();
                Program.PASSWORD = txtPassword.Text.Trim();

                bool result = WaitForm.ShowWaitForm(this, Program.ConnectToSQL, "Connecting...", "Please wait!");

                if (result == true)
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.ERROR, "Error while connecting SQL Server\nPlease check the Servername, Username and Password again.");
                }
            }
        }

        //btnCanel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }

        //Check if input is valid
        private Boolean isInputValid()
        {
            int count = 0;

            if (cbxServerName.Text.Trim().Length == 0)
            {
                count++;
                lbServerName.Visible = true;
                lbServerName.Text = "This field cannot be empty!";
            }
            else
            {
                lbServerName.Visible = false;
            }

            if (txtLoginName.Text.Trim().Length == 0)
            {
                count++;
                lbLoginName.Visible = true;
                lbLoginName.Text = "This field cannot be empty!";
            }
            else
            {

                lbLoginName.Visible = false;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                count++;
                lbPassword.Visible = true;
                lbPassword.Text = "This field cannot be empty!";
            }
            else
            {

                lbPassword.Visible = false;
            }

            return count == 0;
        }

        private void GetDataSources()
        {
            string ServerName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    cbxServerName.Properties.Items.Clear();
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        Console.WriteLine(ServerName + "\\" + instanceName);
                        if (instanceName.ToString().Equals("MSSQLSERVER"))
                            cbxServerName.Properties.Items.Add(ServerName);
                        else
                            cbxServerName.Properties.Items.Add(ServerName + "\\" + instanceName);
                    }
                }
            }
        }

        private void cbxServerName_QueryPopUp(object sender, CancelEventArgs e)
        {
            //getServerList();
            WaitForm.ShowWaitForm(this, GetDataSources, "Please Wait", "Loading Servers...");

        }
    }
}
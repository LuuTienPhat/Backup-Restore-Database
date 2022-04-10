using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }

        private Boolean isInputValid()
        {
            int count = 0;

            if (txtServerName.Text.Trim().Length == 0)
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isInputValid() == true)
            {
                Program.SERVER_NAME = txtServerName.Text.Trim();
                Program.LOGIN = txtLoginName.Text.Trim();
                Program.PASSWORD = txtPassword.Text.Trim();

                if(Program.ConnectToSQL() == 1)
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
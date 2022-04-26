using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Backup_Restore
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        public MainForm()
        {
            InitializeComponent();
            txtDatabaseName.DataBindings.Add(new Binding("EditValue", databasesBindingSource, "name", true));
            txtPosition.DataBindings.Add(new Binding("EditValue", sP_STT_BACKUPBindingSource, "position", true));
            datePicker.DateTime = DateTime.Now;
            timePicker.Time = DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.backup_devices' table. You can move, or remove it, as needed.
            this.backup_devicesTableAdapter.Fill(this.dS.backup_devices);
            // TODO: This line of code loads data into the 'dS.databases' table. You can move, or remove it, as needed.
            this.databasesTableAdapter.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.databasesTableAdapter.Fill(this.dS.databases);

            if (Program.ConnectToSQL())
            {
                //XtraMessageBox.Show("Connect Failed", this.Name, MessageBoxButtons.OK);
            }
            else
            {
                //XtraMessageBox.Show("Connect Succesfully", "", MessageBoxButtons.OK);
                //String query = "SELECT name, database_id FROM sys.databases WHERE (database_id >= 5) AND (NOT(name LIKE N'ReportServer%')) ORDER BY name";
                //DataTable dt = Program.ExecSqlDataTable(query);
                //databases = dt;
                //dt.Columns.RemoveAt(1);
                //databaseList.DataSource = dt;
            }
            //string o = ((DataRowView)sP_STT_BACKUPBindingSource[sP_STT_BACKUPBindingSource.Position])["backup_start_date"].ToString();
        }

        //btnDisconnect
        private void btnDisconnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            
            new LoginForm().ShowDialog();
            this.Close();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void databasesGridControl_Click(object sender, EventArgs e)
        {

        }


        private void btnBackup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string query_string;
            string database_name = txtDatabaseName.Text.Trim();
            string device_name = String.Format(Program.PREFIX_DEVICE_NAME, database_name);


            if (database_name == "" || device_name == "") return;

            query_string = String.Format(Program.BACKUP_DATABASE, database_name, device_name);

            string backup_descriptiton = XtraInputBox.Show("Enter backup description", "Backupset Name", "").Trim();
            if (backup_descriptiton.Length > 0)
            {
                query_string += " WITH NAME = N'" + backup_descriptiton + "'";
            }

            if (chbxDeleteAllBackupBefore.Checked == true)
            {
                if (XtraMessageBox.Show("All the old backups will be permanently deleted. Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    query_string += ", INIT";
                }
            }

            int execute_result = Program.ExecSqlNonQuery(query_string, Program.CONNECTION_STRING, "");

            if (execute_result != 0)
            {
                XtraMessageBox.Show("Error while backing up " + database_name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                XtraMessageBox.Show("Backed up successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBackups(database_name);
                btnRestore.Enabled = true;
            }
        }

        private bool IsPointInTimeValid(string point_in_time_string, string selected_backup_time_string)
        {
            DateTime selected_backup_time = DateTime.Parse(selected_backup_time_string);
            DateTime point_in_time = DateTime.Parse(point_in_time_string);

            if (point_in_time > DateTime.Now)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The backup point must be after the selected backup date and before the moment");
                return false;
            }
            else if (point_in_time.Date == DateTime.Now.Date)
            {
                if (point_in_time.TimeOfDay.Add(new TimeSpan(0, 1, 0)) >= DateTime.Now.TimeOfDay)
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The restore time must be before the moment time at least 1 minute");
                    return false;
                }
            }


            if (point_in_time < selected_backup_time)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The backup point must be after the selected backup date and before the moment");
                return false;
            }
            else
            {
                if (point_in_time.TimeOfDay.Subtract(new TimeSpan(0, 1, 0)) <= selected_backup_time.TimeOfDay)
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The restore time must be after the last backup time at least 1 minute");
                    return false;
                }
            }

            //else
            //{
            //    if (point_in_time.TimeOfDay.Add(new TimeSpan(0, 1, 0)) >= DateTime.Now.TimeOfDay)
            //    {
            //        CustomMessageBox.Show("warning", "The restore time must be before the moment time at least 1 minute");
            //        return false;
            //    }
            //    else if (point_in_time.TimeOfDay.Subtract(new TimeSpan(0, 1, 0)) <= selected_backup_time.TimeOfDay)
            //    {
            //        CustomMessageBox.Show("warning", "The restore time must be after the last backup time at least 1 minute");
            //        return false;
            //    }
            //}

            return true;
        }

        private async Task<int> RestoreToPointInTime(string databaseName, int position, string pointInTime, string selectedBackupTime)
        {
            string device_name = string.Format(Program.PREFIX_DEVICE_NAME, databaseName);
            string backup_log_path = Program.BACKUP_PATH + "\\" + "LOG_" + databaseName + ".trn";
            string device_path = Program.BACKUP_PATH + "\\" + "DEVICE_" + databaseName + ".bak";
            string excute_statement;

            int excute_result = 0;

            excute_statement = "ALTER DATABASE [" + databaseName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
                               "BACKUP LOG [" + databaseName + "] TO DISK = '" + backup_log_path + "' WITH INIT\n" +
                               "USE MASTER\n" +
                               "RESTORE DATABASE [" + databaseName + "] FROM [" + device_name + "] WITH FILE = " + position + ", REPLACE, NORECOVERY\n" +
                               "RESTORE DATABASE [" + databaseName + "] FROM DISK = '" + backup_log_path + "' WITH STOPAT = '" + pointInTime + "'";

            excute_result = await Task.Run(() =>
            {
                return Program.ExecSqlNonQuery(excute_statement, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            });

            //if (excute_result == 0)
            //{
            //    excute_statement = "";

            //    excute_result = await Task.Run(() =>
            //    {
            //        return Program.ExecSqlNonQuery(excute_statement, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            //    });

            //    SetDatabaseToMultiUser(databaseName);
            //}

            SetDatabaseToMultiUser(databaseName);

            return excute_result;
        }


        private async void SetDatabaseToMultiUser(string database_name)
        {
            string excute_statement = "ALTER DATABASE [" + database_name + "]  SET MULTI_USER";

            await Task.Run(() =>
            {
                Program.ExecSqlNonQuery(excute_statement, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            });
        }

        private async void btnRestore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string database_name = txtDatabaseName.Text.Trim();
            string device_name = string.Format(Program.PREFIX_DEVICE_NAME, database_name);
            string queryString;
            int position = int.Parse(((DataRowView)sP_STT_BACKUPBindingSource[sP_STT_BACKUPBindingSource.Position])["position"].ToString());

            if (this.sP_STT_BACKUPBindingSource.Count == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "There is no backup"); return;
            }

            if (position == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "Please choose one backup to restore"); return;
            }

            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
            {
                Program.conn.Close(); //Đóng kết nối của chính ta
            }


            if (database_name == "" || device_name == "") return;

            if (chbxToPointInTime.Checked == false)
            {
                queryString = string.Format(Program.RESTORE_DATABASE, database_name, device_name, position.ToString());

                int exucute_result = Program.ExecSqlNonQuery(queryString, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
                if (exucute_result == 0)
                {
                    //
                    //
                    XtraMessageBox.Show($"Restore database [{database_name}] successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    XtraMessageBox.Show($"Restore database [{database_name}] failed", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //
                }
            }
            else
            {
                string date_picked = datePicker.DateTime.Date.ToString("yyyy-MM-dd");
                string time_picked = timePicker.Time.ToString("HH:mm:ss");
                string point_in_time_string = date_picked + " " + time_picked;
                string selected_backup_time_string = ((DataRowView)sP_STT_BACKUPBindingSource[sP_STT_BACKUPBindingSource.Position])["backup_start_date"].ToString();
                DateTime selected_backup_time = DateTime.Parse(selected_backup_time_string);
                selected_backup_time_string = selected_backup_time.ToString("yyyy-MM-dd HH:mm:ss");

                if (IsPointInTimeValid(point_in_time_string, selected_backup_time_string))
                {
                    string backup_log_path = Program.BACKUP_PATH + "\\" + "LOG_" + database_name + ".trn";

                    queryString = string.Format(Program.RESTORE_DATABASE_TO_POINT_IN_TIME, database_name, backup_log_path, position, point_in_time_string);

                    int exucute_result = await RestoreToPointInTime(database_name, position, point_in_time_string, selected_backup_time_string);
                    //Program.ExecSqlNonQuery(queryString, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
                    if (exucute_result == 0)
                    {

                        XtraMessageBox.Show($"Restore database [{database_name}] successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        XtraMessageBox.Show($"Restore database [{database_name}] failed", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void LoadBackups(string database_name)
        {
            if (database_name.Equals("")) return;
            try
            {
                this.sP_STT_BACKUPTableAdapter.Connection.ConnectionString = Program.CONNECTION_STRING;
                this.sP_STT_BACKUPTableAdapter.Fill(this.dS.SP_STT_BACKUP, database_name);
                if (sP_STT_BACKUPBindingSource.Count == 0)
                {
                    txtPosition.Text = "0";
                }
                else
                {
                    txtPosition.Text = int.Parse(((DataRowView)sP_STT_BACKUPBindingSource[0])["position"].ToString()).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sP_STT_BACKUPGridControl_Click(object sender, EventArgs e)
        {
            if (sP_STT_BACKUPBindingSource.Position == -1 || sP_STT_BACKUPBindingSource.Count == 0 || sP_STT_BACKUPGridControl.DataSource == null)
            {
                txtPosition.Text = "0";
            }
            else
            {
                int position = int.Parse(((DataRowView)sP_STT_BACKUPBindingSource[sP_STT_BACKUPBindingSource.Position])["position"].ToString());
                txtPosition.Text = position.ToString();
            }
        }

        private void unknowMethod()
        {
            this.backup_devicesTableAdapter.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.backup_devicesTableAdapter.Fill(this.dS.backup_devices);

            this.databasesTableAdapter.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.databasesTableAdapter.Fill(this.dS.databases);

            databasesBindingSource.Position = 0;
            //databasesGridControl_Click(sender, e);
            //dateStop.DateTime = DateTime.Now.Date;
            //timeStop.Time = DateTime.Now;
            //panelThoigian.Visible = btnNangCao.Checked = false;
        }

        private void btnNewDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string dev_type = "disk";
            string logical_name = String.Format(Program.PREFIX_DEVICE_NAME, txtDatabaseName.Text.Trim());
            string physical_name = Program.BACKUP_PATH + "\\" + logical_name + ".dat";
            string query_string = String.Format(Program.CREATE_DEVICE, dev_type, logical_name, physical_name);

            int result = Program.ExecSqlNonQuery(query_string, Program.CONNECTION_STRING, "");
            if (result == 0)
            {
                XtraMessageBox.Show("Create Backup Device Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.backup_devicesTableAdapter.Fill(this.dS.backup_devices);
                int database_position = databasesBindingSource.Position;
                databasesBindingSource.MoveFirst();
                databasesBindingSource.Position = database_position;
            }
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void LoadDatabases()
        {
            this.databasesTableAdapter.Fill(this.dS.databases);
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDatabases();
        }

        private void databasesGridControl_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {

        }

        private bool HasDevice(string databaseName)
        {
            string logical_name = string.Format(Program.PREFIX_DEVICE_NAME, databaseName);
            if (backup_devicesBindingSource.Find("name", logical_name) != -1)
            {
                return true;
            }
            return false;
        }

        private bool HasBackups()
        {
            if (this.sP_STT_BACKUPBindingSource.Count == 0 || this.sP_STT_BACKUPBindingSource.DataSource == null || this.sP_STT_BACKUPBindingSource.Position == -1)
                return false;
            return true;
        }

        private void txtDatabaseName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string database_name = (sender as TextEdit).Text;
                if (string.IsNullOrEmpty(database_name))
                {
                    return;
                }

                this.sP_STT_BACKUPTableAdapter.Fill(this.dS.SP_STT_BACKUP, database_name);

                if (HasDevice(database_name))
                {
                    ToggleBarItems(true);
                    btnNewDevice.Enabled = false;
                    if (!HasBackups())
                    {
                        btnRestore.Enabled = chbxToPointInTime.Enabled = false;
                        txtPosition.Text = "0";
                    }
                }
                else
                {
                    ToggleBarItems(false);
                    btnNewDevice.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ToggleBarItems(bool isEnable)
        {
            foreach (var barItem in barManager1.Items)
            {
                (barItem as BarBaseButtonItem).Enabled = isEnable;
            }

            btnNewDevice.Enabled = btnSettings.Enabled = btnDisconnect.Enabled = btnRefresh.Enabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;

namespace Backup_Restore
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        public MainForm()
        {
            InitializeComponent();
            txtDatabaseName.DataBindings.Add(new Binding("EditValue", bdsDatabases, "name", true));
            txtPosition.DataBindings.Add(new Binding("EditValue", bdsBackups, "position", true));
            datePicker.DateTime = DateTime.Now;
            timePicker.Time = DateTime.Now;
            lbInstruction.Visible = false;
            panelRestorePoint.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.backup_devices' table. You can move, or remove it, as needed.
            //this.taBackupDevices.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.taBackupDevices.Fill(this.dS.backup_devices);
            // TODO: This line of code loads data into the 'dS.databases' table. You can move, or remove it, as needed.
            this.taDatabases.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.taDatabases.Fill(this.dS.databases);

            //if (Program.ConnectToSQL())
            //{
            //XtraMessageBox.Show("Connect Failed", this.Name, MessageBoxButtons.OK);
            // }
            //else
            //{
            //XtraMessageBox.Show("Connect Succesfully", "", MessageBoxButtons.OK);
            //String query = "SELECT name, database_id FROM sys.databases WHERE (database_id >= 5) AND (NOT(name LIKE N'ReportServer%')) ORDER BY name";
            //DataTable dt = Program.ExecSqlDataTable(query);
            //databases = dt;
            //dt.Columns.RemoveAt(1);
            //databaseList.DataSource = dt;
            //}
            //string o = ((DataRowView)sP_STT_BACKUPBindingSource[sP_STT_BACKUPBindingSource.Position])["backup_start_date"].ToString();
        }

        //btnBackup
        private void btnBackup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string query_string; // chứa câu truy vấn
            string database_name = txtDatabaseName.Text.Trim(); // lấy tên database
            string backup_name;
            bool isInit = false; // có xóa hết các database cũ trong device hay không?

            if (database_name == "") return; // nếu database_name hoặc device_name rỗng thì return;

            if (chbxDeleteAllBackupBefore.Checked == true) // nếu người dùng chấp nhận xóa hết backup
            {
                if (XtraMessageBox.Show("All the old backups will be permanently deleted. Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    isInit = true;
                    //ClearBackupInBackupSet();
                }
                else return;
            }

            backup_name = XtraInputBox.Show("Enter backup description", "Backupset Name", "").Trim(); // lấy tên của bản backup

            query_string = Query.CreateBackupQuery(database_name, backup_name, isInit); //tạo câu truy vấn backup database

            //Wait Form
            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription("Backuping...");
            SplashScreenManager.Default.SetWaitFormCaption("Please Wait!");

            int execute_result;

            try
            {
                execute_result = Program.ExecSqlNonQuery(query_string, Program.CONNECTION_STRING, "");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

            if (execute_result != 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.ERROR, $"Error while backing up database [{database_name}]");
                return;
            }
            else
            {
                CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, $"Back up database [{database_name}] successfully");
                LoadBackups(database_name);
                LoadBackupDevices();
                btnRestore.Enabled = true;
            }
        }

        //Kiểm tra thời gian lựa chọn có phù hợp hay không?
        private bool IsPointInTimeValid(string PointInTimeString, string selectedBakupTimeString)
        {
            DateTime selected_backup_time = DateTime.Parse(selectedBakupTimeString);
            DateTime point_in_time = DateTime.Parse(PointInTimeString);

            if (point_in_time > DateTime.Now) // nếu thời gian lựa chọn lớn hơn thời điểm hiện tại
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The backup point must be after the selected backup date and before the moment");
                return false;
            }
            else if (point_in_time.Date == DateTime.Now.Date) // nếu ngày chọn phục hồi == ngày hiện tại
            {
                // Kiểm tra xem thời gian chọn phục hồi có nhỏ hơn thời điểm hiện tại 3 phút hay không
                if (point_in_time.TimeOfDay.Add(new TimeSpan(0, 1, 0)) >= DateTime.Now.TimeOfDay)
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The restore time must be before the moment time at least 1 minute");
                    return false;
                }
            }


            if (point_in_time < selected_backup_time) // nếu ngày chọn phục hồi < ngày chọn bản backup gần nhất
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "The backup point must be after the selected backup date and before the moment");
                return false;
            }
            else
            {
                //kiểm tra xem ngày chọn phục hồi có lớn hơn thời gian backup gần nhất 3 phút hay không
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

        private void Restore(string databaseName, int position)
        {
            string queryString = Query.CreateRestoreQuery(databaseName, position);

            //Wait Form
            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Restoring...");
            SplashScreenManager.Default.SetWaitFormDescription("Please Wait!");

            int execute_result;
            try
            {
                execute_result = Program.ExecSqlNonQuery(queryString, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

            if (execute_result == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, $"Restore database [{databaseName}] successfully");
            }
        }

        private void RestoreToPointInTime(string databaseName, int position, string pointInTime)
        {
            string excute_statement = Query.CreateRestoreToPointInTimeQuery(databaseName, position, pointInTime);

            SplashScreenManager.ShowForm(this, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Restoring...");
            SplashScreenManager.Default.SetWaitFormDescription("Please Wait!");

            int execute_result;
            try
            {
                execute_result = Program.ExecSqlNonQuery(excute_statement, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

            if (execute_result == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, $"Restore database [{databaseName}] successfully");
            }
            //else
            //{
            //    XtraMessageBox.Show($"Restore database [{databaseName}] failed", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //else if (execute_result != 0)
            //{
            //    string deviceName = Interpolation.CreateDeviceName(databaseName);
            //    excute_statement = $"RESTORE DATABASE [{databaseName}] FROM [{deviceName}] WITH FILE = {position}\n";
            //    execute_result = Program.ExecSqlNonQuery(excute_statement, Program.CONNECTION_STRING, "Lỗi phục hồi CSDL.");
            //}
        }

        //btnRestore
        private void btnRestore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string databaseName = txtDatabaseName.Text.Trim(); // lấy tên database đang chọn hiện tại
            int position = int.Parse(((DataRowView)bdsBackups[bdsBackups.Position])["position"].ToString()); // lấy vị trí bản backup

            // nếu không có bản backup nào 
            if (this.bdsBackups.Count == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "There is no backup"); return;
            }
            //nếu không chọn bản backup nào
            if (position == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "Please choose one backup to restore"); return;
            }

            //Đóng kết nối của chính ta
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
            {
                Program.conn.Close(); //Đóng kết nối của chính ta
            }

            //nếu không chọn thì phục hồi bình thường
            if (chbxToPointInTime.Checked == false)
            {
                Restore(databaseName, position);
            }
            else //chọn phục hồi về 1 điểm bất kì
            {
                position = int.Parse(((DataRowView)bdsBackups[0])["position"].ToString());

                string date_picked = datePicker.DateTime.Date.ToString("yyyy-MM-dd");
                string time_picked = timePicker.Time.ToString("HH:mm:ss");
                string point_in_time_string = date_picked + " " + time_picked;
                //string selected_backup_time_string = ((DataRowView)bdsBackups[bdsBackups.Position])["backup_start_date"].ToString();

                string selected_backup_time_string = ((DataRowView)bdsBackups[0])["backup_start_date"].ToString();
                DateTime selected_backup_time = DateTime.Parse(selected_backup_time_string);
                selected_backup_time_string = selected_backup_time.ToString("yyyy-MM-dd HH:mm:ss");

                if (IsPointInTimeValid(point_in_time_string, selected_backup_time_string))
                {
                    if (XtraMessageBox.Show($"Are you sure restore database to '{point_in_time_string}'", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        RestoreToPointInTime(databaseName, position, point_in_time_string);
                    }

                }
            }

            Program.ConnectToSQL(); // kết nối lại đến sql server
        }

        private void sP_STT_BACKUPGridControl_Click(object sender, EventArgs e)
        {
            if (bdsBackups.Position == -1 || bdsBackups.Count == 0 || sP_STT_BACKUPGridControl.DataSource == null)
            {
                txtPosition.Text = "0";
            }
            else
            {
                int position = int.Parse(((DataRowView)bdsBackups[bdsBackups.Position])["position"].ToString());
                txtPosition.Text = position.ToString();
            }
        }

        //btnNewDevice
        private void btnNewDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string databaseName = txtDatabaseName.Text.Trim();

            string devType = "disk"; //kiểu của backup device, nvarchar(20), backup device là 1 file trên ổ đĩa cục bộ
            string logicalName = Interpolation.CreateDeviceName(databaseName); // logical name chính là device name trên ổ đĩa
            string physicalName = Interpolation.CreateDeviceAbsolutePath(logicalName); // tạo đường dẫn đến file backup device đó, extension mặc định là '.dat'
            string queryString = Query.CreateBackupDeviceQuery(devType, logicalName, physicalName); // tạo câu truy vấn tạo backup device

            int result = Program.ExecSqlNonQuery(queryString, Program.CONNECTION_STRING, "");
            if (result == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, "Create Backup Device Successfully");

                LoadBackupDevices();
                LoadBackups(databaseName); // load các bản backup trong device đó

                int databasePosition = bdsDatabases.Position;
                bdsDatabases.MoveFirst();
                bdsDatabases.Position = databasePosition;
            }
        }

        //btnRefresh
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDatabases();
            LoadBackupDevices();
        }

        private bool HasDevice(string databaseName)
        {
            string logical_name = Interpolation.CreateDeviceName(databaseName); //Test -> DEVICE_Test
            if (bdsBackupDevices.Find("name", logical_name) != -1)
            {
                return true;
            }
            return false;
        }

        //Kiểm tra xem backup device đang chọn có bản backup nào không?
        private bool HasBackups()
        {
            if (this.bdsBackups.Count == 0 || this.bdsBackups.DataSource == null || this.bdsBackups.Position == -1)
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

                LoadBackups(database_name);

                if (HasDevice(database_name)) // nếudatabase đó đã tạo backup device
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
                    btnNewDevice.Enabled = true;
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

        //btnDisconnect
        private void btnDisconnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
            this.Close();
        }

        private void gvDatabases_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (HasDevice(txtDatabaseName.Text))
                {
                    btnDeleteBackupDevice.Enabled = true;
                }
                else
                {
                    btnDeleteBackupDevice.Enabled = false;
                }
                popupMenu.ShowPopup(Cursor.Position);
            }
        }

        //btnDeleteBackupDevice
        private void btnDeleteBackupDevice_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.SmartTextWrap = true;
            if (XtraMessageBox.Show("The backup device of this database and all of backup within it will be deleted. Are you sure?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string databaseName = txtDatabaseName.Text.Trim();
                string deviceName = Interpolation.CreateDeviceName(databaseName);
                string queryString = Query.CreateDeleteBackupDeviceQuery(deviceName);

                int result = Program.ExecSqlNonQuery(queryString, Program.CONNECTION_STRING, "");
                if (result == 0)
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, "The backup device deleted successfully!");
                    ToggleBarItems(false);
                    btnNewDevice.Enabled = true;
                }
                //else
                //    CustomMessageBox.Show(CustomMessageBox.Type.ERROR, "Error while deleting backup device");
                LoadBackupDevices();
            }
        }

        private void chbxToPointInTime_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (chbxToPointInTime.Checked)
            {
                lbInstruction.Visible = true;
                panelRestorePoint.Visible = true;
            }
            else
            {
                lbInstruction.Visible = false;
                panelRestorePoint.Visible = false;
            }
        }

        //Load các bản backup trong database đã chọn
        private void LoadBackups(string databaseName)
        {
            if (databaseName.Equals("")) return;
            try
            {
                this.taBackups.Connection.ConnectionString = Program.CONNECTION_STRING;
                this.taBackups.Fill(this.dS.SP_STT_BACKUP, databaseName);
                if (bdsBackups.Count == 0)
                {
                    txtPosition.Text = "0";
                }
                else
                {
                    txtPosition.Text = int.Parse(((DataRowView)bdsBackups[0])["position"].ToString()).ToString();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.ERROR, ex.Message);
            }
        }

        //Load các backup devices
        private void LoadBackupDevices()
        {
            this.taBackupDevices.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.taBackupDevices.Fill(this.dS.backup_devices);
        }

        //Load các database
        private void LoadDatabases()
        {
            this.taDatabases.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.taDatabases.Fill(this.dS.databases);
        }

        private int DeleteBackup(int backupSetId)
        {
            taKeys.Connection.ConnectionString = Program.CONNECTION_STRING;
            taKeys.Fill(this.dS.keys, backupSetId);
            string query = Query.CreateDeleteBackupQuery(backupSetId);

            int restoreCount = bdsKeys.Count;
            if (restoreCount > 0)
            {
                string queryDeleteRestoreHistory = "";
                for (int index = 0; index < restoreCount; index++)
                {
                    string restore_history_id = ((DataRowView)bdsKeys[index])["restore_history_id"].ToString();
                    queryDeleteRestoreHistory += Query.CreateDeleteRestoreHistoryQuery(restore_history_id);
                }
                query = queryDeleteRestoreHistory + query;
            }

            return Program.ExecSqlNonQuery(query, Program.CONNECTION_STRING, "");
        }

        private void btnDeleteBackup_ItemClick(object sender, ItemClickEventArgs e)
        {
            int backupSetId = int.Parse(((DataRowView)bdsBackups.Current)["backup_set_id"].ToString());

            if (XtraMessageBox.Show("Are yout sure to delete this backup?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            int resultExec = DeleteBackup(backupSetId);

            if (resultExec == 0)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.SUCCESS, "The backup deleted successfully");
            }
            else
            {
                CustomMessageBox.Show(CustomMessageBox.Type.ERROR, $"Error while deleting backup\nError code: {resultExec}");
            }

            string databaseName = txtDatabaseName.Text.Trim();
            LoadBackups(databaseName);
            if (!HasBackups())
            {
                btnRestore.Enabled = chbxToPointInTime.Enabled = false;
            }
            else
            {
                btnRestore.Enabled = chbxToPointInTime.Enabled = true;
            }
        }

        private void gvBackups_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(Cursor.Position);
            }
        }

        private void ClearBackupInBackupSet()
        {
            string dbName = txtDatabaseName.Text;
            // trong bdsBackup có thể không đếm đủ toàn bộ backup set
            this.taBackupset.Connection.ConnectionString = Program.CONNECTION_STRING;
            this.taBackupset.Fill(this.dS.backupset, dbName);
            int countBackup = this.bdsBackupset.Count;
            for (int index = 0; index < countBackup; index++)
            {
                string backup_set_id = ((DataRowView)this.bdsBackupset[index])["backup_set_id"].ToString();
                DeleteBackup(int.Parse(backup_set_id));
            }
        }

    }
}

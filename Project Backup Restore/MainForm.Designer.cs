using System.Data;

namespace Backup_Restore
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
            {
                Program.conn.Close(); //Đóng kết nối của chính ta
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnBackup = new DevExpress.XtraBars.BarButtonItem();
            this.btnRestore = new DevExpress.XtraBars.BarButtonItem();
            this.chbxToPointInTime = new DevExpress.XtraBars.BarCheckItem();
            this.btnNewDevice = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnSettings = new DevExpress.XtraBars.BarButtonItem();
            this.btnDisconnect = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteBackupDevice = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteBackup = new DevExpress.XtraBars.BarButtonItem();
            this.gcDatabases = new DevExpress.XtraGrid.GridControl();
            this.bdsDatabases = new System.Windows.Forms.BindingSource(this.components);
            this.dS = new Backup_Restore.DS();
            this.gvDatabases = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.taDatabases = new Backup_Restore.DSTableAdapters.databasesTableAdapter();
            this.tableAdapterManager = new Backup_Restore.DSTableAdapters.TableAdapterManager();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.sP_STT_BACKUPGridControl = new DevExpress.XtraGrid.GridControl();
            this.bdsBackups = new System.Windows.Forms.BindingSource(this.components);
            this.gvBackups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colposition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colname1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbackup_start_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluser_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.lbInstruction = new DevExpress.XtraEditors.LabelControl();
            this.panelRestorePoint = new DevExpress.Utils.Layout.StackPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.datePicker = new DevExpress.XtraEditors.DateEdit();
            this.timePicker = new DevExpress.XtraEditors.TimeEdit();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPosition = new DevExpress.XtraEditors.TextEdit();
            this.chbxDeleteAllBackupBefore = new DevExpress.XtraEditors.CheckEdit();
            this.ds1 = new Backup_Restore.DS();
            this.bdsBackupset = new System.Windows.Forms.BindingSource(this.components);
            this.taBackups = new Backup_Restore.DSTableAdapters.SP_STT_BACKUPTableAdapter();
            this.bdsBackupDevices = new System.Windows.Forms.BindingSource(this.components);
            this.taBackupDevices = new Backup_Restore.DSTableAdapters.backup_devicesTableAdapter();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bdsKeys = new System.Windows.Forms.BindingSource(this.components);
            this.taKeys = new Backup_Restore.DSTableAdapters.keysTableAdapter();
            this.taBackupset = new Backup_Restore.DSTableAdapters.backupsetTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDatabases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDatabases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sP_STT_BACKUPGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBackups)).BeginInit();
            this.sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRestorePoint)).BeginInit();
            this.panelRestorePoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datePicker.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePicker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timePicker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbxDeleteAllBackupBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackupset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackupDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsKeys)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.btnRestore,
            this.btnBackup,
            this.btnRefresh,
            this.btnNewDevice,
            this.btnDisconnect,
            this.btnSettings,
            this.chbxToPointInTime,
            this.btnDeleteBackupDevice,
            this.btnDeleteBackup});
            this.barManager1.MaxItemId = 11;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.KeyTip, this.btnBackup, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard, "", ""),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRestore),
            new DevExpress.XtraBars.LinkPersistInfo(this.chbxToPointInTime),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNewDevice),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSettings),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDisconnect)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // btnBackup
            // 
            this.btnBackup.Caption = "Backup";
            this.btnBackup.Hint = "Backup selected database";
            this.btnBackup.Id = 2;
            this.btnBackup.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBackup.ImageOptions.SvgImage")));
            this.btnBackup.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBackup_ItemClick);
            // 
            // btnRestore
            // 
            this.btnRestore.Caption = "Restore";
            this.btnRestore.Hint = "Restore database from data";
            this.btnRestore.Id = 1;
            this.btnRestore.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRestore.ImageOptions.SvgImage")));
            this.btnRestore.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRestore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRestore_ItemClick);
            // 
            // chbxToPointInTime
            // 
            this.chbxToPointInTime.Caption = "To Point-In-Time";
            this.chbxToPointInTime.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.chbxToPointInTime.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.chbxToPointInTime.Id = 8;
            this.chbxToPointInTime.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("chbxToPointInTime.ImageOptions.SvgImage")));
            this.chbxToPointInTime.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.chbxToPointInTime.Name = "chbxToPointInTime";
            this.chbxToPointInTime.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.chbxToPointInTime.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chbxToPointInTime_CheckedChanged);
            // 
            // btnNewDevice
            // 
            this.btnNewDevice.Caption = "New Backup Device";
            this.btnNewDevice.Id = 4;
            this.btnNewDevice.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNewDevice.ImageOptions.SvgImage")));
            this.btnNewDevice.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnNewDevice.Name = "btnNewDevice";
            this.btnNewDevice.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNewDevice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewDevice_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Refresh";
            this.btnRefresh.Id = 3;
            this.btnRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefresh.ImageOptions.SvgImage")));
            this.btnRefresh.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnSettings
            // 
            this.btnSettings.Caption = "Settings";
            this.btnSettings.Id = 6;
            this.btnSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSettings.ImageOptions.SvgImage")));
            this.btnSettings.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSettings.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnSettings.VisibleInSearchMenu = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Caption = "Disconnect";
            this.btnDisconnect.Id = 5;
            this.btnDisconnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDisconnect.ImageOptions.SvgImage")));
            this.btnDisconnect.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnDisconnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDisconnect_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(1129, 42);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 660);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1129, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 42);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 618);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1129, 42);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 618);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Delete Backup";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnDeleteBackupDevice
            // 
            this.btnDeleteBackupDevice.Caption = "Delete Backup Device";
            this.btnDeleteBackupDevice.Id = 9;
            this.btnDeleteBackupDevice.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteBackupDevice.ImageOptions.SvgImage")));
            this.btnDeleteBackupDevice.Name = "btnDeleteBackupDevice";
            this.btnDeleteBackupDevice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteBackupDevice_ItemClick);
            // 
            // btnDeleteBackup
            // 
            this.btnDeleteBackup.Caption = "Delete Backup";
            this.btnDeleteBackup.Id = 10;
            this.btnDeleteBackup.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteBackup.ImageOptions.SvgImage")));
            this.btnDeleteBackup.Name = "btnDeleteBackup";
            this.btnDeleteBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteBackup_ItemClick);
            // 
            // gcDatabases
            // 
            this.gcDatabases.DataSource = this.bdsDatabases;
            this.gcDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDatabases.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gcDatabases.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDatabases.Location = new System.Drawing.Point(0, 0);
            this.gcDatabases.MainView = this.gvDatabases;
            this.gcDatabases.Margin = new System.Windows.Forms.Padding(0);
            this.gcDatabases.MenuManager = this.barManager1;
            this.gcDatabases.Name = "gcDatabases";
            this.gcDatabases.Size = new System.Drawing.Size(245, 618);
            this.gcDatabases.TabIndex = 9;
            this.gcDatabases.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDatabases});
            // 
            // bdsDatabases
            // 
            this.bdsDatabases.DataMember = "databases";
            this.bdsDatabases.DataSource = this.dS;
            // 
            // dS
            // 
            this.dS.DataSetName = "DS";
            this.dS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDatabases
            // 
            this.gvDatabases.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colname});
            this.gvDatabases.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvDatabases.GridControl = this.gcDatabases;
            this.gvDatabases.Name = "gvDatabases";
            this.gvDatabases.OptionsBehavior.Editable = false;
            this.gvDatabases.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDatabases.OptionsView.ShowGroupPanel = false;
            this.gvDatabases.OptionsView.ShowIndicator = false;
            this.gvDatabases.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvDatabases_RowCellClick);
            // 
            // colname
            // 
            this.colname.Caption = "Database";
            this.colname.FieldName = "name";
            this.colname.Name = "colname";
            this.colname.OptionsColumn.AllowEdit = false;
            this.colname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colname.Visible = true;
            this.colname.VisibleIndex = 0;
            // 
            // taDatabases
            // 
            this.taDatabases.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.backupsetTableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.keysTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Backup_Restore.DSTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.sP_STT_BACKUPGridControl);
            this.panelControl2.Controls.Add(this.sidePanel1);
            this.panelControl2.Controls.Add(this.stackPanel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(245, 42);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(884, 618);
            this.panelControl2.TabIndex = 9;
            // 
            // sP_STT_BACKUPGridControl
            // 
            this.sP_STT_BACKUPGridControl.DataSource = this.bdsBackups;
            this.sP_STT_BACKUPGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sP_STT_BACKUPGridControl.Location = new System.Drawing.Point(0, 37);
            this.sP_STT_BACKUPGridControl.MainView = this.gvBackups;
            this.sP_STT_BACKUPGridControl.Margin = new System.Windows.Forms.Padding(0);
            this.sP_STT_BACKUPGridControl.MenuManager = this.barManager1;
            this.sP_STT_BACKUPGridControl.Name = "sP_STT_BACKUPGridControl";
            this.sP_STT_BACKUPGridControl.Size = new System.Drawing.Size(884, 413);
            this.sP_STT_BACKUPGridControl.TabIndex = 8;
            this.sP_STT_BACKUPGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBackups});
            // 
            // bdsBackups
            // 
            this.bdsBackups.DataMember = "SP_STT_BACKUP";
            this.bdsBackups.DataSource = this.dS;
            // 
            // gvBackups
            // 
            this.gvBackups.ActiveFilterEnabled = false;
            this.gvBackups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colposition,
            this.colname1,
            this.colbackup_start_date,
            this.coluser_name});
            this.gvBackups.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvBackups.GridControl = this.sP_STT_BACKUPGridControl;
            this.gvBackups.Name = "gvBackups";
            this.gvBackups.OptionsBehavior.Editable = false;
            this.gvBackups.OptionsCustomization.AllowFilter = false;
            this.gvBackups.OptionsCustomization.AllowSort = false;
            this.gvBackups.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvBackups.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBackups.OptionsView.ShowGroupPanel = false;
            this.gvBackups.OptionsView.ShowIndicator = false;
            this.gvBackups.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvBackups_RowCellClick);
            // 
            // colposition
            // 
            this.colposition.Caption = "#";
            this.colposition.FieldName = "position";
            this.colposition.Name = "colposition";
            this.colposition.Visible = true;
            this.colposition.VisibleIndex = 0;
            // 
            // colname1
            // 
            this.colname1.Caption = "description";
            this.colname1.FieldName = "name";
            this.colname1.Name = "colname1";
            this.colname1.Visible = true;
            this.colname1.VisibleIndex = 1;
            // 
            // colbackup_start_date
            // 
            this.colbackup_start_date.Caption = "backup date";
            this.colbackup_start_date.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colbackup_start_date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colbackup_start_date.FieldName = "backup_start_date";
            this.colbackup_start_date.Name = "colbackup_start_date";
            this.colbackup_start_date.Visible = true;
            this.colbackup_start_date.VisibleIndex = 2;
            // 
            // coluser_name
            // 
            this.coluser_name.Caption = "username";
            this.coluser_name.FieldName = "user_name";
            this.coluser_name.Name = "coluser_name";
            this.coluser_name.Visible = true;
            this.coluser_name.VisibleIndex = 3;
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.lbInstruction);
            this.sidePanel1.Controls.Add(this.panelRestorePoint);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel1.Location = new System.Drawing.Point(0, 450);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(884, 168);
            this.sidePanel1.TabIndex = 8;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // lbInstruction
            // 
            this.lbInstruction.AllowHtmlString = true;
            this.lbInstruction.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInstruction.Appearance.Options.UseFont = true;
            this.lbInstruction.Appearance.Options.UseTextOptions = true;
            this.lbInstruction.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbInstruction.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbInstruction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lbInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInstruction.Location = new System.Drawing.Point(0, 61);
            this.lbInstruction.Margin = new System.Windows.Forms.Padding(0);
            this.lbInstruction.Name = "lbInstruction";
            this.lbInstruction.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lbInstruction.Size = new System.Drawing.Size(884, 107);
            this.lbInstruction.TabIndex = 11;
            this.lbInstruction.Text = resources.GetString("lbInstruction.Text");
            // 
            // panelRestorePoint
            // 
            this.panelRestorePoint.Controls.Add(this.labelControl1);
            this.panelRestorePoint.Controls.Add(this.datePicker);
            this.panelRestorePoint.Controls.Add(this.timePicker);
            this.panelRestorePoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRestorePoint.Location = new System.Drawing.Point(0, 1);
            this.panelRestorePoint.Name = "panelRestorePoint";
            this.panelRestorePoint.Size = new System.Drawing.Size(884, 60);
            this.panelRestorePoint.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 23);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Restore Point:";
            // 
            // datePicker
            // 
            this.datePicker.EditValue = null;
            this.datePicker.Location = new System.Drawing.Point(113, 20);
            this.datePicker.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.datePicker.MenuManager = this.barManager1;
            this.datePicker.Name = "datePicker";
            this.datePicker.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePicker.Properties.Appearance.Options.UseFont = true;
            this.datePicker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datePicker.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datePicker.Size = new System.Drawing.Size(150, 20);
            this.datePicker.TabIndex = 8;
            // 
            // timePicker
            // 
            this.timePicker.EditValue = new System.DateTime(2022, 4, 10, 0, 0, 0, 0);
            this.timePicker.Location = new System.Drawing.Point(283, 20);
            this.timePicker.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.timePicker.MenuManager = this.barManager1;
            this.timePicker.Name = "timePicker";
            this.timePicker.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timePicker.Properties.Appearance.Options.UseFont = true;
            this.timePicker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timePicker.Size = new System.Drawing.Size(150, 20);
            this.timePicker.TabIndex = 9;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.labelControl2);
            this.stackPanel1.Controls.Add(this.txtDatabaseName);
            this.stackPanel1.Controls.Add(this.labelControl3);
            this.stackPanel1.Controls.Add(this.txtPosition);
            this.stackPanel1.Controls.Add(this.chbxDeleteAllBackupBefore);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel1.LabelVertAlignment = DevExpress.Utils.Layout.LabelVertAlignment.Center;
            this.stackPanel1.Location = new System.Drawing.Point(0, 0);
            this.stackPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(884, 37);
            this.stackPanel1.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(20, 12);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Database name";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(119, 6);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(0);
            this.txtDatabaseName.MenuManager = this.barManager1;
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtDatabaseName.Properties.Appearance.Options.UseFont = true;
            this.txtDatabaseName.Properties.ReadOnly = true;
            this.txtDatabaseName.Size = new System.Drawing.Size(143, 24);
            this.txtDatabaseName.TabIndex = 1;
            this.txtDatabaseName.EditValueChanged += new System.EventHandler(this.txtDatabaseName_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(282, 12);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "The Backup no";
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(378, 6);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(0);
            this.txtPosition.MenuManager = this.barManager1;
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic);
            this.txtPosition.Properties.Appearance.Options.UseFont = true;
            this.txtPosition.Properties.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(54, 24);
            this.txtPosition.TabIndex = 8;
            // 
            // chbxDeleteAllBackupBefore
            // 
            this.chbxDeleteAllBackupBefore.AutoSizeInLayoutControl = true;
            this.chbxDeleteAllBackupBefore.Location = new System.Drawing.Point(467, 8);
            this.chbxDeleteAllBackupBefore.Margin = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.chbxDeleteAllBackupBefore.MenuManager = this.barManager1;
            this.chbxDeleteAllBackupBefore.Name = "chbxDeleteAllBackupBefore";
            this.chbxDeleteAllBackupBefore.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbxDeleteAllBackupBefore.Properties.Appearance.Options.UseFont = true;
            this.chbxDeleteAllBackupBefore.Properties.AutoWidth = true;
            this.chbxDeleteAllBackupBefore.Properties.Caption = "Delete all the old backups in device before creating new one";
            this.chbxDeleteAllBackupBefore.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Radio;
            this.chbxDeleteAllBackupBefore.Size = new System.Drawing.Size(335, 20);
            this.chbxDeleteAllBackupBefore.TabIndex = 6;
            // 
            // ds1
            // 
            this.ds1.DataSetName = "DS";
            this.ds1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsBackupset
            // 
            this.bdsBackupset.DataMember = "backupset";
            this.bdsBackupset.DataSource = this.dS;
            // 
            // taBackups
            // 
            this.taBackups.ClearBeforeFill = true;
            // 
            // bdsBackupDevices
            // 
            this.bdsBackupDevices.DataMember = "backup_devices";
            this.bdsBackupDevices.DataSource = this.dS;
            // 
            // taBackupDevices
            // 
            this.taBackupDevices.ClearBeforeFill = true;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcDatabases);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 42);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(245, 618);
            this.panelControl1.TabIndex = 4;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteBackupDevice)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteBackup)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // bdsKeys
            // 
            this.bdsKeys.DataMember = "keys";
            this.bdsKeys.DataSource = this.dS;
            // 
            // taKeys
            // 
            this.taKeys.ClearBeforeFill = true;
            // 
            // taBackupset
            // 
            this.taBackupset.ClearBeforeFill = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 660);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup - Restore Database in SQL SERVER";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDatabases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDatabases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sP_STT_BACKUPGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBackups)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelRestorePoint)).EndInit();
            this.panelRestorePoint.ResumeLayout(false);
            this.panelRestorePoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datePicker.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePicker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timePicker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbxDeleteAllBackupBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackupset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsBackupDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsKeys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnBackup;
        private DevExpress.XtraBars.BarButtonItem btnRestore;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.BarButtonItem btnNewDevice;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnDisconnect;
        private DevExpress.XtraBars.BarButtonItem btnSettings;
        private System.Windows.Forms.BindingSource bdsDatabases;
        private DS dS;
        private DSTableAdapters.databasesTableAdapter taDatabases;
        private DSTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraGrid.GridControl gcDatabases;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDatabases;
        private DevExpress.XtraGrid.Columns.GridColumn colname;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.BindingSource bdsBackups;
        private DSTableAdapters.SP_STT_BACKUPTableAdapter taBackups;
        private System.Windows.Forms.BindingSource bdsBackupDevices;
        private DSTableAdapters.backup_devicesTableAdapter taBackupDevices;
        private DevExpress.XtraEditors.TextEdit txtDatabaseName;
        private DevExpress.XtraBars.BarCheckItem chbxToPointInTime;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.TimeEdit timePicker;
        private DevExpress.XtraEditors.DateEdit datePicker;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chbxDeleteAllBackupBefore;
        private DevExpress.Utils.Layout.StackPanel panelRestorePoint;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPosition;
        private DevExpress.XtraBars.BarButtonItem btnDeleteBackupDevice;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraEditors.LabelControl lbInstruction;
        private DevExpress.XtraBars.BarButtonItem btnDeleteBackup;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.BindingSource bdsKeys;
        private DSTableAdapters.keysTableAdapter taKeys;
        private System.Windows.Forms.BindingSource bdsBackupset;
        private DSTableAdapters.backupsetTableAdapter taBackupset;
        private DevExpress.XtraGrid.GridControl sP_STT_BACKUPGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBackups;
        private DevExpress.XtraGrid.Columns.GridColumn colposition;
        private DevExpress.XtraGrid.Columns.GridColumn colname1;
        private DevExpress.XtraGrid.Columns.GridColumn colbackup_start_date;
        private DevExpress.XtraGrid.Columns.GridColumn coluser_name;
        private DS ds1;
    }
}


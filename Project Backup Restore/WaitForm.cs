using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Backup_Restore
{
    public partial class WaitForm : DevExpress.XtraWaitForm.WaitForm
    {
        public WaitForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }

        public static void ShowWaitForm(Form parrent, Action callback, string caption, string description)
        {
            //Open Wait Form
            SplashScreenManager.ShowForm(parrent, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription(description);
            SplashScreenManager.Default.SetWaitFormCaption(caption);

            try
            {
                parrent.Opacity = 0.9d;
                callback();
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                parrent.Opacity = 1.0d;
            }
        }
    }
}
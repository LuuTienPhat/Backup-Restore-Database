using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
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

        public static void ShowWaitForm(DevExpress.XtraEditors.XtraForm parrent, Action callback, string caption, string description)
        {
            //Open Wait Form
            SplashScreenManager.ShowForm(parrent, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription(description);
            SplashScreenManager.Default.SetWaitFormCaption(caption);

            try
            {
                callback();
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        public static bool ShowWaitForm(DevExpress.XtraEditors.XtraForm parrent, Func<bool> callback, string caption, string description)
        {
            bool result;
            //Open Wait Form
            SplashScreenManager.ShowForm(parrent, typeof(WaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription(description);
            SplashScreenManager.Default.SetWaitFormCaption(caption);

            try
            {
                result = callback();
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

            return result;
        }
    }
}
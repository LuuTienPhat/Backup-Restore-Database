using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup_Restore
{
    internal class CustomMessageBox
    {
        public enum Type
        {
            ERROR,
            WARNING,
            SUCCESS
        }

        public static void Show(CustomMessageBox.Type type, string content)
        {
            XtraMessageBox.SmartTextWrap = true;
            //XtraMessageBoxArgs args = new XtraMessageBoxArgs(null, this, "Our application receives error messages that can be wordy and we'd like to limit the width of the message box to a certain maximum width. Right now, XtraMessageBox seems to always use the full screen width when displaying long message. SmartTextWrap and AllowHtmlText have no effect. See an example in the attached screen shot.", "Caption", new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.Yes, System.Windows.Forms.DialogResult.No });
            //args.Showing += Args_Showing;
            //XtraMessageBox.SmartTextWrap = true;
            //XtraMessageBox.Show(args);

            switch (type)
            {
                case CustomMessageBox.Type.ERROR:
                    XtraMessageBox.Show(content, Program.FirstCharToUpper("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CustomMessageBox.Type.WARNING:
                    XtraMessageBox.Show(content, Program.FirstCharToUpper("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case CustomMessageBox.Type.SUCCESS:
                    XtraMessageBox.Show(content, Program.FirstCharToUpper("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }

        private void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Form.MaximumSize = new Size(200, 200);
        }
    }
}

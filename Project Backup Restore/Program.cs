using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Backup_Restore
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static SqlConnection conn = new SqlConnection();
        public static string CONNECTION_STRING = "";
        public static SqlDataAdapter sqlDataAdapter;
        //public static string connstr_publisher = "Data Source=MSI;Initial Catalog=TN_CSDLPT;Integrated Security=True";

        public static SqlDataReader myReader;
        public static string SERVER_NAME = "";
        public static string LOGIN = "";
        public static string PASSWORD = "";

        public static string database = "tempdb";
        public static int startYear = 2016; //để cho cmbNK tự động dựa vào năm này
        public static int flagRestore = 0; //để kiểm tra user có phục hồi csdl
        public static string BACKUP_PATH = "C:\\Backup";

        /// <summary>
        /// {0} dev_type
        /// {1} logical_name or you can name after database_name
        /// {2} the path to device file on disk
        /// </summary>
        public static string CREATE_DEVICE = "EXEC sys.sp_addumpdevice @devtype = '{0}', @logicalname = N'{1}', @physicalname = N'{2}'";

        /// <summary>
        /// {0} database_name
        /// </summary>
        public static string PREFIX_DEVICE_NAME = "DEVICE_{0}";

        /// <summary>
        /// {0} database_name
        /// {1} device_name
        /// </summary>
        public static string BACKUP_DATABASE = "BACKUP DATABASE [{0}] TO [{1}]";
        public static string BACKUP_DATABASE_WIT_INIT = "BACKUP DATABASE [{0}] TO [{1}] WITH INIT";


        /// <summary>
        /// {0} database_name
        /// {1} device_name
        /// {2} position in backupset
        /// </summary>
        public static string RESTORE_DATABASE =
                            "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
                            "USE tempdb\n" +
                            "RESTORE DATABASE [{0}] FROM [{1}] WITH FILE = {2}, REPLACE\n" +
                            "ALTER DATABASE [{0}]  SET MULTI_USER";


        /// <summary>
        /// {0} database_name
        /// {1} backup_log path
        /// {2} position 
        /// {3} point_in_time
        /// </summary>
        public static string RESTORE_DATABASE_TO_POINT_IN_TIME =
                            "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
                            "BACKUP LOG [{0}] TO DISK = '{1}' WITH INIT\n" +
                            "USE MASTER\n" +
                            "RESTORE DATABASE [{0}] FROM DISK = '{2}' WITH NORECOVERY\n" +
                            "RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH STOPAT = '{3}'" +
                            "ALTER DATABASE [{0}]  SET MULTI_USER";

        public static string DELETE_BACKUP = "";

        /// <summary>
        /// {0} device_name 
        /// </summary>
        public static string DELETE_BACKUP_DEVICE = "EXEC sp_dropdevice '{0}', 'delfile' ";


        public static bool ConnectToSQL()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.CONNECTION_STRING = "Data Source=" + Program.SERVER_NAME + ";Initial Catalog=" +
                      Program.database + ";User ID=" +
                      Program.LOGIN + ";password=" + Program.PASSWORD;
                Program.conn.ConnectionString = Program.CONNECTION_STRING;
                Program.conn.Open();
                return true;
            }

            catch (Exception e)
            {
                CustomMessageBox.Show(CustomMessageBox.Type.ERROR, "Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message);
                return false;
            }
        }

        //PHẢI CHỈNH SỬA LẠI PHẦN CONNECTION STRING
        public static SqlDataReader ExecSqlDataReader(String cmd, String connectionString)
        {
            conn.Close();

            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(cmd, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 300;

            if (Program.conn.State == ConnectionState.Closed)
            {
                Program.conn.Open();
            }

            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                XtraMessageBox.Show(ex.Message);
                return null;
            }
        }

        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        //PHẢI CÓ CHỈNH SỬA Ở PHẦN RETURN
        public static int ExecSqlNonQuery(String cmd, String connectionString, String errStr)
        {
            conn = new SqlConnection(connectionString);
            SqlCommand Sqlcmd = new SqlCommand(cmd, conn);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;// 10 phut

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            try
            {
                int errorNumber = Sqlcmd.ExecuteNonQuery();
                conn.Close();
                return 0;

            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Error converting data type varchar to int"))
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.WARNING, "Bạn format Cell lại cột \"Ngày Thi\" qua kiểu Number hoặc mở File Excel, Import lại.");
                }
                else
                {
                    CustomMessageBox.Show(CustomMessageBox.Type.WARNING, errStr + "\n" + ex.Message);
                    conn.Close();
                    return ex.State; // lấy trạng thái lỗi gởi từ RAISERROR trong SQL Server qua
                }

                //MessageBox.Show(ex.Message);

            }
            return -1;
        }

        public static bool KTra_Nhay(char kytu)
        {
            return kytu == '\'';
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        // cái này để tìm trên 1 dòng dùng nhiều cột làm điều kiện trên binding Source
        //public static int Find(this BindingSource source, params Key[] keys)
        //{
        //    PropertyDescriptor[] properties = new PropertyDescriptor[keys.Length];

        //    ITypedList typedList = source as ITypedList;

        //    if (source.Count <= 0) return -1;

        //    PropertyDescriptorCollection props;

        //    if (typedList != null) // obtain the PropertyDescriptors from the list
        //    {
        //        props = typedList.GetItemProperties(null);
        //    }
        //    else // use the TypeDescriptor on the first element of the list
        //    {
        //        props = TypeDescriptor.GetProperties(source[0]);
        //    }

        //    for (int i = 0; i < keys.Length; i++)
        //    {
        //        properties[i] = props.Find(keys[i].PropertyName, true, true); // will throw if the property isn't found
        //    }

        //    for (int i = 0; i < source.Count; i++)
        //    {
        //        object row = source[i];
        //        bool match = true;

        //        for (int p = 0; p < keys.Count(); p++)
        //        {
        //            if (properties[p].GetValue(row) != keys[p].Value)
        //            {
        //                match = false;
        //                break;
        //            }
        //        }

        //        if (match) return i;
        //    }

        //    return -1;
        //}

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}

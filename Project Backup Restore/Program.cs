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

            catch (Exception ex)
            {
                //CustomMessageBox.Show(CustomMessageBox.Type.ERROR, "Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message);
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

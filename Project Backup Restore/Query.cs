using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup_Restore
{
    internal class Query
    {
        public static string CreateBackupQuery(string databaseName, string backupName, bool init)
        {
            string deviceName = Interpolation.CreateDeviceName(databaseName);

            if (init)
                return $"BACKUP DATABASE [{databaseName}] TO [{deviceName}] WITH NAME = '{backupName}', INIT";
            else
                return $"BACKUP DATABASE [{databaseName}] TO [{deviceName}] WITH NAME = '{backupName}'";
        }

        public static string CreateBackupDeviceQuery(string devType, string logicalName, string physicalName)
        {
            return $"EXEC sys.sp_addumpdevice @devtype = '{devType}', @logicalname = N'{logicalName}', @physicalname = N'{physicalName}'";
        }

        public static string CreateRestoreQuery(string databaseName, int position)
        {
            string deviceName = Interpolation.CreateDeviceName(databaseName);
            return
                    $"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
                    $"USE tempdb\n" +
                    $"RESTORE DATABASE [{databaseName}] FROM [{deviceName}] WITH FILE = {position}, REPLACE\n" +
                    $"ALTER DATABASE [{databaseName}]  SET MULTI_USER";
        }
        public static string CreateRestoreToPointInTimeQuery(string databaseName, int position, string pointInTime)
        {
            string deviceName = Interpolation.CreateDeviceName(databaseName);
            string devicePath = Interpolation.CreateDeviceAbsolutePath(deviceName);
            string backupLogPath = Interpolation.CreateLogAbsolutePath(databaseName);

            //string a =
            //        "ALTER DATABASE [" + databaseName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
            //        "BACKUP LOG [" + databaseName + "] TO DISK = '" + backupLogPath + "' WITH INIT\n" +
            //        "USE MASTER\n" +
            //        "RESTORE DATABASE [" + databaseName + "] FROM [" + deviceName + "] WITH FILE = " + position + ", REPLACE, NORECOVERY\n" +
            //        "RESTORE DATABASE [" + databaseName + "] FROM DISK = '" + backupLogPath + "' WITH STOPAT = '" + pointInTime + "'";

            return
                    "ALTER DATABASE [" + databaseName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n" +
                    "BACKUP LOG [" + databaseName + "] TO DISK = '" + backupLogPath + "' WITH INIT\n" +
                     "USE MASTER\n" +
                    "RESTORE DATABASE [" + databaseName + "] FROM DISK = '" + devicePath + "' WITH FILE = " + position + ", REPLACE, NORECOVERY\n" +
                    "RESTORE DATABASE [" + databaseName + "] FROM DISK = '" + backupLogPath + "' WITH STOPAT = '" + pointInTime + "'" +
                    "ALTER DATABASE [" + databaseName + "]  SET MULTI_USER";
        }
        public static string CreateDeleteBackupDeviceQuery(string deviceName)
        {
            return $"EXEC sp_dropdevice '{deviceName}', 'delfile'";
        }

        public static string CreateDeleteBackupQuery(int backupSetId)
        {
            return $"DELETE FROM msdb.dbo.backupfile WHERE backup_set_id = {backupSetId}\n" +
                    $"DELETE FROM msdb.dbo.backupfilegroup WHERE backup_set_id = {backupSetId}\n" +
                    $"DELETE FROM msdb.dbo.backupset WHERE backup_set_id = {backupSetId}\n";
        }

        public static string CreateDeleteRestoreHistoryQuery(string restoreHistoryId)
        {
            return
                $"DELETE FROM msdb.dbo.restorefile WHERE restore_history_id = {restoreHistoryId}\n" +
                $"DELETE FROM msdb.dbo.restorefilegroup WHERE restore_history_id = {restoreHistoryId}\n" +
                $"DELETE FROM msdb.dbo.restorehistory WHERE restore_history_id = {restoreHistoryId}\n";
        }

        public static string CreateDeleteMediaQuery(string restoreHistoryId)
        {
            return
                $"DELETE FROM msdb.dbo.backupmediafamily WHERE media_set_id = {restoreHistoryId}\n" +
                $"DELETE FROM msdb.dbo.backupmediaset WHERE media_set_id = {restoreHistoryId}\n";
        }

    }

    internal class Interpolation
    {
        public static string CreateDeviceName(string databaseName)
        {
            return $"DEVICE_{databaseName}";
        }

        public static string CreateDeviceAbsolutePath(string deviceName, string extension = ".dat")
        {
            return Program.BACKUP_PATH + "\\" + deviceName + extension;
        }

        public static string CreateLogAbsolutePath(string databaseName, string extension = ".trn")
        {
            return Program.BACKUP_PATH + "\\" + "LOG_" + databaseName + extension;
        }
    }
}

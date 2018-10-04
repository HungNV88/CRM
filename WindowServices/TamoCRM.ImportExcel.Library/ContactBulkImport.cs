using System.Data;
using System.Data.SqlClient;

namespace TamoCRM.ImportExcel.Library
{
    public class ContactBulkImport
    {
        public static void ImportContact(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(ImportConfig.ConnectionString, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepIdentity)) // Lock the table
            {           
                sqlBulkCopy.DestinationTableName = "Contacts";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }
        public static void ImportContactTmp(DataTable dtData)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(ImportConfig.ConnectionString, SqlBulkCopyOptions.TableLock)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "ContactTmps";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }
        public static void ImportContactLevel(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "ContactLevelInfos";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }
        public static void ImportContactPhone(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "Phones";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }
        public static void ImportObjectChanges(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "ActivityObjectChanges";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }

        public static void ImportContactDuplicate(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "ContactDuplicates";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }

        public static void ImportLogContainerMOL(DataTable dtData, SqlConnection conn, SqlTransaction transaction)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            using (var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction)) // Lock the table
            {
                sqlBulkCopy.DestinationTableName = "LogContainerMOL";
                sqlBulkCopy.BulkCopyTimeout = 300;
                sqlBulkCopy.WriteToServer(dtData);
                sqlBulkCopy.Close();
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.ImportExcel.Library
{
    public class BulkContact
    {
        private static void InsertDataUsingSqlBulkCopy(IEnumerable<ContactInfo> people, SqlConnection connection)
        {
            var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "Person";
//            bulkCopy.ColumnMappings.Add("Name", "Name")lkCopy.ColumnMappings.Add("DateOfBirth", "DateOfBirth");
            SqlBulkCopyColumnMapping mapping = new SqlBulkCopyColumnMapping();            

            using (var dataReader = new ObjectDataReader<ContactInfo>(people))
            {
                bulkCopy.WriteToServer(dataReader);
            }
        }
    }
}

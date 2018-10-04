using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TamoCRM.Domain.ContactTmps;

namespace TamoCRM.ImportExcel.Library
{
    public class BulkContactTmp
    {
        private static void InsertDataUsingSqlBulkCopy(IEnumerable<ContactTmpInfo> people, SqlConnection connection)
        {
            var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "Person";
            bulkCopy.ColumnMappings.Add("Name", "Name");
            bulkCopy.ColumnMappings.Add("DateOfBirth", "DateOfBirth");

            using (var dataReader = new ObjectDataReader<ContactTmpInfo>(people))
            {
                bulkCopy.WriteToServer(dataReader);
            }
        }
    }
}


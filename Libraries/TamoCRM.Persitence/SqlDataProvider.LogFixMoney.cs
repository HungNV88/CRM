using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void LogFixMoney_Insert(string userTVTS, string notes, string description,DateTime createDate, SqlTransaction tran)
        {
            SqlHelper.ExecuteNonQuery(tran, GetFullyQualifiedName("LogFixMoney_Create"), userTVTS, notes, description, createDate);
        }
    }
}

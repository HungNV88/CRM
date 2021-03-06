using System;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void TmpLogService_Insert(int status, int callType, DateTime? time, string description)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TmpLogService_Insert"), status, callType, time, description);
        }

        public override void LogDashBoard_Insert(DateTime? date, string time, int contactid, string name, int createdby, string runtime, int session)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LogDashBoard_Insert"), date, time, contactid, name, createdby, runtime, session);
        }
    }
}


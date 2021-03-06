using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int MissedCall_Insert(int userId, int contactId, string phoneNumber, string oldCallId, string agentCode, DateTime? missedCallTime, DateTime? createdTime, int status)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("MissedCall_Insert"), userId, contactId, phoneNumber, oldCallId, agentCode, missedCallTime, createdTime, status);
        }

        public override IDataReader MissedCall_GetAll(int userId, int status)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("MissedCall_GetAll_ByUserId"), userId, status);
        }

        public override void MissedCall_UpdateStatus(int contactId, int status)
        {
            SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("MissedCall_UpdateStatus"), contactId, status);
        }
    }
}


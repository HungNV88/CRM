using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int MissedCall_Insert(int userId, int contactId, string phoneNumber, string oldCallId, string agentCode, DateTime? missedCallTime, DateTime? createdTime, int status);
        public abstract IDataReader MissedCall_GetAll(int userId, int status);
        public abstract void MissedCall_UpdateStatus(int contactId, int status);
    }
}


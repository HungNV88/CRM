using System;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void TmpLogService_Insert(int status, int callType, DateTime? time, string description);

        public abstract void LogDashBoard_Insert(DateTime? date, string time, int contactid, string name, int createdby, string runtime, int session);
    }
}


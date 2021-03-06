using System;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int ContactDuplicate_Update(int contactId, int sourceTypeId, int status, int importId, string duplicateInfo, DateTime? createdDate, bool IsNotyfiDuplicate);
        public abstract int ContactDuplicate_CheckExitsNotify(int contactId);
        public abstract int ContactDuplicate_UpdateIsNotify(int contactId);
        public abstract int ContactDuplicate_Insert(int contactId,int sourceTypeId,int statusId,int importId,string duplicateInfo,DateTime? createdDate,bool isNotifyDuplicate);
        
    }
}


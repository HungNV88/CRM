using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int UserDraft_Insert( int userId, int branchId, bool isDraftCollabortor, bool isDraftConsultant, DateTime createdDate);
        public abstract void UserDraft_Update( int id, int userId, int branchId, bool isDraftCollabortor, bool isDraftConsultant,DateTime createdDate);
        public abstract void UserDraft_Delete(int id);
        public abstract IDataReader UserDraft_GetInfo(int id);
        public abstract IDataReader UserDraft_GetAll();  
        public abstract IDataReader UserDraft_Search(string keyword, int pageIndex, int pageSize);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Sources_Insert( string name, string code, string description, int createdBy);
        public abstract void Sources_Update( int sourceId, string name, string code, string description, int changedBy);
        public abstract void Sources_Delete(int sourceId,int deletedby);
        public abstract IDataReader Sources_GetInfo(int sourceId);
        public abstract IDataReader Sources_GetAll();  
        public abstract IDataReader Sources_Search(string keyword, int pageIndex, int pageSize);
    }
}


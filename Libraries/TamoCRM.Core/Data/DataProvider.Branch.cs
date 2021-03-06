using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Branches_Insert( string code, string name, int locationID, string description, int createdBy);
        public abstract void Branches_Update( int branchId, string code, string name, int locationID, string description,int changedBy);
        public abstract void Branches_Delete(int branchId,int deletedby);
        public abstract IDataReader Branches_GetInfo(int branchId);
        public abstract IDataReader Branches_GetAll();  
        public abstract IDataReader Branches_Search(string keyword, int pageIndex, int pageSize);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Collectors_Insert( string code, string name, string description,int createdBy);
        public abstract void Collectors_Update( int collectorId, string code, string name, string description, int changedBy);
        public abstract void Collectors_Delete(int collectorId, int deletedby);
        public abstract IDataReader Collectors_GetInfo(int collectorId);
        public abstract IDataReader Collectors_GetAll();  
        public abstract IDataReader Collectors_Search(string keyword, int pageIndex, int pageSize);
    }
}


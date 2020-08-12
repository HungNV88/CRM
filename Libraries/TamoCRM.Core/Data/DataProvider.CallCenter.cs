using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
       
        public abstract IDataReader CallCenter_GetAll();
        public abstract IDataReader CallCenter_Search(string keyword, int pageIndex, int pageSize);
    }
}

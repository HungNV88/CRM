using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
       
        public abstract IDataReader CallCenter_GetAll();
        public abstract IDataReader CallCenter_Search(string keyword, int pageIndex, int pageSize);
        public abstract int CallCenter_Insert(string name, string phoneNumber, string useFor, string endPoint, string token, int port, int createBy);

        public abstract IDataReader CallCenter_Extension_GetAll();
        public abstract IDataReader CallCenter_Extension_Search(string keyword, int pageIndex, int pageSize);
    }
}

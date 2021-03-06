using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void WebServiceConfig_Update(int id, int branchId, int type, string value);
        public abstract int WebServiceConfig_Insert(int branchId, int type, string value);
        public abstract IDataReader WebServiceConfig_GetInfo(int branchId, int type);
        public abstract IDataReader WebServiceConfig_GetInfo(int id);
        public abstract IDataReader WebServiceConfig_GetAll();
        public abstract void WebServiceConfig_Delete(int id);
    }
}


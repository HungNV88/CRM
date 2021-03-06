using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void Catalog_Delete<T>(int id);
        public abstract IDataReader Catalog_SelectAll<T>();
        public abstract IDataReader Catalog_SelectOne<T>(int id);
        public abstract int Catalog_Insert<T>(string name, object objOrder = null);
        public abstract void Catalog_Update<T>(int id, string name, object objOrder = null);
        public abstract IDataReader Catalog_Search<T>(string keyword, int pageIndex, int pageSize);
        public abstract IDataReader UserRole_SelectAll();
        public abstract IDataReader UserGroup_SelectAll();
        public abstract IDataReader UserBranch_SelectAll();
    }
}


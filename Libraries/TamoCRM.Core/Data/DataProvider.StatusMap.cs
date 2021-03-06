using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader StatusMap_GetAll();
        public abstract IDataReader StatusMap_GetInfo(int id);
        public abstract IDataReader StatusMap_GetAll_ByLevelId(int levelId);
    }
}

using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader Table_GetAll();
        public abstract void Table_Truncate(string names);
    }
}


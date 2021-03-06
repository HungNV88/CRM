using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader CasecAccount_GetAllByContactId(int contactId);
        public abstract IDataReader CasecAccount_Provide(int contactId);
        public abstract IDataReader CasecAccount_GetInfo(int id);
        public abstract IDataReader CasecAccount_Get();
        public abstract int CasecAccount_CountAllNotUse();
        public abstract void CasecAccount_Insert(string account, string password);
        public abstract IDataReader CasecAccount_GetAll();
    }
}


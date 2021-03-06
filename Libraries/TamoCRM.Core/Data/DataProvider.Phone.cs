using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void Phones_Insert(int contactId, string phoneType, string phoneNumber, int isPrimary);
        public abstract void Phones_Update(int contactId, string phoneType, string phoneNumber,int isPrimary);
        public abstract void Phones_Delete(int phoneId);
        public abstract IDataReader Phones_GetInfo(int phoneId);
        public abstract IDataReader Phones_GetAll();
        public abstract IDataReader Phones_GetAll_ForRedis();
        public abstract IDataReader Phones_GetByContact(int contactId);
        public abstract IDataReader Phones_GetByContacts_Xml(string xml);
        public abstract IDataReader Phones_GetByContacts(string contactIds);
        public abstract IDataReader Phones_GetByPhoneNumber(string phoneNumber);
        public abstract IDataReader Phones_Search(string keyword, int pageIndex, int pageSize);
    }
}


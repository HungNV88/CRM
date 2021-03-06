using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void Phones_Insert(int contactId, string phoneType, string phoneNumber, int isPrimary)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Phones_Insert"), contactId, phoneType, phoneNumber, isPrimary);
        }

        public override void Phones_Update(int contactId, string phoneType, string phoneNumber, int isPrimary)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Phones_Update"), contactId, phoneType, phoneNumber, isPrimary);
        }

        public override void Phones_Delete(int phoneId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Phones_Delete"), phoneId);
        }

        public override IDataReader Phones_GetInfo(int phoneId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetInfo"), phoneId);
        }

        public override IDataReader Phones_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetAll"));
        }

        public override IDataReader Phones_GetAll_ForRedis()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetAll_ForRedis"));
        }

        public override IDataReader Phones_GetByContact(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetByContact"), contactId);
        }

        public override IDataReader Phones_GetByContacts_Xml(string xml)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetByContacts_Xml"), xml);
        }

        public override IDataReader Phones_GetByContacts(string contactIds)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetByContacts"), contactIds);
        }

        public override IDataReader Phones_GetByPhoneNumber(string phoneNumber)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_GetByPhoneNumber"), phoneNumber);
        }

        public override IDataReader Phones_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Phones_Search"), keyword, pageIndex, pageSize);
        }
    }
}


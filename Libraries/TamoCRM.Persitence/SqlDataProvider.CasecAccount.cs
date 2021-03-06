using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader CasecAccount_GetAllByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_GetAllByContactId"), contactId);
        }

        //public override IDataReader CasecAccount_GetAllNotUse()
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_GetAllNotUse"));
        //}

        public override IDataReader CasecAccount_Provide(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_Provide"), contactId);
        }

        public override IDataReader CasecAccount_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_GetInfo"), id);
        }
        public override IDataReader CasecAccount_Get()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_Get"));
        }
        public override int CasecAccount_CountAllNotUse()
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CasecAccount_CountAccCasecRest"));
        }
        public override void CasecAccount_Insert(string account, string password)
        {
             SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CasecAccount_InsertAccount"), account, password);
        }

        public override IDataReader CasecAccount_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CasecAccount_GetAll"));
        }
    }
}


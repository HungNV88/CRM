using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int UserDraft_Insert( int userId, int branchId, bool isDraftCollabortor, bool isDraftConsultant, DateTime createdDate)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("UserDraft_Insert"),  userId, branchId, isDraftCollabortor, isDraftConsultant, createdDate);
        }

        public override void UserDraft_Update( int id, int userId, int branchId, bool isDraftCollabortor, bool isDraftConsultant,DateTime createdDate)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserDraft_Update"),  id, userId, branchId, isDraftCollabortor, isDraftConsultant,createdDate);
        }

        public override void UserDraft_Delete(int id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserDraft_Delete"), id);
        }

        public override IDataReader UserDraft_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("UserDraft_GetInfo"), id);
        }

        public override IDataReader UserDraft_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("UserDraft_GetAll"));
        }

        public override IDataReader UserDraft_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("UserDraft_Search"), keyword, pageIndex, pageSize);
        }
    }
}


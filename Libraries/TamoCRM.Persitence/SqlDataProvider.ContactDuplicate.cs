using System;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int ContactDuplicate_Update(int contactId, int sourceTypeId, int status, int importId, string duplicateInfo, DateTime? createdDate, bool IsNotyfiDuplicate)
        {
            return (int)SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactDuplicate_Update"), contactId, sourceTypeId, status, importId, duplicateInfo, createdDate, IsNotyfiDuplicate);
        }

        public override int ContactDuplicate_CheckExitsNotify(int contactId)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("ContactDuplicate_CheckExitsNotify"), contactId);
        }

        public override int ContactDuplicate_UpdateIsNotify(int contactId)
        {
            return (int)SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactDuplicate_UpdateIsNotify"), contactId);
        }

        public override int ContactDuplicate_Insert(int contactId, int sourceTypeId, int statusId, int importId, string duplicateInfo, DateTime? createdDate, bool isNotifyDuplicate)
        {
            throw new NotImplementedException();
        }
    }
}


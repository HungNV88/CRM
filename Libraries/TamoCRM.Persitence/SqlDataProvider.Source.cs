using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Sources_Insert( string name, string code, string description, int createdBy)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Sources_Insert"),  name, code, description,  createdBy);
        }

        public override void Sources_Update( int sourceId, string name, string code, string description, int changedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Sources_Update"),  sourceId, name, code, description,changedBy);
        }

        public override void Sources_Delete(int sourceId,int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Sources_Delete"), sourceId, deletedby);
        }

        public override IDataReader Sources_GetInfo(int sourceId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Sources_GetInfo"), sourceId);
        }

        public override IDataReader Sources_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Sources_GetAll"));
        }

        public override IDataReader Sources_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Sources_Search"), keyword, pageIndex, pageSize);
        }
    }
}


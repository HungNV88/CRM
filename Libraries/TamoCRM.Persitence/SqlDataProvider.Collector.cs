using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Collectors_Insert( string code, string name, string description,  int createdBy)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Collectors_Insert"),  code, name, description,createdBy);
        }

        public override void Collectors_Update( int collectorId, string code, string name, string description, int changedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Collectors_Update"),  collectorId, code, name, description, changedBy);
        }

        public override void Collectors_Delete(int collectorId, int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Collectors_Delete"), collectorId,deletedby);
        }

        public override IDataReader Collectors_GetInfo(int collectorId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Collectors_GetInfo"), collectorId);
        }

        public override IDataReader Collectors_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Collectors_GetAll"));
        }

        public override IDataReader Collectors_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Collectors_Search"), keyword, pageIndex, pageSize);
        }
    }
}


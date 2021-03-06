using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void WebServiceConfig_Update(int id, int branchId, int type, string value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("WebServiceConfig_Update"), id, branchId, type, value);
        }

        public override int WebServiceConfig_Insert(int branchId, int type, string value)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("WebServiceConfig_Insert"), branchId, type, value);
        }

        public override IDataReader WebServiceConfig_GetInfo(int branchId, int type)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("WebServiceConfig_GetInfo_ByFk"), branchId, type);
        }

        public override IDataReader WebServiceConfig_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("WebServiceConfig_GetInfo"), id);
        }

        public override IDataReader WebServiceConfig_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("WebServiceConfig_GetAll"));
        }

        public override void WebServiceConfig_Delete(int id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("WebServiceConfig_Delete"), id);
        }
    }
}


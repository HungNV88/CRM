using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Branches_Insert( string code, string name, int locationID, string description,int createdBy)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Branches_Insert"),  code, name, locationID, description, createdBy);
        }

        public override void Branches_Update( int branchId, string code, string name, int locationID, string description, int changedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Branches_Update"),  branchId, code, name, locationID, description, changedBy);
        }

        public override void Branches_Delete(int branchId,int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Branches_Delete"), branchId,deletedby);
        }

        public override IDataReader Branches_GetInfo(int branchId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Branches_GetInfo"), branchId);
        }

        public override IDataReader Branches_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Branches_GetAll"));
        }

        public override IDataReader Branches_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Branches_Search"), keyword, pageIndex, pageSize);
        }
    }
}


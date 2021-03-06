using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader Table_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Table_Select_All"));
        }

        public override void Table_Truncate(string names)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Table_Truncate"), names);
        }
    }
}


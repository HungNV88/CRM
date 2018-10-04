using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader Quality_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Quality_GetAll"));
        }   
    }
}


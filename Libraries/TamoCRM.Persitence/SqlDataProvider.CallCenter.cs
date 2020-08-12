using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;
using System;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        
        public override IDataReader CallCenter_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_GetAll"));
        }

        public override IDataReader CallCenter_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_Search"), keyword, pageIndex, pageSize);
        }

    }
}

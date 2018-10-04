using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override string Country_GetValueWithId(int id)
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Country_GetValueWithId"), id);
        }
    }
}

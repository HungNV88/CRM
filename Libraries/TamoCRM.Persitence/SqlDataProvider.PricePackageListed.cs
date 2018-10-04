using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override string PricePackageListed_GetNamePackageListed(int id)
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("PricePackageListed_GetPricePackage"), id);
        }
    }
}

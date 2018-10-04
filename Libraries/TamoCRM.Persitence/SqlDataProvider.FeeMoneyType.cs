using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override string FeeMoneyType_GetFeemoneyTypeName(int id)
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("FeeMoneyType_GetNameById"), id);
        }
        public override IDataReader FeeMoneyType_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("FeeMoneyType_GetAll"));
        }   

    }
}

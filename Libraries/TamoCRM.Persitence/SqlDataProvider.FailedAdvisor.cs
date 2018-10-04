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
        public override IDataReader FailedAdvisor_GetAll(int userId, int status)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("FailedAdvisor_GetAll_ByUserId"), userId, status);
        }
    }
}

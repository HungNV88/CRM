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
        public override void InsertLogHandoverAdvisor(string code, string note, int status)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Insert_LogHandoverAdvisor"), code, note, status);
        }
        public override void UpdateLogHandoverAdvisor(string code, string note, int status)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Update_LogHandoverAdvisor"), code, note, status);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TamoCRM.Core.Data;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Services;

namespace TamoCRM.Services.LogHandoverAdvisor
{
    public class LogHandoverAdvisorRepository
    {
        public static void Insert_LogHandoverAdvisor(string code, string note, int status)
        {
            DataProvider.Instance().InsertLogHandoverAdvisor(code, note, status);        
        }

        public static void Update_LogHandoverAdvisor(string code, string note, int status)
        {
            DataProvider.Instance().UpdateLogHandoverAdvisor(code, note, status);
        }

        
    }
}

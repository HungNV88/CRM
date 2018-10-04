using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void InsertLogHandoverAdvisor(string code, string note, int contactid);

        public abstract void UpdateLogHandoverAdvisor(string code, string note, int contactid);

    }
}

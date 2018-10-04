using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Domain.FailedAdvisor
{
    public class FailedAdvisorInfo
    {
        public FailedAdvisorInfo()
        {
            Amount = 1;
        }
        public string Code {get; set;}
        public int Status { get; set; }
        public string Note { get; set; }
        public DateTime? CreateDate { get; set; } 
        public int TypeNotify { get; set; }
        public int Amount { get; set; }
        public string ContactName { get; set; }
        public int ContactId { get; set; }

        public string StringHandoverAdvisorTime
        {
            get { return ObjectHelper.StandardDatetimeToString(CreateDate); }
        }

    }
}

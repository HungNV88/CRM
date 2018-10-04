using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BCCallHistoryModel
    {
        public string AgentCode { get; set; }
        public string Duration { get; set; }
        public string CallCount { get; set; }
        public string RingTime { get; set; }
        public string SumCallInfo { get; set; } // Tong cua Duration voi RingTime

    }
}
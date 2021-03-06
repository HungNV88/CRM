using System.Collections.Generic;
using TamoCRM.Domain.CallHistories;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.CallHistories
{
    public class CallHistoryListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        //Total Pages
        public int Total { get; set; }
        public List<string> UserData { get; set; }
        public IEnumerable<CallHistoryInfo> Rows { get; set; }
    }
}


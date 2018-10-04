using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.CasecAccounts;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Casec
{
    public class CasecListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        //Total Pages
        public int Total { get; set; }
        public IEnumerable<CasecAccountInfo> Rows { get; set; }
    }
}
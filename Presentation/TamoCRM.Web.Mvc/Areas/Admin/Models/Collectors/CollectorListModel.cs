using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Collectors;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Collectors
{
    public class CollectorListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<CollectorInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}


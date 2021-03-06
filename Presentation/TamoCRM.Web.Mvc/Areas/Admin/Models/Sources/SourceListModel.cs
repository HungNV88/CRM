using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Sources;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Sources
{
    public class SourceListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<SourceInfo> Rows { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.SourceTypes;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.SourceTypes
{
    public class SourceTypeListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<SourceTypeInfo> Rows { get; set; }
    }
}


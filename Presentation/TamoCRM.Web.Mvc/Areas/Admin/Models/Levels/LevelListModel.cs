using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Levels;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Levels
{
    public class LevelListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<LevelInfo> Rows { get; set; }
    }
}


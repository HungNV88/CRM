using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.EducationLevels;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.EducationLevels
{
    public class EducationLevelListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<EducationLevelInfo> Rows { get; set; }
    }
}


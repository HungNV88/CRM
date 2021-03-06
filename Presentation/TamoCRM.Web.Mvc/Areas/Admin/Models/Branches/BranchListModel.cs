using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Branches;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Branches
{
    public class BranchListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public int UserData { get; set; }
        public IEnumerable<BranchModel> Rows { get; set; }
    }
}


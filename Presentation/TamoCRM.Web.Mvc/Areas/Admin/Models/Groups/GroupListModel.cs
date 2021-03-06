using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Groups;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Groups
{
    public class GroupListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<GroupInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}


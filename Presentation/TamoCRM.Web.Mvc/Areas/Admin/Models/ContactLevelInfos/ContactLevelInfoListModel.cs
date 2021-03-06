using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.ContactLevelInfos;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos
{
    public class ContactLevelInfoListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<ContactLevelInfo> Rows { get; set; }
    }
}


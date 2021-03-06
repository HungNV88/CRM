using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.ContactTmps;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactTmps
{
    public class ContactTmpListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<ContactTmpInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}


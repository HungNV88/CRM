using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Phones;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Phones
{
    public class PhonListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<PhoneInfo> Rows { get; set; }
    }
}


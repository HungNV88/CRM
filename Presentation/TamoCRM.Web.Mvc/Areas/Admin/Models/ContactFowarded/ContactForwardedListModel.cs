using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFowarded
{
    public class ContactForwardedListModel
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
        public int UserData { get; set; }
        public IEnumerable<ContactForwardedModel> Rows { get; set; }
    }
}
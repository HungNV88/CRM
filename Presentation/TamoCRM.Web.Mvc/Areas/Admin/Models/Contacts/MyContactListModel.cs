using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class MyContactListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        //Total Pages
        public int Total { get; set; }
        public IEnumerable<ContactModel> Rows { get; set; }
        public int UserData { get; set; }
    }
}
using System.Collections.Generic;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        //Total Pages
        public int Total { get; set; }
        public int TotalMoney { get; set; }
        public int UserData { get; set; }
        public IEnumerable<ContactInfo> Rows { get; set; }
    }
}


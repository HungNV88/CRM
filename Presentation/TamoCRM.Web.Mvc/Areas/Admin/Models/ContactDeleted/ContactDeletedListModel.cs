using System.Collections.Generic;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactDeleted
{
    public class ContactDeletedListModel
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
        public int UserData { get; set; }
        public IEnumerable<ContactDeletedModel> Rows { get; set; }
    }
}


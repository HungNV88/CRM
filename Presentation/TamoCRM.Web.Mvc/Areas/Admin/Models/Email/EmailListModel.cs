using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.EmailInfo;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Email
{
    public class EmailListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<EmailInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}
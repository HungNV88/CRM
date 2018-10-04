using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC55Model
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DuplicateInfo { get; set; }
        public DateTime? CallConsultantDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
    }
}
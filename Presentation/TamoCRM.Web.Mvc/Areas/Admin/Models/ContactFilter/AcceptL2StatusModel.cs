using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFilter
{
    public class AcceptL2StatusModel
    {
        public int Status { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Fullname { get; set; }
        public string StatusText { get; set; }
    }
}
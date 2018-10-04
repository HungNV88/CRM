using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Users
{
    public class PhoneSettingModel
    {
        public int UserID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
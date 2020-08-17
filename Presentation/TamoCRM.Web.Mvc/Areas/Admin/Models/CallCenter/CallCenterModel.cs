using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.CallCenter;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter
{
    public class CallCenterModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UseFor { get; set; }
        public string EndPoint { get; set; }
        public string Token { get; set; }
        public int Port { get; set; }
    }
}
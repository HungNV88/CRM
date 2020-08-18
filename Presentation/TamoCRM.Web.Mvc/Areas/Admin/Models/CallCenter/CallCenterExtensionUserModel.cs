using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.CallCenter;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter
{
    public class CallCenterExtensionUserModel
    {
        public int ExtensionId { get; set; }
        public int UserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.CallCenter;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter
{
    public class CallCenterExtensionModel
    {
        public int CallCenterId { get; set; }
        public int Extension { get; set; }
    }
}
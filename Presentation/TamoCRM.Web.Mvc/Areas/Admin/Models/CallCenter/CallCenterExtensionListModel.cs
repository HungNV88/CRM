
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TamoCRM.Domain.CallCenter; 
namespace TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter
{
    public class CallCenterExtensionListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<CallCenterExtensionInfo> Rows { get; set; }
    }
}


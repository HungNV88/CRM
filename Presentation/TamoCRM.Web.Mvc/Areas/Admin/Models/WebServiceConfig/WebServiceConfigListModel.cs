using System.Collections.Generic;
using TamoCRM.Domain.WebServiceConfig;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.WebServiceConfig
{
    public class WebServiceConfigListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public int UserData { get; set; }
        public IEnumerable<WebServiceConfigInfo> Rows { get; set; }
    }
}


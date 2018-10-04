using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.Packge
{
    public class PackgeInfo
    {
        public string status { get; set; }
        public string status_code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

    public class PackgeInfoDetail
    {
        public string cat_code { get; set; }
        public string package_name { get; set; }
        public string package_parent { get; set; }
    }

}

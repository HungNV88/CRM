using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFilter
{
    public class ChannelWithContactCountModel
    {
        public int ChannelId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int L1ContactCount { get; set; }
    }
}
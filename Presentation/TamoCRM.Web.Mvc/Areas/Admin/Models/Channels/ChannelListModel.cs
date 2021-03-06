using System.Collections.Generic;
using TamoCRM.Domain.Channels;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Channels
{
    public class ChannelListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<ChannelInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}


using System.Collections.Generic;
using TamoCRM.Domain.SourceTypes;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class GoFilterSearchModel
    {
        public int? SchoolId { get; set; }
        public int? ChannelId { get; set; }        
        public int? Page { get; set; }
        //Page size params
        public int? Rows { get; set; }
    }
    public class GoFilterModel
    {
        //public PostedChannels PostedChannels { get; set; }
        //public PostedSchools PostedSchools { get; set; }
        //public PostedLevels PostedLevels { get; set; }
        public PostedSourceTypes PostedSourceTypes { get; set; }
        public IEnumerable<SourceTypeInfo> AllSourceTypes { get; set; }
        public IEnumerable<SourceTypeInfo> SelectedSourceTypes { get; set; }
        public int BranchId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Total { get; set; }

    }
    public class PostedSourceTypes
    {
        public int[] SourceTypeIds { get; set; }
    }
    public class PostedChannels
    {
        public int[] ChannelIds { get; set; }
    }
    public class PostedSchools
    {
        public int[] SchoolIds { get; set; }
    }
    public class PostedLevels
    {
        public int[] LevelIds { get; set; }
    }
}

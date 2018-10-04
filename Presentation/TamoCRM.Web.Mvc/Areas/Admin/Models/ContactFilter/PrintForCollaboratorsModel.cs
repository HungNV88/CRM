namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFilter
{
    public class PrintForCollaboratorsModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int MaxRowsPerPage { get; set; }
        //public IEnumerable<ChannelInfo> AllChannelInfo { get; set; } 
        //public IEnumerable<ChannelInfo> SelectedChannelInfo { get; set; } 
    }
    //public class PostedChannels
    //{
    //    public int[] ChannelIds { get; set; }
    //}
}
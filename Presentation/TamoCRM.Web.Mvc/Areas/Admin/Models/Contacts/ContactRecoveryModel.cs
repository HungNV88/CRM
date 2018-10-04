using System.Collections.Generic;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactRecoveryModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        //Total Pages
        public int Total { get; set; }
        public int UserData { get; set; }
        public IEnumerable<ContactRecoveryInfo> Rows { get; set; }
    }

    public class ContactRecoveryInfo
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string SchoolName { get; set; }
        public string MajorName { get; set; }
        public string LocationName { get; set; }
        public string Level { get; set; }
        public string StatusCare { get; set; }
        public string StatusMap { get; set; }
        public string Notes { get; set; }
        public string SourceTypeName { get; set; }
        public string ChannelName { get; set; }
        public string RegisteredDate { get; set; }
        public string School { get; set; }
        public string SchoolCare { get; set; }
        public string UserName { get; set; }
        public string HandoverDate { get; set; }
    }
}
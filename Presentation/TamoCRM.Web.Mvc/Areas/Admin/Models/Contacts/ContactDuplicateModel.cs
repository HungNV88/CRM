using System;
using System.Collections.Generic;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactDuplicateListModel
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
        public int UserData { get; set; }
        public IEnumerable<ContactDuplicateModel> Rows { get; set; }
    }

    public class ContactDuplicateModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Fullname { get; set; }
        public string TypeName { get; set; }
        public string UserName { get; set; }
        public string CallInfo { get; set; }
        public string LevelName { get; set; }
        public string StatusName { get; set; }
        public string ChannelName { get; set; }
        public string DuplicateInfo { get; set; }
        public string StatusMapName { get; set; }
        public string StatusCareName { get; set; }
        public DateTime ImportedDate { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentAmPm { get; set; }
        public string AppointmentTime { get; set; }
    }
}
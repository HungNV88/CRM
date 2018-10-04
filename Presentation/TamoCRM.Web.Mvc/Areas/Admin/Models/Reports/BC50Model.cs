using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC50Model
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public string CallInfoCollaborator { get; set; }
        public DateTime? HandoverCollaboratorDate { get; set; }
        public DateTime? CallCollaboratorDate { get; set; }
        public string StatusCareCollaborator { get; set; }
        public string UserCollaborator { get; set; }
        public int CallCount { get; set; }
    }
}
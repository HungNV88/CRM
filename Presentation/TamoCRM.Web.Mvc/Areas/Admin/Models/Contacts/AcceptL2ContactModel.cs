using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class AcceptL2ContactModel
    {
        public string Fullname { get; set; }
        public string Mobile1 { get; set; }
        public int EducationLevelId { get; set; }
        public string SchoolCode { get; set; }
        public string MajorCode { get; set; }
        public string EducationNotes { get; set; }
        public string CollaboratorName { get; set; }
        public string CalledDate { get; set; }
    }
}

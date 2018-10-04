using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactTmps
{
    public class ContactTmpModel
    {
        public int ContactId { get; set; }

        public int ImportId { get; set; }

        public string StudentId { get; set; }

        public string Fullname { get; set; }

        public string Birth { get; set; }

        public string Mobil1 { get; set; }

        public string Mobil2 { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string RegisteredSchool { get; set; }

        public string RegisterdMajor { get; set; }

        public string Question { get; set; }

        public string EnrollmentMark { get; set; }

        public string EducationalLevel { get; set; }

        public string Location { get; set; }

        public int ErrorStatus { get; set; }
    }
}
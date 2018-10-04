using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC43Model
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Channel { get; set; }
        public string Campaign { get; set; }
        public string LandingPage { get; set; }
        public string Address { get; set; }
        public string Level { get; set; }
        public string StatusCare { get; set; }
        public string StatusMap { get; set; }
        public string Notes { get; set; }
        public DateTime? HadoverDate { get; set; }
        public string Consultant { get; set; }
        public string Acceptance { get; set; }
        public string TemplateAds { get; set; }
        public string SearchKeyword { get; set; }
        public string Source { get; set; }
        public DateTime? RegisterDate { get; set; }
        public int CallCount { get; set; }
    }
}
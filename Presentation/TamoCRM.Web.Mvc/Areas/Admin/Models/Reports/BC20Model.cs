using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC20Model
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public string Campaind { get; set; }
        public string LandingPage { get; set; }
        public string Channel { get; set; }
        public string TemplateAds { get; set; }
        public string SearchKeyword { get; set; }
        public string PackageName { get; set; }
        public string ContactId { get; set; }
        public DateTime? HandoverConsultantDate { get; set; }
        public string Consultant { get; set; }
        public string SourceType { get; set; }
    }
}
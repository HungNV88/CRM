using System;
namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC48Model
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string StatusCare { get; set; }
        public string StatusMap { get; set; }
        public int LevelId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Channel { get; set; }
        public string TemplateAds { get; set; }
        public string SearchKeyword { get; set; }
        public string Notes { get; set; }
        public string Consultant { get; set; }
        public DateTime? HandoverCosulant { get; set; }
        public int CallCount { get; set; }
        public string Acceptance { get; set; }
        public string CallInfoConsultant { get; set; }
        public DateTime? CallConsulatDate { get; set; }
    }
}


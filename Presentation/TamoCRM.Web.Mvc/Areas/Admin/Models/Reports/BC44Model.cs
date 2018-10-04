using System;
namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC44Model
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string Mobile { get; set; }
        public DateTime? AppointmentConsultantDate { get; set; }
        public string CallInfoConsultant { get; set; }
        public DateTime? CallConsultantDate { get; set; }
        public string StatusCare { get; set; }
        public string Consultant { get; set; }
        public string Product { get; set; }
        public int CallCount { get; set; }
        public DateTime? HandoverConsulantDate { get; set; }

      

    }
}
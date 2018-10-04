using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFowarded
{
    public class ContactForwardedModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string StatusCare { get; set; }
        public string ReceivedPerson { get; set; }
        public string ForwardPerson { get; set; }
        public string ForwardedPerson { get; set; }
        public DateTime ForwardDate { get; set; }
    }
}
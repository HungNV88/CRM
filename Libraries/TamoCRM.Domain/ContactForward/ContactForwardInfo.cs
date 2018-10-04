using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.ContactForward
{
    public class ContactForwardInfo
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string StatusCare { get; set; }
        public string ReceivedPerson { get; set; }
        public string ForwardedPerson { get; set; }
        public string ForwardPerson { get; set; }
        public DateTime ForwardDate { get; set; }
    }
}

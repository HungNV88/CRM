using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.CC3Report
{
    public class CC3ReportInfo
    {
        public string FullName {get; set;}
        public string PhoneNumber {get; set;}
        public string Email {get; set;}
        public int Level {get; set;}
        public string CallInfoCollaborator { get; set; }
        public DateTime? HandoverCollaboratorDate { get; set; }
        public DateTime? CallCollaboratorDate { get; set; }
        public int StatusCareCollaboratorId { get; set; }
        public int UserCollaboratorId { get; set; }
        public int CallCount { get; set; }
    }
}

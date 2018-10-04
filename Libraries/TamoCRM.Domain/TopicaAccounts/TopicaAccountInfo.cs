using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.TopicaAccounts
{
    public class TopicaAccountInfo
    {
        public int Id {get; set;}
        public int ContactId { get; set; }
        public int CreatedBy { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusTopicaAccountId { get; set; }
        public string StatusString { get; set; }
        public string DateTimeString { get; set; }
        public string CreatedDateString { get; set; }
        public string username {get; set;}
        public string password { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string confirmed { get; set; }
        public string mnethostid { get; set; }
    }

    public class TopicaCasec
    {
        public int TopicaCount { get; set; }
        public int CasecCount { get; set; }
    }
}

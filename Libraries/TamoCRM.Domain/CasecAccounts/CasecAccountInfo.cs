using System;

namespace TamoCRM.Domain.CasecAccounts
{
    public class CasecAccountInfo
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int CreatedBy { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusCasecAccountId { get; set; }

        public string StatusString { get; set; }
        public string DateTimeString { get; set; }
        public string CreatedDateString { get; set; }
    }
}


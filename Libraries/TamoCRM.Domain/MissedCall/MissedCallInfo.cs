using System;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Domain.MissedCall
{
    public class MissedCallInfo
    {
        public MissedCallInfo()
        {
            Amount = 1;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public int ContactId { get; set; }
        public string OldCallId { get; set; }
        public string AgentCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? MissedCallTime { get; set; }
        public int Amount { get; set; }
        public string StringMissedCallTime
        {
            get { return ObjectHelper.StandardDatetimeToString(MissedCallTime); }
        }
    }
}


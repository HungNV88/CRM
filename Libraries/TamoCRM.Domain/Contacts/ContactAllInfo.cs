using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.MissedCall;

namespace TamoCRM.Domain.Contacts
{
    public class ContactAllInfo
    {
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public MissedCallInfo MissedCallInfo { get; set; }
        public CallHistoryInfo CallHistoryInfo { get; set; }
        public ContactLevelInfo ContactLevelInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Domain.ContactDeleted
{
    public class ContactDeletedInfo 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Json { get; set; }
        public int BranchId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ContactId { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedTime { get; set; }
    }

    public class ContactDeletedJson
    {
        public ContactInfo ContactInfo { get; set; }
        public ContactLevelInfo ContactLevelInfo { get; set; }
        public List<CallHistoryInfo> CallHistories { get; set; }
    }
}


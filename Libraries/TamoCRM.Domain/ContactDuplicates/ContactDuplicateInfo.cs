using System;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Domain.ContactDuplicates
{
    public class ContactDuplicateInfo
    {
        public int Id { get; set; }

		public int ContactId { get;set; }

        public int SourceTypeId { get; set; }

        public int Status { get; set; }

        public int ImportId { get; set; }

        public int LevelId { get; set; }
        public string DuplicateInfo { get; set; }
        public bool IsNotyfiDuplicate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public ContactLevelInfo ContactLevelInfo { get; set; }
    }
}


using System;

namespace TamoCRM.Domain.SourceTypes
{
    public class SourceTypeInfo
    {
		public string Code { get;set; }
		public string Name { get;set; }
        public int ChangedBy { get; set; }
        public int CreatedBy { get; set; }
        public int SourceTypeId { get; set; }
        public bool IsShowHotLine { get; set; }
        public string Description { get; set; }
        public bool IsCheckUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public bool IsCheckDuplicate { get; set; }
        public bool IsShowConsultant { get; set; }
    }
}


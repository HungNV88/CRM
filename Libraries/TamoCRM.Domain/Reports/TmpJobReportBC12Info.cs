using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC12Info 
    {
        public int BranchId { get; set; }
        public int ImportId { get; set; }
        public int ContactId { get; set; }
        public int ChannelId { get; set; }
        public int CreatedBy { get; set; }
        public int StatusMapId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? HandoverDate { get; set; }
        public DateTime? RecoveryDate { get; set; }
    }
}

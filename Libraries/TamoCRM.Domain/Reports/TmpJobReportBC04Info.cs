using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC04Info
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int UserConsultantId { get; set; }
        public int LevelId { get; set; }
        public int MajorId { get; set; }
        public int BranchId { get; set; }
        public int SchoolId { get; set; }
        public int HandoverCount { get; set; }
        public int CompleteInCount { get; set; }
        public int CompleteOutCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}

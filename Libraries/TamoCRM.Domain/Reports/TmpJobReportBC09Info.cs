using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC09Info
    {
        public int Amount { get; set; }
        public int BranchId { get; set; }
        public int ImportId { get; set; }
        public int SourceTypeId { get; set; }
        public DateTime DateTime { get; set; }
        public int ConnectStatus { get; set; }
    }
}

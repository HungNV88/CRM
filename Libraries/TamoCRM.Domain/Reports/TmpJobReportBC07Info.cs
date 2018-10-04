using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC07Info
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int SchoolId { get; set; }
        public int HandoverCount { get; set; }
        public int KLLD { get; set; } 
		public int SaiSo { get; set; }
        public int KDDT { get; set; }
        public int KhacVung { get; set; }
        public int Trung { get; set; }
        public DateTime DateTime { get; set; }
    }
}

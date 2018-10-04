using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC11Info 
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public int CollaboratorId { get; set; }
        public DateTime CalledDate { get; set; }
        public int StatusMapDistributorId { get; set; }
    }
}

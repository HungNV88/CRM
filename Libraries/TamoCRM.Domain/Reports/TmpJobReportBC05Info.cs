using System;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC05Info
    {
        public int Id { get; set; }
        public int UserConsultantId { get; set; }
        public int BranchId { get; set; }
        public int ProductSellId { get; set; }
        public int HandoverCount { get; set; }
        public int KLLD { get; set; } 
		public int SaiSo { get; set; }
        public int KDDT { get; set; }
        public int KhacVung { get; set; }
        public int KNM { get; set; }
        public int ChuaGoi { get; set; }
        public int BGLS { get; set; }
        public DateTime DateTime { get; set; }

        //Them ngay 27/12/2016
        public int SoHotLine { get; set; }
    }
}

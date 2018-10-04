namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC08Model
    {
        public BC08Model()
        {
            L1 = 0;
            L2 = 0;
            Gia = 0;
            KNM = 0;
            KLLD = 0;
            Trung = 0;
            CaoHoc = 0;
            SinhVien = 0;
            SinhVien = 0;
            VungKhac = 0;
            NhamNguoi = 0;
            KhongNhuCau = 0;
            NotCompleteCount = 0;
        }
        public string Import { get; set; }
        public int L1 { get; set; }
        public int L2 { get; set; }
        public int KLLD { get; set; }
        public int KNM { get; set; }
        public int Trung { get; set; }
        public int CaoHoc { get; set; }
        public int Gia { get; set; }
        public int NhamNguoi { get; set; }
        public int SinhVien { get; set; }
        public int KhongNhuCau { get; set; }
        public int VungKhac { get; set; }
        public int NotCompleteCount { get; set; }
    }
}
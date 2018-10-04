using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.LogsMoney
{
    public class LogsMoneyInfo
    {
        public double TienHVNop { get; set; }
        public int KieuThanhToan { get; set; }
        public string sKieuThanhToan { get; set; }
        public DateTime? NgayThucThu { get; set; }
        public string sNgayThucThu { get; set; }
        public int DiaPhuong { get; set; }
        public int ChuDuToan { get; set; }
        public string NoteChuyenKhoan { get; set; }
        public int ContactId { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public string NguoiTao { get; set; }
        public double TienBanGoi { get; set; }
        public bool TrangThai { get; set; }
        public int HistoryId { get; set; }
        public int IdFixMoney { get; set; }
    }
}

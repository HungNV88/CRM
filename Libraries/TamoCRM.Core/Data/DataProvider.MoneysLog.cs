using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void MoneyLogs_Insert(double TienHVNop, double TienBanGoi, string NoteChuyenKhoan, string NguoiTao, DateTime? NgayThucThu, int KieuThanhToan, int DiaPhuong, DateTime CreateDate, int ContactId, string Code, int ChuDuToan, bool TrangThai, int HistoryId, int IdFixMoney, SqlTransaction tran);
        public abstract void MoneyLogs_Insert(double TienHVNop, double TienBanGoi, string NoteChuyenKhoan, string NguoiTao, DateTime? NgayThucThu, int KieuThanhToan, int DiaPhuong, DateTime CreateDate, int ContactId, string Code, int ChuDuToan, bool TrangThai, int HistoryId);
        public abstract IDataReader MoneyLogs_GetAllByContactId(int ContactId);
        public abstract void MoneyLogs_Update(bool trangthai);
        public abstract string MoneyLogs_GetNoteLogMoney(int contactid);
    }
}

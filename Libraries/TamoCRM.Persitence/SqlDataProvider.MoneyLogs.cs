using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void MoneyLogs_Insert(double TienHVNop, double TienBanGoi, string NoteChuyenKhoan, string NguoiTao, DateTime? NgayThucThu, int KieuThanhToan, int DiaPhuong, DateTime CreateDate, int ContactId, string Code, int ChuDuToan, bool TrangThai, int HistoryId,int IdFixMoney, SqlTransaction tran)
        {
            SqlHelper.ExecuteNonQuery(tran, GetFullyQualifiedName("MoneyLogs_Insert"), TienHVNop, TienBanGoi, NoteChuyenKhoan, NguoiTao, NgayThucThu, KieuThanhToan, DiaPhuong, CreateDate, ContactId, Code, ChuDuToan, TrangThai, HistoryId, IdFixMoney);
        }
        public override void MoneyLogs_Insert(double TienHVNop, double TienBanGoi, string NoteChuyenKhoan, string NguoiTao, DateTime? NgayThucThu, int KieuThanhToan, int DiaPhuong, DateTime CreateDate, int ContactId, string Code, int ChuDuToan, bool TrangThai, int HistoryId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("MoneyLogs_Insert"), TienHVNop, TienBanGoi, NoteChuyenKhoan, NguoiTao, NgayThucThu, KieuThanhToan, DiaPhuong, CreateDate, ContactId, Code, ChuDuToan, TrangThai, HistoryId);
        }

        public override IDataReader MoneyLogs_GetAllByContactId(int contactid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("MoneyLogs_GetAllByContactId"), contactid);
        }

        public override void MoneyLogs_Update(bool trangthai)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("MoneyLogs_UpdateTrangThai"), trangthai);
        }

        public override string MoneyLogs_GetNoteLogMoney(int contactid)
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("MoneyLogs_GetNoteLogMoney"), contactid);
        }
    }
}

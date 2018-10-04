using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Services.MoneyLogs;

namespace TamoCRM.Services.MoneyLogs
{
    public class MoneyLogsRepository
    {
        public static void Create(LogsMoneyInfo info, SqlTransaction tran)
        {
            DataProvider.Instance().MoneyLogs_Insert(info.TienHVNop,info.TienBanGoi,info.NoteChuyenKhoan,info.NguoiTao,info.NgayThucThu,info.KieuThanhToan,info.DiaPhuong,info.CreateDate,info.ContactId,info.Code,info.ChuDuToan,info.TrangThai,info.HistoryId,info.IdFixMoney,tran);
        }
        public static void Create(LogsMoneyInfo info)
        {
            DataProvider.Instance().MoneyLogs_Insert(info.TienHVNop, info.TienBanGoi, info.NoteChuyenKhoan, info.NguoiTao, info.NgayThucThu, info.KieuThanhToan, info.DiaPhuong, info.CreateDate, info.ContactId, info.Code, info.ChuDuToan, info.TrangThai, info.HistoryId);
        }
        public static List<LogsMoneyInfo> GetAllByContactId(int contactId)
        {
            return ObjectHelper.FillCollection<LogsMoneyInfo>(DataProvider.Instance().MoneyLogs_GetAllByContactId(contactId));
        }
        public static string GetNoteLogMoney(int contactid)
        {
            return DataProvider.Instance().MoneyLogs_GetNoteLogMoney(contactid);
        }

        public static void UpdateTrangThai(bool trangThai)
        {
            DataProvider.Instance().MoneyLogs_Update(trangThai);
        }

    }
}

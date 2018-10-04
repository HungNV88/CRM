using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        //Dat check point log load trang
        public abstract int GetSessionLog();
        public abstract IDataReader GetReportLogDashBoard_chamsoccontact(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_lichlamviectvts(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_timkiemcontacttvts(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_danhsachcontacttvts(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_callcontacttvts(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_callhistory(DateTime datetime);
        public abstract IDataReader GetReportLogDashBoard_thongkecuocgoi(DateTime datetime, int groupId, int userId);


        // báo cáo chất lượng contact
        public abstract IDataReader ReportQuality_Contact(DateTime from, DateTime to);
        // báo cáo nghiệm thu contact lọc
        public abstract IDataReader ReportFilter_Acceptance(DateTime from, DateTime to);
        public abstract IDataReader TmpJobReportBC04_GetAll(DateTime from, DateTime to);

        //Bao cao khoi luong cong viec ver 2.0
        public abstract IDataReader TmpJobReportBC53_GetAll(DateTime from, DateTime to);
        public abstract IDataReader TmpJobReportBC05_GetAll(DateTime FromDate, DateTime ToDate);
        public abstract IDataReader TmpJobReportBC07_GetAll(DateTime dateTime);
        public abstract IDataReader TmpJobReportBC08_GetAll(DateTime dateTime);
        public abstract IDataReader TmpJobReportBC300_GetAll(DateTime dateTime);
        public abstract IDataReader TmpJobReportBC300_GetAll(DateTime dateTime, int statusType);
        public abstract IDataReader TmpJobReportBC54_GetAll(DateTime dateTime, int statusType);
        public abstract IDataReader TmpJobReportBC09_GetAll(int branchId, int sourceTypeId, int userId, DateTime? startDate, DateTime? endDate);
        public abstract IDataReader TmpJobReportBC10_GetAll(int branchId, int sourceTypeId, int userId, int collaboratorId, DateTime? consultantStartDate, DateTime? consultantEndDate, DateTime? collaboratorStartDate, DateTime? collaboratorEndDate);
        public abstract IDataReader TmpJobReportBC11_GetAll(int branchId, int collaboratorId, DateTime? startDate, DateTime? endDate);
        public abstract IDataReader TmpJobReportBC12_GetAll(int branchId, int importId, int channelId, DateTime? handoverStartDate, DateTime? handoverEndDate, DateTime? recoveryStartDate, DateTime? recoveryEndDate);
        public abstract void TmpJobReportBC04_Create(DateTime dateTime);
        public abstract void TmpJobReportBC05_Create(DateTime dateTime);
        public abstract void TmpJobReportBC300_Create(DateTime dateTime);
        // phulv
        public abstract IDataReader TmpJobReportBC300_Get_Contacts_Arise(DateTime fromdate,DateTime todate);

        public abstract IDataReader TmpJobReportImportExport(DateTime? reportDate, int typeReport);
        public abstract IDataReader TmpJobReportImportExport(DateTime? reportDateFrom, DateTime? reportDateTo, string channels);
    }
}

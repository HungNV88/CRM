using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int GetSessionLog()
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("GetSessionLog"));
        }
        public override IDataReader GetReportLogDashBoard_chamsoccontact(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_formchamsoctvts"), datetime);
        }
        public override IDataReader GetReportLogDashBoard_lichlamviectvts(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_lichlamviectvts"), datetime);
        }
        public override IDataReader GetReportLogDashBoard_timkiemcontacttvts(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_timkiemcontacttvts"), datetime);
        }
        public override IDataReader GetReportLogDashBoard_danhsachcontacttvts(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_danhsachcontacttvts"), datetime);
        }

        public override IDataReader GetReportLogDashBoard_callcontacttvts(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_callcontacttvts"), datetime);
        }

        public override IDataReader GetReportLogDashBoard_callhistory(DateTime datetime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_CallHistory"), datetime);
        }

        public override IDataReader GetReportLogDashBoard_thongkecuocgoi(DateTime datetime, int groupId, int userId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetReportLogDashBoard_thongkecuocgoi"), datetime, groupId, userId);
        }
        
        // báo cáo tổng hợp chất lượng contact
        public override IDataReader ReportQuality_Contact(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ReportQuality_Contact"), from, to);
        }
        //báo cáo nghiệm thu contact lọc
        public override IDataReader ReportFilter_Acceptance(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ReportFilter_Acceptance"), from, to);
        }
        public override IDataReader TmpJobReportBC04_GetAll(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC04_GetAll"), from, to);
        }

        public override IDataReader TmpJobReportBC05_GetAll(DateTime FromDate, DateTime ToDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpReport_View_Bc05"), FromDate, ToDate);
        }

        public override IDataReader TmpJobReportBC07_GetAll(DateTime dateTime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpReport_View_Bc07"), dateTime);
        }

        public override IDataReader TmpJobReportBC08_GetAll(DateTime dateTime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpReport_View_Bc08"), dateTime);
        }

        public override IDataReader TmpJobReportBC300_GetAll(DateTime dateTime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC300_GetAll"), dateTime);
        }

        public override IDataReader TmpJobReportBC300_GetAll(DateTime dateTime, int statusType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC300_GetAll_Ver2"), dateTime, statusType);
        }

        public override IDataReader TmpJobReportBC54_GetAll(DateTime dateTime,int statusType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC54_GetAll"), dateTime);
        }

        public override IDataReader TmpJobReportBC09_GetAll(int branchId, int sourceTypeId, int userId, DateTime? startDate, DateTime? endDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC09_GetAll"), branchId, sourceTypeId, userId, startDate, endDate);
        }

        public override IDataReader TmpJobReportBC10_GetAll(int branchId, int sourceTypeId, int userId, int collaboratorId, DateTime? consultantStartDate, DateTime? consultantEndDate, DateTime? collaboratorStartDate, DateTime? collaboratorEndDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC10_GetAll"), branchId, sourceTypeId, userId, collaboratorId, consultantStartDate, consultantEndDate, collaboratorStartDate, collaboratorEndDate);
        }

        public override IDataReader TmpJobReportBC11_GetAll(int branchId, int collaboratorId, DateTime? startDate, DateTime? endDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC11_GetAll"), branchId, collaboratorId, startDate, endDate);
        }

        public override IDataReader TmpJobReportBC12_GetAll(int branchId, int importId, int channelId, DateTime? handoverStartDate, DateTime? handoverEndDate, DateTime? recoveryStartDate, DateTime? recoveryEndDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("RecoveryDistributors_Filter"), branchId, importId, channelId, handoverStartDate, handoverEndDate, recoveryStartDate, recoveryEndDate);
        }

        public override void TmpJobReportBC04_Create(DateTime dateTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TmpReport_Create_Bc04"), dateTime);
        }

        public override void TmpJobReportBC05_Create(DateTime dateTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TmpReport_Create_Bc05"), dateTime);
        }

        public override void TmpJobReportBC300_Create(DateTime dateTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TmpReport_Create_Bc300"), dateTime);
        }
        //phulv
        public override IDataReader TmpJobReportBC300_Get_Contacts_Arise(DateTime fromdate, DateTime todate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CONTACTS_ARISE"), fromdate, todate);
        }

        public override IDataReader TmpJobReportBC53_GetAll(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportBC53_GetAll"), from, to);
        }

        public override IDataReader TmpJobReportImportExport(DateTime? reportDate, int typeReport)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportExportImportContactMOL_GetAll"), reportDate);
        }

        public override IDataReader TmpJobReportImportExport(DateTime? reportDateFrom, DateTime? reportDateTo, string channels)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TmpJobReportExportImport_GetFromDateToDate"), reportDateFrom, reportDateTo, channels);
        }
    }
}


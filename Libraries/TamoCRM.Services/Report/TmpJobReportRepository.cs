using System;
using System.Collections.Generic;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ReportFilter;
using TamoCRM.Domain.Reports;

namespace TamoCRM.Services.Report
{
    public class TmpJobReportRepository
    {
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_chamsoccontact(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_chamsoccontact(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_lichlamviectvts(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_lichlamviectvts(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_timkiemcontacttvts(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_timkiemcontacttvts(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_danhsachcontacttvts(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_danhsachcontacttvts(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_callcontacttvts(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_callcontacttvts(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_callhistory(DateTime datetime)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_callhistory(datetime));
        }
        public static List<ReportLogDashBoardInfo> GetReportLogDashBoard_thongkecuocgoi(DateTime datetime, int groupId, int userId)
        {
            return ObjectHelper.FillCollection<ReportLogDashBoardInfo>(DataProvider.Instance().GetReportLogDashBoard_thongkecuocgoi(datetime , groupId, userId));
        }

        public static List<ReportFilter> ReportQuality_Contact(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ReportFilter>(DataProvider.Instance().ReportQuality_Contact(from, to));
        }
        public static List<ReportFilter> ReportFilter_Acceptance(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ReportFilter>(DataProvider.Instance().ReportFilter_Acceptance(from, to));
        }
        public static List<TmpJobReportBC04Info> GetAllBC04(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC04Info>(DataProvider.Instance().TmpJobReportBC04_GetAll(from, to));
        }

        public static List<TmpJobReportBC53> GetAllBC53(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC53>(DataProvider.Instance().TmpJobReportBC53_GetAll(from, to)); 
        }

        public static List<TmpJobReportBC05Info> GetAllBC05(DateTime FromDate, DateTime ToDate)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC05Info>(DataProvider.Instance().TmpJobReportBC05_GetAll(FromDate, ToDate));
        }


        public static List<TmpJobReportBC07Info> GetAllBC07(DateTime dateTime)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC07Info>(DataProvider.Instance().TmpJobReportBC07_GetAll(dateTime));
        }

        public static List<TmpJobReportBC08Info> GetAllBC08(DateTime dateTime)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC08Info>(DataProvider.Instance().TmpJobReportBC08_GetAll(dateTime));
        }

        public static List<TmpJobReportBC09Info> GetAllBC09(int branchId, int sourceTypeId, int userId, DateTime? startDate, DateTime? endDate)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC09Info>(DataProvider.Instance().TmpJobReportBC09_GetAll(branchId, sourceTypeId, userId, startDate, endDate));
        }

        public static List<TmpJobReportBC10Info> GetAllBC10(int branchId, int sourceTypeId, int userId, int collaboratorId, DateTime? consultantStartDate, DateTime? consultantEndDate, DateTime? collaboratorStartDate, DateTime? collaboratorEndDate)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC10Info>(DataProvider.Instance().TmpJobReportBC10_GetAll(branchId, sourceTypeId, userId, collaboratorId, consultantStartDate, consultantEndDate, collaboratorStartDate, collaboratorEndDate));
        }

        public static List<TmpJobReportBC11Info> GetAllBC11(int branchId, int collaboratorId, DateTime? startDate, DateTime? endDate)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC11Info>(DataProvider.Instance().TmpJobReportBC11_GetAll(branchId, collaboratorId, startDate, endDate));
        }

        public static List<TmpJobReportBC12Info> GetAllBC12(int branchId, int importId, int channelId, DateTime? handoverStartDate, DateTime? handoverEndDate, DateTime? recoveryStartDate, DateTime? recoveryEndDate)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC12Info>(DataProvider.Instance().TmpJobReportBC12_GetAll(branchId, importId, channelId, handoverStartDate, handoverEndDate, recoveryStartDate, recoveryEndDate));
        }

        public static List<TmpJobReportBC300Info> GetAllBC300(DateTime dateTime)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC300Info>(DataProvider.Instance().TmpJobReportBC300_GetAll(dateTime));
        }

        public static List<TmpJobReportBC300Info> GetAllBC300(DateTime dateTime, int statusType)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC300Info>(DataProvider.Instance().TmpJobReportBC300_GetAll(dateTime, statusType));
        }

        public static List<TmpJobReportBC300Info> GetAllBC54(DateTime dateTime, int statusType)
        {
            return ObjectHelper.FillCollection<TmpJobReportBC300Info>(DataProvider.Instance().TmpJobReportBC54_GetAll(dateTime, statusType));
        }


        public static void CreateBC04(DateTime dateTime)
        {
            DataProvider.Instance().TmpJobReportBC04_Create(dateTime);
        }

        public static void CreateBC05(DateTime dateTime)
        {
            DataProvider.Instance().TmpJobReportBC05_Create(dateTime);
        }

        public static void CreateBC300(DateTime dateTime)
        {
            DataProvider.Instance().TmpJobReportBC300_Create(dateTime);
        }

        // phulv
        public static List<TmpJobReportContactAriseBC300Info> GetAll_Contacts_Arise(DateTime fromdate, DateTime todate)
        {
            return ObjectHelper.FillCollection<TmpJobReportContactAriseBC300Info>(DataProvider.Instance().TmpJobReportBC300_Get_Contacts_Arise(fromdate, todate));
        }

        public static List<TmpJobReportImportExportInfo> GetAllInfoImportExport(DateTime? reportDate, int typeReport)
        {
            return ObjectHelper.FillCollection<TmpJobReportImportExportInfo>(DataProvider.Instance().TmpJobReportImportExport(reportDate, typeReport));
        }

        public static List<TmpJobReportImportExportInfo> GetAllInfoImportExport(DateTime? reportDateFrom, DateTime? reportDateTo, string channels)
        {
            return ObjectHelper.FillCollection<TmpJobReportImportExportInfo>(DataProvider.Instance().TmpJobReportImportExport(reportDateFrom, reportDateTo, channels));
        }

        public static int GetSessionLog()
        {
            return DataProvider.Instance().GetSessionLog();
        }

    }        
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Persitence;
using System.Data.Entity;
using System.Web;
using Microsoft.Reporting.WebForms;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Reports;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Services.Report;
using TamoCRM.Services.SourceTypes;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Reports;
using TamoCRM.Services.Channels;
using TamoCRM.Web.Mvc.Areas.Admin.WebApi.Users;
using System.Text.RegularExpressions;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.TemplateAd;
using Excel;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;
using TamoCRM.Web.Framework.Common;
using System.Diagnostics;
using TamoCRM.Services.Catalogs;
using TamoCRM.Domain.Groups;
using TamoCRM.Services.Groups;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{

    public class ReportController : AdminController
    {
        /// <summary>
        /// Báo cáo lọc trùng L0 L1
        /// </summary>
        public ActionResult BC01()
        {
            ViewBag.Collectors = CollectorRepository.GetAll();
            return View();
        }
        public ActionResult ShowBC01(int collectorId, string from, string to, int reportType)
        {
            var dtFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
            var collector = StoreData.ListCollector.FirstOrDefault(c => c.CollectorId == collectorId) ??
                            CollectorRepository.GetInfo(collectorId);
            var infomation = string.Empty;
            if (collector != null) infomation += "Người lấy: " + collector.Name + " - ";
            infomation += "Từ ngày: " + dtFrom.ToString("dd/MM/yyyy") + " - Đến ngày: " + dtTo.ToString("dd/MM/yyyy");

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC01.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            var lstImports = ImportExcelRepository.GetForBC01(collectorId, dtFrom, dtTo);
            var lstData = new List<BC01Model>();
            foreach (var info in lstImports)
            {
                var typeInfo = SourceTypeRepository.GetInfo(info.TypeId);
                var objModel = new BC01Model
                {
                    L0A = info.TotalRow,
                    Duplicate = info.DuplicateCount,
                    L0B = info.TotalRow - info.InternalDuplicateCount,
                    SourceType = typeInfo != null ? typeInfo.Code : "Không xác định",
                    Filename = info.FilePath.Substring(info.FilePath.LastIndexOf("\\") + 1),
                    L1 = info.TotalRow - info.ErrorCount - info.DuplicateCount - info.InternalDuplicateCount
                };
                objModel.L1Percentage = objModel.L0A > 0 ? (objModel.L1 * 100 / objModel.L0A) + "%" : "0%";
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC01", lstData);
            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType             
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx             
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.5in</MarginBottom>" +
                                      "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            string type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }

            //Render the report             
            var renderedBytes = localReport.Render(
                type,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            //Render the report             

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC01_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }

            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo tỷ lệ L5B/L2 và L8/L2
        /// </summary>
        public ActionResult BC02()
        {
            var dayReportTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(DayReportType)))
                dayReportTypes.Add((int)item, ObjectExtensions.GetEnumDescription((DayReportType)item));
            ViewBag.DayReportTypes = new SelectList(dayReportTypes, "Key", "Value");
            return View();
        }
        public ActionResult ShowBC02(int dayReportType, int groupId, int userId, int schoolId, int majorId, int branchId, int sourceTypeId, string date, int lumpedId, int latchId, string hFromDate, string hToDate)
        {
            // Param datetime
            var dtTo = hToDate.ToDateTime() ?? DateTime.Now;
            var dtFrom = hFromDate.ToDateTime() ?? DateTime.Now;

            var listUser = StoreData.ListUser.AsReadOnly();
            if (groupId > 0) listUser = listUser.Where(c => c.GroupId == groupId).ToList().AsReadOnly();

            var infomation = "Ngày bàn giao từ ngày: {0} - đến ngày: {1}";
            switch ((DayReportType)dayReportType)
            {
                case DayReportType.Day:
                    dtTo = hToDate.ToDateTime() ?? DateTime.Now.Date;
                    dtFrom = hFromDate.ToDateTime() ?? DateTime.Now.Date;
                    break;
                case DayReportType.Week:
                    dtTo = dtFrom.AddDays(7 - (int)dtFrom.DayOfWeek);
                    dtFrom = dtFrom.AddDays(-(int)dtFrom.DayOfWeek + 1);
                    break;
                case DayReportType.Month:
                    dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);
                    infomation = "Tháng: " + dtFrom.Month + " - " + infomation;
                    break;
                case DayReportType.Year:
                    dtTo = new DateTime(dtFrom.Year, 12, 31);
                    dtFrom = new DateTime(dtFrom.Year, 12, 1);
                    infomation = "Năm: " + dtFrom.Year + " - " + infomation;
                    break;
            }

            if (dtTo == DateTime.MinValue || dtTo > DateTime.Now.Date) dtTo = DateTime.Now.Date;
            if (dtFrom == DateTime.MinValue || dtFrom > DateTime.Now.Date) dtFrom = DateTime.Now.Date;
            var handoverToDate = dtTo;
            var handoverFromDate = dtFrom;
            handoverToDate = handoverToDate.AddDays(1).AddSeconds(-1);

            // Load & filter data
            var dateTime = date.ToDateTime() ?? DateTime.Now.Date;
            var lstData = TmpJobReportRepository.GetAllBC300(dateTime) ?? new List<TmpJobReportBC300Info>();
            if (userId > 0) lstData = lstData.Where(c => c.UserConsultantId == userId).ToList();
            if (majorId > 0) lstData = lstData.Where(c => c.MajorId == majorId).ToList();
            if (schoolId > 0) lstData = lstData.Where(c => c.SchoolId == schoolId).ToList();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();
            if (sourceTypeId > 0) lstData = lstData.Where(c => c.SourceTypeId == sourceTypeId).ToList();
            if (!hFromDate.IsStringNullOrEmpty() || !hToDate.IsStringNullOrEmpty())
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }
            else if ((DayReportType)dayReportType == DayReportType.Lumped || (DayReportType)dayReportType == DayReportType.Latch)
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }
            else infomation = "Ngày xem báo cáo: " + dateTime.ToString("dd/MM/yyyy");

            var sourceTypes = StoreData.ListSourceType
                .Where(c => !c.Name.IsStringNullOrEmpty())
                .ToList();
            var listDataSource = new List<BC02Model>();
            foreach (var id in lstData.Select(c => c.UserConsultantId).Distinct())
            {
                var user = listUser.FirstOrDefault(p => p.UserID == id);
                if (user == null) continue;

                foreach (var sourceType in sourceTypes)
                {
                    var entity = new BC02Model
                    {
                        Level2 = 0,
                        Level8 = 0,
                        Level5B = 0,
                        User = user.FullName,
                        Group = user.GroupName,
                        SourceType = sourceType.Code,
                        SourceTypeId = sourceType.SourceTypeId,
                    };
                    listDataSource.Add(entity);
                }
            }

            foreach (var item in lstData)
            {
                // User
                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.UserConsultantId) ?? UserRepository.GetInfo(item.UserConsultantId);
                if (user == null) continue;

                // SourceType
                var sourceType = sourceTypes.FirstOrDefault(c => c.SourceTypeId == item.SourceTypeId);
                if (sourceType == null) continue;

                // List temp
                var listTemp = lstData
                    .Where(c => c.UserConsultantId == item.UserConsultantId)
                    .Where(c => c.SourceTypeId == sourceType.SourceTypeId)
                    .ToList();

                // Create entity
                var entity = listDataSource
                    .Where(c => c.User == user.FullName)
                    .Where(c => c.Group == user.GroupName)
                   .Where(c => c.SourceTypeId == sourceType.SourceTypeId)
                    .FirstOrDefault();
                if (entity != null)
                {
                    entity.Level2 = listTemp.Where(c => c.LevelId >= (int)LevelType.L2).Sum(c => c.Count);
                    entity.Level8 = listTemp.Where(c => c.LevelId >= (int)LevelType.L8).Sum(c => c.Count);
                    entity.Level5B = listTemp.Where(c => c.LevelId >= (int)LevelType.L5B).Sum(c => c.Count);
                }
            }

            // Tính tổng
            const string userSum = "Tổng";
            var groupSum = string.Empty;
            foreach (var sourceType in sourceTypes)
            {
                if (sourceTypeId > 0 && sourceTypeId != sourceType.SourceTypeId) continue;
                var listTemp = lstData.Where(c => c.SourceTypeId == sourceType.SourceTypeId).ToList();

                listDataSource.Insert(0, new BC02Model
                {
                    User = userSum,
                    Group = groupSum,
                    SourceType = sourceType.Code,
                    SourceTypeId = sourceType.SourceTypeId,
                    Level2 = listTemp.Where(c => c.LevelId >= (int)LevelType.L2).Sum(c => c.Count),
                    Level8 = listTemp.Where(c => c.LevelId >= (int)LevelType.L8).Sum(c => c.Count),
                    Level5B = listTemp.Where(c => c.LevelId >= (int)LevelType.L5B).Sum(c => c.Count),
                });
            }

            // Tính trung bình
            var groups = listDataSource.Where(c => !c.Group.IsStringNullOrEmpty()).Select(c => c.Group).Distinct().ToList();
            foreach (var group in groups)
            {
                const string userTB = "Trung bình";
                foreach (var sourceType in sourceTypes)
                {
                    if (sourceTypeId > 0 && sourceTypeId != sourceType.SourceTypeId) continue;
                    listDataSource.Add(new BC02Model
                    {
                        User = userTB,
                        Group = group,
                        SourceType = sourceType.Code,
                        SourceTypeId = sourceType.SourceTypeId,
                        Level2 = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.SourceTypeId == sourceType.SourceTypeId).Average(c => c.Level2), 1),
                        Level8 = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.SourceTypeId == sourceType.SourceTypeId).Average(c => c.Level8), 1),
                        Level5B = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.SourceTypeId == sourceType.SourceTypeId).Average(c => c.Level5B), 1),
                    });
                }
            }

            listDataSource.RemoveAll(c => c.SourceType.IsStringNullOrEmpty());
            listDataSource = listDataSource.OrderBy(c => c.SourceTypeId).ToList();
            var reportDataSource = new ReportDataSource("BC02", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC02.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;
            const string reportType = "pdf";
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>8.5in</PageWidth>" +
                                       "  <PageHeight>11in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";
            string[] streams;
            Warning[] warnings;
            var renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo nghiệm thu MO
        /// </summary>
        public ActionResult BC03()
        {
            return View();
        }
        public ActionResult ShowBC03(string from, string to, int reportType)
        {
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var dtFrom = from.ToDateTime() ?? DateTime.Now; dtFrom = dtFrom.Date;
            var dtTo = to.ToDateTime() ?? DateTime.Now; dtTo = dtTo.Date.AddDays(1).AddSeconds(-1);

            // Filter
            var lstContacts = ContactRepository.GetBC03(dtFrom, dtTo) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC03.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + dtFrom.ToString("dd/MM/yyyy") + " - Đến ngày: " + dtTo.ToString("dd/MM/yyyy") + ")"));

            var lstData = new List<BC03Model>();
            foreach (var info in lstContacts)
            {
                var objModel = BCModelContact.Create<BC03Model>(info);
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC03", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC03_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC43()
        {
            var StatusTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(StatusContact)))
                StatusTypes.Add((int)item, ObjectExtensions.GetEnumDescription((StatusContact)item));
            ViewBag.Status = new SelectList(StatusTypes, "Key", "Value");
            return View();
        }

        public ActionResult ShowBC43(string fromRegister, string toRegister, string fromHandover, string toHandover, int reportType, int statusType, string userIds)
        {
            var branchId = UserContext.GetDefaultBranch();

            var dtFromRegister = fromRegister.ToDateTime("ddMMyyyy");
            var dtToRegister = toRegister.ToDateTime("ddMMyyyy");
            var dtFromHandover = fromHandover.ToDateTime("ddMMyyyy");
            var dtToHandover = toHandover.ToDateTime("ddMMyyyy");

            // Filter
            var lstContacts = ContactRepository.GetBC43(dtFromRegister, dtToRegister, dtFromHandover, dtToHandover, userIds, statusType) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC43.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Bao cao TIC"));

            var lstData = new List<BC43Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC43Model
                {
                    FullName = info.Fullname,
                    Email = info.Email,
                    Level = info.Level,
                    PhoneNumber = info.PhoneNumber,
                    Channel = info.Channel,
                    Campaign = info.CampaindTpe,
                    LandingPage = info.LandingPage,
                    Address = info.Address,
                    StatusCare = info.StatusCare,
                    StatusMap = info.StatusMap,
                    Notes = info.Notes,
                    HadoverDate = info.HandoverConsultantDate,
                    Consultant = String.IsNullOrEmpty(info.UserName) ? "Đã được thu hồi về kho" : info.UserName,
                    TemplateAds = info.TemplateAds,
                    SearchKeyword = info.SearchKeyword,
                    Source = info.SourceType,
                    CallCount = info.CallCount,
                    RegisterDate = info.RegisteredDate == null ? DateTime.Now : info.RegisteredDate,
                    Acceptance = info.Level.ToString() == "L1" ? "Không" : "Có"
                };
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC43", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC43_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }
        /// <summary>
        /// Báo cáo BC44 QL TVTS xuất contact
        /// </summary>
        public ActionResult BC44()
        {
            return View();
        }

        public ActionResult ShowBC44( int statusMapId, int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int qualityId, int productSellId, string userIds, int sourceType, int employeeTypeId, int reportType)
        {
            var statusId = 0;
            var userName = UserContext.GetCurrentUser().UserName;

            #region "Start Checkpoint"
            CheckPointApi checkPointApi = new CheckPointApi();
            var watch = new Stopwatch();
            watch.Start();
            checkPointApi.CheckPointNew(userName, "ShowBC44", "Start", 0);
            #endregion 

            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);
            
            var lstContacts = ContactRepository.GetBC44(statusId, branchId, userIds, statusMapId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, qualityId, productSellId, sourceType, employeeType) ?? new List<ContactInfo>();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC44.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Chuc nang in danh sach nhu crm cu"));
            var lstData = new List<BC44Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC44Model
                {
                    Fullname = info.Fullname,
                    Email = info.Email,
                    Level = info.Level,
                    Mobile = info.PhoneNumber,
                    AppointmentConsultantDate = info.AppointmentConsultantDate,
                    CallInfoConsultant = info.CallInfoConsultant,
                    CallConsultantDate = info.CallConsultantDate,
                    StatusCare = info.StatusCareConsultant,
                    Product = info.ProductSellName,
                    Consultant = info.Consultant,
                    HandoverConsulantDate = info.HandoverConsultantDate,
                    CallCount = info.CallCount
                };
                lstData.Add(objModel);
            }

            //return View(lstData);

            var reportDataSource = new ReportDataSource("BC44", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC44_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }

            //#region "End CheckPoint"
            //watch.Stop();
            //checkPointApi.CheckPointNew(userName, "ShowBC44", "End", watch.ElapsedMilliseconds);
            //#endregion

            return File(renderedBytes, mimeType);
        }
        /// <summary>
        /// Báo cáo khối lượng công việc
        /// </summary>
        public ActionResult BC04()
        {
            return View();
        }
        public ActionResult ShowBC04(int groupId, int userId, int branchId, string from, string to, int reportType)
        {
            // Param datetime
            var dtFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

            var listUser = StoreData.ListUser.AsReadOnly();
            if (groupId > 0) listUser = listUser.Where(c => c.GroupId == groupId).ToList().AsReadOnly();

            // Load & filter data
            var lstDataTemp = TmpJobReportRepository.GetAllBC04(dtFrom, dtTo) ?? new List<TmpJobReportBC04Info>();
            lstDataTemp = lstDataTemp
                .Where(c => c.LevelId != (int)LevelType.L0)
                .Where(c => c.LevelId != (int)LevelType.L9)
                .ToList();
            if (userId > 0) lstDataTemp = lstDataTemp.Where(c => c.UserConsultantId == userId).ToList();
            if (branchId > 0) lstDataTemp = lstDataTemp.Where(c => c.BranchId == branchId).ToList();

            var listDataSource = new List<BC04Model>();
            var levels = StoreData.ListLevel
                .Where(c => c.LevelId != (int)LevelType.L0)
                .Where(c => c.LevelId != (int)LevelType.L9)
                .Where(c => !c.Name.IsStringNullOrEmpty())
                .ToList();
            lstDataTemp = lstDataTemp.Where(c => levels.Exists(p => p.LevelId == c.LevelId)).ToList();
            var lstData = new List<TmpJobReportBC04Info>();
            foreach (var item in lstDataTemp)
            {
                var user = listUser.FirstOrDefault(c => c.UserID == item.UserConsultantId);
                if (user == null) continue;
                if (groupId > 0 && user.GroupId != groupId) continue;
                lstData.Add(item);
            }

            // select danh sach contact co hen nhung chua gọi
            foreach (var itemUserId in lstData.Select(c => c.UserConsultantId).Distinct())
            {
                var listAppointment = ContactRepository.ContactReportAppointment(itemUserId, branchId, dtFrom, dtTo) ?? new List<ContactAppointmentInfo>();
                var user = listUser.FirstOrDefault(c => c.UserID == itemUserId);
                if (user == null) continue;
                if (groupId > 0 && user.GroupId != groupId) continue;

                foreach (var level in levels)
                {
                    var itemAppointment = listAppointment.FirstOrDefault(c => c.LevelId == level.LevelId);
                    var entity = new BC04Model
                    {
                        HandoverCount = 0,
                        Level = level.Name,
                        CompleteInCount = 0,
                        CompleteOutCount = 0,
                        User = user.FullName,
                        Group = user.GroupName,
                        LevelId = level.LevelId,
                        DateTime = dtFrom.ToString("dd/MM/yyyy"),
                        Type = level.LevelId == (int)LevelType.L1 ? "Chăm sóc lần đầu" : "Chăm sóc lại",
                        NotCompleteCount = itemAppointment == null ? 0 : itemAppointment.NotCompleteCount,
                    };
                    listDataSource.Add(entity);
                }
            }

            foreach (var item in lstData.OrderByDescending(c => c.Type))
            {
                var user = listUser.FirstOrDefault(p => p.UserID == item.UserConsultantId);
                if (user == null) continue;
                if (groupId > 0 && user.GroupId != groupId) continue;

                // Type
                var type = item.LevelId == (int)LevelType.L1 ? "Chăm sóc lần đầu" : "Chăm sóc lại";

                // Level
                var level = levels.FirstOrDefault(p => p.LevelId == item.LevelId);
                if (level == null) continue;
                var levelName = level.Name;

                var entity = listDataSource
                    .Where(c => c.Type == type)
                    .Where(c => c.User == user.FullName)
                    .Where(c => c.Group == user.GroupName)
                    .FirstOrDefault(c => c.Level == levelName);
                if (entity != null)
                {
                    entity.HandoverCount += item.HandoverCount;
                    entity.CompleteInCount += item.CompleteInCount;
                    entity.CompleteOutCount += item.CompleteOutCount;
                }
            }
            listDataSource = listDataSource.OrderBy(c => c.User).ToList();

            // Tính tổng
            var groupSum = string.Empty;
            const string userSum = "Tổng";
            foreach (var level in levels)
            {
                var listTemp = lstData.Where(c => c.LevelId == level.LevelId).ToList();

                listDataSource.Insert(0, new BC04Model
                {
                    User = userSum,
                    Group = groupSum,
                    Level = level.Name,
                    LevelId = level.LevelId,
                    DateTime = dtFrom.ToString("dd/MM/yyyy"),
                    Type = level.LevelId == (int)LevelType.L1 ? "Chăm sóc lần đầu" : "Chăm sóc lại",
                    HandoverCount = listTemp.IsNullOrEmpty() ? 0 : listTemp.Sum(c => c.HandoverCount),
                    CompleteInCount = listTemp.IsNullOrEmpty() ? 0 : listTemp.Sum(c => c.CompleteInCount),
                    CompleteOutCount = listTemp.IsNullOrEmpty() ? 0 : listTemp.Sum(c => c.CompleteOutCount),
                    NotCompleteCount = listDataSource.IsNullOrEmpty() ? 0 : listDataSource.Where(c => c.LevelId == level.LevelId).Sum(c => c.NotCompleteCount),
                });
            }

            // Tính trung bình
            var groups = listDataSource.Where(c => !c.Group.IsStringNullOrEmpty()).Select(c => c.Group).Distinct().ToList();
            foreach (var group in groups)
            {
                const string userTB = "Trung bình";
                foreach (var level in levels)
                {
                    listDataSource.Add(new BC04Model
                    {
                        User = userTB,
                        Group = group,
                        Level = level.Name,
                        LevelId = level.LevelId,
                        DateTime = dtFrom.ToString("dd/MM/yyyy"),
                        Type = level.LevelId == (int)LevelType.L1 ? "Chăm sóc lần đầu" : "Chăm sóc lại",
                        HandoverCount = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.LevelId == level.LevelId).Average(c => c.HandoverCount), 2),
                        CompleteInCount = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.LevelId == level.LevelId).Average(c => c.CompleteInCount), 2),
                        CompleteOutCount = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.LevelId == level.LevelId).Average(c => c.CompleteOutCount), 2),
                        NotCompleteCount = Math.Round(listDataSource.Where(c => c.Group == group).Where(c => c.LevelId == level.LevelId).Average(c => c.NotCompleteCount), 2),
                    });
                }
            }

            var reportDataSource = new ReportDataSource("BC04", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC04.rdlc") };
            localReport.SetParameters(new ReportParameter("FromDate", dtFrom.ToString("dd/MM/yyyy")));
            localReport.SetParameters(new ReportParameter("ToDate", dtTo.ToString("dd/MM/yyyy")));
            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;

            string[] streams;
            Warning[] warnings;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }

            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.2in</MarginBottom>" +
                                      "</DeviceInfo>";
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC08_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }

            localReport.ShowDetailedSubreportMessages = false;

            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo nghiệm thu tổng hợp
        /// </summary>
        public ActionResult BC05()
        {
            return View();
        }
        public ActionResult ShowBC05(int groupId, int userId, int branchId, string fromdate, string todate, int reportType)
        {
            // Param datetime
            var dtFrom = string.IsNullOrEmpty(fromdate) ? DateTime.Now.Date : DateTime.ParseExact(fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtTo = string.IsNullOrEmpty(todate) ? DateTime.Now.Date : DateTime.ParseExact(todate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var listUser = StoreData.ListUser.AsReadOnly();
            if (groupId > 0) listUser = listUser.Where(c => c.GroupId == groupId).ToList().AsReadOnly();

            // Load & filter data
            var lstData = TmpJobReportRepository.GetAllBC05(dtFrom, dtTo) ?? new List<TmpJobReportBC05Info>();
            if (userId > 0) lstData = lstData.Where(c => c.UserConsultantId == userId).ToList();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();

            var listDataSource = new List<BC05Model>();

            // Tổng
            var ProductSellIds = lstData.Select(c => c.ProductSellId).Distinct();
            foreach (var productSellId in ProductSellIds)
            {
                var lstTemp = lstData.Where(c => c.ProductSellId == productSellId).ToList();
                var objModel = new BC05Model
                {
                    Consultant = " Tổng",
                    KDDT = lstTemp.Sum(c => c.KDDT),
                    KLLD = lstTemp.Sum(c => c.KLLD),
                    SaiSo = lstTemp.Sum(c => c.SaiSo),
                    KNM = lstTemp.Sum(c => c.KNM),
                    ChuaGoi = lstTemp.Sum(c => c.ChuaGoi),
                    BGLS = lstTemp.Sum(c => c.BGLS),
                    TotalHandover = lstTemp.Sum(c => c.HandoverCount),
                };
                objModel.TotalReturn = objModel.KDDT + objModel.KLLD + objModel.ChuaGoi + objModel.SaiSo + objModel.KNM + objModel.BGLS;
                objModel.ThucNhan = objModel.TotalHandover - objModel.TotalReturn;

                if (objModel.TotalHandover > 0) objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;

                listDataSource.Add(objModel);
            }

            foreach (var info in lstData)
            {
                // User
                var user = listUser.FirstOrDefault(p => p.UserID == info.UserConsultantId);
                var consultantName = user == null ? string.Empty : user.FullName;
                if (consultantName.IsStringNullOrEmpty()) continue;

                var objModel = listDataSource.FirstOrDefault(c => c.Consultant == consultantName);
                if (objModel == null)
                {
                    objModel = new BC05Model
                    {
                        KNM = info.KNM,
                        KDDT = info.KDDT,
                        KLLD = info.KLLD,
                        SaiSo = info.SaiSo,
                        ChuaGoi = info.ChuaGoi,
                        BGLS = info.BGLS,
                        Consultant = consultantName,
                        TotalHandover = info.HandoverCount,
                        TotalReturn = info.KDDT + info.KLLD + info.KhacVung + info.SaiSo + info.KNM,
                        ThucNhan = info.HandoverCount - (info.KDDT + info.KLLD + info.ChuaGoi + info.BGLS + info.SaiSo + info.KNM),
                    };
                    if (objModel.TotalHandover > 0)
                        objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;
                    listDataSource.Add(objModel);
                }
                else
                {
                    objModel.KNM += info.KNM;
                    objModel.KDDT += info.KDDT;
                    objModel.KLLD += info.KLLD;
                    objModel.SaiSo += info.SaiSo;
                    objModel.ChuaGoi += info.ChuaGoi;
                    objModel.BGLS += info.BGLS;
                    objModel.TotalHandover += info.HandoverCount;
                    objModel.TotalReturn = objModel.KDDT + objModel.KLLD + objModel.ChuaGoi + objModel.BGLS + objModel.SaiSo + objModel.KNM;
                    objModel.ThucNhan = objModel.TotalHandover - objModel.TotalReturn;
                    if (objModel.TotalHandover > 0) objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;
                }
            }
            foreach (var item in listDataSource.Where(c => c.SchoolName == "Tất cả"))
                item.SchoolName = "Chưa biết";
            listDataSource = listDataSource.OrderBy(c => c.SchoolName).ToList();
            foreach (var item in listDataSource.Select(c => c.Consultant).Distinct().ToList())
            {
                var infos = listDataSource.Where(c => c.Consultant == item).ToList();
                var objModel = new BC05Model
                {
                    Consultant = item,
                    SchoolName = "Tổng",
                    KNM = infos.Sum(c => c.KNM),
                    KDDT = infos.Sum(c => c.KDDT),
                    KLLD = infos.Sum(c => c.KLLD),
                    SaiSo = infos.Sum(c => c.SaiSo),
                    ThucNhan = infos.Sum(c => c.ThucNhan),
                    ChuaGoi = infos.Sum(c => c.ChuaGoi),
                    BGLS = infos.Sum(c => c.BGLS),
                    TotalReturn = infos.Sum(c => c.TotalReturn),
                    TotalHandover = infos.Sum(c => c.TotalHandover),
                };
                if (objModel.TotalHandover > 0)
                    objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;
                listDataSource.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC05", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC05.rdlc") };
            localReport.SetParameters(new ReportParameter("QueryTime", string.Format(dtFrom.ToString("dd/MM/yyyy")) + " - " + string.Format(dtTo.ToString("dd/MM/yyyy"))));
            localReport.DataSources.Add(reportDataSource);
            //const string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType             
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx             
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>11.69in</PageWidth>" +
                                      "  <PageHeight>8.27in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.2in</MarginBottom>" +
                                      "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);             
            //Session["PrintId"] = null;
            return File(renderedBytes, mimeType);
        }



        /// <summary>
        /// Báo cáo chi tiết trả lại
        /// </summary>
        public ActionResult BC06()
        {
            return View();
        }
        public ActionResult ShowBC06(string from, string to, int reportType)
        {
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var dtFrom = from.ToDateTime() ?? DateTime.Now; dtFrom = dtFrom.Date;
            var dtTo = to.ToDateTime() ?? DateTime.Now; dtTo = dtTo.Date.AddDays(1).AddSeconds(-1);

            // Filter
            var lstContacts = ContactRepository.GetBC06(dtFrom, dtTo) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC06.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + dtFrom.ToString("dd/MM/yyyy") + " - Đến ngày: " + dtTo.ToString("dd/MM/yyyy") + ")"));

            var lstData = BCModelContact.Create<BC06Model>(lstContacts);
            var reportDataSource = new ReportDataSource("BC06", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC06_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo nghiệm thu tổng hợp cho đội lọc
        /// </summary>
        public ActionResult BC07()
        {
            return View();
        }
        public ActionResult ShowBC07(int branchId, string date, int reportType)
        {
            // Param datetime
            var dtTo = string.IsNullOrEmpty(date) ? DateTime.Now.Date : DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtFrom = string.IsNullOrEmpty(date) ? DateTime.Now.Date : DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Load & filter data
            var lstData = TmpJobReportRepository.GetAllBC07(dtTo) ?? new List<TmpJobReportBC07Info>();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();

            var listDataSource = new List<BC07Model>();
            foreach (var info in lstData)
            {
                // School
                const string schoolName = "Chưa biết";
                var objModel = listDataSource.FirstOrDefault(c => c.SchoolName == schoolName);
                if (objModel == null)
                {
                    objModel = new BC07Model
                    {
                        KDDT = info.KDDT,
                        KLLD = info.KLLD,
                        SaiSo = info.SaiSo,
                        Duplicate = info.Trung,
                        SchoolName = schoolName,
                        KhacVung = info.KhacVung,
                        TotalHandover = info.HandoverCount,
                        TotalReturn = info.KDDT + info.KLLD + info.KhacVung + info.SaiSo + info.Trung,
                        ThucNhan = info.HandoverCount - (info.KDDT + info.KLLD + info.KhacVung + info.SaiSo + info.Trung),
                    };
                    if (objModel.TotalHandover > 0) objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;
                    listDataSource.Add(objModel);
                }
                else
                {
                    objModel.KDDT += info.KDDT;
                    objModel.KLLD += info.KLLD;
                    objModel.SaiSo += info.SaiSo;
                    objModel.Duplicate += info.Trung;
                    objModel.KhacVung += info.KhacVung;
                    objModel.TotalHandover += info.HandoverCount;
                    objModel.TotalReturn += info.KDDT + info.KLLD + info.KhacVung + info.SaiSo + info.Trung;
                    objModel.ThucNhan += info.HandoverCount - (info.KDDT + info.KLLD + info.KhacVung + info.SaiSo + info.Trung);
                    if (objModel.TotalHandover > 0) objModel.TotalPercentage = (double)objModel.TotalReturn / objModel.TotalHandover;
                }
            }
            foreach (var item in listDataSource.Where(c => c.SchoolName == "Tất cả")) item.SchoolName = "Chưa biết";
            var reportDataSource = new ReportDataSource("BC07", listDataSource.OrderBy(c => c.SchoolName).ToList());
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC07.rdlc") };
            localReport.SetParameters(new ReportParameter("QueryTime", string.Format(dtFrom.ToString("dd/MM/yyyy"))));
            localReport.DataSources.Add(reportDataSource);
            //const string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType             
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx             
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>11.69in</PageWidth>" +
                                      "  <PageHeight>8.27in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.2in</MarginBottom>" +
                                      "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);             
            //Session["PrintId"] = null;
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC07_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo nghiệm thu kết quả L1
        /// </summary>
        public ActionResult BC08()
        {
            return View();
        }
        public ActionResult ShowBC08(int branchId, string date, int reportType)
        {
            // Param datetime
            var dtTo = string.IsNullOrEmpty(date) ? DateTime.Now.Date : DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtFrom = string.IsNullOrEmpty(date) ? DateTime.Now.Date : DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Load & filter data
            var lstData = TmpJobReportRepository.GetAllBC08(dtTo) ?? new List<TmpJobReportBC08Info>();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();

            var listDataSource = new List<BC08Model>();
            foreach (var info in lstData)
            {
                // School
                var import = ImportExcelRepository.GetInfo(info.ImportId);
                var importName = import != null ? (new FileInfo(import.FilePath)).Name : "Chưa biết";
                var objModel = listDataSource.FirstOrDefault(c => c.Import == importName);
                if (objModel == null)
                {
                    objModel = new BC08Model
                    {
                        Gia = info.Gia,
                        KNM = info.KNM,
                        KLLD = info.KLLD,
                        Trung = info.Trung,
                        Import = importName,
                        CaoHoc = info.CaoHoc,
                        VungKhac = info.VungKhac,
                        SinhVien = info.SinhVien,
                        NhamNguoi = info.NhamNguoi,
                        KhongNhuCau = info.KhongNhuCau,
                        L1 = info.LevelId <= 1 ? info.Amount : 0,
                        L2 = info.LevelId >= 2 ? info.Amount : 0,
                    };
                    listDataSource.Add(objModel);
                }
                else
                {
                    objModel.Gia += info.Gia;
                    objModel.KNM += info.KNM;
                    objModel.KLLD += info.KLLD;
                    objModel.Trung += info.Trung;
                    objModel.CaoHoc += info.CaoHoc;
                    objModel.VungKhac += info.VungKhac;
                    objModel.SinhVien += info.SinhVien;
                    objModel.NhamNguoi += info.NhamNguoi;
                    objModel.KhongNhuCau += info.KhongNhuCau;
                    objModel.L1 += info.LevelId <= 1 ? info.Amount : 0;
                    objModel.L2 += info.LevelId >= 2 ? info.Amount : 0;
                }
            }

            foreach (var model in listDataSource)
            {
                model.L1 = model.L1 + model.L2;
                model.NotCompleteCount = model.L1 - model.L2 - model.KLLD - model.KNM - model.Trung - model.CaoHoc -
                                         model.Gia - model.SinhVien - model.KhongNhuCau - model.VungKhac - model.NhamNguoi;
                if (model.NotCompleteCount < 0) model.NotCompleteCount = 0;

            }

            var reportDataSource = new ReportDataSource("BC08", listDataSource.OrderBy(c => c.Import).ToList());
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC08.rdlc") };
            localReport.SetParameters(new ReportParameter("QueryTime", string.Format(dtFrom.ToString("dd/MM/yyyy"))));
            localReport.DataSources.Add(reportDataSource);
            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }

            //Render the report             
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>12in</PageWidth>" +
                                      "  <PageHeight>8.27in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.2in</MarginBottom>" +
                                      "</DeviceInfo>";
            var renderedBytes = localReport.Render(type, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC08_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo tỷ lệ
        /// </summary>
        public ActionResult BC300()
        {
            var dayReportTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(DayReportType)))
                dayReportTypes.Add((int)item, ObjectExtensions.GetEnumDescription((DayReportType)item));
            ViewBag.DayReportTypes = new SelectList(dayReportTypes, "Key", "Value");

            var StatusTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(StatusContact)))
                StatusTypes.Add((int)item, ObjectExtensions.GetEnumDescription((StatusContact)item));
            ViewBag.Status = new SelectList(StatusTypes, "Key", "Value");
            return View();
        }
        public ActionResult ShowBC300(int dayReportType, string groupId, int userId, int branchId, int statusType, string date, string hFromDate, string hToDate, int reportType)
        {
            // Param datetime
            var dtTo = hToDate.ToDateTime() ?? DateTime.Now;
            var dtFrom = hFromDate.ToDateTime() ?? DateTime.Now;

            List<UserInfo> listUser = new List<UserInfo>();
            string[] groupIds = Regex.Split(groupId, @",");

            foreach (string value in groupIds)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    var listUserFake = StoreData.ListUser.AsReadOnly();
                    if (i > 0)
                    {
                        listUserFake = listUserFake.Where(c => c.GroupId == i).ToList().AsReadOnly();
                    }
                    listUser.AddRange(listUserFake);
                }
            }

            var infomation = "Ngày bàn giao từ ngày: {0} - đến ngày: {1}";
            switch ((DayReportType)dayReportType)
            {
                case DayReportType.Day:
                    dtTo = hToDate.ToDateTime() ?? DateTime.Now.Date;
                    dtFrom = hFromDate.ToDateTime() ?? DateTime.Now.Date;
                    break;
                case DayReportType.Week:
                    dtTo = dtFrom.AddDays(7 - (int)dtFrom.DayOfWeek);
                    dtFrom = dtFrom.AddDays(-(int)dtFrom.DayOfWeek + 1);
                    break;
                case DayReportType.Month:
                    dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);
                    infomation = "Tháng: " + dtFrom.Month + " - " + infomation;
                    break;
                case DayReportType.Year:
                    dtTo = new DateTime(dtFrom.Year, 12, 31);
                    dtFrom = new DateTime(dtFrom.Year, 12, 1);
                    infomation = "Năm: " + dtFrom.Year + " - " + infomation;
                    break;
            }

            if (dtTo == DateTime.MinValue || dtTo > DateTime.Now.Date) dtTo = DateTime.Now.Date;
            if (dtFrom == DateTime.MinValue || dtFrom > DateTime.Now.Date) dtFrom = DateTime.Now.Date;
            var handoverToDate = dtTo;
            var handoverFromDate = dtFrom;
            handoverToDate = handoverToDate.AddDays(1).AddSeconds(-1);

            // Load & filter data
            var dateTime = date.ToDateTime() ?? DateTime.Now.Date;
            var lstData = TmpJobReportRepository.GetAllBC300(dateTime, statusType) ?? new List<TmpJobReportBC300Info>();

            var lstContactArise = TmpJobReportRepository.GetAll_Contacts_Arise(dtFrom, dtTo) ?? new List<TmpJobReportContactAriseBC300Info>();

            if (userId > 0) lstData = lstData.Where(c => c.UserConsultantId == userId).ToList();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();
            //if (sourceTypeId > 0) lstData = lstData.Where(c => c.SourceTypeId == sourceTypeId).ToList();
            if (!hFromDate.IsStringNullOrEmpty() || !hToDate.IsStringNullOrEmpty())
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }
            else if ((DayReportType)dayReportType == DayReportType.Lumped || (DayReportType)dayReportType == DayReportType.Latch)
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }

            else infomation = "Ngày xem báo cáo: " + dateTime.ToString("dd/MM/yyyy");
            var listDataSource = new List<BC300Model>();

            #region tính tổng tỷ lệ cho toàn bảng
            {
                var listTemp = lstData.Where(c => listUser.Any(p => c.UserConsultantId == p.UserID)).ToList();
                var level1 = listTemp.Where(c => c.LevelId == (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level2 = listTemp.Where(c => c.LevelId == (int)LevelType.L2 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level3 = listTemp.Where(c => c.LevelId == (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level4 = listTemp.Where(c => c.LevelId == (int)LevelType.L4 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level5 = listTemp.Where(c => c.LevelId == (int)LevelType.L5 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level6 = listTemp.Where(c => c.LevelId == (int)LevelType.L6 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level7 = listTemp.Where(c => c.LevelId == (int)LevelType.L7 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level8 = listTemp.Where(c => c.LevelId == (int)LevelType.L8 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                //var level9 = listTemp.Where(c => c.LevelId >= (int)LevelType.L9 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var totalcountLevel = listTemp.Where(c => c.LevelId >= (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var totalcountLevelFromL3 = listTemp.Where(c => c.LevelId >= (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var listTempArise = lstContactArise.Where(c => listUser.Any(p => c.TVTSId == p.UserID)).ToList();
                var level1Arise = listTempArise.Where(c => c.Level == (int)LevelType.L1 && c.Level < (int)LevelType.L3M).Count();
                var level2Arise = listTempArise.Where(c => c.Level == (int)LevelType.L2).Count();
                var level3Arise = listTempArise.Where(c => c.Level == (int)LevelType.L3).Count();
                var level4Arise = listTempArise.Where(c => c.Level == (int)LevelType.L4).Count();
                var level5Arise = listTempArise.Where(c => c.Level == (int)LevelType.L5).Count();
                var level6Arise = listTempArise.Where(c => c.Level == (int)LevelType.L6).Count();
                var level7Arise = listTempArise.Where(c => c.Level == (int)LevelType.L7).Count();
                var level8Arise = listTempArise.Where(c => c.Level == (int)LevelType.L8).Count();
                var entity = new BC300Model
                {
                    User = "Tổng",
                    Group = string.Empty,
                    Name = "Tỷ lệ %(Lx/Ly)",
                    Level1 = (level1 > 0 ? Math.Round((double)level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level2 = (level1 > 0 ? Math.Round((double)level2 / totalcountLevel * 100, 1) : 0) + "%",
                    Level3 = (level2 > 0 ? Math.Round((double)level3 / totalcountLevel * 100, 1) : 0).ToString("0.0") + "%",
                    Level4 = (level3 > 0 ? Math.Round((double)level4 / totalcountLevelFromL3 * 100, 1) : 0).ToString("0.0") + "%",
                    Level5 = "", //Bo
                    Level6 = (level5 > 0 ? Math.Round((double)level6 / totalcountLevelFromL3 * 100, 1) : 0).ToString("0.0") + "%",
                    Level7 = "", //Bo
                    Level8 = (level7 > 0 ? Math.Round((double)level8 / totalcountLevel * 100, 1) : 0).ToString("0.0") + "%",
                    NameArise = "Tổng Số Contact",
                    Arise = (level1 + level2 + level2Arise + level3 + level3Arise + level4 + level4Arise + level5 + level5Arise + level6 + level6Arise + level7 + level7Arise + level8 + level8Arise).ToString(),
                    Arise_L1 = level1.ToString(),
                    Arise_L2 = (level2 + level2Arise).ToString(),
                    Arise_L3 = (level3 + level3Arise).ToString(),
                    Arise_L4 = (level4 + level4Arise).ToString(),
                    Arise_L5 = (level5 + level5Arise).ToString(),
                    Arise_L6 = (level6 + level6Arise).ToString(),
                    Arise_L7 = (level7 + level7Arise).ToString(),
                    Arise_L8 = (level8 + level8Arise).ToString(),
                };
                listDataSource.Add(entity);
            }
            #endregion

            #region tính cho từng TVTS

            foreach (var id in lstData.Select(c => c.UserConsultantId).Distinct())
            {
                var user = listUser.FirstOrDefault(p => p.UserID == id);
                if (user == null) continue;

                var listTemp = lstData.Where(c => c.UserConsultantId == user.UserID).ToList();
                var level1 = listTemp.Where(c => c.LevelId == (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level2 = listTemp.Where(c => c.LevelId == (int)LevelType.L2 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level3 = listTemp.Where(c => c.LevelId == (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level4 = listTemp.Where(c => c.LevelId == (int)LevelType.L4 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level5 = listTemp.Where(c => c.LevelId == (int)LevelType.L5 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level6 = listTemp.Where(c => c.LevelId == (int)LevelType.L6 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level7 = listTemp.Where(c => c.LevelId == (int)LevelType.L7 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var level8 = listTemp.Where(c => c.LevelId == (int)LevelType.L8 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                //var level9 = listTemp.Where(c => c.LevelId == (int)LevelType.L9 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var totalcountLevel = listTemp.Where(c => c.LevelId >= (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var totalcountLevelFromL3 = listTemp.Where(c => c.LevelId >= (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var listTempArise = lstContactArise.Where(c => listUser.Any(p => c.TVTSId == user.UserID)).ToList();
                var level1Arise = listTempArise.Where(c => c.Level == (int)LevelType.L1).Count();
                List<TmpJobReportContactAriseBC300Info> mlist = listTempArise.Where(c => c.Level == (int)LevelType.L2).ToList();
                var level2Arise = mlist.Count();
                var level3Arise = listTempArise.Where(c => c.Level == (int)LevelType.L3).Count();
                var level4Arise = listTempArise.Where(c => c.Level == (int)LevelType.L4).Count();
                var level5Arise = listTempArise.Where(c => c.Level == (int)LevelType.L5).Count();
                var level6Arise = listTempArise.Where(c => c.Level == (int)LevelType.L6).Count();
                var level7Arise = listTempArise.Where(c => c.Level == (int)LevelType.L7).Count();
                var level8Arise = listTempArise.Where(c => c.Level == (int)LevelType.L8).Count();

                var entity = new BC300Model
                {
                    User = user.FullName,
                    Group = user.GroupName,
                    Name = "Tỷ lệ %(Lx/Ly)",
                    Level1 = (level1 > 0 ? Math.Round((double)level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level2 = (level1 > 0 ? Math.Round((double)(level2) / totalcountLevel * 100, 1) : 0) + "%",
                    Level3 = (level2 > 0 ? Math.Round((double)(level3) / totalcountLevel * 100, 1) : 0).ToString("0.0") + "%",
                    Level4 = (level3 > 0 ? Math.Round((double)(level4) / totalcountLevelFromL3 * 100, 1) : 0).ToString("0.0") + "%",                 
                    Level5 = "",
                    Level6 = (Math.Round((double)(level6) / totalcountLevelFromL3 * 100, 1)).ToString("0.0") + "%",
                    Level7 = "",               
                    Level8 = (Math.Round((double)(level8) / level1 * 100, 1)).ToString("0.0") + "%",           
                    NameArise = "Tổng Số Contact",
                    Arise = (level1 + level2 + level2Arise + level3 + level3Arise + level4 + level4Arise + level5 + level5Arise + level6 + level6Arise + level7 + level7Arise + level8 + level8Arise).ToString(),
                    Arise_L1 = level1.ToString(),
                    Arise_L2 = (level2 + level2Arise).ToString(),
                    Arise_L3 = (level3 + level3Arise).ToString(),
                    Arise_L4 = (level4 + level4Arise).ToString(),
                    Arise_L5 = (level5 + level5Arise).ToString(),
                    Arise_L6 = (level6 + level6Arise).ToString(),
                    Arise_L7 = (level7 + level7Arise).ToString(),
                    Arise_L8 = (level8 + level8Arise).ToString(),
                };
                listDataSource.Add(entity);

                entity = new BC300Model
                {
                    Group = user.GroupName,
                    User = user.FullName,
                    Name = "Số lượng (contact)",
                    NameArise = "Số lượng(contact phát sinh)",
                    Level = (level1 + level2 + level3 + level4 + level5 + level6 + level7 + level8).ToString(),
                    Level1 = level1.ToString("0"),
                    Level2 = level2.ToString("0"),
                    Level3 = level3.ToString("0"),
                    Level4 = level4.ToString("0"),
                    Level5 = level5.ToString("0"),
                    Level6 = level6.ToString("0"),
                    Level7 = level7.ToString("0"),
                    Level8 = level8.ToString("0"),
                    //Level9 = level9.ToString("0"),
                    Arise = (level2Arise + level3Arise + level4Arise + level5Arise + level6Arise + level7Arise + level8Arise).ToString(),
                    Arise_L1 = "",
                    Arise_L2 = level2Arise.ToString(),
                    Arise_L3 = level3Arise.ToString(),
                    Arise_L4 = level4Arise.ToString(),
                    Arise_L5 = level5Arise.ToString(),
                    Arise_L6 = level6Arise.ToString(),
                    Arise_L7 = level7Arise.ToString(),
                    Arise_L8 = level8Arise.ToString(),
                    //Arise_L9 = level9Arise.ToString()
                };
                listDataSource.Add(entity);
            }

            #endregion


            #region tính trung bình cho toàn nhóm
            // Trung bình
            var groups = listDataSource
                .Where(c => !c.Group.IsStringNullOrEmpty())
                .Select(c => c.Group)
                .Distinct()
                .ToList();
            foreach (var group in groups)
            {
                var listTemp = listDataSource
                    .Where(c => c.Group == group)
                    .Where(c => c.Name == "Số lượng (contact)")
                    .ToList();
                if (listTemp.IsNullOrEmpty()) continue;

                var level1 = listTemp.Sum(c => c.Level1.ToDecimal());
                var level2 = listTemp.Sum(c => c.Level2.ToDecimal());
                var level3 = listTemp.Sum(c => c.Level3.ToDecimal());
                var level4 = listTemp.Sum(c => c.Level4.ToDecimal());
                var level5 = listTemp.Sum(c => c.Level5.ToDecimal());
                var level6 = listTemp.Sum(c => c.Level6.ToDecimal());
                var level7 = listTemp.Sum(c => c.Level7.ToDecimal());
                var level8 = listTemp.Sum(c => c.Level8.ToDecimal());
                //var level9 = listTemp.Sum(c => c.Level9.ToDecimal());
                var level1Arises = listTemp.Sum(c => c.Arise_L1.ToDecimal());
                var level2Arise = listTemp.Sum(c => c.Arise_L2.ToDecimal());
                var level3Arise = listTemp.Sum(c => c.Arise_L3.ToDecimal());
                var level4Arise = listTemp.Sum(c => c.Arise_L4.ToDecimal());
                var level5Arise = listTemp.Sum(c => c.Arise_L5.ToDecimal());
                var level6Arise = listTemp.Sum(c => c.Arise_L6.ToDecimal());
                var level7Arise = listTemp.Sum(c => c.Arise_L7.ToDecimal());
                var level8Arise = listTemp.Sum(c => c.Arise_L8.ToDecimal());
                //var level9Arise = listTemp.Sum(c => c.Arise_L9.ToDecimal());
                var entity = new BC300Model
                {
                    Group = group,
                    User = "Trung bình",
                    Name = "Tỷ lệ %(Lx/Ly)",
                    Level1 = (level1 > 0 ? Math.Round(level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level2 = (level1 > 0 ? Math.Round((level2 + level2Arise) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level3 = (level2 > 0 ? Math.Round((level3 + level3Arise) / level2 * 100, 1) : 0).ToString("0.0") + "%",
                    Level4 = (level3 > 0 ? Math.Round((level4 + level4Arise) / level3 * 100, 1) : 0).ToString("0.0") + "%",
                    Level5 = (level4 > 0 ? Math.Round((level5 + level5Arise) / level4 * 100, 1) : 0).ToString("0.0") + "%",
                    Level6 = (level5 > 0 ? Math.Round((level6 + level6Arise) / level5 * 100, 1) : 0).ToString("0.0") + "%",
                    Level7 = (level6 > 0 ? Math.Round((level7 + level7Arise) / level6 * 100, 1) : 0).ToString("0.0") + "%",
                    Level8 = (level7 > 0 ? Math.Round((level8 + level8Arise) / level7 * 100, 1) : 0).ToString("0.0") + "%",
                    //Level9 = (level8 > 0 ? Math.Round((level9 + level9Arise) / level8 * 100, 1) : 0).ToString("0.0") + "%",
                    NameArise = "Tổng Số Contact",
                    Arise_L1 = level1.ToString(),
                    Arise_L2 = (level2 + level2Arise).ToString(),
                    Arise_L3 = (level3 + level3Arise).ToString(),
                    Arise_L4 = (level4 + level4Arise).ToString(),
                    Arise_L5 = (level5 + level5Arise).ToString(),
                    Arise_L6 = (level6 + level6Arise).ToString(),
                    Arise_L7 = (level7 + level7Arise).ToString(),
                    Arise_L8 = (level8 + level8Arise).ToString(),
                    //Arise_L9 = (level9 + level9Arise).ToString(),
                };
                //Level9 = (level8 > 0 ? Math.Round(level8  / level7 * 100, 1) : 0).ToString("0.0") + "%",
                listDataSource.Add(entity);
            }

            #endregion

            #region tính tổng số lượng contact của toàn bản báo cáo

            var listTempTotal = listDataSource
                   .Where(c => c.Name == "Số lượng (contact)")
                   .ToList();
            var listTempTotalArise = listDataSource
                   .Where(c => c.Name == "Số lượng(contact phát sinh)")
                   .ToList();
            var entityTotal = new BC300Model
            {
                User = "Tổng",
                Group = string.Empty,
                Name = "Số lượng (contact)",
                NameArise = "Số lượng(contact phát sinh)",
                Level = (listTempTotal.Sum(c => c.Level1.ToDecimal())
                            + listTempTotal.Sum(c => c.Level2.ToDecimal())
                            + listTempTotal.Sum(c => c.Level3.ToDecimal())
                            + listTempTotal.Sum(c => c.Level4.ToDecimal())
                            + listTempTotal.Sum(c => c.Level5.ToDecimal())
                            + listTempTotal.Sum(c => c.Level6.ToDecimal())
                            + listTempTotal.Sum(c => c.Level7.ToDecimal())
                            + listTempTotal.Sum(c => c.Level8.ToDecimal())).ToString(),
                Level1 = listTempTotal.Sum(c => c.Level1.ToDecimal()).ToString("0"),
                Level2 = listTempTotal.Sum(c => c.Level2.ToDecimal()).ToString("0"),
                Level3 = listTempTotal.Sum(c => c.Level3.ToDecimal()).ToString("0"),
                Level4 = listTempTotal.Sum(c => c.Level4.ToDecimal()).ToString("0"),
                Level5 = listTempTotal.Sum(c => c.Level5.ToDecimal()).ToString("0"),
                Level6 = listTempTotal.Sum(c => c.Level6.ToDecimal()).ToString("0"),
                Level7 = listTempTotal.Sum(c => c.Level7.ToDecimal()).ToString("0"),
                Level8 = listTempTotal.Sum(c => c.Level8.ToDecimal()).ToString("0"),
                //Level9 = listTempTotal.Sum(c => c.Level9.ToDecimal()).ToString("0"),

                Arise = (listTempTotal.Sum(c => c.Arise_L1.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L2.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L3.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L4.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L5.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L6.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L7.ToDecimal())
                            + listTempTotal.Sum(c => c.Arise_L8.ToDecimal())).ToString(),
                Arise_L1 = listTempTotal.Sum(c => c.Arise_L1.ToDecimal()).ToString("0"),
                Arise_L2 = listTempTotal.Sum(c => c.Arise_L2.ToDecimal()).ToString("0"),
                Arise_L3 = listTempTotal.Sum(c => c.Arise_L3.ToDecimal()).ToString("0"),
                Arise_L4 = listTempTotal.Sum(c => c.Arise_L4.ToDecimal()).ToString("0"),
                Arise_L5 = listTempTotal.Sum(c => c.Arise_L5.ToDecimal()).ToString("0"),
                Arise_L6 = listTempTotal.Sum(c => c.Arise_L6.ToDecimal()).ToString("0"),
                Arise_L7 = listTempTotal.Sum(c => c.Arise_L7.ToDecimal()).ToString("0"),
                Arise_L8 = listTempTotal.Sum(c => c.Arise_L8.ToDecimal()).ToString("0"),
            };
            listDataSource.Insert(1, entityTotal);
            BC300Model mtamp1 = listDataSource[0],
                mtamp2 = listDataSource[1];
            mtamp1.Arise_L1 = mtamp2.Level1;
            {

                int l1 = int.Parse(mtamp2.Level1),
                    l2 = int.Parse(mtamp2.Level2),
                    l3 = int.Parse(mtamp2.Level3),
                    l4 = int.Parse(mtamp2.Level4),
                    l5 = int.Parse(mtamp2.Level5),
                    l6 = int.Parse(mtamp2.Level6),
                    l7 = int.Parse(mtamp2.Level7),
                    l8 = int.Parse(mtamp2.Level8); //l9 = int.Parse(mtamp2.Level9) + int.Parse(mtamp2.Arise_L9)
                int tong = l1 + l2 + l3 + l4 + l5 + l6 + l7 + l8;
                int tongL3 = l3 + l4 + l5 + l6 + l7 + l8;
                //int tongL3 = 
                //mtamp1.Level1 = string.Format("{0:0.00}%", l1 != 0 ? ((float)l1 * 100 / l1) : 0.0f);
                mtamp1.Level2 = string.Format("{0:0.00}%", l1 != 0 ? ((float)l2 * 100 / tong) : 0.0f);
                mtamp1.Level3 = string.Format("{0:0.00}%", l2 != 0 ? ((float)l3 * 100 / tong) : 0.0f);
                mtamp1.Level4 = string.Format("{0:0.00}%", l3 != 0 ? ((float)l4 * 100 / tongL3) : 0.0f);
                mtamp1.Level5 = "";
                mtamp1.Level6 = string.Format("{0:0.00}%", l5 != 0 ? ((float)l6 * 100 / tongL3) : 0.0f);
                mtamp1.Level7 = "";
                mtamp1.Level8 = string.Format("{0:0.00}%", l7 != 0 ? ((float)l8 * 100 / tong) : 0.0f);
                
                mtamp1.Arise_L1 = (l1 + int.Parse(mtamp2.Arise_L1)).ToString();
                mtamp1.Arise_L2 = (l2 + int.Parse(mtamp2.Arise_L2)).ToString();
                mtamp1.Arise_L3 = (l3 + int.Parse(mtamp2.Arise_L3)).ToString();
                mtamp1.Arise_L4 = (l4 + int.Parse(mtamp2.Arise_L4)).ToString();
                mtamp1.Arise_L5 = (l5 + int.Parse(mtamp2.Arise_L5)).ToString();
                mtamp1.Arise_L6 = (l6 + int.Parse(mtamp2.Arise_L6)).ToString();
                mtamp1.Arise_L7 = (l7 + int.Parse(mtamp2.Arise_L7)).ToString();
                mtamp1.Arise_L8 = (l8 + int.Parse(mtamp2.Arise_L8)).ToString();            
            }
            listDataSource.RemoveAt(0);
            listDataSource.Insert(0, mtamp1);

            #endregion
            if (listDataSource.Count <= 2) listDataSource.Clear();
            var reportDataSource = new ReportDataSource("BC300", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC300.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;
            //const string reportType = "pdf";
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>11in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";
            string[] streams;
            Warning[] warnings;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC300_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo tình trạng kết nối của Contact L1
        /// </summary>
        public ActionResult BC09()
        {
            ViewBag.Collectors = CollectorRepository.GetAll();
            return View();
        }
        public ActionResult ShowBC09(int sourceTypeId, int userId, string fromDate, string toDate, int reportType)
        {
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var startDate = fromDate.ToDateTime("ddMMyyyy");
            if (startDate.HasValue) startDate = startDate.Value.Date;
            var endDate = toDate.ToDateTime("ddMMyyyy");
            if (endDate.HasValue) endDate = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            // Source
            var listSource = TmpJobReportRepository.GetAllBC09(branchId, sourceTypeId, userId, startDate, endDate) ?? new List<TmpJobReportBC09Info>();
            //StoreData.ListImportExcel = ImportExcelRepository.GetAll(10000);
            var lstData = new List<BC09Model>();
            foreach (var item in listSource.OrderBy(c => c.DateTime))
            {
                var import = StoreData.ListImportExcel.FirstOrDefault(c => c.ImportId == item.ImportId);
                var sourceType = StoreData.ListSourceType.FirstOrDefault(c => c.SourceTypeId == item.SourceTypeId);
                var entity = new BC09Model
                {
                    L1 = item.Amount,
                    DateTime = item.DateTime.ToString("dd/MM/yyyy"),
                    SourceType = sourceType != null ? sourceType.Name : "Chưa biết",
                    L1A = item.ConnectStatus == (int)StatusConnectType.Connect ? item.Amount : 0,
                    L1B = item.ConnectStatus != (int)StatusConnectType.Connect ? item.Amount : 0,
                    ImportName = import != null ? (new FileInfo(import.FilePath)).Name : "Chưa biết",
                };

                var entityDb = lstData
                    .Where(c => c.DateTime == entity.DateTime)
                    .Where(c => c.SourceType == entity.SourceType)
                    .Where(c => c.ImportName == entity.ImportName)
                    .FirstOrDefault();
                if (entityDb == null)
                {
                    lstData.Add(entity);
                }
                else
                {
                    entityDb.L1 += entity.L1;
                    entityDb.L1A += entity.L1A;
                    entityDb.L1B += entity.L1B;
                }
            }

            // Init Report
            string infomation;
            if (!listSource.IsNullOrEmpty())
                infomation = "(Từ ngày: " + listSource.Min(c => c.DateTime).ToString("dd/MM/yyyy") + " - Đến ngày: " + listSource.Max(c => c.DateTime).ToString("dd/MM/yyyy") + ")";
            else if (startDate.HasValue && endDate.HasValue)
                infomation = "(Từ ngày: " + startDate.Value.ToString("dd/MM/yyyy") + " - Đến ngày: " + endDate.Value.ToString("dd/MM/yyyy") + ")";
            else
                infomation = string.Empty;
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC09.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            var reportDataSource = new ReportDataSource("BC09", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>11in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";

            //Render the report             
            var renderedBytes = localReport.Render(type, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC09_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo tình trạng chi tiết Contact
        /// </summary>
        public ActionResult BC10()
        {
            return View();
        }
        public ActionResult ShowBC10(int sourceTypeId, int userId, string consultantFromDate, string consultantToDate, int reportType)
        {
            int collaboratorId = 0;
            string collaboratorFromDate = "";
            string collaboratorToDate = "";
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var consultantStartdate = consultantFromDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            var consultanEndDate = consultantToDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            consultanEndDate = consultanEndDate.Date.AddDays(1).AddSeconds(-1);
            var collaboratorStartdate = collaboratorFromDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            var collaboratorEndDate = collaboratorToDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            collaboratorEndDate = collaboratorEndDate.Date.AddDays(1).AddSeconds(-1);

            // Source
            var listSource = TmpJobReportRepository.GetAllBC10(branchId, sourceTypeId, userId, collaboratorId, consultantStartdate, consultanEndDate, collaboratorStartdate, collaboratorEndDate) ?? new List<TmpJobReportBC10Info>();
            var listContact = new List<ContactInfo>();
            foreach (var info in listSource) listContact.Add(info);
            var lstData = BC10Model.CreateBc10Model(listContact);
            var lstTempt = BC10Model.CreateBc10Model(new List<ContactInfo>());
            foreach (var item in listSource.OrderBy(c => c.ChangedDate))
            {
                var entity = lstData.FirstOrDefault(c => c.Id == item.Id);
                if (entity == null)
                {
                    entity = BC10Model.CreateBc10Model(item);
                    lstData.Add(entity);
                }
                if (!item.ChangedDate.HasValue) continue;

                switch (item.PropertyValueInt)
                {
                    case 1:
                        if (entity.L1.IsStringNullOrEmpty() || item.ChangedDate >= entity.L1.ToDateTime())
                        {
                            entity.L1 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 2:
                        if (entity.L2.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime()))
                        {
                            entity.L2 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 3:
                        if (entity.L3.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime()))
                        {
                            entity.L3 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 4:
                        if (entity.L4.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime()))
                        {
                            entity.L4 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 5:
                        if (entity.L5.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime() &&
                            item.ChangedDate >= entity.L5.ToDateTime()))
                        {
                            entity.L5 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 6:
                        if (entity.L6.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime() &&
                            item.ChangedDate >= entity.L5.ToDateTime() &&
                            item.ChangedDate >= entity.L6.ToDateTime()))
                        {
                            entity.L6 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 7:
                        if (entity.L7.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime() &&
                            item.ChangedDate >= entity.L5.ToDateTime() &&
                            item.ChangedDate >= entity.L6.ToDateTime() &&
                            item.ChangedDate >= entity.L7.ToDateTime()))
                        {
                            entity.L7 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 8:
                        if (entity.L8.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime() &&
                            item.ChangedDate >= entity.L5.ToDateTime() &&
                            item.ChangedDate >= entity.L6.ToDateTime() &&
                            item.ChangedDate >= entity.L7.ToDateTime() &&
                            item.ChangedDate >= entity.L8.ToDateTime()))
                        {
                            entity.L8 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                    case 9:
                        if (entity.L9.IsStringNullOrEmpty() ||
                            (item.ChangedDate >= entity.L1.ToDateTime() &&
                            item.ChangedDate >= entity.L2.ToDateTime() &&
                            item.ChangedDate >= entity.L3.ToDateTime() &&
                            item.ChangedDate >= entity.L4.ToDateTime() &&
                            item.ChangedDate >= entity.L5.ToDateTime() &&
                            item.ChangedDate >= entity.L6.ToDateTime() &&
                            item.ChangedDate >= entity.L7.ToDateTime() &&
                            item.ChangedDate >= entity.L8.ToDateTime() &&
                            item.ChangedDate >= entity.L9.ToDateTime()))
                        {
                            entity.L9 = item.ChangedDate.Value.ToString("dd/MM/yyyy");
                        }
                        break;
                }
                if (!(lstTempt.Where(c => c.Id == entity.Id).Count() > 0))
                    lstTempt.Add(entity);
            }
            // Init Report
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC10.rdlc") };
            var reportDataSource = new ReportDataSource("BC10", lstTempt);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>24in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";

            //Render the report             
            var renderedBytes = localReport.Render(type, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC10_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }


        /// <summary>
        /// Báo cáo năng suất cuộc gọi của CTV
        /// </summary>
        public ActionResult BC11()
        {
            return View();
        }
        public ActionResult ShowBC11(int userId, string fromDate, string toDate, int reportType)
        {
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var startDate = fromDate.ToDateTime("ddMMyyyy"); if (startDate.HasValue) startDate = startDate.Value.Date;
            var endDate = toDate.ToDateTime("ddMMyyyy"); if (endDate.HasValue) endDate = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            // Source
            var listSource = TmpJobReportRepository.GetAllBC11(branchId, userId, startDate, endDate) ?? new List<TmpJobReportBC11Info>();
            var lstData = new List<BC11Model>();
            var statusMapL2 = StoreData.ListStatusMap
                .Where(c => c.LevelIdNext == (int)LevelType.L2)
                .Where(c => c.StatusMapType == (int)EmployeeType.Collaborator)
                .ToList();
            var statusMapL3 = StoreData.ListStatusMap
                .Where(c => c.LevelIdNext == (int)LevelType.L3M)
                .Where(c => c.StatusMapType == (int)EmployeeType.Collaborator)
                .ToList();
            foreach (var item in listSource)
            {
                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.CollaboratorId) ?? UserRepository.GetInfo(item.CollaboratorId);
                if (user == null) continue;

                var entity = lstData.FirstOrDefault(c => c.Collaborator == user.FullName);
                if (entity == null)
                {
                    entity = new BC11Model { Collaborator = user.FullName };
                    lstData.Add(entity);
                }

                entity.Total += 1;
                if (statusMapL2.Any(c => c.Id == item.StatusMapDistributorId))
                    entity.Level2 += 1;
                //if (item.StatusMapDistributorId == 0 && item.LevelId == (int)LevelType.L2) entity.Level2 += 1;
                else if (statusMapL3.Any(c => c.Id == item.StatusMapDistributorId))
                    entity.Level3A += 1;
                if (item.StatusMapDistributorId == 0 && item.LevelId == (int)LevelType.L3) entity.Level3A += 1;

                if (item.LevelId >= (int)LevelType.L3 && item.LevelId < (int)LevelType.L3M) entity.Level3 += 1;
                if (item.LevelId >= (int)LevelType.L4 && item.LevelId < (int)LevelType.L3M) entity.Level4 += 1;
                if (item.LevelId >= (int)LevelType.L5A && item.LevelId < (int)LevelType.L3M) entity.Level5 += 1;
                if (item.LevelId >= (int)LevelType.L5B && item.LevelId < (int)LevelType.L3M) entity.Level5B += 1;
                if (item.LevelId >= (int)LevelType.L6 && item.LevelId < (int)LevelType.L3M) entity.Level6 += 1;
                if (item.LevelId >= (int)LevelType.L7 && item.LevelId < (int)LevelType.L3M) entity.Level7 += 1;
                if (item.LevelId >= (int)LevelType.L8 && item.LevelId < (int)LevelType.L3M) entity.Level8 += 1;
                if (item.LevelId >= (int)LevelType.L9 && item.LevelId < (int)LevelType.L3M) entity.Level9 += 1;
            }

            // Init Report
            var infomation = !listSource.IsNullOrEmpty()
                                 ? "(Từ ngày: " + listSource.Min(c => c.CalledDate).ToString("dd/MM/yyyy") + " - Đến ngày: " + listSource.Max(c => c.CalledDate).ToString("dd/MM/yyyy") + ")"
                                 : " ";
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC11.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            var reportDataSource = new ReportDataSource("BC11", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>12.6in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";

            //Render the report             
            var renderedBytes = localReport.Render(type, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC11_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        /// <summary>
        /// Báo cáo năng suất cuộc gọi của CTV
        /// </summary>
        public ActionResult BC12()
        {
            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch());
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcels = importExcels.OrderByDescending(c => c.ImportedDate).ToList();

            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch());
            ViewBag.Channels = channels;
            return View();
        }
        public ActionResult ShowBC12(int importId, int channelId, string handoverStartDate, string handoverEndDate, string recoveryStartDate, string recoveryEndDate, int reportType)
        {
            // Param
            var branchId = UserContext.GetDefaultBranch();
            var handoverFromDate = handoverStartDate.ToDateTime("ddMMyyyy"); if (handoverFromDate.HasValue) handoverFromDate = handoverFromDate.Value.Date;
            var recoveryFromDate = recoveryStartDate.ToDateTime("ddMMyyyy"); if (recoveryFromDate.HasValue) recoveryFromDate = recoveryFromDate.Value.Date;
            var handoverToDate = handoverEndDate.ToDateTime("ddMMyyyy"); if (handoverToDate.HasValue) handoverToDate = handoverToDate.Value.Date.AddDays(1).AddSeconds(-1);
            var recoveryToDate = recoveryEndDate.ToDateTime("ddMMyyyy"); if (recoveryToDate.HasValue) recoveryToDate = recoveryToDate.Value.Date.AddDays(1).AddSeconds(-1);
            if (handoverFromDate.HasValue && !handoverToDate.HasValue) handoverToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            if (recoveryFromDate.HasValue && !recoveryToDate.HasValue) recoveryToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            // Source
            var listSource = TmpJobReportRepository.GetAllBC12(branchId, importId, channelId, handoverFromDate, handoverToDate, recoveryFromDate, recoveryToDate) ?? new List<TmpJobReportBC12Info>();
            var lstData = new List<BC12Model>();
            foreach (var item in listSource)
            {
                // CreatedBy
                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.CreatedBy) ?? UserRepository.GetInfo(item.CreatedBy);
                if (user == null) continue;

                // Import
                var import = StoreData.ListImportExcel.FirstOrDefault(c => c.ImportId == item.ImportId) ?? ImportExcelRepository.GetInfo(item.ImportId);

                // Channel
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == item.ChannelId) ?? ChannelRepository.GetInfo(item.ChannelId);

                // StatusMap
                var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapId) ?? StatusMapRepository.GetInfo(item.StatusMapId);

                var entity = new BC12Model
                {
                    FullName = item.FullName,
                    CreatedBy = user.FullName,
                    PhoneNumber = item.PhoneNumber,
                    ChannelName = channel != null ? channel.Name : string.Empty,
                    ImportName = import != null ? import.FilePath : string.Empty,
                    StatusMapName = statusMap != null ? statusMap.Name : string.Empty,
                    HandoverDate = item.HandoverDate.HasValue ? item.HandoverDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    RecoveryDate = item.RecoveryDate.HasValue ? item.RecoveryDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                };
                lstData.Add(entity);
            }

            // Init Report
            var infomation = " ";
            if (handoverFromDate.HasValue)
                infomation = "(Ngày bào giao từ: " + handoverFromDate.Value.ToString("dd/MM/yyyy") + " - Đến ngày: " + handoverToDate.Value.ToString("dd/MM/yyyy") + ")";
            else if (recoveryFromDate.HasValue)
                infomation = "(Ngày bào giao từ: " + recoveryFromDate.Value.ToString("dd/MM/yyyy") + " - Đến ngày: " + recoveryToDate.Value.ToString("dd/MM/yyyy") + ")";

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC12.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            var reportDataSource = new ReportDataSource("BC12", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>11in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";

            //Render the report             
            var renderedBytes = localReport.Render(type, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC12_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        //Thanghq
        public ActionResult BC41()
        {
            return View();
        }

        public ActionResult ShowBC41(int groupId, int userId, string from, string to, int reportType)
        {
            var branchId = UserContext.GetDefaultBranch();
            var dtFrom = from.ToDateTime() ?? DateTime.Now; dtFrom = dtFrom.Date;
            var dtTo = to.ToDateTime() ?? DateTime.Now; dtTo = dtTo.Date.AddDays(1).AddSeconds(-1);

            // Filter
            var lstContacts = ContactRepository.GetBC41(dtFrom, dtTo) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();
            if (groupId > 0) lstContacts = lstContacts.Where(c => c.GroupId == groupId).ToList();
            if (userId > 0) lstContacts = lstContacts.Where(c => c.UserConsultantId == userId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC41.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + dtFrom.ToString("dd/MM/yyyy") + " - Đến ngày: " + dtTo.ToString("dd/MM/yyyy") + ")"));

            var lstData = new List<BC41Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC41Model
                {
                    Fullname = info.Fullname,
                    Mobile = info.PhoneNumber,
                    Email = info.Email,
                    Level = info.LevelId,
                    CallConsultantDate = info.CallConsultantDate == null ? string.Empty : info.CallConsultantDate.Value.ToString("dd/MM/yyyy"),
                    Consultant = info.UserName,
                    SourceType = info.Name,
                    CampainTpe = info.CampaindTpe,
                    CampainTpeId = info.CampaindTpeId,
                    Code = info.Code
                };
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC41", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC41_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }
        /// <summary>
        /// Báo cáo nghiệm thu hotline
        /// </summary>
        public ActionResult BC40()
        {
            return View();
        }

        public ActionResult ShowBC40(string from, string to, int reportType)
        {
            var branchId = UserContext.GetDefaultBranch();
            var dtFrom = from.ToDateTime() ?? DateTime.Now; dtFrom = dtFrom.Date;
            var dtTo = to.ToDateTime() ?? DateTime.Now; dtTo = dtTo.Date.AddDays(1).AddSeconds(-1);

            // Filter
            var lstContacts = ContactRepository.GetBC40(dtFrom, dtTo) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC40.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + dtFrom.ToString("dd/MM/yyyy") + " - Đến ngày: " + dtTo.ToString("dd/MM/yyyy") + ")"));

            var lstData = new List<BC40Model>();
            foreach (var info in lstContacts)
            {
                var objModel = BCModelContact.Create<BC40Model>(info);
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC40", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC40_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC20()
        {
            return View();
        }
        public ActionResult ShowBC20(int groupId, int userId, string hanoverDate, int reportType)
        {
            var branchId = UserContext.GetDefaultBranch();
            var hanoverFDate = hanoverDate.ToDateTime() ?? DateTime.Now;
            hanoverFDate = hanoverFDate.Date;
            var qlct = UserContext.GetCurrentUser(); //lay thong tin id cua quan ly contact hien tai
            int id_user = qlct.UserID;
            var lstContacts = ContactRepository.GetBC20(hanoverFDate) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(c => c.BranchId == branchId).ToList();
            lstContacts = lstContacts.Where(c => c.UserImportId == id_user).ToList();
            if (groupId > 0) lstContacts = lstContacts.Where(c => c.GroupId == groupId).ToList();
            if (userId > 0) lstContacts = lstContacts.Where(c => c.UserConsultantId == userId).ToList();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC20.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Ngày: " + hanoverFDate.ToString("dd/MM/yyyy")));

            var lstData = new List<BC20Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC20Model
                {
                    FullName = info.Fullname,
                    Mobile = info.Mobile1,
                    Email = info.Email,
                    Address = info.Address,
                    RegisteredDate = info.RegisteredDate == null ? string.Empty : info.RegisteredDate.Value.ToString("dd/MM/yyyy"),
                    Campaind = info.CampaindTpe,
                    LandingPage = info.LandingPage,
                    Channel = info.Channel,
                    TemplateAds = info.TemplateAds,
                    SearchKeyword = info.SearchKeyword,
                    PackageName = info.Package,
                    ContactId = info.Code,
                    HandoverConsultantDate = info.HandoverConsultantDate == null ? string.Empty : info.HandoverConsultantDate.Value.ToString("dd/MM/yyyy"),
                    Consultant = info.UserName,
                    SourceType = info.SourceType
                };
                lstData.Add(objModel);
            }
            var reportDataSource = new ReportDataSource("BC20", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC20_" + hanoverFDate.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }
        public ActionResult BC42()
        {
            return View();
        }

        public ActionResult ShowBC42(int statusMapId, int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int qualityId, int productSellId, int employeeTypeId, int reportType)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            var userIds = user.UserID.ToString();
            var lstContacts = ContactRepository.GetBC42(branchId, userIds, statusMapId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, qualityId, productSellId, employeeType) ?? new List<ContactInfo>();
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC42.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Chuc nang in danh sach nhu crm cu"));
            var lstData = new List<BC42Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC42Model
                {
                    Fullname = info.Fullname,
                    Email = info.Email,
                    Level = info.Level,
                    Mobile = info.PhoneNumber,
                    AppointmentConsultantDate = info.AppointmentConsultantDate,
                    CallInfoConsultant = info.CallInfoConsultant,
                    CallConsultantDate = info.CallConsultantDate,
                    StatusCare = info.StatusCareConsultant,
                    Product = info.ProductSellName,
                    CallCount = info.CallCount
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC42", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC42_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC45()
        {
            ViewBag.Channels = ChannelRepository.GetAll();
            ViewBag.ListTemplateAds = TemplateAdsRepository.GetAll();
            return View();
        }

        public ActionResult ShowBC45(string handoverFromDate, string handoverToDate, string registerFromDate, string registerToDate, int channelId, int templateAds, int reportType)
        {

            var registerTDate = registerToDate.ToDateTime("ddMMyyyy");
            var registerFDate = registerFromDate.ToDateTime("ddMMyyyy");
            if (!registerTDate.HasValue) registerTDate = registerFDate;
            if (registerFDate.HasValue) registerTDate = registerTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            if (handoverTDate.IsDateTimeNull())
            {
                handoverTDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (handoverFDate.IsDateTimeNull())
            {
                handoverFDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (registerTDate.IsDateTimeNull())
            {
                registerTDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (registerFDate.IsDateTimeNull())
            {
                registerFDate = "01011900".ToDateTime("ddMMyyyy");
            }

            var lstDataReport = ContactRepository.GetBC45(handoverFDate, handoverTDate, registerFDate, registerTDate, channelId, templateAds);

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC45.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Bao cao chat luong contact"));
            var lstData = new List<BC45Model>();
            foreach (var info in lstDataReport)
            {
                var objModel = new BC45Model
                {
                    Channel = info.Channel,
                    TemplateAds = info.TemplateAds,
                    L1 = info.L1,
                    L2 = info.L2,
                    L3 = info.L3,
                    L4 = info.L4,
                    L5 = info.L5,
                    L6 = info.L6,
                    L7 = info.L7,
                    L8 = info.L8,
                    Sum = info.Sum,
                    L38Sum = info.L38Sum,
                    L8Sum = info.L8Sum,
                    SumL38 = info.SumL38
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC45", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC45_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);

        }

        public ActionResult BC46()
        {
            ViewBag.Channels = ChannelRepository.GetAll();
            ViewBag.ListTemplateAds = TemplateAdsRepository.GetAll();
            var StatusTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(StatusContact)))
                StatusTypes.Add((int)item, ObjectExtensions.GetEnumDescription((StatusContact)item));
            ViewBag.Status = new SelectList(StatusTypes, "Key", "Value");
            return View();
        }

        public ActionResult ShowBC46(string handoverFromDate, string handoverToDate, string registerFromDate, string registerToDate, int channelId, int reportType, int statustType)
        {

            var registerTDate = registerToDate.ToDateTime("ddMMyyyy");
            var registerFDate = registerFromDate.ToDateTime("ddMMyyyy");
            if (!registerTDate.HasValue) registerTDate = registerFDate;
            if (registerFDate.HasValue) registerTDate = registerTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            if (handoverTDate.IsDateTimeNull())
            {
                handoverTDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (handoverFDate.IsDateTimeNull())
            {
                handoverFDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (registerTDate.IsDateTimeNull())
            {
                registerTDate = "01011900".ToDateTime("ddMMyyyy");
            }

            if (registerFDate.IsDateTimeNull())
            {
                registerFDate = "01011900".ToDateTime("ddMMyyyy");
            }

            var lstDataReport = ContactRepository.GetBC46(handoverFDate, handoverTDate, registerFDate, registerTDate, channelId, statustType);

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC46.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Bao cao chat luong contact 2"));
            var lstData = new List<BC46Model>();
            foreach (var info in lstDataReport)
            {
                var objModel = new BC46Model
                {
                    Channel = info.Channel,
                    SumL = info.TONG,
                    Proportion = info.TyTrong,
                    L8 = info.L8,
                    L8Sum = info.L8Sum,
                    L38 = info.L38,
                    L38Sum = info.L38Sum
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC46", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC46_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);

        }


        public ActionResult BC47()
        {
            return View();
        }

        public ActionResult ShowBC47(string name, string mobile, string email, string userIds, string listedprice, int sourcetype, string datewanttolearn, string handoverfromdateadvisor, string handovertodateadvisor, int reportType)
        {
            var handoverFromDateAdvisor = handoverfromdateadvisor.ToDateTime("ddMMyyyy");
            var handoverToDateAdvisor = handovertodateadvisor.ToDateTime("ddMMyyyy");
            if (!handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverFromDateAdvisor;
            if (handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverToDateAdvisor.Value.AddDays(1).AddSeconds(-1);
            var dateWantToLearn = datewanttolearn.ToDateTime("ddMMyyyy");

            var lstDataReport = ContactRepository.GetBC47(name, mobile, email, userIds, listedprice, sourcetype, dateWantToLearn, handoverFromDateAdvisor, handoverToDateAdvisor);

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC47.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Danh sách contact bàn giao thành công"));
            var lstData = new List<BC47Model>();
            foreach (var info in lstDataReport)
            {
                var objModel = new BC47Model
                {
                    HandoverAdvisorSuccessTime = info.HandoverAdvisorSuccessTime,
                    UserName = info.UserName,
                    Product = info.Product,
                    FullName = info.Fullname,
                    Code = info.Code,
                    Email = info.Email,
                    FeeEdu = info.FeeEdu.ToString("#,##0"),
                    LearnNumberDay = info.LearnNumberDay,
                    LevelId = info.LevelId,
                    Package = info.Package,
                    PackagePriceSale = info.PackagePriceSale.ToString("#,##0"),
                    sHandoverStatusAdvisor = "Bàn giao thành công",
                    SourceType = info.SourceType,
                    PricePackageListedId = info.PricePackageListedId.ToString("#,##0"),
                    PhoneNumber = info.PhoneNumber
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC47", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC47_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC48()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowBC48(FormCollection forms, HttpPostedFileBase excelfile)
        {
            if (excelfile.FileName.EndsWith(".xls") || excelfile.FileName.EndsWith(".xlsx"))
            {
                var now = DateTime.Now;
                var fullFileDir = Server.MapPath("/UploadFileQuality");
                var fullFilePath = Server.MapPath("/UploadFileQuality/") + string.Format("{0}_{1}_{2}_{3}_{4}_{5}_", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + excelfile.FileName;
                if (!Directory.Exists(fullFileDir)) Directory.CreateDirectory(fullFileDir);
                excelfile.SaveAs(fullFilePath);

                using (var stream = System.IO.File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var dr = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        List<BC48Model> lstCts = new List<BC48Model>();
                        while (dr.Read())
                        {
                            var phone = dr[0] == null ? string.Empty : dr[0].ToString().Trim();
                            ContactInfo cts = ContactRepository.GetInfoByMobile(phone);
                            if (cts != null)
                            {
                                BC48Model c = new BC48Model();

                                c.FullName = cts.Fullname;
                                c.Mobile = cts.Mobile1;
                                c.LevelId = cts.LevelId;
                                c.Email = String.IsNullOrEmpty(cts.Email) ? " " : cts.Email;
                                c.StatusCare = String.IsNullOrEmpty(cts.StatusCare) ? " " : cts.StatusCare;
                                c.StatusMap = String.IsNullOrEmpty(cts.StatusMap) ? " " : cts.StatusMap;
                                c.Consultant = cts.Consultant;
                                c.Channel = cts.Channel;
                                c.RegisterDate = cts.RegisteredDate;
                                c.TemplateAds = cts.TemplateAds;
                                c.Notes = cts.Notes;
                                c.SearchKeyword = cts.SearchKeyword;
                                c.HandoverCosulant = cts.HandoverConsultantDate;
                                c.Acceptance = cts.LevelId == 1 ? "Không" : "Có";
                                c.CallCount = cts.CallCount;
                                c.CallInfoConsultant = cts.CallInfoConsultant;
                                c.CallConsulatDate = cts.CallConsultantDate;
                                lstCts.Add(c);
                            }
                        }
                        var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC48.rdlc") };
                        localReport.SetParameters(new ReportParameter("Infomation", "Xuất chất lượng"));
                        var reportDataSource = new ReportDataSource("BC48", lstCts);
                        localReport.DataSources.Add(reportDataSource);

                        string[] streams;
                        Warning[] warnings;
                        string mimeType, encoding, fileNameExtension, type = "excel";
                        var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        //Render the report             
                        return File(renderedBytes, mimeType, "BC48_" + DateTime.Now.ToString() + ".xls");
                    }
                }
            }
            else
            {
                ViewBag.Message = "InvalidFileFormat";
            }
            return View();
        }

        public ActionResult ShowBC49(string from, string to, int type, string levels, string userIds, int employeeTypeId)
        {
            var fromDate = string.IsNullOrEmpty(from)
                ? DateTime.Now.Date
                : DateTime.ParseExact(from, "ddMMyyyy", CultureInfo.InvariantCulture);
            var toDate = string.IsNullOrEmpty(to)
                ? fromDate.AddDays(1).AddSeconds(-1)
                : DateTime.ParseExact(to, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
            if (string.IsNullOrEmpty(levels)) levels = string.Empty;
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            int totalRecords;

            var result = new ContactListModel();
            switch (type)
            {
                case (int)TodayType.NewAndAppointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNewAndAppointment(userIds, fromDate, toDate, branchId, employeeType, 1, 10000, out totalRecords),
                        };
                    }
                    break;
                case (int)TodayType.Appointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAppointment(userIds, fromDate, toDate, levels, branchId, employeeType, 1, 10000, out totalRecords),
                        };
                    }
                    break;
                case (int)TodayType.New:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNew(userIds, fromDate, toDate, branchId, employeeType, 1, 10000, out totalRecords)
                        };
                    }
                    break;
                case (int)TodayType.All:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAll(userIds, fromDate, toDate, levels, branchId, employeeType, 1, 10000, out totalRecords)
                        };
                    }
                    break;
            }

            if (result.Rows.Any())
            {
                foreach (var item in result.Rows.Where(c => c.AppointmentConsultantDate == DateTime.MinValue))
                {
                    item.AppointmentConsultantDate = item.HandoverConsultantDate;
                }
            }

            var localReport = new LocalReport { ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Areas/Admin/Reports/rptBC49.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Thông tin contact"));
            var reportDataSource = new ReportDataSource("BC49", result.Rows);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension;
            var renderedBytes = localReport.Render("excel", string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Render the report             
            return File(renderedBytes, mimeType, "Baocao" + DateTime.Now.ToString() + ".xls");
        }

        // báo cáo tổng hợp chất lượng contact
        [HttpGet]
        public ActionResult ReportQuality_Contact()
        {
            return View();
        }

        // xuất báo báo cáo tổng hợp chất lượng contact
        public ActionResult ShowReportQuality_Contact(string from, string to, int reportType)
        {
            var fromDate = from.ToDateTime() ?? DateTime.Now;
            var toDate = to.ToDateTime() ?? DateTime.Now;
            toDate = toDate.AddDays(1).AddSeconds(-1);
            var lstDataReport = TmpJobReportRepository.ReportQuality_Contact(fromDate, toDate);

            var lstData = new List<ReportContactModel>();

            foreach (var item in lstDataReport)
            {
                var bc = new ReportContactModel
                {
                    GroupName = item.GroupName,
                    TVTS = item.TVTS,
                    Handover = item.Handover,
                    Calls = item.Calls,
                    NotCalls = item.NotCalls,
                    Wrong = item.Wrong,
                    Incorrect = item.Incorrect,
                    NotConnected = item.NotConnected,
                    MissCall = item.MissCall,
                    Busy = item.Busy,
                    NotAcceptance = item.NotCalls + item.Incorrect + item.Wrong + item.NotConnected,
                    ProportionNotAcceptance = (item.Calls != 0) ? (item.NotCalls + item.Incorrect + item.Wrong + item.NotConnected) / item.Calls : 0,
                    L2 = item.L2,
                    L3 = item.L3,
                    L6 = item.L6,
                    L8 = item.L8,
                    ProportionL2 = (item.Calls != 0) ? ((item.L2 / item.Calls) * 100) : 0,
                    ProportionL3 = (item.Calls != 0) ? ((item.L3 / item.Calls) * 100) : 0,
                    ProportionL6 = (item.Calls != 0) ? ((item.L6 / item.Calls)) * 100 : 0,
                    ProportionL8 = (item.Calls != 0) ? ((item.L8 / item.Calls)) * 100 : 0
                };
                lstData.Add(bc);
            }

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptQualityContact.rdlc") };
            var reportDataSource = new ReportDataSource("ReportTotal", lstData);
            localReport.DataSources.Add(reportDataSource);
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + fromDate.ToString("dd/MM/yyyy") + " - Đến ngày: " + toDate.ToString("dd/MM/yyyy") + ")"));
            localReport.SetParameters(new ReportParameter("CreatedDate", "(Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy") + ")"));

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "ReportQuality_Contact_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        // báo cáo nghiệm thu contact lọc
        [HttpGet]
        public ActionResult ReportFilter_Acceptance()
        {
            return View();
        }

        // xuất báo báo cáo nghiệm thu contact lọc
        public ActionResult ShowReportFilter_Acceptance(string from, string to, int reportType)
        {
            var fromDate = from.ToDateTime() ?? DateTime.Now;
            var toDate = to.ToDateTime() ?? DateTime.Now;
            toDate = toDate.AddDays(1).AddSeconds(-1);
            var lstDataReport = TmpJobReportRepository.ReportFilter_Acceptance(fromDate, toDate);

            var lstData = new List<ReportContactModel>();

            foreach (var item in lstDataReport)
            {
                var bc = new ReportContactModel
                {
                    GroupName = item.GroupName,
                    TVTS = item.TVTS,
                    Handover = item.Handover,
                    Calls = item.Calls,
                    NotCalls = item.NotCalls,
                    Wrong = item.Wrong,
                    Incorrect = item.Incorrect,
                    NotConnected = item.NotConnected,
                    MissCall = item.MissCall,
                    Busy = item.Busy,
                    NotAcceptance = item.NotCalls + item.Incorrect + item.Wrong + item.NotConnected,
                    ProportionNotAcceptance = (item.Calls != 0) ? (item.NotCalls + item.Incorrect + item.Wrong + item.NotConnected) / item.Calls : 0,
                    L2 = item.L2 + item.L3 + item.L4 + item.L5 + item.L6 + item.L7 + item.L8,
                    L3 = item.L3 + item.L4 + item.L5 + item.L6 + item.L7 + item.L8,
                    L6 = item.L6 + item.L7 + item.L8,
                    L8 = item.L8,
                    ProportionL2 = (item.Calls != 0) ? (((item.L2 + item.L3 + item.L4 + item.L5 + item.L6 + item.L7 + item.L8) / item.Calls) * 100) : 0,
                    ProportionL3 = (item.Calls != 0) ? (((item.L3 + item.L4 + item.L5 + item.L6 + item.L7 + item.L8) / item.Calls) * 100) : 0,
                    ProportionL6 = (item.Calls != 0) ? (((item.L6 + item.L7 + item.L8) / item.Calls)) * 100 : 0,
                    ProportionL8 = (item.Calls != 0) ? ((item.L8 / item.Calls)) * 100 : 0
                };
                lstData.Add(bc);
            }

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptFilterAcceptance.rdlc") };
            var reportDataSource = new ReportDataSource("ReportTotal", lstData);
            localReport.DataSources.Add(reportDataSource);
            localReport.SetParameters(new ReportParameter("Infomation", "(Từ ngày: " + fromDate.ToString("dd/MM/yyyy") + " - Đến ngày: " + toDate.ToString("dd/MM/yyyy") + ")"));
            localReport.SetParameters(new ReportParameter("CreatedDate", "(Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy") + ")"));

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "ReportFilter_Acceptance_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC50()
        {
            return View();
        }

        public ActionResult ShowBC50(int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int employeeTypeId, int reportType)
        {
            int branchId = UserContext.GetDefaultBranch();
            EmployeeType employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            var lstContacts = ContactRepository.GetBC50(branchId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, employeeType) ?? new List<ContactInfo>();
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC50.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Báo cáo cc3"));
            var lstData = new List<BC50Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC50Model
                {
                    FullName = info.Fullname,
                    PhoneNumber = info.PhoneNumber,
                    Email = info.Email,
                    Level = info.LevelCC3,
                    CallInfoCollaborator = info.CallInfoCollaborator,
                    HandoverCollaboratorDate = info.HandoverCollaboratorDate,
                    CallCollaboratorDate = info.CallCollaboratorDate,
                    StatusCareCollaborator = info.StatusCareCollaborator,
                    UserCollaborator = info.UserCollaborator,
                    CallCount = info.CallCount
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC50", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC50_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC51()
        {
            return View();
        }

        public ActionResult ShowBC51(int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int employeeTypeId, int reportType)
        {
            int branchId = UserContext.GetDefaultBranch();
            int userCollaboratorId = UserContext.GetCurrentUser().UserID;

            EmployeeType employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            var lstContacts = ContactRepository.GetBC50(branchId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, employeeType) ?? new List<ContactInfo>();
            lstContacts = lstContacts.Where(x => x.UserCollaboratorId == userCollaboratorId).ToList();
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC50.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Báo cáo cc3 cho CTV lọc"));
            var lstData = new List<BC50Model>();
            foreach (var info in lstContacts)
            {
                var objModel = new BC50Model
                {
                    FullName = info.Fullname,
                    PhoneNumber = info.PhoneNumber,
                    Email = info.Email,
                    Level = info.LevelCC3,
                    CallInfoCollaborator = info.CallInfoCollaborator,
                    HandoverCollaboratorDate = info.HandoverCollaboratorDate,
                    CallCollaboratorDate = info.CallCollaboratorDate,
                    StatusCareCollaborator = info.StatusCareCollaborator,
                    UserCollaborator = info.UserCollaborator,
                    CallCount = info.CallCount
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC50", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC51_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult BC52()
        {
            return View();
        }

        public ActionResult ShowBC52(string callFromDate, string callToDate, string userids, int employeeTypeId, int reportType)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            //var userIds = user.UserID.ToString();
            var lstContacts = ContactRepository.GetCallHistoryReport(userids, callFDate, callTDate) ?? new List<ContactInfo>();
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptCallReport.rdlc") };
            //localReport.SetParameters(new ReportParameter("Infomation", "Chuc nang xuat lich su cuoc goi"));
            var lstData = new List<BCCallHistoryModel>();
            foreach (var info in lstContacts)
            {
                var objModel = new BCCallHistoryModel
                {
                    AgentCode = info.AgentCode,
                    CallCount = info.CallCount.ToString(),
                    Duration = info.Duration.ToString(),
                    RingTime = info.RingTime.ToString(),
                    SumCallInfo = (info.Duration + info.RingTime).ToString()
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC52", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BaoCaoLichSuCuocGoi_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }


        //Báo cáo khối lượng công việc ver 2.0
        public ActionResult BC53()
        {
            return View();
        }
        public ActionResult ShowBC53(string groupId, int userId, int branchId, string from, string to, int reportType)
        {
            // Param datetime
            var dtFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

            //var listUser = StoreData.ListUser.AsReadOnly();
            List<UserInfo> listUser = new List<UserInfo>();
            //listUser = StoreData.ListUser.AsReadOnly().ToList();
            string[] groupIds = Regex.Split(groupId, @",");
            foreach (string value in groupIds)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    var listUserFake = StoreData.ListUser.AsReadOnly();
                    if (i > 0)
                    {
                        listUserFake = listUserFake.Where(c => c.GroupId == i).ToList().AsReadOnly();
                    }
                    listUser.AddRange(listUserFake);
                }
            }
            //if (groupId > 0) listUser = listUser.Where(c => c.GroupId == groupId).ToList().AsReadOnly();

            // Load & filter data
            var lstDataTemp = TmpJobReportRepository.GetAllBC53(dtFrom, dtTo) ?? new List<TmpJobReportBC53>();

            if (userId > 0) lstDataTemp = lstDataTemp.Where(c => c.TVTS == userId).ToList();
            if (branchId > 0) lstDataTemp = lstDataTemp.Where(c => c.ChiNhanh == branchId).ToList();

            var listDataSource = new List<BC53Model>();

            var groupSum = string.Empty;
            const string userSum = "Tổng";

            listDataSource.Insert(0, new BC53Model
            {
                Nhom = groupSum,
                ChiNhanh = 4,
                TongCuocGoi = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.TongCuocGoi),
                ContactTon = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.PhaiGoiTheoLich) - lstDataTemp.Sum(c => c.TrongLich),
                ContactTuongTac = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.ContactTuongTac),
                NgoaiLich = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.NgoaiLich),
                PhaiGoiTheoLich = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.PhaiGoiTheoLich),
                TrongLich = lstDataTemp.IsNullOrEmpty() ? 0 : lstDataTemp.Sum(c => c.TrongLich),
                TVTS = "Tổng"
            });

            foreach (var item in lstDataTemp)
            {
                var user = listUser.FirstOrDefault(c => c.UserID == item.TVTS);
                if (user == null) continue;

                if (groupId.ToInt32() > 0 && user.GroupId != groupId.ToInt32()) continue;

                listDataSource.Add(new BC53Model
                {
                    Nhom = user.GroupName,
                    ChiNhanh = item.ChiNhanh,
                    TongCuocGoi = item.TongCuocGoi,
                    ContactTon = Int32.Parse(item.PhaiGoiTheoLich.ToString()) - Int32.Parse(item.TrongLich.ToString()),
                    ContactTuongTac = item.ContactTuongTac,
                    NgoaiLich = item.NgoaiLich,
                    PhaiGoiTheoLich = item.PhaiGoiTheoLich,
                    TrongLich = item.TrongLich,
                    TVTS = user.UserName
                });
            }

            var reportDataSource = new ReportDataSource("BC53", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC53.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", " "));

            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;

            string[] streams;
            Warning[] warnings;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }

            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11in</PageHeight>" +
                                      "  <MarginTop>0.2in</MarginTop>" +
                                      "  <MarginLeft>0.2in</MarginLeft>" +
                                      "  <MarginRight>0.2in</MarginRight>" +
                                      "  <MarginBottom>0.2in</MarginBottom>" +
                                      "</DeviceInfo>";
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BaoCaoKhoiLuongCongViec_" + dtFrom.ToString("ddMMyyyy") + ".xls");
            }

            localReport.ShowDetailedSubreportMessages = false;

            return File(renderedBytes, mimeType);
        }

        public ActionResult BC54()
        {
            var dayReportTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(DayReportType)))
                dayReportTypes.Add((int)item, ObjectExtensions.GetEnumDescription((DayReportType)item));
            ViewBag.DayReportTypes = new SelectList(dayReportTypes, "Key", "Value");
            var StatusTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(StatusContact)))
                StatusTypes.Add((int)item, ObjectExtensions.GetEnumDescription((StatusContact)item));
            ViewBag.Status = new SelectList(StatusTypes, "Key", "Value");
            return View();
        }

        public ActionResult ShowBC54(int dayReportType, string groupId, int userId, int branchId, int statusType, string date, string hFromDate, string hToDate, int reportType)
        {
            // Param datetime
            var dtTo = hToDate.ToDateTime() ?? DateTime.Now;
            var dtFrom = hFromDate.ToDateTime() ?? DateTime.Now;

            List<UserInfo> listUser = new List<UserInfo>();
            string[] groupIds = Regex.Split(groupId, @",");

            foreach (string value in groupIds)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    var listUserFake = StoreData.ListUser.AsReadOnly();
                    if (i > 0)
                    {
                        listUserFake = listUserFake.Where(c => c.GroupId == i).ToList().AsReadOnly();
                    }
                    listUser.AddRange(listUserFake);
                }
            }

            var infomation = "Ngày bàn giao từ ngày: {0} - đến ngày: {1}";
            switch ((DayReportType)dayReportType)
            {
                case DayReportType.Day:
                    dtTo = hToDate.ToDateTime() ?? DateTime.Now.Date;
                    dtFrom = hFromDate.ToDateTime() ?? DateTime.Now.Date;
                    break;
                case DayReportType.Week:
                    dtTo = dtFrom.AddDays(7 - (int)dtFrom.DayOfWeek);
                    dtFrom = dtFrom.AddDays(-(int)dtFrom.DayOfWeek + 1);
                    break;
                case DayReportType.Month:
                    dtTo = new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month));
                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);
                    infomation = "Tháng: " + dtFrom.Month + " - " + infomation;
                    break;
                case DayReportType.Year:
                    dtTo = new DateTime(dtFrom.Year, 12, 31);
                    dtFrom = new DateTime(dtFrom.Year, 12, 1);
                    infomation = "Năm: " + dtFrom.Year + " - " + infomation;
                    break;
            }

            if (dtTo == DateTime.MinValue || dtTo > DateTime.Now.Date) dtTo = DateTime.Now.Date;
            if (dtFrom == DateTime.MinValue || dtFrom > DateTime.Now.Date) dtFrom = DateTime.Now.Date;
            var handoverToDate = dtTo;
            var handoverFromDate = dtFrom;
            handoverToDate = handoverToDate.AddDays(1).AddSeconds(-1);

            // Load & filter data
            var dateTime = date.ToDateTime() ?? DateTime.Now.Date;
            var lstData = TmpJobReportRepository.GetAllBC300(dateTime) ?? new List<TmpJobReportBC300Info>();

            if (userId > 0) lstData = lstData.Where(c => c.UserConsultantId == userId).ToList();
            if (branchId > 0) lstData = lstData.Where(c => c.BranchId == branchId).ToList();

            if (!hFromDate.IsStringNullOrEmpty() || !hToDate.IsStringNullOrEmpty())
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }
            else if ((DayReportType)dayReportType == DayReportType.Lumped || (DayReportType)dayReportType == DayReportType.Latch)
            {
                infomation = string.Format(infomation, dtFrom.ToString("dd/MM/yyyy"), dtTo.ToString("dd/MM/yyyy"));
                lstData = lstData
                    .Where(c => c.HandoverConsultantDate >= handoverFromDate)
                    .Where(c => c.HandoverConsultantDate <= handoverToDate)
                    .ToList();
            }

            else infomation = "Ngày xem báo cáo: " + dateTime.ToString("dd/MM/yyyy");
            var listDataSource = new List<BC300Model>();

            #region tính tổng tỷ lệ cho toàn bảng
            {
                var listTemp = lstData.Where(c => listUser.Any(p => c.UserConsultantId == p.UserID)).ToList();
                var level1 = listTemp.Where(c => c.LevelId >= (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level2 = listTemp.Where(c => c.LevelId >= (int)LevelType.L2 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level3 = listTemp.Where(c => c.LevelId >= (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level4 = listTemp.Where(c => c.LevelId >= (int)LevelType.L4 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level5 = listTemp.Where(c => c.LevelId >= (int)LevelType.L5 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level6 = listTemp.Where(c => c.LevelId >= (int)LevelType.L6 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level7 = listTemp.Where(c => c.LevelId >= (int)LevelType.L7 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level8 = listTemp.Where(c => c.LevelId >= (int)LevelType.L8 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var entity = new BC300Model
                {
                    User = "Tổng",
                    Group = string.Empty,
                    Name = "Tỷ lệ %(Lx/Ly)",
                    Level1 = (level1 > 0 ? Math.Round((double)level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level2 = (level1 > 0 ? Math.Round((double)(level2) / level1 * 100, 1) : 0) + "%",
                    Level3 = (level2 > 0 ? Math.Round((double)(level3) / level2 * 100, 1) : 0).ToString("0.0") + "%",
                    Level4 = (level3 > 0 ? Math.Round((double)(level4) / level3 * 100, 1) : 0).ToString("0.0") + "%",
                    Level5 = (level4 > 0 ? Math.Round((double)(level5) / level4 * 100, 1) : 0).ToString("0.0") + "%",
                    Level6 = (level5 > 0 ? Math.Round((double)(level6) / level5 * 100, 1) : 0).ToString("0.0") + "%",
                    Level7 = (level6 > 0 ? Math.Round((double)(level7) / level6 * 100, 1) : 0).ToString("0.0") + "%",
                    Level8 = (level7 > 0 ? Math.Round((double)(level8) / level7 * 100, 1) : 0).ToString("0.0") + "%",
                    NameArise = "Tổng Số Contact",
                };
                listDataSource.Add(entity);

            }
            #endregion

            #region tính cho từng TVTS

            foreach (var id in lstData.Select(c => c.UserConsultantId).Distinct())
            {
                var user = listUser.FirstOrDefault(p => p.UserID == id);
                if (user == null) continue;

                var listTemp = lstData.Where(c => c.UserConsultantId == user.UserID).ToList();
                var level1 = listTemp.Where(c => c.LevelId >= (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level2 = listTemp.Where(c => c.LevelId >= (int)LevelType.L2 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level3 = listTemp.Where(c => c.LevelId >= (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level4 = listTemp.Where(c => c.LevelId >= (int)LevelType.L4 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level5 = listTemp.Where(c => c.LevelId >= (int)LevelType.L5 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level6 = listTemp.Where(c => c.LevelId >= (int)LevelType.L6 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level7 = listTemp.Where(c => c.LevelId >= (int)LevelType.L7 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var level8 = listTemp.Where(c => c.LevelId >= (int)LevelType.L8 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                //So level o thoi diem hien tai
                var L1 = listTemp.Where(c => c.LevelId == (int)LevelType.L1 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L2 = listTemp.Where(c => c.LevelId == (int)LevelType.L2 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L3 = listTemp.Where(c => c.LevelId == (int)LevelType.L3 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L4 = listTemp.Where(c => c.LevelId == (int)LevelType.L4 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L5 = listTemp.Where(c => c.LevelId == (int)LevelType.L5 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L6 = listTemp.Where(c => c.LevelId == (int)LevelType.L6 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L7 = listTemp.Where(c => c.LevelId == (int)LevelType.L7 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);
                var L8 = listTemp.Where(c => c.LevelId == (int)LevelType.L8 && c.LevelId < (int)LevelType.L3M).Sum(c => c.Count);

                var entity = new BC300Model
                {
                    User = user.FullName,
                    Group = user.GroupName,
                    Name = "Tỷ lệ %(Lx/Ly)",
                    Level1 = (level1 > 0 ? Math.Round((double)level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level2 = (level1 > 0 ? Math.Round((double)(level2) / level1 * 100, 1) : 0) + "%",
                    Level3 = (level1 > 0 ? Math.Round((double)(level3) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level4 = (level1 > 0 ? Math.Round((double)(level4) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level5 = (level1 > 0 ? Math.Round((double)(level5) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level6 = (level1 > 0 ? Math.Round((double)(level6) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level7 = (level1 > 0 ? Math.Round((double)(level7) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    Level8 = (level1 > 0 ? Math.Round((double)(level8) / level1 * 100, 1) : 0).ToString("0.0") + "%",
                    NameArise = "Tổng Số Contact",
                    Arise_L1 = (L1).ToString(),
                    Arise_L2 = (L2).ToString(),
                    Arise_L3 = (L3).ToString(),
                    Arise_L4 = (L4).ToString(),
                    Arise_L5 = (L5).ToString(),
                    Arise_L6 = (L6).ToString(),
                    Arise_L7 = (L7).ToString(),
                    Arise_L8 = (L8).ToString(),
                };
                listDataSource.Add(entity);
            }

            #endregion


            //#region tính trung bình cho toàn nhóm
            //// Trung bình
            //var groups = listDataSource
            //    .Where(c => !c.Group.IsStringNullOrEmpty())
            //    .Select(c => c.Group)
            //    .Distinct()
            //    .ToList();
            //foreach (var group in groups)
            //{
            //    var listTemp = listDataSource
            //        .Where(c => c.Group == group)
            //        .Where(c => c.Name == "Số lượng (contact)")
            //        .ToList();
            //    if (listTemp.IsNullOrEmpty()) continue;

            //    var level1 = listTemp.Sum(c => c.Level1.ToDecimal());
            //    var level2 = listTemp.Sum(c => c.Level2.ToDecimal());
            //    var level3 = listTemp.Sum(c => c.Level3.ToDecimal());
            //    var level4 = listTemp.Sum(c => c.Level4.ToDecimal());
            //    var level5 = listTemp.Sum(c => c.Level5.ToDecimal());
            //    var level6 = listTemp.Sum(c => c.Level6.ToDecimal());
            //    var level7 = listTemp.Sum(c => c.Level7.ToDecimal());
            //    var level8 = listTemp.Sum(c => c.Level8.ToDecimal());
            //    var entity = new BC300Model
            //    {
            //        Group = group,
            //        User = "Trung bình",
            //        Name = "Tỷ lệ %(Lx/Ly)",
            //        Level1 = (level1 > 0 ? Math.Round(level1 / level1 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level2 = (level1 > 0 ? Math.Round((level2) / level1 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level3 = (level2 > 0 ? Math.Round((level3) / level2 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level4 = (level3 > 0 ? Math.Round((level4) / level3 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level5 = (level4 > 0 ? Math.Round((level5) / level4 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level6 = (level5 > 0 ? Math.Round((level6) / level5 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level7 = (level6 > 0 ? Math.Round((level7) / level6 * 100, 1) : 0).ToString("0.0") + "%",
            //        Level8 = (level7 > 0 ? Math.Round((level8) / level7 * 100, 1) : 0).ToString("0.0") + "%",
            //        NameArise = "Tổng Số Contact",
            //        Arise_L1 = level1.ToString(),
            //        Arise_L2 = (level2).ToString(),
            //        Arise_L3 = (level3).ToString(),
            //        Arise_L4 = (level4).ToString(),
            //        Arise_L5 = (level5).ToString(),
            //        Arise_L6 = (level6).ToString(),
            //        Arise_L7 = (level7).ToString(),
            //        Arise_L8 = (level8).ToString(),
            //    };
            //    listDataSource.Add(entity);
            //}

            //#endregion


            //#region tính tổng số lượng contact của toàn bản báo cáo

            //var listTempTotal = listDataSource
            //       .Where(c => c.Name == "Số lượng (contact)")
            //       .ToList();
            //var listTempTotalArise = listDataSource
            //       .Where(c => c.Name == "Số lượng(contact phát sinh)")
            //       .ToList();
            //var entityTotal = new BC300Model
            //{
            //    User = "Tổng",
            //    Group = string.Empty,
            //    Name = "Số lượng (contact)",
            //    NameArise = "Số lượng(contact phát sinh)",
            //    Level1 = listTempTotal.Sum(c => c.Level1.ToDecimal()).ToString("0"),
            //    Level2 = listTempTotal.Sum(c => c.Level2.ToDecimal()).ToString("0"),
            //    Level3 = listTempTotal.Sum(c => c.Level3.ToDecimal()).ToString("0"),
            //    Level4 = listTempTotal.Sum(c => c.Level4.ToDecimal()).ToString("0"),
            //    Level5 = listTempTotal.Sum(c => c.Level5.ToDecimal()).ToString("0"),
            //    Level6 = listTempTotal.Sum(c => c.Level6.ToDecimal()).ToString("0"),
            //    Level7 = listTempTotal.Sum(c => c.Level7.ToDecimal()).ToString("0"),
            //    Level8 = listTempTotal.Sum(c => c.Level8.ToDecimal()).ToString("0"),
            //    Level9 = listTempTotal.Sum(c => c.Level9.ToDecimal()).ToString("0"),

            //    Arise_L1 = listTempTotal.Sum(c => c.Arise_L1.ToDecimal()).ToString("0"),
            //    Arise_L2 = listTempTotal.Sum(c => c.Arise_L2.ToDecimal()).ToString("0"),
            //    Arise_L3 = listTempTotal.Sum(c => c.Arise_L3.ToDecimal()).ToString("0"),
            //    Arise_L4 = listTempTotal.Sum(c => c.Arise_L4.ToDecimal()).ToString("0"),
            //    Arise_L5 = listTempTotal.Sum(c => c.Arise_L5.ToDecimal()).ToString("0"),
            //    Arise_L6 = listTempTotal.Sum(c => c.Arise_L6.ToDecimal()).ToString("0"),
            //    Arise_L7 = listTempTotal.Sum(c => c.Arise_L7.ToDecimal()).ToString("0"),
            //    Arise_L8 = listTempTotal.Sum(c => c.Arise_L8.ToDecimal()).ToString("0"),
            //    Arise_L9 = listTempTotal.Sum(c => c.Arise_L9.ToDecimal()).ToString("0"),


            //};
            //listDataSource.Insert(1, entityTotal);
            //BC300Model mtamp1 = listDataSource[0],
            //    mtamp2 = listDataSource[1];
            //mtamp1.Arise_L1 = mtamp2.Level1;
            //{

            //    int l1 = int.Parse(mtamp2.Level1) + int.Parse(mtamp2.Arise_L1), l2 = int.Parse(mtamp2.Level2) + int.Parse(mtamp2.Arise_L2), l3 = int.Parse(mtamp2.Level3) + int.Parse(mtamp2.Arise_L3), l4 = int.Parse(mtamp2.Level4) + int.Parse(mtamp2.Arise_L4),
            //        l5 = int.Parse(mtamp2.Level5) + int.Parse(mtamp2.Arise_L5), l6 = int.Parse(mtamp2.Level6) + int.Parse(mtamp2.Arise_L6), l7 = int.Parse(mtamp2.Level7) + int.Parse(mtamp2.Arise_L7),
            //        l8 = int.Parse(mtamp2.Level8) + int.Parse(mtamp2.Arise_L8), l9 = int.Parse(mtamp2.Level9) + int.Parse(mtamp2.Arise_L9);
            //    mtamp1.Level1 = string.Format("{0:0.00}%", l1 != 0 ? ((float)l1 * 100 / l1) : 0.0f);
            //    mtamp1.Level2 = string.Format("{0:0.00}%", l1 != 0 ? ((float)l2 * 100 / l1) : 0.0f);
            //    mtamp1.Level3 = string.Format("{0:0.00}%", l2 != 0 ? ((float)l3 * 100 / l2) : 0.0f);
            //    mtamp1.Level4 = string.Format("{0:0.00}%", l3 != 0 ? ((float)l4 * 100 / l3) : 0.0f);
            //    mtamp1.Level5 = string.Format("{0:0.00}%", l4 != 0 ? ((float)l5 * 100 / l4) : 0.0f);
            //    mtamp1.Level6 = string.Format("{0:0.00}%", l5 != 0 ? ((float)l6 * 100 / l5) : 0.0f);
            //    mtamp1.Level7 = string.Format("{0:0.00}%", l6 != 0 ? ((float)l7 * 100 / l6) : 0.0f);
            //    mtamp1.Level8 = string.Format("{0:0.00}%", l7 != 0 ? ((float)l8 * 100 / l7) : 0.0f);
            //    mtamp1.Level9 = string.Format("{0:0.00}%", l8 != 0 ? ((float)l9 * 100 / l8) : 0.0f);
            //    mtamp1.Arise_L1 = l1.ToString();
            //    mtamp1.Arise_L2 = l2.ToString();
            //    mtamp1.Arise_L3 = l3.ToString();
            //    mtamp1.Arise_L4 = l4.ToString();
            //    mtamp1.Arise_L5 = l5.ToString();
            //    mtamp1.Arise_L6 = l6.ToString();
            //    mtamp1.Arise_L7 = l7.ToString();
            //    mtamp1.Arise_L8 = l8.ToString();
            //    mtamp1.Arise_L9 = l9.ToString();
            //}
            //listDataSource.RemoveAt(0);
            //listDataSource.Insert(0, mtamp1);

            //#endregion


            if (listDataSource.Count <= 2) listDataSource.Clear();
            var reportDataSource = new ReportDataSource("BC300", listDataSource);
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptTyLeLevel.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", infomation));
            localReport.DataSources.Add(reportDataSource);
            string mimeType;
            string encoding;
            string fileNameExtension;
            //const string reportType = "pdf";
            const string deviceInfo = "<DeviceInfo>" +
                                       "  <OutputFormat>PDF</OutputFormat>" +
                                       "  <PageWidth>11in</PageWidth>" +
                                       "  <PageHeight>8.5in</PageHeight>" +
                                       "  <MarginTop>0.5in</MarginTop>" +
                                       "  <MarginLeft>0.2in</MarginLeft>" +
                                       "  <MarginRight>0.2in</MarginRight>" +
                                       "  <MarginBottom>1in</MarginBottom>" +
                                       "</DeviceInfo>";
            string[] streams;
            Warning[] warnings;
            string type_bc = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type_bc = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type_bc = "excel";
                    break;
            }
            var renderedBytes = localReport.Render(
                type_bc,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC300_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }

        public ActionResult ShowBC55(int branchId, string importDate, int importId)
        {
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            var importFDate = importDate.ToDateTime("ddMMyyyy");
            var data = ContactRepository.SearchDuplicateMo(branchId, importFDate, importId);

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC55.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Báo cáo cc3"));
            var lstData = new List<BC55Model>();

            foreach (var info in data)
            {
                var objModel = new BC55Model
                {
                   FullName = info.Fullname,
                   Mobile = info.Mobile1,
                   Email = info.Email,
                   DuplicateInfo = info.DuplicateInfo,
                   RegisteredDate = info.RegisteredDate,
                   UserName = info.UserName
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC55", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "excel";
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

         
           return File(renderedBytes, mimeType, "BC55_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");

        }

        [HttpGet]
        public ActionResult FilterHandover(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int employeeTypeId, int statusCareId, int statusMapId, int reportType)
        {
            var branchId = UserContext.GetDefaultBranch();
            var userImportId = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)employeeTypeId;
            var listContacts = ContactRepository.FilterHandover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId) ?? new List<ContactInfo>();

            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC44.rdlc") };
            localReport.SetParameters(new ReportParameter("Infomation", "Chuc nang in danh sach nhu crm cu"));
            var lstData = new List<BC44Model>();
            foreach (var info in listContacts)
            {
                var objModel = new BC44Model
                {
                    Fullname = info.Fullname,
                    Email = info.Email,
                    Level = info.LevelId.ToString(),
                    Mobile = info.Mobile1,
                    AppointmentConsultantDate = info.AppointmentConsultantDate,
                    CallInfoConsultant = info.CallInfoConsultant,
                    CallConsultantDate = info.CallConsultantDate,
                    StatusCare = info.StatusCareConsultant,
                    Product = info.ProductSellName,
                    Consultant = info.Consultant,
                    HandoverConsulantDate = info.HandoverConsultantDate,
                    CallCount = info.CallCount
                };
                lstData.Add(objModel);
            }

            var reportDataSource = new ReportDataSource("BC44", lstData);
            localReport.DataSources.Add(reportDataSource);

            string[] streams;
            Warning[] warnings;
            string mimeType, encoding, fileNameExtension, type = "pdf";
            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    type = "pdf";
                    break;
                case (int)ReportType.Excel:
                    type = "excel";
                    break;
            }
            //Render the report             
            var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            switch (reportType)
            {
                case (int)ReportType.Pdf:
                    return File(renderedBytes, mimeType);
                case (int)ReportType.Excel:
                    return File(renderedBytes, mimeType, "BC44_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            }
            return File(renderedBytes, mimeType);
        }
        /// <summary>
        /// Bao cao danh sach contact Loc
        /// </summary>
        /// <returns></returns>
        public ActionResult BCReportInfoContactLoc()
        {
            return View();
        }


        /// <summary>
        ///Bao cao xuat nhap ton kho cua MOL  
        /// </summary>
        /// <returns></returns>
        public ActionResult BCReportExportImportContactMOL()
        {
            ViewBag.Channels = ChannelRepository.GetAll();
            return View();
        }

        //Xuat excel bao cao xuat nhap contact
        public void ShowBCReportExportImportContactMOL(string dateReportTimeFrom, string dateReportTimeTo, string channels)
        {
            var fromDate = dateReportTimeFrom.ToDateTime("ddMMyyyy");
            var toDate = dateReportTimeTo.ToDateTime("ddMMyyyy");
          
            if (!toDate.HasValue) toDate = fromDate;
            if (toDate.HasValue) toDate = toDate.Value.AddDays(1).AddSeconds(-1);

            ReportModel model = new ReportModel();
            model.DateTimeFrom = fromDate;
            model.DateTimeTo = toDate;

            var result = new List<TmpJobReportImportExportInfo>();

            //Ham get du lieu trong StoreProcedure, chuyen thanh list object TmpJobReportImportExportInfo
            result = TmpJobReportRepository.GetAllInfoImportExport(fromDate, toDate, channels);

            ReportHelper.ToExcel(result, model);
        }

        public ActionResult DashBoard()
        {
            ViewBag.Group =  GroupRepository.GetAll() ?? new List<GroupInfo>();
            var userCurent = UserContext.GetCurrentUser();
            if (userCurent != null)
            {
                ViewBag.GroupId1 = userCurent.GroupId;
            }

                return View();
        }

        public ActionResult TableDashBoard(string date)
        {
            DateTime datetime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var list_chamsoccontact = TmpJobReportRepository.GetReportLogDashBoard_chamsoccontact(datetime) ?? new List<ReportLogDashBoardInfo>();
            var list_lichlamviectvts = TmpJobReportRepository.GetReportLogDashBoard_lichlamviectvts(datetime) ?? new List<ReportLogDashBoardInfo>();
            var list_timkiemcontacttvts = TmpJobReportRepository.GetReportLogDashBoard_timkiemcontacttvts(datetime) ?? new List<ReportLogDashBoardInfo>();
            var list_danhsachcontacttvts = TmpJobReportRepository.GetReportLogDashBoard_danhsachcontacttvts(datetime) ?? new List<ReportLogDashBoardInfo>();
            var list_callcontacttvts = TmpJobReportRepository.GetReportLogDashBoard_callcontacttvts(datetime) ?? new List<ReportLogDashBoardInfo>();
            var list_callhistory = TmpJobReportRepository.GetReportLogDashBoard_callhistory(datetime) ?? new List<ReportLogDashBoardInfo>();

            ViewBag.DashBoard_chamsoccontact = list_chamsoccontact;
            ViewBag.DashBoard_lichlamviectvts = list_lichlamviectvts;
            ViewBag.DashBoard_timkiemcontacttvts = list_timkiemcontacttvts;
            ViewBag.DashBoard_danhsachcontacttvts = list_danhsachcontacttvts;
            ViewBag.DashBoard_callcontacttvts = list_callcontacttvts;
            ViewBag.DashBoard_callhistory = list_callhistory;

            return View();
        }

        public ActionResult TableDashBoardCall(string date, int groupId, int userId)
        {
            DateTime datetime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var list_thongkecuocgoi = TmpJobReportRepository.GetReportLogDashBoard_thongkecuocgoi(datetime, groupId, userId) ?? new List<ReportLogDashBoardInfo>();
            ViewBag.DashBoard_thongkecuocgoi = list_thongkecuocgoi;
            return View();
        }
    }
}
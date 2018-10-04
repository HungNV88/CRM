using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Activity;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Domain.EmailInfo;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Domain.SourceTypes;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Services.Activity;
using TamoCRM.Services.Catalogs;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.ActionFilters;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Framework.Controllers
{
    [AdminActionFilter]
    public class AdminController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            //string filePath = filterContext.HttpContext.Request.FilePath;
            //string userName = UserContext.GetCurrentUser().UserName;
            //LogTracking.CreateCheckPoint("CRM_NATIVE_VN", filePath, "Begin Request", userName, DateTime.Now, "LogTime", (int)CheckPoint.time);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //base.OnResultExecuted(filterContext);
            //string filePath = filterContext.HttpContext.Request.FilePath;
            //string userName = UserContext.GetCurrentUser().UserName;
            //LogTracking.CreateCheckPoint("CRM_NATIVE_VN", filePath, "End Request", userName, DateTime.Now, "LogTime", (int)CheckPoint.time);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            try
            {
                var userCurent = UserContext.GetCurrentUser();
                if (userCurent != null)
                {
                    ViewBag.GroupId = userCurent.GroupId;
                    ViewBag.GroupType = userCurent.GroupConsultantType;
                    ViewBag.UserBranches = UserRepository.GetBranchOfUser(userCurent.UserID);
                }

                //Gói niêm yết
                ViewBag.PackagePriceListed = CatalogRepository.GetAll<PackagePriceListed>();

                // GroupType
                var wbServiceConfigTypes = new Dictionary<int, string>();
                foreach (var item in Enum.GetValues(typeof(WebServiceConfigType)))
                    wbServiceConfigTypes.Add((int)item, ObjectExtensions.GetEnumDescription((WebServiceConfigType)item));
                ViewBag.WebServiceConfigTypes = wbServiceConfigTypes.Select(c => new { Id = c.Key, Name = c.Value });

                // GroupType
                var groupTypes = new Dictionary<int, string>();
                foreach (var item in Enum.GetValues(typeof(GroupConsultantType)))
                    groupTypes.Add((int)item, ObjectExtensions.GetEnumDescription((GroupConsultantType)item));
                ViewBag.GroupTypes = groupTypes.Select(c => new { Id = c.Key, Name = c.Value });

                //Branches
                filterContext.Controller.ViewBag.BranchId = UserContext.GetDefaultBranch();

                //UserId
                filterContext.Controller.ViewBag.UserId = UserContext.GetCurrentUser().UserID;

                // Group
                StoreData.ListGroup.RemoveAll(c => c.GroupId == 0);
                filterContext.Controller.ViewBag.Groups = StoreData.ListGroup;

                // User
                StoreData.ListUser.RemoveAll(c => c.UserID == 0);
                filterContext.Controller.ViewBag.Users = StoreData.ListUser;

                // Level
                filterContext.Controller.ViewBag.Levels = StoreData.ListLevel;

                // Branch
                filterContext.Controller.ViewBag.Branches = StoreData.ListBranch;

                //Collectors
                filterContext.Controller.ViewBag.Collectors = StoreData.ListCollector;

                //StatusMap
                filterContext.Controller.ViewBag.StatusMaps = StoreData.ListStatusMap ?? new List<StatusMapInfo>();

                //StatusCare
                filterContext.Controller.ViewBag.StatusCares = StoreData.ListStatusCare ?? new List<StatusCareInfo>();

                // SourceType
                filterContext.Controller.ViewBag.SourceTypes = StoreData.ListSourceType ?? new List<SourceTypeInfo>();

                //Email
                filterContext.Controller.ViewBag.Email = StoreData.ListEmail ?? new List<EmailInfo>();

                //EducationLevel
                filterContext.Controller.ViewBag.EducationLevels = StoreData.ListEducationLevel ?? new List<EducationLevelInfo>();

                //TimeSlot
                filterContext.Controller.ViewBag.TimeSlots = StoreData.ListTimeSlot ?? new List<TimeSlotInfo>();

                //TeacherType
                filterContext.Controller.ViewBag.TeacherTypes = StoreData.ListTeacherType ?? new List<TeacherTypeInfo>();

                // Container
                filterContext.Controller.ViewBag.Containers = StoreData.ListContainer ?? new List<ContainerInfo>();

                //Import Excels
                filterContext.Controller.ViewBag.ImportExcels = StoreData.ListImportExcel ?? new List<ImportExcelInfo>();

                // WebServiceConfig
                StoreData.ListWebServiceConfig.RemoveAll(c => c.Id == 0);

                // StatusConnectType
                var statusConnectType = new Dictionary<int, string>();
                foreach (var item in Enum.GetValues(typeof(StatusConnectType)))
                    statusConnectType.Add((int)item, ObjectExtensions.GetEnumDescription((StatusConnectType)item));
                ViewBag.StatusConnectTypes = statusConnectType.Select(c => new { Id = c.Key, Name = c.Value });

                // Product
                //if (StoreData.ListProduct.IsNullOrEmpty())
                //{
                //    StoreData.ListProduct = ProductRepository.GetAll() ?? new List<ProductInfo>();
                //}
                filterContext.Controller.ViewBag.Products = StoreData.ListProduct ?? new List<ProductInfo>();

                // Quality
                //if (StoreData.ListQuality.IsNullOrEmpty())
                //{
                //    StoreData.ListQuality = QualityRepository.GetAll() ?? new List<QualityInfo>();
                //}
                filterContext.Controller.ViewBag.Quality = StoreData.ListQuality ?? new List<QualityInfo>();

                // FeeMoneyType
                filterContext.Controller.ViewBag.FeeMoneyType = StoreData.ListFeeMoneyType;

                // PackageFeeEdu
                filterContext.Controller.ViewBag.PackageFeeEdu = StoreData.ListPackageFeeEdu;
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Apply logging
                var objChanges = HttpContext.Items[ContactInfo.CONTACT_CHANGE] as List<ActivityObjectChangeInfo>;
                if (objChanges != null)
                {
                    var activityInfo = new ActivityLogInfo
                    {
                        FunctionId = 0,
                        CreatedBy = UserContext.GetCurrentUser().UserID
                    };
                    activityInfo.Id = ActivityLogRepository.Create(activityInfo);
                    foreach (var objChange in objChanges)
                    {
                        objChange.ActivityId = activityInfo.Id;
                        ActivityObjectChangeRepository.Create(objChange);
                    }
                }
                HttpContext.Items[ContactInfo.CONTACT_CHANGE] = null;
            }
            catch
            {
                //Dont throw exception if loging failed
            }
        }
    }
}
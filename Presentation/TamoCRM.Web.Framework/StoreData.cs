using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using TamoCRM.Call;
using TamoCRM.Core;
using TamoCRM.Core.Caching;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Channels;
using TamoCRM.Domain.Collectors;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Domain.Groups;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Domain.Levels;
using TamoCRM.Domain.Phones;
using TamoCRM.Domain.SourceTypes;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Domain.UserDraft;
using TamoCRM.Domain.UserRole;
using TamoCRM.Domain.WebServiceConfig;
using TamoCRM.Services.Branches;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.Catalogs;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.EducationLevels;
using TamoCRM.Services.Groups;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Services.Levels;
using TamoCRM.Services.Phones;
using TamoCRM.Services.SourceTypes;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.UserDraft;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.TemplateAd;
using System.Linq;
using TamoCRM.Services.WebServiceConfig;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Domain.EmailInfo;
using TamoCRM.Services.Email;
using TamoCRM.Domain.Logs;
using System;
using TamoCRM.Services.Logs;
using TamoCRM.Services.Product;
using TamoCRM.Services.Quality;
using TamoCRM.Services.TimeSlot;
using TamoCRM.Services.Container;
using TamoCRM.Services.StatusCare;
using TamoCRM.Services.TeacherType;
using TamoCRM.Services.FeeMoneyType;
using TamoCRM.Services.PackageFeeEdu;


namespace TamoCRM.Web.Framework
{
    public class StoreData
    {
        public static List<UserInfo> _listUser = new List<UserInfo>();
        public static object _lockListUser = new object();
        public static List<UserInfo> ListUser
        {
            
            get
            {
                List<UserInfo> listUser;
                if (_listUser.IsNullOrEmpty())
                {
                    lock (_lockListUser)
                    {
                        if (_listUser.IsNullOrEmpty())
                        {
                            _listUser = CatalogRepository.GetAll<UserInfo>() ?? new List<UserInfo>();
                            
                        }
                    }
                    listUser = new List<UserInfo>(_listUser);
                }
                else
                {
                    listUser = new List<UserInfo>(_listUser);
                }
                return listUser;
            }
        }

        //Group
        public static List<GroupInfo> _listGroup = new List<GroupInfo>();
        public static object _lockListGroup = new object();
        public static List<GroupInfo> ListGroup
        {
            get
            {
                List<GroupInfo> listGroup;
                if (_listGroup.IsNullOrEmpty())
                {
                    lock (_lockListGroup)
                    {
                        if (_listGroup.IsNullOrEmpty())
                        {
                            _listGroup = CatalogRepository.GetAll<GroupInfo>() ?? new List<GroupInfo>();
                        }
                    }
                    listGroup = new List<GroupInfo>(_listGroup);
                }
                else
                {
                    listGroup = new List<GroupInfo>(_listGroup);
                }
                return listGroup;
            }
        }

        //Level
        public static List<LevelInfo> _listLevel = new List<LevelInfo>();
        public static object _lockListLevel = new object();
        public static List<LevelInfo> ListLevel
        {
            get
            {
                List<LevelInfo> listLevel;
                if (_listLevel.IsNullOrEmpty())
                {
                    lock (_lockListLevel)
                    {
                        if(_listLevel.IsNullOrEmpty())
                        {
                            _listLevel = CatalogRepository.GetAll<LevelInfo>() ?? new List<LevelInfo>();
                        }
                    }
                    listLevel = new List<LevelInfo>(_listLevel);
                }
                else
                {
                    listLevel = new List<LevelInfo>(_listLevel);
                }
                return listLevel;
            }
        }

        //ImportExcel
        public static List<ImportExcelInfo> _listImportExcel = new List<ImportExcelInfo>();
        public static object _lockListImportExcel = new object();
        public static List<ImportExcelInfo> ListImportExcel
        {
            get
            {
                List<ImportExcelInfo> listImportExcel;
                if (_listImportExcel.IsNullOrEmpty())
                {
                    lock (_lockListImportExcel)
                    {
                        if (_listImportExcel.IsNullOrEmpty())
                        {
                            _listImportExcel = ImportExcelRepository.GetAll(1000) ?? new List<ImportExcelInfo>();
                        }
                    }
                    listImportExcel = new List<ImportExcelInfo>(_listImportExcel);
                }
                else
                {
                    listImportExcel = new List<ImportExcelInfo>(_listImportExcel);
                }
                return listImportExcel;
            }
        }

        //Branch
        public static List<BranchInfo> _listBranch = new List<BranchInfo>();
        public static object _lockListBranch = new object();
        public static List<BranchInfo> ListBranch
        {
            get
            {
                List<BranchInfo> listBranch;
                if (_listBranch.IsNullOrEmpty())
                {
                    lock (_lockListBranch)
                    {
                        if (_listBranch.IsNullOrEmpty())
                        {
                            _listBranch = BranchRepository.GetAll() ?? new List<BranchInfo>();
                        }
                    }
                    listBranch = new List<BranchInfo>(_listBranch);
                }
                else
                {
                    listBranch = new List<BranchInfo>(_listBranch);
                }
                return listBranch;
            }
        }

        //Products
        public static List<ProductInfo> _listProduct = new List<ProductInfo>();
        public static object _lockListProduct = new object();
        public static List<ProductInfo> ListProduct
        {
            get
            {
                List<ProductInfo> listProduct;
                if (_listProduct.IsNullOrEmpty())
                {
                    lock (_lockListProduct)
                    {
                        if (_listProduct.IsNullOrEmpty())
                        {
                            _listProduct = ProductRepository.GetAll() ?? new List<ProductInfo>();
                        }
                    }
                    listProduct = new List<ProductInfo>(_listProduct);
                }
                else
                {
                    listProduct = new List<ProductInfo>(_listProduct);
                }
                return listProduct;
            }
        }

        //Channel
        public static List<ChannelInfo> _listChannel = new List<ChannelInfo>();
        public static object _lockListChannel = new object();
        public static List<ChannelInfo> ListChannel
        {
            get
            {
                List<ChannelInfo> listChannel;
                if (_listChannel.IsNullOrEmpty())
                {
                    lock (_lockListChannel)
                    {
                        if (_listChannel.IsNullOrEmpty())
                        {
                            _listChannel = ChannelRepository.GetAll() ?? new List<ChannelInfo>();
                        }
                    }
                    listChannel = new List<ChannelInfo>(_listChannel);
                }
                else
                {
                    listChannel = new List<ChannelInfo>(_listChannel);
                }
                return listChannel;
            }
        }

        // QualityInfo
        public static List<QualityInfo> _listQuality = new List<QualityInfo>();
        public static object _locklistQuality = new object();
        public static List<QualityInfo> ListQuality
        {
            get
            {
                List<QualityInfo> listQuality;
                if (_listQuality.IsNullOrEmpty())
                {
                    lock (_locklistQuality)
                    {
                        if (_listQuality.IsNullOrEmpty())
                        {
                            _listQuality = QualityRepository.GetAll() ?? new List<QualityInfo>();
                        }
                    }
                    listQuality = new List<QualityInfo>(_listQuality);
                }
                else
                {
                    listQuality = new List<QualityInfo>(_listQuality);
                }
                return listQuality;
            }
        }

        //Package
        public static List<PackageInfo> ListPackage = new List<PackageInfo>();

        //TimeSlot
        public static List<TimeSlotInfo> _listTimeSlot = new List<TimeSlotInfo>();
        public static object _locklistTimeSlot = new object();
        public static List<TimeSlotInfo> ListTimeSlot
        {
            get
            {
                List<TimeSlotInfo> listTimeSlot;
                if (_listTimeSlot.IsNullOrEmpty())
                {
                    lock (_locklistTimeSlot)
                    {
                        if (_listTimeSlot.IsNullOrEmpty())
                        {
                            _listTimeSlot = TimeSlotRepository.GetAll() ?? new List<TimeSlotInfo>();
                        }
                    }
                    listTimeSlot = new List<TimeSlotInfo>(_listTimeSlot);
                }
                else
                {
                    listTimeSlot = new List<TimeSlotInfo>(_listTimeSlot);
                }
                return listTimeSlot;
            }
        }

        //Container
        public static List<ContainerInfo> _listContainer = new List<ContainerInfo>();
        public static object _locklistContainer = new object();
        public static List<ContainerInfo> ListContainer
        {
            get
            {
                List<ContainerInfo> listContainer;
                if (_listContainer.IsNullOrEmpty())
                {
                    lock (_locklistContainer)
                    {
                        if (_listContainer.IsNullOrEmpty())
                        {
                            _listContainer = ContainerRepository.GetAll() ?? new List<ContainerInfo>();
                        }
                    }
                    listContainer = new List<ContainerInfo>(_listContainer);
                }
                else
                {
                    listContainer = new List<ContainerInfo>(_listContainer);
                }
                return listContainer;
            }
        }

        //Collector
        public static List<CollectorInfo> _listCollector = new List<CollectorInfo>();
        public static object _locklistCollector = new object();
        public static List<CollectorInfo> ListCollector
        {
            get
            {
                List<CollectorInfo> listCollector;
                if (_listCollector.IsNullOrEmpty())
                {
                    lock (_locklistCollector)
                    {
                        if (_listCollector.IsNullOrEmpty())
                        {
                            _listCollector = CollectorRepository.GetAll() ?? new List<CollectorInfo>();
                        }
                    }
                    listCollector = new List<CollectorInfo>(_listCollector);
                }
                else
                {
                    listCollector = new List<CollectorInfo>(_listCollector);
                }
                return listCollector;
            }
        }

        //StatusMap
        public static List<StatusMapInfo> _listStatusMap = new List<StatusMapInfo>();
        public static object _locklistStatusMap = new object();
        public static List<StatusMapInfo> ListStatusMap
        {
            get
            {
                List<StatusMapInfo>  listStatusMap;
                if (_listStatusMap.IsNullOrEmpty())
                {
                    lock (_locklistStatusMap)
                    {
                        if (_listStatusMap.IsNullOrEmpty())
                        {
                            _listStatusMap = StatusMapRepository.GetAll() ?? new List<StatusMapInfo>();
                        }
                    }
                    listStatusMap = new List<StatusMapInfo>(_listStatusMap);
                }
                else
                {
                    listStatusMap = new List<StatusMapInfo>(_listStatusMap);
                }
                return listStatusMap;
            }
        }


        //GroupType
        public static List<GroupTypeInfo> ListGroupType = new List<GroupTypeInfo>();

        //StatusCare
        public static List<StatusCareInfo> _listStatusCare = new List<StatusCareInfo>();
        public static object _locklistStatusCare = new object();
        public static List<StatusCareInfo> ListStatusCare
        {
            get
            {
                List<StatusCareInfo> listStatusCare;
                if (_listStatusCare.IsNullOrEmpty())
                {
                    lock (_locklistStatusCare)
                    {
                        if (_listStatusCare.IsNullOrEmpty())
                        {
                            _listStatusCare = StatusCareRepository.GetAll() ?? new List<StatusCareInfo>();
                        }
                    }
                    listStatusCare = new List<StatusCareInfo>(_listStatusCare);
                }
                else
                {
                    listStatusCare = new List<StatusCareInfo>(_listStatusCare);
                }
                return listStatusCare;
            }
        }

        //SourceType
        public static List<SourceTypeInfo> _listSourceType = new List<SourceTypeInfo>();
        public static object _locklistSourceType = new object();
        public static List<SourceTypeInfo> ListSourceType
        {
            get
            {
                List<SourceTypeInfo> listSourceType;
                if (_listSourceType.IsNullOrEmpty())
                {
                    lock (_locklistStatusCare)
                    {
                        if (_listSourceType.IsNullOrEmpty())
                        {
                            _listSourceType = SourceTypeRepository.GetAll() ?? new List<SourceTypeInfo>();
                        }
                    }
                    listSourceType = new List<SourceTypeInfo>(_listSourceType);
                }
                else
                {
                    listSourceType = new List<SourceTypeInfo>(_listSourceType);
                }
                return listSourceType;
            }
        }

        //TeacherType
        public static List<TeacherTypeInfo> _listTeacherType = new List<TeacherTypeInfo>();
        public static object _locklistTeacherType = new object();
        public static List<TeacherTypeInfo> ListTeacherType
        {
            get
            {
                List<TeacherTypeInfo> listTeacherType;
                if (_listTeacherType.IsNullOrEmpty())
                {
                    lock (_locklistTeacherType)
                    {
                        if (_listTeacherType.IsNullOrEmpty())
                        {
                            _listTeacherType = TeacherTypeRepository.GetAll() ?? new List<TeacherTypeInfo>();
                        }
                    }
                    listTeacherType = new List<TeacherTypeInfo>(_listTeacherType);
                }
                else
                {
                    listTeacherType = new List<TeacherTypeInfo>(_listTeacherType);
                }
                return listTeacherType;
            }
        }


        //FeeMoneyType
        public static List<FeeMoneyTypeInfo> _listFeeMoneyType = new List<FeeMoneyTypeInfo>();
        public static object _locklistFeeMoneyType = new object();
        public static List<FeeMoneyTypeInfo> ListFeeMoneyType
        {
            get
            {
                List<FeeMoneyTypeInfo> listFeeMoneyType;
                if (_listFeeMoneyType.IsNullOrEmpty())
                {
                    lock (_locklistFeeMoneyType)
                    {
                        if (_listFeeMoneyType.IsNullOrEmpty())
                        {
                            _listFeeMoneyType = FeeMoneyTypeRepository.GetAll() ?? new List<FeeMoneyTypeInfo>();
                        }
                    }
                    listFeeMoneyType = new List<FeeMoneyTypeInfo>(_listFeeMoneyType);
                }
                else
                {
                    listFeeMoneyType = new List<FeeMoneyTypeInfo>(_listFeeMoneyType);
                }
                return listFeeMoneyType;
            }
        }

        //PackageFeeEdu
        public static List<PackageFeeEduInfo> _listPackageFeeEdu = new List<PackageFeeEduInfo>();
        public static object _locklistPackageFeeEdu = new object();
        public static List<PackageFeeEduInfo> ListPackageFeeEdu
        {
            get
            {
                List<PackageFeeEduInfo> listPackageFeeEdu;
                if (_listPackageFeeEdu.IsNullOrEmpty())
                {
                    lock (_locklistPackageFeeEdu)
                    {
                        if (_listPackageFeeEdu.IsNullOrEmpty())
                        {
                            _listPackageFeeEdu = PackageFeeEduRepository.GetAll() ?? new List<PackageFeeEduInfo>();
                        }
                    }
                    listPackageFeeEdu = new List<PackageFeeEduInfo>(_listPackageFeeEdu);
                }
                else
                {
                    listPackageFeeEdu = new List<PackageFeeEduInfo>(_listPackageFeeEdu);
                }
                return listPackageFeeEdu;
            }
        }

        //EducationLevel
        public static List<EducationLevelInfo> _listEducationLevel = new List<EducationLevelInfo>();
        public static object _locklistEducationLevel = new object();
        public static List<EducationLevelInfo> ListEducationLevel
        {
            get
            {
                List<EducationLevelInfo> listEducationLevel;
                if (_listEducationLevel.IsNullOrEmpty())
                {
                    lock (_locklistEducationLevel)
                    {
                        if (_listEducationLevel.IsNullOrEmpty())
                        {
                            _listEducationLevel = EducationLevelRepository.GetAll() ?? new List<EducationLevelInfo>();
                        }
                    }
                    listEducationLevel = new List<EducationLevelInfo>(_listEducationLevel);
                }
                else
                {
                    listEducationLevel = new List<EducationLevelInfo>(_listEducationLevel);
                }
                return listEducationLevel;
            }
        }

        //WebServiceConfig
        public static List<WebServiceConfigInfo> _listWebServiceConfig = new List<WebServiceConfigInfo>();
        public static object _locklistWebServiceConfig = new object();
        public static List<WebServiceConfigInfo> ListWebServiceConfig
        {
            get
            {
                List<WebServiceConfigInfo> listWebServiceConfig;
                if (_listWebServiceConfig.IsNullOrEmpty())
                {
                    lock (_locklistEducationLevel)
                    {
                        if (_listWebServiceConfig.IsNullOrEmpty())
                        {
                            _listWebServiceConfig = WebServiceConfigRepository.GetAll() ?? new List<WebServiceConfigInfo>();
                        }
                    }
                    listWebServiceConfig = new List<WebServiceConfigInfo>(_listWebServiceConfig);
                }
                else
                {
                    listWebServiceConfig = new List<WebServiceConfigInfo>(_listWebServiceConfig);
                }
                return listWebServiceConfig;
            }
        }

        //Email
        public static List<EmailInfo> _listEmail = new List<EmailInfo>();
        public static object _locklistEmail = new object();
        public static List<EmailInfo> ListEmail
        {
            get
            {
                List<EmailInfo> listEmail;
                if (_listEmail.IsNullOrEmpty())
                {
                    lock (_locklistEmail)
                    {
                        if (_listEmail.IsNullOrEmpty())
                        {
                            _listEmail = EmailRepository.GetAll() ?? new List<EmailInfo>();
                        }
                    }
                    listEmail = new List<EmailInfo>(_listEmail);
                }
                else
                {
                    listEmail = new List<EmailInfo>(_listEmail);
                }
                return listEmail;
            }
        }

        /// <summary>
        ///Ham nay load trong Api/ImportExcelController/Get(id), muc dich load lai list danh sach file excel duoc update khi import them 1 file contact moi.
        /// </summary>
        public static void LoadImportExcel()
        {
            _listImportExcel = ImportExcelRepository.GetAll(1000) ?? new List<ImportExcelInfo>();
            foreach (var item in _listImportExcel) item.FilePath = (new FileInfo(item.FilePath)).Name;
        }

        public static void LoadData()
        {
            _listUser = UserRepository.GetAll() ?? new List<UserInfo>();

            _listGroup = GroupRepository.GetAll() ?? new List<GroupInfo>();

            _listLevel = LevelRepository.GetAll() ?? new List<LevelInfo>();

            _listFeeMoneyType = FeeMoneyTypeRepository.GetAll() ?? new List<FeeMoneyTypeInfo>();

            _listProduct = ProductRepository.GetAll() ?? new List<ProductInfo>();

            _listQuality = QualityRepository.GetAll() ?? new List<QualityInfo>();

            _listBranch = BranchRepository.GetAll() ?? new List<BranchInfo>(); 

            _listCollector = CollectorRepository.GetAll() ?? new List<CollectorInfo>();

            _listStatusMap = StatusMapRepository.GetAll() ?? new List<StatusMapInfo>(); 
            
            _listSourceType = SourceTypeRepository.GetAll() ?? new List<SourceTypeInfo>();

            _listTimeSlot = TimeSlotRepository.GetAll() ?? new List<TimeSlotInfo>();
           
            _listContainer = ContainerRepository.GetAll() ?? new List<ContainerInfo>();

            _listChannel = ChannelRepository.GetAll() ?? new List<ChannelInfo>();

            _listEducationLevel = EducationLevelRepository.GetAll() ?? new List<EducationLevelInfo>();

            _listStatusCare = StatusCareRepository.GetAll() ?? new List<StatusCareInfo>();

            _listTeacherType = TeacherTypeRepository.GetAll() ?? new List<TeacherTypeInfo>();
           
            _listWebServiceConfig = WebServiceConfigRepository.GetAll() ?? new List<WebServiceConfigInfo>();
           
            _listEmail = EmailRepository.GetAll() ?? new List<EmailInfo>();
           
            _listPackageFeeEdu = PackageFeeEduRepository.GetAll() ?? new List<PackageFeeEduInfo>();

            _listImportExcel = ImportExcelRepository.GetAll(10000) ?? new List<ImportExcelInfo>();

            foreach (var item in _listImportExcel) item.FilePath = (new FileInfo(item.FilePath)).Name;

            ListGroupType = new List<GroupTypeInfo>();
            var listCollaboratorType = Util.SelectedList<GroupCollaboratorType>()
                .Select(c => new GroupTypeInfo
                             {
                                 Name = c.Text,
                                 Id = c.Value.ToInt32(),
                                 EmployeeTypeId = (int)EmployeeType.Collaborator
                             })
                .ToList();
            if (!listCollaboratorType.IsNullOrEmpty()) ListGroupType.AddRange(listCollaboratorType);

            var listConsultantType = Util.SelectedList<GroupConsultantType>()
                .Select(c => new GroupTypeInfo
                {
                    Id = c.Value.ToInt32(),
                    Name = c.Text,
                    EmployeeTypeId = (int)EmployeeType.Consultant
                })
                .ToList();
            if (!listConsultantType.IsNullOrEmpty()) ListGroupType.AddRange(listConsultantType);
        }

        public static void ReloadData<T>()
        {
            var type = typeof(T);
            if (type == typeof(LevelInfo))
            {
                _listLevel = LevelRepository.GetAll() ?? new List<LevelInfo>();
            }
            if (type == typeof(SourceTypeInfo))
            {
                _listSourceType = SourceTypeRepository.GetAll() ?? new List<SourceTypeInfo>();
            }
            if (type == typeof(ContainerInfo))
            {
                _listContainer = ContainerRepository.GetAll() ?? new List<ContainerInfo>();
            }
            if (type == typeof(ImportExcelInfo))
            {
                _listImportExcel = ImportExcelRepository.GetAll(1000) ?? new List<ImportExcelInfo>();
            }
            if (type == typeof(ProductInfo))
            {
                _listProduct = ProductRepository.GetAll() ?? new List<ProductInfo>();
            }
            if (type == typeof(TimeSlotInfo))
            {
                _listTimeSlot = TimeSlotRepository.GetAll() ?? new List<TimeSlotInfo>();
            }
            if (type == typeof(TeacherTypeInfo))
            {
                _listTeacherType = TeacherTypeRepository.GetAll() ?? new List<TeacherTypeInfo>();
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                _listPackageFeeEdu = CatalogRepository.GetAll<PackageFeeEduInfo>() ?? new List<PackageFeeEduInfo>();
            }
            if (type == typeof(StatusCareInfo))
            {
                _listStatusCare = StatusCareRepository.GetAll() ?? new List<StatusCareInfo>();
            }
            if (type == typeof(CollectorInfo))
            {
                _listCollector = CollectorRepository.GetAll() ?? new List<CollectorInfo>();
            }
            if (type == typeof(StatusMapInfo))
            {
                _listStatusMap = StatusMapRepository.GetAll() ?? new List<StatusMapInfo>();
            }
            if (type == typeof(BranchInfo))
            {
                _listBranch = BranchRepository.GetAll() ?? new List<BranchInfo>();
            }
            if (type == typeof(UserInfo))
            {
                _listUser = UserRepository.GetAll() ?? new List<UserInfo>();
            }
            if (type == typeof(GroupInfo))
            {
                _listGroup = GroupRepository.GetAll() ?? new List<GroupInfo>();
            }
            if (type == typeof(ChannelInfo))
            {
                
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                
            }
            if (type == typeof(QualityInfo))
            {
                
            }
            if (type == typeof(PackageInfo))
            {
                ListPackage = CatalogRepository.GetAll<PackageInfo>() ?? new List<PackageInfo>();
            }
        }
        
        public static void LoadRedis(int contactId)
        {
            if (contactId == 0) return;
            var lstPhones = PhoneRepository.GetByContact(contactId) ?? new List<PhoneInfo>();
            foreach (var phone in lstPhones.Where(c => !string.IsNullOrEmpty(c.PhoneNumber)))
                CachingProvider.Instance().Set(phone.PhoneNumber, phone.ContactId.ToString());

            var contactInfo = ContactRepository.GetInfo(contactId) ?? new ContactInfo();
            if (!contactInfo.Email.IsStringNullOrEmpty()) CachingProvider.Instance().Set(contactInfo.Email, contactInfo.Id.ToString());
            if (!contactInfo.Email2.IsStringNullOrEmpty()) CachingProvider.Instance().Set(contactInfo.Email2, contactInfo.Id.ToString());
        }

        public static void DeleteRedis(int contactId)
        {
            var contactInfo = ContactRepository.GetInfo(contactId) ?? new ContactInfo();
            var phones = PhoneRepository.GetByContact(contactId) ?? new List<PhoneInfo>();

            // Redis
            foreach (var item in phones) CachingProvider.Instance().Del(item.PhoneNumber);
            if (!contactInfo.Email.IsStringNullOrEmpty()) CachingProvider.Instance().Del(contactInfo.Email);
            if (!contactInfo.Email2.IsStringNullOrEmpty()) CachingProvider.Instance().Del(contactInfo.Email2);
        }

        public static void WsUpdateCallHistoryInfo(int callHistoryId)
        {
            if (callHistoryId.IsIntegerNull()) return;
            var entity = CallHistoryRepository.GetInfo(callHistoryId);
           
            if (entity.CallId != "KO_CO")
            {
                WsUpdateCallHistoryInfo(entity);
            }        
        }

        public static void Job_WsUpdateCallHistoryInfo(int callHistoryId)
        {
            if (callHistoryId.IsIntegerNull()) return;
            var entity = CallHistoryRepository.GetInfo(callHistoryId);
            if (entity.CallId != "KO_CO")
            {
                WsUpdateCallHistoryInfo(entity);
            }
        }

        public static void WsUpdateCallHistoryInfo(CallHistoryInfo entity)
        {
            if (entity.CallId.IsStringNullOrEmpty() || entity.CallId == "-1") return;
            var item = HelpUtils.GetCallNew(entity.CallId, entity.MobilePhone, entity.StationId, entity.AgentCode, LinkCallCenter);
           
            if (item == null) return;

            entity.Status = item.status;
            entity.CallId = item.call_id;
            entity.Duration = item.duration;
            entity.RingTime = item.ringtime;
            entity.StationId = item.station_id;
            entity.AgentCode = item.agent_code;
            entity.ErrorCode = item.error_code;
            entity.ErrorDesc = item.error_desc;
            entity.MessageCode = item.message_code;
            entity.MobilePhone = item.mobile_phone;
            entity.EndTime = item.end_time.ToDateTime("yyyyMMddHHmmss");
            entity.StartTime = item.start_time.ToDateTime("yyyyMMddHHmmss");
            entity.ResponseTime = item.datetime_response.ToDateTime("yyyyMMddHHmmss");
            entity.LinkRecord = item.link_down_record.IsStringNullOrEmpty()
                ? string.Empty
                : (entity.UserLogType != (int)EmployeeType.Collaborator
                    ? item.link_down_record.Replace("/var/spool/asterisk/monitor", LinkRecordCRM())
                    : item.link_down_record.Replace("/var/spool/asterisk/monitor", LinkRecordTCL()));
            try
            {
                CallHistoryRepository.RepairCall(entity);
            }
            catch
            {
                
            }
        }

        public static CallHistoryInfo WsUpdateCallHistoryInfoTCL(int callHistoryId, int campainId)
        {
            if (callHistoryId.IsIntegerNull()) return null;
            var entity = CallHistoryRepository.GetInfo(callHistoryId);
            return WsUpdateCallHistoryInfoTCL(entity, campainId);
        }

        public static CallHistoryInfo WsUpdateCallHistoryInfoTCL(CallHistoryInfo entity, int campainId)
        {
            if (entity.CallId.IsStringNullOrEmpty() || entity.CallId == "-1") return entity;
            var item = HelpUtils.GetCallTcl(entity.CallId, entity.MobilePhone, entity.StationId, entity.AgentCode, campainId, TPCAutoDialSoapBinding);
            if (item == null) return entity;

            entity.Status = item.status;
            entity.CallId = item.call_id;
            entity.Duration = item.duration;
            entity.RingTime = item.ringtime;
            entity.StationId = item.station_id;
            entity.AgentCode = item.agent_code;
            entity.ErrorCode = item.error_code;
            entity.ErrorDesc = item.error_desc;
            entity.MessageCode = item.message_code;
            entity.MobilePhone = item.mobile_phone;
            entity.EndTime = item.end_time.ToDateTime("yyyyMMddHHmmss");
            entity.StartTime = item.start_time.ToDateTime("yyyyMMddHHmmss");
            entity.ResponseTime = item.datetime_response.ToDateTime("yyyyMMddHHmmss");
            entity.LinkRecord = item.link_down_record.IsStringNullOrEmpty()
                ? string.Empty
                : (entity.UserLogType != (int)EmployeeType.Collaborator
                    ? item.link_down_record.Replace("/var/spool/asterisk/monitor", LinkRecordCRM())
                    : item.link_down_record.Replace("/var/spool/asterisk/monitor", LinkRecordTCL()));
            return entity;
        }

        public static void DeleteRedis(string mobile1, string mobile2, string tel, string email, string email2, int contactId = 0)
        {
            if (!tel.IsStringNullOrEmpty()) CachingProvider.Instance().Del(tel);
            if (!email.IsStringNullOrEmpty()) CachingProvider.Instance().Del(email);
            if (!email2.IsStringNullOrEmpty()) CachingProvider.Instance().Del(email2);
            if (!mobile1.IsStringNullOrEmpty()) CachingProvider.Instance().Del(mobile1);
            if (!mobile2.IsStringNullOrEmpty()) CachingProvider.Instance().Del(mobile2);
            if (!contactId.IsIntegerNull()) LoadRedis(contactId);
        }

        public static string LinkCallCenter
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkCallCenter);
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkCallCenter"];
                }
                catch
                {
                    return ConfigurationManager.AppSettings["LinkCallCenter"];
                }
            }
        }
        public static string LinkInterviewSchedule
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkInterviewSchedule);
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkInterviewSchedule"];
                }
                catch
                {
                    return ConfigurationManager.AppSettings["LinkInterviewSchedule"];
                }
            }
        }

        public static string LinkSpecializeInterview
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkSpecializeInterview);
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkSpecializeInterview"];
                }
                catch
                {
                    return ConfigurationManager.AppSettings["LinkSpecializeInterview"];
                }
            }
        }
        public static string LinkEditContact
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkEditContact);
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkEditContact"];
                }
                catch
                {
                    return ConfigurationManager.AppSettings["LinkEditContact"];
                }
            }
        }
        public static string CallCenterSoapBinding
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.CallCenterSoapBinding);
                    return link != null ? link.Value : string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public static string TPCAutoDialSoapBinding
        {
            get
            {
                try
                {
                    var branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.TPCAutoDialSoapBinding);
                    return link != null ? link.Value : string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public static string LinkRecordCRM(int branchId = 0)
        {
            try
            {
                if (branchId.IsIntegerNull())
                {
                    branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkRecordCRM);
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkRecordCRM"];
                }
                else
                {
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkRecordCRM);
                    if (link == null)
                    {
                        link = WebServiceConfigRepository.GetInfo(branchId, (int)WebServiceConfigType.LinkRecordCRM);
                        if (link != null) ListWebServiceConfig.Add(link);
                    }
                    return link != null ? link.Value : ConfigurationManager.AppSettings["LinkRecordCRM"];
                }
            }
            catch
            {
                return ConfigurationManager.AppSettings["LinkRecordCRM"];
            }
        }
        public static string LinkRecordTCL(int branchId = 0)
        {
            try
            {
                if (branchId.IsIntegerNull())
                {
                    branchId = UserContext.GetDefaultBranch();
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkRecordTCL);
                    return link != null ? link.Value : System.Web.Configuration.WebConfigurationManager.AppSettings["LinkRecordTCL"];
                }
                else
                {
                    var link = ListWebServiceConfig
                        .Where(c => c.BranchId == branchId)
                        .FirstOrDefault(c => c.Type == (int)WebServiceConfigType.LinkRecordTCL);
                    if (link == null)
                    {
                        link = WebServiceConfigRepository.GetInfo(branchId, (int)WebServiceConfigType.LinkRecordTCL);
                        if (link != null) ListWebServiceConfig.Add(link);
                    }
                    return link != null ? link.Value : System.Web.Configuration.WebConfigurationManager.AppSettings["LinkRecordTCL"];
                }
            }
            catch
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["LinkRecordTCL"];
            }
        }

        public static SelectList SelectedList<T>(bool existAll = true)
        {
            var type = typeof(T);
            if (type == typeof(LevelInfo))
            {
                ListLevel.RemoveAll(c => c.LevelId == 0);
                if (existAll) ListLevel.Insert(0, new LevelInfo { LevelId = 0, Name = "Tất cả" });
                return new SelectList(ListLevel, "LevelId", "Name");
            }
            if (type == typeof(SourceTypeInfo))
            {
                ListSourceType.RemoveAll(c => c.SourceTypeId == 0);
                List<SourceTypeInfo> listSourceType = new List<SourceTypeInfo>(ListSourceType);
                if (existAll)
                {
                    listSourceType.Insert(0, new SourceTypeInfo() { SourceTypeId = 0, Name = "Tất cả" });
                }
                return new SelectList(listSourceType, "SourceTypeId", "Name");
            }
            if (type == typeof(ContainerInfo))
            {
                ListContainer.RemoveAll(c => c.Id == 0);
                if (existAll) ListContainer.Insert(0, new ContainerInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(ListContainer, "Id", "Name");
            }
            if (type == typeof(ImportExcelInfo))
            {
                var branchId = UserContext.GetDefaultBranch();
                var temps = ListImportExcel.Where(c => c.BranchId == branchId).ToList();

                temps.RemoveAll(c => c.ImportId == 0);
                if (existAll) temps.Insert(0, new ImportExcelInfo { ImportId = 0, FilePath = "Tất cả" });
                return new SelectList(temps, "ImportId", "FilePath");
            }
            if (type == typeof(ProductInfo))
            {
                if (_listProduct.IsNullOrEmpty()) _listProduct = ProductRepository.GetAll();
                _listProduct.RemoveAll(c => c.Id == 0);
                if (existAll) _listProduct.Insert(0, new ProductInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listProduct, "Id", "Name");
            }
            if (type == typeof(TimeSlotInfo))
            {
                if (_listTimeSlot.IsNullOrEmpty()) _listTimeSlot = TimeSlotRepository.GetAll();
                _listTimeSlot.RemoveAll(c => c.Id == 0);
                if (existAll) _listTimeSlot.Insert(0, new TimeSlotInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listTimeSlot, "Id", "Name");
            }
            if (type == typeof(TeacherTypeInfo))
            {
                if (_listTeacherType.IsNullOrEmpty()) _listTeacherType = TeacherTypeRepository.GetAll();
                _listTeacherType.RemoveAll(c => c.Id == 0);
                if (existAll) _listTeacherType.Insert(0, new TeacherTypeInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listTeacherType, "Id", "Name");
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                if (_listPackageFeeEdu.IsNullOrEmpty()) _listPackageFeeEdu = CatalogRepository.GetAll<PackageFeeEduInfo>();
                _listPackageFeeEdu.RemoveAll(c => c.Id == 0);
                if (existAll) _listPackageFeeEdu.Insert(0, new PackageFeeEduInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listPackageFeeEdu, "Id", "Name");
            }
            if (type == typeof(StatusCareInfo))
            {
                if (_listStatusCare.IsNullOrEmpty()) _listStatusCare = StatusCareRepository.GetAll();
                _listStatusCare.RemoveAll(c => c.Id == 0);
                if (existAll) _listStatusCare.Insert(0, new StatusCareInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listStatusCare, "Id", "Name");
            }
            if (type == typeof(CollectorInfo))
            {
                if (_listCollector.IsNullOrEmpty()) _listCollector = CollectorRepository.GetAll();
                _listCollector.RemoveAll(c => c.CollectorId == 0);
                if (existAll) _listCollector.Insert(0, new CollectorInfo { CollectorId = 0, Name = "Tất cả" });
                return new SelectList(_listCollector, "CollectorId", "Name");
            }
            if (type == typeof(StatusMapInfo))
            {
                if (_listStatusMap.IsNullOrEmpty()) _listStatusMap = StatusMapRepository.GetAll();
                _listStatusMap.RemoveAll(c => c.Id == 0);
                if (existAll) _listStatusMap.Insert(0, new StatusMapInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listStatusMap, "Id", "Name");
            }
            if (type == typeof(BranchInfo))
            {
                if (_listBranch.IsNullOrEmpty()) _listBranch = BranchRepository.GetAll();
                _listBranch.RemoveAll(c => c.BranchId == 0);
                if (existAll) _listBranch.Insert(0, new BranchInfo { BranchId = 0, Name = "Tất cả" });
                return new SelectList(_listBranch, "BranchId", "Name");
            }
            if (type == typeof(UserInfo))
            {
                if (_listUser.IsNullOrEmpty()) _listUser = UserRepository.GetAll();
                _listUser.RemoveAll(c => c.UserID == 0);
                if (existAll) _listUser.Insert(0, new UserInfo { UserID = 0, FullName = "Tất cả" });
                return new SelectList(_listUser, "UserID", "FullName");
            }
            if (type == typeof(ChannelInfo))
            {
                if (_listChannel.IsNullOrEmpty()) _listChannel = ChannelRepository.GetAll();
                _listChannel.RemoveAll(c => c.ChannelId == 0);
                if (existAll) _listChannel.Insert(0, new ChannelInfo { ChannelId = 0, Name = "Tất cả" });
                return new SelectList(_listChannel, "ChannelId", "Name");
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                if (_listFeeMoneyType.IsNullOrEmpty()) _listFeeMoneyType = FeeMoneyTypeRepository.GetAll();
                _listFeeMoneyType.RemoveAll(c => c.Id == 0);
                if (existAll) _listFeeMoneyType.Insert(0, new FeeMoneyTypeInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listFeeMoneyType, "Id", "Description");
            }
            if (type == typeof(QualityInfo))
            {
                if (_listQuality.IsNullOrEmpty()) _listQuality = QualityRepository.GetAll();
                _listQuality.RemoveAll(c => c.Id == 0);
                if (existAll) _listQuality.Insert(0, new QualityInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(_listQuality, "Id", "Name");
            }
            if (type == typeof(PackageInfo))
            {
                if (ListPackageFeeEdu.IsNullOrEmpty()) ListPackage = CatalogRepository.GetAll<PackageInfo>();
                ListPackageFeeEdu.RemoveAll(c => c.Id == 0);
                if (existAll) ListPackage.Insert(0, new PackageInfo { Id = 0, Name = "Tất cả" });
                return new SelectList(ListPackage, "Id", "Name");
            }
            return null;
        }

        public static SelectList SelectedList<T>(bool existAll, string allValue)
        {
            var type = typeof(T);
            if (type == typeof(LevelInfo))
            {
                ListLevel.RemoveAll(c => c.LevelId == 0);
                if (existAll) ListLevel.Insert(0, new LevelInfo { LevelId = 0, Name = allValue });
                return new SelectList(ListLevel, "LevelId", "Name");
            }
            if (type == typeof(SourceTypeInfo))
            {
                ListSourceType.RemoveAll(c => c.SourceTypeId == 0);
                if (existAll) ListSourceType.Insert(0, new SourceTypeInfo { SourceTypeId = 0, Name = allValue });
                return new SelectList(ListSourceType, "SourceTypeId", "Name");
            }
            if (type == typeof(ContainerInfo))
            {
                ListContainer.RemoveAll(c => c.Id == 0);
                if (existAll) ListContainer.Insert(0, new ContainerInfo { Id = 0, Name = allValue });
                return new SelectList(ListContainer, "Id", "Name");
            }
            if (type == typeof(ImportExcelInfo))
            {
                var branchId = UserContext.GetDefaultBranch();
                var temps = ListImportExcel.Where(c => c.BranchId == branchId).ToList();

                temps.RemoveAll(c => c.ImportId == 0);
                if (existAll) temps.Insert(0, new ImportExcelInfo { ImportId = 0, FilePath = allValue });
                return new SelectList(temps, "ImportId", "FilePath");
            }
            if (type == typeof(ProductInfo))
            {
                //if (ListProduct.IsNullOrEmpty()) ListProduct = ProductRepository.GetAll();
                //ListProduct.RemoveAll(c => c.Id == 0);
                //if (existAll) ListProduct.Insert(0, new ProductInfo { Id = 0, Name = allValue });
                return new SelectList(ProductRepository.GetAll(), "Id", "Name");
            }
            if (type == typeof(TimeSlotInfo))
            {
                if (_listTimeSlot.IsNullOrEmpty()) _listTimeSlot = TimeSlotRepository.GetAll();
                _listTimeSlot.RemoveAll(c => c.Id == 0);
                if (existAll) _listTimeSlot.Insert(0, new TimeSlotInfo { Id = 0, Name = allValue });
                return new SelectList(_listTimeSlot, "Id", "Name");
            }
            if (type == typeof(TeacherTypeInfo))
            {
                if (_listTeacherType.IsNullOrEmpty()) _listTeacherType = TeacherTypeRepository.GetAll();
                _listTeacherType.RemoveAll(c => c.Id == 0);
                if (existAll) _listTeacherType.Insert(0, new TeacherTypeInfo { Id = 0, Name = allValue });
                return new SelectList(_listTeacherType, "Id", "Name");
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                if (_listPackageFeeEdu.IsNullOrEmpty()) _listPackageFeeEdu = CatalogRepository.GetAll<PackageFeeEduInfo>();
                _listPackageFeeEdu.RemoveAll(c => c.Id == 0);
                if (existAll) _listPackageFeeEdu.Insert(0, new PackageFeeEduInfo { Id = 0, Name = allValue });
                return new SelectList(_listPackageFeeEdu, "Id", "Name");
            }
            if (type == typeof(StatusCareInfo))
            {
                if (_listStatusCare.IsNullOrEmpty()) _listStatusCare = StatusCareRepository.GetAll();
                _listStatusCare.RemoveAll(c => c.Id == 0);
                if (existAll) _listStatusCare.Insert(0, new StatusCareInfo { Id = 0, Name = allValue });
                return new SelectList(_listStatusCare, "Id", "Name");
            }
            if (type == typeof(CollectorInfo))
            {
                if (_listCollector.IsNullOrEmpty()) _listCollector = CollectorRepository.GetAll();
                _listCollector.RemoveAll(c => c.CollectorId == 0);
                if (existAll) _listCollector.Insert(0, new CollectorInfo { CollectorId = 0, Name = allValue });
                return new SelectList(_listCollector, "CollectorId", "Name");
            }
            if (type == typeof(StatusMapInfo))
            {
                if (_listStatusMap.IsNullOrEmpty()) _listStatusMap = StatusMapRepository.GetAll();
                _listStatusMap.RemoveAll(c => c.Id == 0);
                if (existAll) _listStatusMap.Insert(0, new StatusMapInfo { Id = 0, Name = allValue });
                return new SelectList(_listStatusMap, "Id", "Name");
            }
            if (type == typeof(BranchInfo))
            {
                if (_listBranch.IsNullOrEmpty()) _listBranch = BranchRepository.GetAll();
                _listBranch.RemoveAll(c => c.BranchId == 0);
                if (existAll) _listBranch.Insert(0, new BranchInfo { BranchId = 0, Name = allValue });
                return new SelectList(_listBranch, "BranchId", "Name");
            }
            if (type == typeof(UserInfo))
            {
                if (ListUser.IsNullOrEmpty()) _listUser = UserRepository.GetAll();
                if (existAll)
                {
                    List<UserInfo> listUser = new List<UserInfo>(ListUser);
                    if (existAll) listUser.Insert(0, new UserInfo { UserID = 0, FullName = "Tất cả" });
                    return new SelectList(listUser, "UserID", "FullName");
                }
                return new SelectList(ListUser, "UserID", "FullName");
            }
            if (type == typeof(ChannelInfo))
            {
                //if (ListChannel.IsNullOrEmpty()) ListChannel = ChannelRepository.GetAll();
                //ListChannel.RemoveAll(c => c.ChannelId == 0);
                //if (existAll) ListChannel.Insert(0, new ChannelInfo { ChannelId = 0, Name = allValue });
                //return new SelectList(ListChannel, "ChannelId", "Name");
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                //if (ListFeeMoneyType.IsNullOrEmpty()) ListFeeMoneyType = CatalogRepository.GetAll<FeeMoneyTypeInfo>();
                //ListFeeMoneyType.RemoveAll(c => c.Id == 0);
                //if (existAll) ListFeeMoneyType.Insert(0, new FeeMoneyTypeInfo { Id = 0, Name = allValue });
                return new SelectList(FeeMoneyTypeRepository.GetAll(), "Id", "Name");
            }
            if (type == typeof(QualityInfo))
            {
                if (_listQuality.IsNullOrEmpty()) _listQuality = QualityRepository.GetAll();
                _listQuality.RemoveAll(c => c.Id == 0);
                if (existAll) _listQuality.Insert(0, new QualityInfo { Id = 0, Name = allValue });
                return new SelectList(_listQuality, "Id", "Name");
            }
            if (type == typeof(PackageInfo))
            {
                if (ListPackageFeeEdu.IsNullOrEmpty()) ListPackage = CatalogRepository.GetAll<PackageInfo>();
                ListPackageFeeEdu.RemoveAll(c => c.Id == 0);
                if (existAll) ListPackage.Insert(0, new PackageInfo { Id = 0, Name = allValue });
                return new SelectList(ListPackage, "Id", "Name");
            }
            return null;
        }

        public static SelectList SelectedListUser(int branchId, int groupId, EmployeeType employeeType)
        {
            var userGroups = CatalogRepository.UserGroupGetAll() ?? new List<UserGroupInfo>();
            var userBranchs = CatalogRepository.UserBranchGetAll() ?? new List<UserBranchInfo>();

            var groups = new List<GroupInfo> { new GroupInfo { GroupId = groupId } };
            if (groupId == 0) groups = ListGroup.Where(c => c.EmployeeTypeId == (int)employeeType).ToList();

            var userInBranchs = branchId == 0
                ? userBranchs.Select(c => c.UserId).Distinct().ToList()
                : userBranchs.Where(c => c.BranchId == branchId).Select(c => c.UserId).Distinct().ToList();
            var userInGroups = userGroups.Where(c => groups.Any(p => p.GroupId == c.GroupId)).Select(c => c.UserId).Distinct().ToList();
            var users = ListUser ?? UserRepository.GetAll();
            users = users.Where(c => userInGroups.Any(p => p == c.UserID))
                .Where(c => groups.Any(p => p.GroupId == c.GroupId))
                .Where(c => userInBranchs.Any(p => p == c.UserID))
                .ToList();

            var result = new List<UserInfo>();
            foreach (var entity in users)
            {
                entity.Norms = employeeType == EmployeeType.Collaborator
                    ? entity.NormsCollaborator
                    : entity.NormsConsultant;
                if (result.Any(c => c.UserID == entity.UserID)) continue;
                result.Add(entity);
            }
            return new SelectList(result, "UserID", "FullName");
        }

        public static string CheckAccountUseHandover(EmployeeType employeeType)
        {
            var userDrafts = UserDraftRepository.GetAll() ?? new List<UserDraftInfo>();
            var userDraft = userDrafts.FirstOrDefault(c => c.IsDraftCollabortor || c.IsDraftConsultant);
            if (userDraft != null)
            {
                var function = userDraft.IsDraftCollabortor ? "Lọc" : "TVTS";
                var userInfo = ListUser.FirstOrDefault(c => c.UserID == userDraft.UserId) ?? new UserInfo();
                if (userInfo.UserID != UserContext.GetCurrentUser().UserID)
                    return "Hiện tại \"" + userInfo.FullName + "\" đang sử dụng chức năng phân contact cho " + function + ", vui lòng chờ";
                switch (employeeType)
                {
                    case EmployeeType.Collaborator:
                        {
                            if (userDraft.IsDraftConsultant)
                                return "Hiện tại bạn đang sử dụng chức năng phân contact cho " + function + ", vui lòng xóa nháp";
                        }
                        break;
                    case EmployeeType.Consultant:
                        {
                            if (userDraft.IsDraftCollabortor)
                                return "Hiện tại bạn đang sử dụng chức năng phân contact cho " + function + ", vui lòng xóa nháp";
                        }
                        break;
                }
            }
            return string.Empty;
        }
    }
}

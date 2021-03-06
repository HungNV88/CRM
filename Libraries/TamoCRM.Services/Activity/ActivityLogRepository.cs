using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Activity;
using TamoCRM.Domain.Contacts;
using TamoCRM.Services.UserRole;

namespace TamoCRM.Services.Activity
{
    public class ActivityLogRepository
    {
        public static int Create(ActivityLogInfo info)
        {
            return DataProvider.Instance().ActivityLogs_Insert(info.FunctionId, info.FunctionType, info.CreatedBy, info.CreatedDate);
        }

        public static int LogCareContact(ContactInfo entity, params LogPropertyType[] types)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    FunctionType = (int)LogFunctionType.CareContactConsultant,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log, types);
                return log.Id;
            }
            catch
            {

            }
            return 0;
        }

        public static int LogCreateContactTvts(ContactInfo entity)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                    FunctionType = (int)LogFunctionType.CreateContactTvts,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log,
                                LogPropertyType.Contacts_StatusMap_Consultant,
                                LogPropertyType.Contacts_StatusCare_Consultant,
                                LogPropertyType.Contacts_Level,
                                LogPropertyType.Contacts_Type,
                                LogPropertyType.Contacts_Channel,
                                LogPropertyType.Contacts_Branch,
                                LogPropertyType.Contacts_Status,
                                LogPropertyType.Contacts_User_Consultant,
                                LogPropertyType.Contacts_HandoverDate_Consultant,
                                LogPropertyType.Contacts_AppointmentDate_Consultant);
                return log.Id;
            }
            catch
            {
            }
            return 0;
        }

        public static int LogCreateContactTvtsDuplicate(ContactInfo entity)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                    FunctionType = (int)LogFunctionType.CreateContactTvts,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log, LogPropertyType.Contacts_User_Consultant);
                return log.Id;
            }
            catch
            {
            }
            return 0;
        }

        public static int LogCreateContactHotline(ContactInfo entity)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                    FunctionType = (int)LogFunctionType.CreateContactHotline,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log,
                                LogPropertyType.Contacts_StatusMap_Consultant,
                                LogPropertyType.Contacts_StatusCare_Consultant,
                                LogPropertyType.Contacts_Level,
                                LogPropertyType.Contacts_Type,
                                LogPropertyType.Contacts_Channel,
                                LogPropertyType.Contacts_Branch,
                                LogPropertyType.Contacts_Status);
                if (entity.UserConsultantId > 0) LogObjectChange(entity, log, LogPropertyType.Contacts_User_Consultant);
                return log.Id;
            }
            catch
            {
            }
            return 0;
        }

        public static int LogRecoveryContact(ContactInfo entity)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    FunctionType = (int)LogFunctionType.RecoveryConsultant,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log,
                                LogPropertyType.Contacts_Status,
                                LogPropertyType.Contacts_RecoveryDate_Consultant,
                                LogPropertyType.Contacts_AppointmentDate_Consultant);
                return log.Id;
            }
            catch
            {
            }
            return 0;
        }

        public static int LogHandoverContact(ContactInfo entity)
        {
            try
            {
                var log = new ActivityLogInfo
                {
                    FunctionId = entity.Id,
                    CreatedBy = UserRepository.GetCurrentUserInfo().UserID,
                    FunctionType = (int)LogFunctionType.HandoverToConsultant,
                };
                log.Id = Create(log);

                LogObjectChange(entity, log,
                                LogPropertyType.Contacts_User_Consultant,
                                LogPropertyType.Contacts_Status,
                                LogPropertyType.Contacts_HandoverDate_Consultant,
                                LogPropertyType.Contacts_StatusMap_Consultant,
                                LogPropertyType.Contacts_StatusCare_Consultant,
                                LogPropertyType.Contacts_AppointmentDate_Consultant);
                return log.Id;
            }
            catch
            {
            }
            return 0;
        }

        private static void LogObjectChange(ContactInfo entity, ActivityLogInfo log, params LogPropertyType[] types)
        {
            foreach (var type in types)
            {
                var logObject = new ActivityObjectChangeInfo
                {
                    ActivityId = log.Id,
                    ObjectId = entity.Id,
                    PropertyType = (int)type,
                    ObjectType = (int)LogObjectType.Contact,
                };
                switch (type)
                {
                    case LogPropertyType.Contacts_User_Consultant:
                        logObject.PropertyValueInt = entity.UserConsultantId;
                        break;
                    case LogPropertyType.Contacts_Type:
                        logObject.PropertyValueInt = entity.TypeId;
                        break;
                    case LogPropertyType.Contacts_Level:
                        logObject.PropertyValueInt = entity.LevelId;
                        break;
                    case LogPropertyType.Contacts_Channel:
                        logObject.PropertyValueInt = entity.ChannelId;
                        break;
                    case LogPropertyType.Contacts_Branch:
                        logObject.PropertyValueInt = entity.BranchId;
                        break;
                    case LogPropertyType.Contacts_Status:
                        logObject.PropertyValueInt = entity.StatusId;
                        break;
                    case LogPropertyType.Contacts_AppointmentDate_Consultant:
                        logObject.PropertyValueDateTime = entity.AppointmentConsultantDate;
                        break;
                    case LogPropertyType.Contacts_HandoverDate_Consultant:
                        logObject.PropertyValueDateTime = entity.HandoverConsultantDate;
                        break;
                    case LogPropertyType.Contacts_CallDate_Consultant:
                        logObject.PropertyValueDateTime = entity.CallConsultantDate;
                        break;
                    case LogPropertyType.Contacts_RecoveryDate_Consultant:
                        logObject.PropertyValueDateTime = entity.RecoveryConsultantDate;
                        break;
                    case LogPropertyType.Contacts_StatusCare_Consultant:
                        logObject.PropertyValueInt = entity.StatusCareConsultantId;
                        break;
                    case LogPropertyType.Contacts_StatusMap_Consultant:
                        logObject.PropertyValueInt = entity.StatusMapConsultantId;
                        break;
                }
                ActivityObjectChangeRepository.Create(logObject);
            }
        }
    }        
}

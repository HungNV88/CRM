using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Core.Commons.Utilities;


namespace TamoCRM.Services.ContactLevelInfos
{
    public class ContactLevelInfoRepository
    {
        public static int Update(ContactLevelInfo info)
        {
            return 1;
            //return DataProvider.Instance().ContactLevelInfos_Update(info.ContactId, info.HaveJob, info.JobTitle, info.EducationLevelId, info.EducationSchoolName, info.CalledDate, info.CollaboratorName, info.SchoolName, info.WantStudyEnglish, info.WantStudyNotes, info.Majors, info.HasAppointment, info.AppointmentTime, info.ApointmentType, info.ApointmentNotes, info.SubmitedForm, info.SubmitedFormNotes, info.FeeReview, info.FeeReviewTime, info.FeeReviewNotes, info.AcceptedReview, info.AcceptedReviewNotes, info.FeeEdu, info.FeeEduNotes, info.SubmitedProfile, info.SubmitedProfileType, info.SubmitedProfileNotes, info.LumpedAdmissionId, info.Completed, info.CompletedTime, info.CompletedNotes, info.LumpedCareId, info.LumpedFeeId, info.LatchId, info.SchoolGrad, info.MajorGrad, info.HaveLearnTransfer);
        }
        public static int UpdateInsertTvts(ContactLevelInfo info)
        {
            return DataProvider.Instance().ContactLevelInfos_Update_InsertTvts(info.ContactId);
        }
        public static ContactLevelInfo GetInfo(int contactLevelInfoId)
        {
            return ObjectHelper.FillObject<ContactLevelInfo>(DataProvider.Instance().ContactLevelInfos_GetInfo(contactLevelInfoId));
        }
        public static List<ContactLevelInfo> GetInfos_Xml(string xml)
        {
            return ObjectHelper.FillCollection<ContactLevelInfo>(DataProvider.Instance().ContactLevelInfos_GetInfos_Xml(xml));
        }
        public static List<ContactLevelInfo> GetInfos(string ids)
        {
            return ObjectHelper.FillCollection<ContactLevelInfo>(DataProvider.Instance().ContactLevelInfos_GetInfos(ids));
        }
        
        public static ContactLevelInfo GetInfoByContactId(int contactId)
        {
            return ObjectHelper.FillObject<ContactLevelInfo>(DataProvider.Instance().ContactLevelInfos_GetInfo_ByContactId(contactId));
        }
        public static List<ContactLevelInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ContactLevelInfo>(DataProvider.Instance().ContactLevelInfos_GetAll());
        }
        
        public static List<ContactLevelInfo> Search(int collectorId, int sourceTypeId, int levelId, int channelId, int statusId, int statusMapId, int branchId , int page, int rows, out int totalRecords)
        {
            return FillContactLevelInfoCollection(DataProvider.Instance().ContactLevelInfos_Search(collectorId, sourceTypeId, levelId, channelId, statusId, statusMapId, branchId, page, rows), out totalRecords);
        }
        private static List<ContactLevelInfo> FillContactLevelInfoCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<ContactLevelInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new ContactLevelInfo
                                   {
                                       ContactId = ConvertHelper.ToInt32(reader["ContactId"]),
                                       EducationSchoolName = ConvertHelper.ToString(reader["EducationLevelNotes"]),
                                       WantStudyEnglish = ConvertHelper.ToBoolean(reader["WantStudy"]),
                                       HasAppointment = ConvertHelper.ToBoolean(reader["Appointment"]),
                                       ApointmentType = ConvertHelper.ToInt32(reader["ApointmentType"]),
                                       ApointmentNotes = ConvertHelper.ToString(reader["ApointmentNotes"]),
                                   };
                    retVal.Add(info);
                }
                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }
                                
            }
            catch
            {
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }

        public static void UpdateHasAppointmentInterview(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasAppointmentInterview(contactId, value);
        }
        public static void UpdateHasCasecAccount(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasCasecAccount(contactId, value);
        }
        public static void UpdateHasTopicaAccount(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasTopicaAccount(contactId, value);
        }
        public static void UpdateHasHandoverAdvisor(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HandoverAdvisor(contactId, value);
        }
        public static void UpdateHasLinkSb100(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasLinkSb100(contactId, value);
        }
        public static void UpdateHasLinkSb100Topica(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasLinkSb100Topica(contactId, value);
        }
        public static void UpdateHasSetSb100(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasSetSb100(contactId, value);
        }

        public static void UpdateHasSetSb100Topica(int contactId, bool value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HasSetSb100Topica(contactId, value);
        }
        public static void UpdateHandoverAdvisorStatus(string code, int value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HandoverAdvisorStatus(code, value);
        }

        public static void UpdateHandoverAdvisorStatusWithContactId(int contactid, int value)
        {
            DataProvider.Instance().ContactLevelInfos_Update_HandoverAdvisorStatusWithContactId(contactid, value);
        }

        public static void UpdateLevelStudyAdvisor(int contactid,int levelstudyadvisorid)
        {
            DataProvider.Instance().ContactLevelInfo_Update_LevelStudyAdvisor(contactid, levelstudyadvisorid);
        }

        public static void Update_DateWantToLearn(int contactid,DateTime? datewanttolearn)
        {
            DataProvider.Instance().ContactLevelInfo_Update_DateWantToLearn(contactid, datewanttolearn);
        }
        public static void Update_HasAccPayment(int contactid)
        {
            DataProvider.Instance().ContactLevelInfo_Update_HasAccPayment(contactid);
        }
        public static void Update_FeeEdu_PackagePriceSale(string code, int feeedu)
        {
            DataProvider.Instance().ContactLevelInfo_Update_FeeEdu_PackagePriceSale(code, feeedu);
        }
       
    }        
}

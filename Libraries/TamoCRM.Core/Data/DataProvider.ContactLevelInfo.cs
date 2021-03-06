using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int ContactLevelInfos_Update(int contactId, bool haveJob, string jobTitle, int educationLevelId, string educationNotes, DateTime? calledDate, string collaboratorName, string schoolName, bool wantStudy, string wantStudyNotes, string majors, bool appointment, DateTime? appointmentTime, int apointmentType, string apointmentNotes, bool submitedForm, string submitedFormNotes, bool feeReview, DateTime? feeReviewTime, string feeReviewNotes, bool acceptedReview, string acceptedReviewNotes, bool feeEdu, string feeEduNotes, bool submitedProfile, int submitedProfileType, string submitedProfileNotes, int lumpedAdmissionId, bool completed, DateTime? completedTime, string completedNotes, int lumpedCareId, int lumpedFeeId, int latchId, string schoolGrad, string majorGrad, bool haveLearnTransfer);
        public abstract int ContactLevelInfos_Update_InsertTvts(int contactId);
        public abstract IDataReader ContactLevelInfos_GetInfos(string ids);
        public abstract IDataReader ContactLevelInfos_GetInfos_Xml(string xml);
        public abstract IDataReader ContactLevelInfos_GetInfo(int contactLevelInfoId);
        public abstract IDataReader ContactLevelInfos_GetInfo_ByContactId(int contactId);
        public abstract IDataReader ContactLevelInfos_GetAll();
        public abstract IDataReader ContactLevelInfos_Search(int collectorId, int souceTypeId, int levelId, int channelId, int statusId, int statusMapId, int branchId, int page, int rows);

        public abstract void ContactLevelInfos_Update_HasAppointmentInterview(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasCasecAccount(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasTopicaAccount(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HandoverAdvisor(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasLinkSb100(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasLinkSb100Topica(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasSetSb100(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HasSetSb100Topica(int contactId, bool value);
        public abstract void ContactLevelInfos_Update_HandoverAdvisorStatus(string contactId, int value);
        public abstract void ContactLevelInfos_Update_HandoverAdvisorStatusWithContactId(int contactid, int value);
        public abstract void ContactLevelInfo_Update_LevelStudyAdvisor(int contactid,int level_study_advisor);
        public abstract void ContactLevelInfo_Update_DateWantToLearn(int contactid, DateTime? date_time_to_learn);
        public abstract void ContactLevelInfo_Update_HasAccPayment(int contactid);
        public abstract void ContactLevelInfo_Update_FeeEdu_PackagePriceSale(string code, int feeedu);

    }
}


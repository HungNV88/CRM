using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int ContactLevelInfos_Update(int contactId, bool haveJob, string jobTitle, int educationLevelId, string educationNotes, DateTime? calledDate, string collaboratorName, string schoolName, bool wantStudy, string wantStudyNotes, string majors, bool appointment, DateTime? appointmentTime, int apointmentType, string apointmentNotes, bool submitedForm, string submitedFormNotes, bool feeReview, DateTime? feeReviewTime, string feeReviewNotes, bool acceptedReview, string acceptedReviewNotes, bool feeEdu, string feeEduNotes, bool submitedProfile, int submitedProfileType, string submitedProfileNotes, int lumpedAdmissionId, bool completed, DateTime? completedTime, string completedNotes, int lumpedCareId, int lumpedFeeId, int latchId, string schoolGrad, string majorGrad, bool haveLearnTransfer)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update"), contactId, haveJob, jobTitle, educationLevelId, educationNotes, calledDate, collaboratorName, schoolName, wantStudy, wantStudyNotes, majors, appointment, appointmentTime, apointmentType, apointmentNotes, submitedForm, submitedFormNotes, feeReview, feeReviewTime, feeReviewNotes, acceptedReview, acceptedReviewNotes, feeEdu, feeEduNotes, submitedProfile, submitedProfileType, submitedProfileNotes, lumpedAdmissionId, completed, completedTime, completedNotes, lumpedCareId, lumpedFeeId, latchId, schoolGrad, majorGrad, haveLearnTransfer);
        }

        public override int ContactLevelInfos_Update_InsertTvts(int contactId)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_InsertTvts"), contactId);
        }

        public override IDataReader ContactLevelInfos_GetInfos_Xml(string xml)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_GetInfos_Xml"), xml);
        }

        public override IDataReader ContactLevelInfos_GetInfo(int contactLevelInfoId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_GetInfo"), contactLevelInfoId);
        }

        public override IDataReader ContactLevelInfos_GetInfos(string ids)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_GetInfos"), ids);
        }

        public override IDataReader ContactLevelInfos_GetInfo_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_GetInfo_ByContactId"), contactId);
        }

        public override IDataReader ContactLevelInfos_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_GetAll"));
        }

        public override IDataReader ContactLevelInfos_Search(int collectorId, int sourceTypeId, int levelId, int channelId, int statusId, int statusMapId, int branchId, int page, int rows)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Search"), collectorId, sourceTypeId, levelId, channelId, statusId, statusMapId, branchId, page, rows);
        }

        public override void ContactLevelInfos_Update_HasAppointmentInterview(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasAppointmentInterview"), contactId, value);
        }

        public override void ContactLevelInfos_Update_HasCasecAccount(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasCasecAccount"), contactId, value);
        }
        
        public override void ContactLevelInfos_Update_HasTopicaAccount(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasTopicaAccount"), contactId, value);
        }

        public override void ContactLevelInfos_Update_HandoverAdvisor(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HandoverAdvisor"), contactId, value);
        }

        public override void ContactLevelInfos_Update_HasLinkSb100(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasLinkSb100"), contactId, value);
        }

        public override void ContactLevelInfos_Update_HasLinkSb100Topica(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasLinkSb100Topica"), contactId, value);
        }

        public override void ContactLevelInfos_Update_HasSetSb100(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasSetSb100"), contactId, value);    
        }

        public override void ContactLevelInfos_Update_HasSetSb100Topica(int contactId, bool value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HasSetSb100Topica"), contactId, value);    
        }

        public override void ContactLevelInfos_Update_HandoverAdvisorStatus(string code, int value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HandoverAdvisorStatus"), code, value);
        }

        public override void ContactLevelInfos_Update_HandoverAdvisorStatusWithContactId(int contactid, int value)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_HandoverAdvisorStatusWithContactId"), contactid, value);
        }

        public override void ContactLevelInfo_Update_LevelStudyAdvisor(int contactid, int levelstudyadvisor)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_LevelStudyAdvisor"), contactid, levelstudyadvisor);
        }

        public override void ContactLevelInfo_Update_DateWantToLearn(int contactid, DateTime? date_time_to_learn)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfos_Update_DateTimeWantToLearn"), contactid, date_time_to_learn);
        }

        public override void ContactLevelInfo_Update_HasAccPayment(int contactid)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfo_Update_HasAccPayment"), contactid);
        }

        public override void ContactLevelInfo_Update_FeeEdu_PackagePriceSale(string code, int feeedu)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactLevelInfo_Update_FeeEdu_PackagePriceSale"), code, feeedu);
        }

    }
}


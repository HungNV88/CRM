using System;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Contacts_Update_Care_Consultant(int contactId, string fullName, string address, DateTime? birthDay, int gender,
             string email1, string email2, string notes, DateTime? apointmentDate, DateTime? callDate, int levelId, int statusId,
             int statusMapId, int statusCareId, string callInfo, int callCount, int productSellId, int productSoldId, int productSaleId,
             int handoverHistoryId, int qualityId, int nationId, string educationSchoolName, bool wantStudyEnglish, bool hasAppointment,
             DateTime? appointmentTime, int apointmentType, string apointmentNotes, bool hasAppointmentInterview,
             int appointmentInterviewId, bool hasCasecAccount, bool hasPointTestCasec, bool hasPointInterview, bool hasSetSb100,
             bool hasLinkSb100,bool hasAccPayment,int HandoverAdvisor, int feePackageType, int feeTuitionType, int feeMoneyType, double feeEdu, double feeEduYet,
             string feeEduNotes, double packagePriceSale, string dacdiemhocvien, int pricePackageListedId, int feePackageTypeL7L8, string packageWantToLearn, int learnNumberDay, int feeEduDealsReason, string noteDealsReason, int numberDealsGroup,bool hasTestTechnical, int callHistoryId, int statusUpdate, int callType, int userLogType, string mobile1,
             string mobile2, string mobile3, int statusMissedCall, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Update_Care_Consultant"),
                contactId, fullName, address, birthDay, gender, email1, email2, notes, apointmentDate, callDate, levelId, statusId,
                statusMapId, statusCareId, callInfo, callCount, productSellId, productSoldId, productSaleId, handoverHistoryId, qualityId, nationId,
                educationSchoolName, wantStudyEnglish, hasAppointment, appointmentTime,
                apointmentType, apointmentNotes, hasAppointmentInterview, appointmentInterviewId, hasCasecAccount, hasPointTestCasec,
                hasPointInterview, hasSetSb100, hasLinkSb100, hasAccPayment, HandoverAdvisor, feePackageType, feeTuitionType, 
                feeMoneyType, feeEdu, feeEduYet, feeEduNotes, packagePriceSale, dacdiemhocvien, pricePackageListedId,
                feePackageTypeL7L8, packageWantToLearn, learnNumberDay, feeEduDealsReason, noteDealsReason, numberDealsGroup, hasTestTechnical, callHistoryId,
                statusUpdate, callType, userLogType, mobile1, mobile2, mobile3, statusMissedCall, createdBy);
        }

        public override int Contacts_Update_Care_Collaborator(int contactId, string fullName, string address, DateTime? birthDay, int gender,
            string email1, string email2, string notes, DateTime? apointmentDate, DateTime? callDate, int levelId, int statusId,
            int statusMapId, int statusCareId, string callInfo, int callCount, int handoverHistoryId, int qualityId, int userCollaboratorId,
            bool wantStudyEnglish, bool hasAppointment, DateTime? appointmentTime, int apointmentType, string apointmentNotes,
            int callHistoryId, int statusUpdate, int callType, int userLogType, string mobile1, string mobile2, string mobile3,
            int statusMissedCall, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Update_Care_Collaborator"),
                contactId, fullName, address, birthDay, gender, email1, email2, notes, apointmentDate, callDate, levelId, statusId,
                statusMapId, statusCareId, callInfo, callCount, handoverHistoryId, qualityId, userCollaboratorId, wantStudyEnglish, hasAppointment, appointmentTime,
                apointmentType, apointmentNotes, callHistoryId, statusUpdate, callType, userLogType, mobile1, mobile2,
                mobile3, statusMissedCall, createdBy);
        }
    }
}


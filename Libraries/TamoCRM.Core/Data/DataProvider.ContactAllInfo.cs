using System;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Contacts_Update_Care_Consultant(int contactId, string fullName, string address,
            DateTime? birthDay, int gender, string email1, string email2, string notes, DateTime? appointmentDate,
            DateTime? callDate, int levelId, int statusId, int statusMapId, int statusCareId, string callInfo, int callCount,
            int productSellId, int productSoldId, int productSaleId, int handoverHistoryId, int qualityId, int nationId, string educationSchoolName, bool wantStudyEnglish, bool hasAppointment, 
            DateTime? appointmentTime, int apointmentType, string apointmentNotes, bool hasAppointmentInterview, int appointmentInterviewId, bool hasCasecAccount,
            bool hasPointTestCasec, bool hasPointInterview, bool hasSetSb100, bool hasLinkSb100, bool hasAccPayment, int HandoverAdvisor, int feePackageType, int feeTuitionType, int feeMoneyType, double feeEdu,
            double feeEduYet, string feeEduNotes, double packagePriceSale, string dacdiemhocvien, int pricePackageListedId, int feePackageTypeL7L8, string packageWantToLearn, int LearnNumberDay, int feeEduDealsReason, string noteDealsReason, int NumberDealsGroup,bool HasTestTechnical, int callHistoryId, int statusUpdate, int callType, int userLogType, string mobile1, string mobile2, string mobile3, int statusMissedCall, int createdBy);

        public abstract int Contacts_Update_Care_Collaborator(int contactId, string fullName, string address, DateTime? birthDay, 
            int gender, string email1, string email2, string notes, DateTime? apointmentDate, DateTime? callDate, int levelId, int statusId,
            int statusMapId, int statusCareId, string callInfo, int callCount, int handoverHistoryId, int qualityId, int userCollaboratorId, bool wantStudyEnglish, bool hasAppointment, 
            DateTime? appointmentTime, int apointmentType, string apointmentNotes, int callHistoryId, int statusUpdate, int callType, int userLogType, string mobile1, string mobile2, string mobile3, int statusMissedCall, int createdBy);
    }
}


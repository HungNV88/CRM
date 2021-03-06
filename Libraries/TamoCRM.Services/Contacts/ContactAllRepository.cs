using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Services.Contacts
{
    public class ContactAllRepository
    {
        public static int Update(ContactAllInfo info, EmployeeType type)
        {
            if (info.Mobile1.StartsWith("0")) info.Mobile1 = info.Mobile1.Substring(1);
            if (info.Mobile2.StartsWith("0")) info.Mobile2 = info.Mobile2.Substring(1);
            if (info.Mobile3.StartsWith("0")) info.Mobile3 = info.Mobile3.Substring(1);
            switch (type)
            {
                case EmployeeType.Collaborator:
                    return DataProvider.Instance().Contacts_Update_Care_Collaborator(info.ContactInfo.Id,
                        info.ContactInfo.Fullname,
                        info.ContactInfo.Address,
                        info.ContactInfo.Birthday,
                        info.ContactInfo.Gender,
                        info.ContactInfo.Email,
                        info.ContactInfo.Email2,
                        info.ContactInfo.Notes,
                        info.ContactInfo.AppointmentCollaboratorDate,
                        info.ContactInfo.CallCollaboratorDate,
                        info.ContactInfo.LevelId,
                        info.ContactInfo.StatusId,
                        info.ContactInfo.StatusMapCollaboratorId,
                        info.ContactInfo.StatusCareCollaboratorId,
                        info.ContactInfo.CallInfoCollaborator,
                        info.ContactInfo.CallCount,
                        info.ContactInfo.HandoverHistoryCollaboratorId,
                        info.ContactInfo.QualityId,
                        info.ContactInfo.UserCollaboratorId,
                        info.ContactLevelInfo.WantStudyEnglish,
                        info.ContactLevelInfo.HasAppointment,
                        info.ContactLevelInfo.AppointmentTime,
                        info.ContactLevelInfo.ApointmentType,
                        info.ContactLevelInfo.ApointmentNotes,
                        info.CallHistoryInfo.CallHistoryId,
                        info.CallHistoryInfo.StatusUpdate,
                        info.CallHistoryInfo.CallType,
                        info.CallHistoryInfo.UserLogType,
                        info.Mobile1,
                        info.Mobile2,
                        info.Mobile3,
                        info.MissedCallInfo.Status,
                        info.ContactInfo.CreatedBy);
                case EmployeeType.Consultant:
                    return DataProvider.Instance().Contacts_Update_Care_Consultant(info.ContactInfo.Id,
                        info.ContactInfo.Fullname,
                        info.ContactInfo.Address,
                        info.ContactInfo.Birthday,
                        info.ContactInfo.Gender,
                        info.ContactInfo.Email,
                        info.ContactInfo.Email2,
                        info.ContactInfo.Notes,
                        info.ContactInfo.AppointmentConsultantDate,
                        info.ContactInfo.CallConsultantDate,
                        info.ContactInfo.LevelId,
                        info.ContactInfo.StatusId,
                        info.ContactInfo.StatusMapConsultantId,
                        info.ContactInfo.StatusCareConsultantId,
                        info.ContactInfo.CallInfoConsultant,
                        info.ContactInfo.CallCount,
                        info.ContactInfo.ProductSellId,
                        info.ContactInfo.ProductSoldId,
                        info.ContactInfo.ProductSaleId,
                        info.ContactInfo.HandoverHistoryConsultantId,
                        info.ContactInfo.QualityId,
                        info.ContactInfo.NationId,
                        info.ContactLevelInfo.EducationSchoolName,
                        info.ContactLevelInfo.WantStudyEnglish,
                        info.ContactLevelInfo.HasAppointment,
                        info.ContactLevelInfo.AppointmentTime,
                        info.ContactLevelInfo.ApointmentType,
                        info.ContactLevelInfo.ApointmentNotes,
                        info.ContactLevelInfo.HasAppointmentInterview,
                        info.ContactLevelInfo.AppointmentInterviewId,
                        info.ContactLevelInfo.HasCasecAccount,
                        info.ContactLevelInfo.HasPointTestCasec,
                        info.ContactLevelInfo.HasPointInterview,
                        info.ContactLevelInfo.HasSetSb100,
                        info.ContactLevelInfo.HasLinkSb100,
                        info.ContactLevelInfo.HasAccPayment,
                        info.ContactLevelInfo.HandoverAdvisor,
                        info.ContactLevelInfo.FeePackageType,
                        info.ContactLevelInfo.FeeTuitionType,
                        info.ContactLevelInfo.FeeMoneyType,
                        info.ContactLevelInfo.FeeEdu,
                        info.ContactLevelInfo.FeeEduYet,
                        info.ContactLevelInfo.FeeEduNotes,
                        info.ContactLevelInfo.PackagePriceSale,
                        info.ContactLevelInfo.DacDiemHocVien,
                        info.ContactLevelInfo.PricePackageListedId,
                        info.ContactLevelInfo.FeePackageTypeL7L8,
                        info.ContactLevelInfo.PackageWantToLearn,
                        info.ContactLevelInfo.LearnNumberDay,
                        info.ContactLevelInfo.FeeEduDealsReason,
                        info.ContactLevelInfo.NoteDealsReason,
                        info.ContactLevelInfo.NumberDealsGroup,
                        info.ContactLevelInfo.HasTestTechnical,
                        info.CallHistoryInfo.CallHistoryId,
                        info.CallHistoryInfo.StatusUpdate,
                        info.CallHistoryInfo.CallType,
                        info.CallHistoryInfo.UserLogType,
                        info.Mobile1,
                        info.Mobile2,
                        info.Mobile3,
                        info.MissedCallInfo.Status,
                        info.ContactInfo.CreatedBy);
            }            
            return 0;
        }
    }
}

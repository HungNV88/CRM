using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactModel
    {
        public int Id { get; set; }
        
        public int ContactId { get; set; }

        public string UserName { get; set; }
        public int ConsultantUserId { get; set; }

        public string Code { get; set; }

        public string Fullname { get; set; }

        public DateTime? Birth { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Mobile2 { get; set; }

        public string Tel { get; set; }

        public string Address { get; set; }

        public int Gender { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int ChannelId { get; set; }

        public string ChannelName { get; set; }

        public int SchoolId { get; set; }

        public string SchoolName { get; set; }
        public string Landingpage { get; set; }
        public int LevelId { get; set; }

        public string LevelName { get; set; }

        public int CollectorId { get; set; }

        public string CollectorName { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public int StatusMapId { get; set; }

        public string StatusMapName { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public string Notes { get; set; }

        public int UserId { get; set; }

        public int RecoveryStatus { get; set; }

        public DateTime RecoveryDate { get; set; }

        public DateTime? CalledDate { get; set; }

        public string CollaboratorName { get; set; }

        public int? EducationLevelId { get; set; }

        public string EducationLevelName { get; set; }

        public string EducationLevelNotes { get; set; }

        public bool HaveJob { get; set; }

        public string JobTitle { get; set; }

        public bool WantStudy { get; set; }

        public string WantStudyNotes { get; set; }

        public bool Want_BA { get; set; }

        public bool Want_FA { get; set; }

        public bool Want_IT { get; set; }

        public bool Want_FB { get; set; }

        public bool Want_Law { get; set; }

        public bool Want_Others { get; set; }

        public bool Appointment { get; set; }

        public int ApointmentType { get; set; }

        public string ApointmentNotes { get; set; }

        public bool SubmitedForm { get; set; }

        public string SubmitedFormNotes { get; set; }

        public bool FeeReview { get; set; }

        public DateTime? FeeReviewTime { get; set; }

        public string FeeReviewNotes { get; set; }

        public bool AcceptedReview { get; set; }

        public string AcceptedReviewNotes { get; set; }

        public double FeeEdu { get; set; }

        public string FeeEduNotes { get; set; }

        public bool SubmitedProfile { get; set; }

        public string SubmitedProfileNotes { get; set; }

        public DateTime? AdmissionTime { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string CompletedNotes { get; set; }

        public string SchoolGrad { get; set; }

        public string MajorGrad { get; set; }

        public int LocationId { get; set; }

        public int MajorId { get; set; }
        public int? StatusMapDistributorId { get; set; }
        public string DuplicateInfo { get; set; } 

        // thêm 10/12/2015
        public DateTime? CallConsultantDate { get; set; } 
        
    }
}
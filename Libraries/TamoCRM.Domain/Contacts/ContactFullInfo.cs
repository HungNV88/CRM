using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TamoCRM.Domain.Contacts
{
    public class ContactFullInfo : ContactInfo
    {
        public int ContactId { get; set; }

        public bool HaveJob { get; set; }

        public string JobTitle { get; set; }

        public int EducationLevelId { get; set; }

        public string EducationLevelNotes { get; set; }

        public string SchoolName { get; set; }

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

        public int ApointmentNotes { get; set; }

        public bool SubmitedForm { get; set; }

        public string SubmitedFormNotes { get; set; }

        public bool FeeReview { get; set; }

        public DateTime FeeReviewTime { get; set; }

        public string FeeReviewNotes { get; set; }

        public bool AcceptedReview { get; set; }

        public string AcceptedReviewNotes { get; set; }

        public bool FeeEdu { get; set; }

        public string FeeEduNotes { get; set; }

        public bool SubmitedProfile { get; set; }

        public string SubmitedProfileNotes { get; set; }

        public DateTime AdmissionTime { get; set; }

        public bool Completed { get; set; }

        public DateTime CompletedTime { get; set; }

        public string CompletedNotes { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TamoCRM.Domain.ContactLevelInfos
{
    public class ContactLevelInfo
    {
		public int ContactId { get;set; }
        public string EducationSchoolName { get; set; }
        public bool WantStudyEnglish { get; set; }

        public bool HasAppointment { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public int ApointmentType { get; set; }
        public string ApointmentNotes { get; set; }

        public bool HasAppointmentInterviewHidden { get; set; }
        public bool HasAppointmentInterview { get; set; }
        public int AppointmentInterviewId { get; set; }

        public bool HasCasecAccountHidden { get; set; }
        public bool HasCasecAccount { get; set; }
        public bool HasTopicaAccount { get; set; }
        public int CasecAccountId { get; set; }

        public int TopicaAccountId { get; set; }
        public bool HasPointTestCasecHidden { get; set; }
        public bool HasPointTestCasec { get; set; }
        public bool HasPointTestTopica { get; set; }
        public bool HasPointInterviewHidden { get; set; }
        public bool HasPointInterview { get; set; }
        public bool HasSetSb100Hidden { get; set; }
        public bool HasSetSb100 { get; set; }
        public bool HasLinkSb100Hidden { get; set; }
        public bool HasLinkSb100 { get; set; }
        public bool HasSetSb100HiddenTopica { get; set; }
        public bool HasSetSb100Topica { get; set; }
        public bool HasLinkSb100HiddenTopica { get; set; }
        public bool HasLinkSb100Topica { get; set; }
        public bool HasAccPayment { get; set; }
        public int HandoverAdvisor { get; set; }
        public int FeePackageType { get; set; }
        public int FeePackageTypeL7L8 { get; set; } //tuy chon, thoa thich. Hoc vien luc dat sb100 co the chon thoa thich, nhung khi nop tien co the chon tuy chon ...
        public int FeeTuitionType { get; set; }
        public int FeeMoneyType { get; set; }
        public double FeeEdu { get; set; }
        public double FeeEduYet { get; set; }
        public string FeeEduNotes { get; set; }
        public double PackagePriceSale { get; set; }
        public string TransactionReason { get; set; }
        public string DacDiemHocVien { get; set; }
        public int PricePackageListedId { get; set; }
        public string PackageWantToLearn { get; set; }      
        public int FeeEduDealsReason { get; set; }
        public int LearnNumberDay { get; set; } //So ngay hoc thoa thich
        public int NumberDealsGroup { get; set; }
        public string NoteDealsReason { get; set; }
        public bool HasTestTechnical { get; set; }
        public string Payments { get; set; }
        public int LevelStudyAdvisor { get; set; }
        public DateTime DateWantToLearn { get; set; } 
    }
}


using System;

namespace TamoCRM.Domain.Contacts
{
    public class ContactInfo
    {
        public const string CONTACT_CHANGE = "CONTACT_CHANGE";
        public int Id { get; set; }
        public string Code { get; set; }
        public string Fullname { get; set; }
        public string Name { get; set; }  //ten nguon trong bang Source Type
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Notes { get; set; }
        public string ProductSellName { get; set; }
        public string StatusCareConsultant { get; set; }
        public string Acceptance { get; set; } //nghiem thu 
        public DateTime? RegisteredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ImportedDate { get; set; }

        public DateTime? HandoverCollaboratorDate { get; set; }
        public DateTime? RecoveryCollaboratorDate { get; set; }
        private DateTime? _appointmentCollaboratorDate;
        public DateTime? AppointmentCollaboratorDate
        {
            get { return _appointmentCollaboratorDate; }
            set
            {
                if (value == _appointmentCollaboratorDate) return;

                _appointmentCollaboratorDate = value;
                AppointmentCollaboratorTime = _appointmentCollaboratorDate;
                if (_appointmentCollaboratorDate.HasValue)
                    AppointmentCollaboratorAmPm = _appointmentCollaboratorDate.Value.Hour == 0
                        ? string.Empty
                        : _appointmentCollaboratorDate.Value.Hour >= 12 ? "Chiều" : "Sáng";
                else
                    AppointmentCollaboratorAmPm = string.Empty;
            }
        }
        public string AppointmentCollaboratorAmPm { get; set; }
        public DateTime? AppointmentCollaboratorTime { get; set; }
        public string AppointmentCollaboratorTimeString
        {
            get
            {
                if (!_appointmentCollaboratorDate.HasValue) return string.Empty;
                return _appointmentCollaboratorDate.Value.Hour == 0
                    ? string.Empty
                    : _appointmentCollaboratorDate.Value.ToString("HH:mm");
            }
        }
        public DateTime? CallCollaboratorDate { get; set; }

        public DateTime? HandoverConsultantDate { get; set; }
        public DateTime? RecoveryConsultantDate { get; set; }
        private DateTime? _appointmentConsultantDate;
        public DateTime? AppointmentConsultantDate
        {
            get { return _appointmentConsultantDate; }
            set
            {
                if (value == _appointmentConsultantDate) return;

                _appointmentConsultantDate = value;
                AppointmentConsultantTime = _appointmentConsultantDate;
                if (_appointmentConsultantDate.HasValue)
                    AppointmentConsultantAmPm = _appointmentConsultantDate.Value.Hour == 0
                        ? string.Empty
                        : _appointmentConsultantDate.Value.Hour >= 12 ? "Chiều" : "Sáng";
                else
                    AppointmentConsultantAmPm = string.Empty;
            }
        }
        public string AppointmentConsultantAmPm { get; set; }
        public DateTime? AppointmentConsultantTime { get; set; }
        public string AppointmentConsultantTimeString
        {
            get
            {
                if (!_appointmentConsultantDate.HasValue) return string.Empty;
                return _appointmentConsultantDate.Value.Hour == 0
                    ? string.Empty
                    : _appointmentConsultantDate.Value.ToString("HH:mm");
            }
        }
        
        public DateTime? CallConsultantDate { get; set; } //ngay goi gan nhat

        public DateTime? CallHistoryDate { get; set; } // ngay co phat sinh cuoc goi
        public int CollectorId { get; set; }
        public int TypeId { get; set; }
        public int ChannelId { get; set; }
        public string Channel { get; set; }
        public int CampaindTpeId { get; set; }
        public string CampaindTpe { get; set; }
        public int LandingPageId { get; set; }
        public string LandingPage { get; set; }
        public int TemplateAdsId { get; set; }
        public string TemplateAds { get; set; }
        public int SearchKeywordId { get; set; }
        public string SearchKeyword { get; set; }
        public int PackageId { get; set; }
        public string Package { get; set; }
        public string Level { get; set; }
        public int LevelCC3 { get; set; }
        public int LevelId { get; set; }
        public int BranchId { get; set; }
        public int StatusId { get; set; }
        public int ImportId { get; set; }
        public int ContainerId { get; set; }
        public int ProductSellId { get; set; }
        public int ProductSoldId { get; set; }
        public int ProductSaleId { get; set; }
        public int QualityId { get; set; }
        public int StatusMapCollaboratorId { get; set; }
        public int StatusCareCollaboratorId { get; set; }
        public string StatusCareCollaborator { get; set; }

        public int StatusMapConsultantId { get; set; }
        public int StatusCareConsultantId { get; set; }
        public string StatusMap { get; set; }
        public string StatusCare { get; set; }
        public int UserImportId { get; set; }
        public int UserErrorId { get; set; }
        public int UserHandoverCollaboratorId { get; set; }
        public int UserCollaboratorId { get; set; }
        public string UserCollaborator { get; set; }
        public int UserRecoveryCollaboratorId { get; set; }
        public int UserHandoverConsultantId { get; set; }
        public int UserConsultantId { get; set; }
        public string Consultant { get; set; }
        public string UserName { get; set; }   //lay ten TVTS trong bao cao nghiem thu L8- Thanghq
        public int GroupId { get; set; } //Thanghq
        public int UserRecoveryConsultantId { get; set; }
        public int DraftCollaborator { get; set; }
        public int DraftConsultant { get; set; }
        public string CallInfoCollaborator { get; set; }
        public string CallInfoConsultant { get; set; }
        public int HandoverHistoryCollaboratorId { get; set; }
        public int HandoverHistoryConsultantId { get; set; }

        public string PhoneNumber { get; set; } 
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public int CreatedBy { get; set; }
        public int CallCount { get; set; }
        public int CallAmount { get; set; }
        public string DuplicateInfo { get; set; }
        public string SourceType { get; set; }
        public int L1 { get; set; }
        public int L2 { get; set; }
        public int L3 { get; set; }
        public int L4 { get; set; }
        public int L5 { get; set; }
        public int L6 { get; set; }
        public int L7 { get; set; }
        public int L8 { get; set; }
        public int Sum { get; set; }
        public decimal L8Sum { get; set; }
        public int SumL38 { get; set; }
        public decimal L38Sum { get; set; }      

        //Them vao de hien danh sach contact can ban giao cho advisor
        public double ActuallyPaid { get; set; }
        public double PackagePriceSale { get; set; }
        public string IdTVTS { get; set; }
        public string NameTvts { get; set; }
        public string TransactionReason { get; set; } // noi dung chuyen khoan
        public string LinkSB100 { get; set; }
        public int iHandoverStatusAdvisor { get; set; }
        public string sHandoverStatusAdvisor { get; set; }
        public string NoteAdvisor { get; set; }
        public double FeeEdu { get; set; }
        public int PricePackageListedId { get; set; } //giá niêm yết
        public int LearnNumberDay { get; set; }
        public DateTime? HandoverAdvisorSuccessTime { get; set; }
        public int NationId { get; set; }
        public string Job { get; set; }
        public int AppointmentTypeId { get; set; }
        public string AppointmentType { get; set; }
        public string AccountTopica { get; set; }
        public string PassTopica { get; set; }

        //thanghq 18/03/2016
        public string AgentCode { get; set; }
        public int Duration { get; set; }       
        public int RingTime { get; set; }
        public int SumCallInfo { get; set; }

        //13/11/2017
        public string SourceName { get; set; } //Tên nguồn CC MO ...
    }
}


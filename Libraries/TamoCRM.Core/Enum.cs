using System.ComponentModel;

namespace TamoCRM.Core
{
    public enum CheckPoint
    {
        [Description("CheckPoint Error")]
        error = 0,
        [Description("CheckPoint log time")]
        time = 1
    }
    public enum StatusType
    {
        [Description("Mới (TVTS)")]
        New = 1,
        [Description("Mới (Lọc)")]
        NewCollaborator = 7,
        [Description("Bàn giao Đội lọc")]
        HandoverCollaborator = 2,
        [Description("Đội lọc nghiệm thu")]
        AcceptCollaborator = 3,
        [Description("Thu hồi từ Đội lọc")]
        RecoveryCollaborator = 4,
        [Description("Bàn giao TVTS")]
        HandoverConsultant = 5,
        [Description("Thu hồi từ TVTS")]
        RecoveryConsultant = 6,

        //11/03/2016 Thanghq 
        [Description("Kho MOL")] 
        ContainerMOL = 8,
        [Description("Kho Contact trùng")]
        DuplicateContact = 9,

        //Thêm 1 kho contact L8 để chứa các contact L8 đã được thu hồi lại, tách riêng với các kho khác
        [Description("Kho Contact L8")]
        L8Container = 10     
    }

    public enum TodayType
    {
        All = 1,
        New = 2,
        Appointment = 3,
        NewAndAppointment = 4,
        AllByLevels = 5,
        AllByLevelsCollaborator = 6
    }

    public enum DiviceType
    {
        [Description("Phân đều theo từng kênh")]
        SameChannel = 4,
    }

    public enum SourceType
    {
        CC = 3,
        MO = 4,
        MF = 10,
    }

    public enum ChuDuToan //bo di sau
    {
        [Description("Topica Native")]
        TOM = 1,
        [Description("TAW_Native")]
        TAW = 2,
        [Description("TopicaMemo")]
        TCS = 3,
        [Description("TL100")]
        TL100 = 4,
        [Description("TAW_MC")]
        MOCAP = 5
    }

    public enum GoiHoc   //Bo di sau
    {
        [Description("Thỏa thích 30")]
        TT30 = 1,
        [Description("Thỏa thích 50")]
        TT50 = 2,
        [Description("Thỏa thích 70")]
        TT70 = 3,
        [Description("Thỏa thích 90")]
        TT90 = 4,
        [Description("Thỏa thích 135")]
        TT135 = 5
    }

    /// <summary>
    /// Mức Level của contact
    /// </summary>
    public enum LevelType
    {
        L0 = 0,
        L1 = 1,
        L2 = 2,
        L3 = 3,
        L4 = 4,
        L5 = 5,
        L6 = 6,
        L7 = 7,
        L8 = 8,
        L9 = 9,

        L5A = 50,//thuoc level 5
        L5B = 51,//
        L3M = 101,
    }

    public enum DiaPhuong
    {
        [Description("Việt Nam")]
        VN = 1,
        [Description("Thái Lan")]
        TL = 2,
    }
    public enum AppointmentType //KieuThanhToan trong Table LogsMoney
    {
        [Description("Tiền mặt Hà Nội")]   
        CASH_HN = 1,
        [Description("Tiền mặt Hồ Chí Minh")]
        CASH_HCM = 2,
        [Description("Chuyển khoản")]   //Chuyển khoản
        BANKING = 3,
        [Description("POS")]    //POS
        POS = 4,
        [Description("COD")]    //COD
        COD = 5,
    }

    public enum EstimateOwner   //Chủ dự toán
    {
        [Description("Topica Native")]
        TOM = 1,
        [Description("Topica Memo")]
        TCS = 2,
        [Description("Topica Uni")]
        TAW_Native = 3,
        [Description("TL100")]
        TL100 = 4,
        [Description("Mocap")]
        MOCAP = 5
    }

    public enum StatusCareType
    {
        UnKnown = 1,
        ContinueCare = 2,
        ContinueCareTime = 3,
        DontCall = 4,
    }

    public enum LogFunctionType
    {
        // Nghiệp vụ chính
        NotSql = 0,  // Nhung hanh dong khong thong qua chuc nang ma thao tac truc tiep tren database
        ImportExcel = 1,
        CreateContactTvts = 2,
        CreateContactHotline = 3,
        ChangeContainer = 4,
        Webserice = 5,

        HandoverToCollaborator = 11,
        CareContactCollaborator = 12,
        RecoveryCollaborator = 13,
        ForwardCollaborator = 14,

        HandoverToConsultant = 21,
        CareContactConsultant = 22,
        RecoveryConsultant = 23,
        ForwardConsultant = 24,
    }

    public enum LogPropertyType
    {
        // Contacts
        Contacts_Type = 2,
        Contacts_Level = 4,
        Contacts_Channel = 5,
        Contacts_Branch = 6,
        Contacts_Status = 7,
        Contacts_ProductSell = 8,
        Contacts_ProductSold = 9,
        Contacts_Container = 10,

        Contacts_User_Collaborator = 11,
        Contacts_HandoverDate_Collaborator = 12,
        Contacts_AppointmentDate_Collaborator = 13,
        Contacts_StatusMap_Collaborator = 14,
        Contacts_StatusCare_Collaborator = 15,
        Contacts_CallDate_Collaborator = 16,
        Contacts_RecoveryDate_Collaborator = 17,

        Contacts_User_Consulant_Foward = 18, //log luu chuyen tu TVTS nao (trong chuc nang chuyen contact giua 2 TVTS)
        Contacts_User_Consultant = 21,
        Contacts_HandoverDate_Consultant = 22,
        Contacts_AppointmentDate_Consultant = 23,
        Contacts_StatusMap_Consultant = 24,
        Contacts_StatusCare_Consultant = 25,
        Contacts_CallDate_Consultant = 26,
        Contacts_RecoveryDate_Consultant = 27,

        // User
        User_Norms = 101,
        User_IsActive = 100,
    }

    public enum LogObjectType
    {
        User = 1,
        Contact = 2,
    }

    public enum ReportType
    {
        Pdf = 1,
        Excel = 2,
    }
    public enum StatusContact { 
    
        [Description("Tổng Số Contact")]
        Total=1,
        [Description("Contact Mới")]
        New=2,
        [Description("Contact Cũ")]// đã bị thu hồi RecoveryConsultantDate !=null (bảng Contacts)
        ReTrieve=3,
        [Description("Contact đã qua đội lọc")]// đã bị thu hồi RecoveryConsultantDate !=null (bảng Contacts)
        PassFilter = 4,
    }

    public enum DayReportType
    {
        [Description("Theo Ngày")]
        Day = 1,
        [Description("Theo Tuần")]
        Week = 2,
        [Description("Theo Tháng")]
        Month = 3,
        [Description("Theo Năm")]
        Year = 4,
        [Description("Theo Chốt")]
        Latch = 5,
        [Description("Theo Gộp")]
        Lumped = 6,
    }


    public enum StatusConnectType
    {
        [Description("Chưa xác định")]
        Unknown = 0,
        [Description("Kết nối được")]
        Connect = 1,
        [Description("Không kết nối được")]
        DontConnect = 2,
    }

    public enum ErrorServiceType
    {
        Success = 0,
        NullOrEmpty = 1,
        NotFormat = 2,
        Exception = 3,
    }

    public enum CallType
    {
        Outcoming = 0,
        MissCall = 1,
        Incoming = 2,
        NoCall = 4,
        SendCampain = 5,
        UpdateCampain = 6,
        GetCallInfo = 7,
        IncomingUpdate = 8,

        UpdateCloseCampain = 101,
        UpdateConnectStatus = 102,
        UpdateAppointment = 103,

        UpdateCasecMark = 104,
        
        UpdateInterviewMark = 105,
        UpdateLinkSB100 = 106,
        UpdateCallInfoCM = 107,
        SendCasecAccount = 108,
        SendInterviewInfo = 109,
        UpdateChangeInterview = 110,
        SendRequestSB100 = 111,    
        UpdateStatusInterview = 112,
        SendChangeInterviewInfo = 113,
        SendCancelInterview = 114,
        SendRequestSB100Topica = 115,
        UpdateLinkSB100Topica = 116,
        UpdateTopicaMark = 117,

        ExceptionLogGetLinkRecord = 118,  //save log get link ghi am service
        ExceptionGetNamePackageLearn = 119, //log exception lay goi hoc

        CheckGetCall = 202
    }

    public enum StatusCollaboratorAppointment
    {
        [Description("Chưa gọi")]
        YetCall = 1,
        [Description("Đã gọi lại")]
        Called = 2,
        [Description("Không gọi")]
        NoCall = 3,
    }

    public enum WebServiceConfigType
    {
        [Description("LinkRecordTCL")]
        LinkRecordTCL = 1,
        [Description("LinkRecordCRM")]
        LinkRecordCRM = 2,
        [Description("LinkEditContact")]
        LinkEditContact = 3,
        [Description("LinkCallCenter")]
        LinkCallCenter = 4,
        [Description("CallCenterSoapBinding")]
        CallCenterSoapBinding = 5,
        [Description("TPCAutoDialSoapBinding")]
        TPCAutoDialSoapBinding = 6,
        [Description("LinkInterviewSchedule")]
        LinkInterviewSchedule = 7,
        [Description("LinkSpecializeInterview")]
        LinkSpecializeInterview = 8,
    }

    public enum StatusInterviewType
    {
        [Description("Đã đặt hàng")]
        DaDatHang = 13,
        [Description("Đã tiếp nhận")]
        DaTiepNhan = 14,
        [Description("Đã hoàn thành")]
        DaHoanThanh = 15,
        [Description("HV đã đổi lịch")]
        HocVienDoiLichPhongVan = 38,
        [Description("HV hủy lịch")]
        HocVienHuyPhongVan = 39,
        [Description("Không liên lạc được")]
        KhongLienLacDuoc = 72,
        [Description("Máy bận")]
        MayBan = 73,
        [Description("Không nhấc máy")]
        KhongNhacMay = 74,
        [Description("Chưa có lịch hẹn")]
        ChuaCoLichHen = 70,
    }

    public enum LevelCasecType
    {
        [Description("Basic 100")]
        Basic100 = 1,
        [Description("Basic 200")]
        Basic200 = 3,
        [Description("Basic 300")]
        Basic300 = 4,
        [Description("Intermediate 100")]
        Intermediate100 = 32,
        [Description("Intermediate 200")]
        Intermediate200 = 33,
        [Description("Intermediate 300")]
        Intermediate300 = 34,
        [Description("Advanced 100")]
        Advanced100 = 35,
        [Description("Advanced 200")]
        Advanced200 = 36,
        [Description("Advanced 300")]
        Advanced300 = 37,
    }

    public enum LevelTopicaType
    {
        [Description("Basic 100")]
        Basic100 = 1,
        [Description("Basic 200")]
        Basic200 = 3,
        [Description("Basic 300")]
        Basic300 = 4,
        [Description("Intermediate 100")]
        Intermediate100 = 32,
        [Description("Intermediate 200")]
        Intermediate200 = 33,
        [Description("Intermediate 300")]
        Intermediate300 = 34,
        [Description("Advanced 100")]
        Advanced100 = 35,
        [Description("Advanced 200")]
        Advanced200 = 36,
        [Description("Advanced 300")]
        Advanced300 = 37,
    }

    public enum LevelSpeakingType
    {
        [Description("Basic 100")]
        Basic100 = 78,
        [Description("Basic 200")]
        Basic200 = 79,
        [Description("Basic 300")]
        Basic300 = 80,
        [Description("Intermediate 100")]
        Intermediate100 = 81,
        [Description("Intermediate 200")]
        Intermediate200 = 82,
        [Description("Intermediate 300")]
        Intermediate300 = 83,
        [Description("Advanced 100")]
        Advanced100 = 84,
        [Description("Advanced 200")]
        Advanced200 = 85,
        [Description("Advanced 300")]
        Advanced300 = 86,
    }

    public enum FeeTuitionType
    {
        [Description("Đóng từng phần")]
        Basic = 1,
        [Description("Đóng hết")]
        Free = 2,
    }

    public enum MoneyUnit
    {
        [Description("VND")]
        vnd = 1,
        [Description("Baht")]
        bath = 2,
    }

    public enum RatePointType
    {
        [Description("1")]
        One = 1,
        [Description("2")]
        Two = 2,
        [Description("3")]
        Three = 3,
        [Description("4")]
        Four = 4,
        [Description("5")]
        Five = 5,
    }

    public enum EmployeeType
    {
        [Description("Tất cả")]
        All = 0,
        [Description("Nhân viên thu thập")]
        Collector = 1,
        [Description("Nhân viên lọc")]
        Collaborator = 2,
        [Description("Tư vấn tuyển sinh")]
        Consultant = 3,
        [Description("Chuyên môn")]
        ChuyenMon = 4,
        [Description("Quản lý Contact")]
        ManagerContact = 5,
    }

    public enum GroupConsultantType
    {
        [Description("Nhóm Nhân viên")]
        Consultant = 1,
        [Description("Nhóm Leader")]
        LeaderConsultant = 2,
        [Description("Nhóm Manager")]
        ManagerConsultant = 3,
        [Description("Nhóm Quản lý contact")]
        ManagerContacts = 4,
    }

    public enum GroupCollaboratorType
    {
        [Description("Nhóm Nhân viên")]
        Collaborator = 1,
        [Description("Nhóm Leader")]
        LeaderCollaborator = 2,
        [Description("Nhóm Manager")]
        ManagerCollaborator = 3,
        [Description("Nhóm Quản lý contact")]
        ManagerContacts = 4,
    }

    public enum EmailTemplateType
    {
        [Description("Mail chảo hỏi")]
        Greeting = 1,
        [Description("Mail giáo thiệu 2A")]
        Introducation2A = 2,
        [Description("Mail giáo thiệu 2B")]
        Introducation2B = 3,
        [Description("Mail hướng dẫn Test")]
        TutorialTest = 4,
        [Description("Mail hướng dẫn học phi")]
        TutorialFee = 5,
        [Description("Mail trả điểm và lộ trình học tập")]
        ReturnSB100 = 6,
        [Description("Mail hướng dẫn làm test tiếng anh topica")]
        TutorialTestTopica = 7,

        //br16 
        [Description("Br16 Mail chào hỏi")]
        Br16 = 8,
        [Description("Br16 Mail hướng dẫn làm test hệ thống tiếng anh Topica")]
        br162 = 9,
        [Description("Br16 Mail trả lộ trình hoc tập")]
        br163 = 10,
        [Description("Br16 Mail hướng dẫn học phí")]
        br164 = 11,
    }

    public enum StatusCasecType
    {
        [Description("Mới")]
        New = 1,
        [Description("Đang dùng")]
        Used = 2,
        [Description("Đã sử dụng")]
        Remove = 3,
    }

    public enum StatusTopicaType
    {
        [Description("Đang dùng")]
        Used = 1,
        [Description("Đã sử dụng")]
        Remove = 2,
    }

    public enum GenderType
    {
        [Description("Nam")]
        Male = 1,
        [Description("Nữ")]
        Female = 2,
    }
    public enum StatusUserType
    {
        [Description("Bình thường")]
        Nomal = 0,
        [Description("Đã khóa")]
        Locked = 1,
    }

    public enum StatusHandoverAdvisor
    {
        [Description("Chưa bàn giao")]
        NotHandover = 1,
        [Description("Đã bàn giao, chờ xử lý")]
        Handover = 2,
        [Description("Bàn giao nhưng không nhận")]
        HandoverFailed = 3,
        [Description("Bàn giao thành công")]
        HandoverSuccess = 4,
    }

    public enum Bank
    {
        [Description("Vietcombank")]
        Vietcombank = 1,
        [Description("Techcombank")]
        Techcombank = 2,
        [Description("VietinBank")]
        VietinBank = 3,
        [Description("VPBank")]
        VPBank = 4,
        [Description("BIDV")]
        BIDV = 5,
        [Description("AGRIBANK")]
        AGRIBANK = 6,
        [Description("ACB")]
        ACB = 7,
    }

    public enum LyDoBanGoiThap
    {
        [Description("Ưu đãi theo tháng")]
        KhuyenMai = 1,
        [Description("Ưu đãi học bổng của Memo")]
        HocBongMemo = 2,
        [Description("Ưu đãi học viên cử nhân")]
        CuNhan = 3,
        [Description("Ưu đãi nhóm")]
        Nhom = 4,
        [Description("Ưu đãi khác")]
        Khac = 5    
    }

    public enum GoiThoaThich
    {
        [Description("30 ngày")]
        Goi30 = 30,
        [Description("50 ngày")]
        Goi50 = 50,
        [Description("70 ngày")]
        Goi70 = 70,
        [Description("90 ngày")]
        Goi90 = 90,
        [Description("135 ngày")]
        Goi135 = 135,
        [Description("180 ngày")]
        Goi180 = 180,
        [Description("270 ngày")]
        Goi270 = 270,
        [Description("360 ngày")]
        Goi360 = 360,
        [Description("540 ngày")]
        Goi540 = 640,
        [Description("720 ngày")]
        Goi720 = 720
    }

    public enum UuDaiNhom
    {
        [Description("Số học viên từ 2 trở lên và nhỏ hơn 5")]
        UuDai1 = 1,
        [Description("Số học viên từ 5 trở lên và nhỏ hơn 10")]
        UuDai2 = 2,
        [Description("Số học viên từ 10 trở lên")]
        UuDai3 = 3
    }

    public enum LevelStudyAdvisor  //he thong advisor
    {
        [Description("Basic 100")]
        basic100 = 1,
        [Description("Basic 200")]
        basic200 = 2,
        [Description("Basic 300")]
        basic300 = 3,
        [Description("Inter 100")]
        inter100 = 4,
        [Description("Inter 200")]
        inter200 = 5,
        [Description("Inter 300")]
        inter300 = 6,
        [Description("Advanced 100")]
        advan100 = 7,
        [Description("Advanced 200")]
        advan200 = 8,
        [Description("Advanced 300")]
        advan300 = 9,
        [Description("sBasic")]
        sbasic = 10
    }

    public enum StatusContainerTypeMOL
    {
        [Description("Nhập kho MOL")]
        ImportContainer = 1,
        [Description("Xuất kho MOL")]
        ExportContainer = -1
    }

    public enum StatusAddContact
    {
        [Description("Import contact")]
        ImportContact = 1,
        [Description("TVTS thêm lẻ contact")]
        AddContactTVTS = 2,
        [Description("Thêm contact hotline")]
        AddContactHotLine = 3
        
    }
}
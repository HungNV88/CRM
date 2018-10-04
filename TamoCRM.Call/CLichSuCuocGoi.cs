using System;
namespace TamoCRM.Call
{
    /// <summary>
    /// Summary description for DALichSuCuocGoi
    /// </summary>
    /// <summary>
    /// Summary description for DALichSuCuocGoi
    /// </summary>
    public class CLichSuCuocGoi
    {
        public CallInfor data { get; set; }
        public CasecCallInfor dataCasec { get; set; }
    }

    public class CallInfor
    {
        public string message_code { get; set; }
        public string agent_code { get; set; }
        public string station_id { get; set; }
        public string mobile_phone { get; set; }
        public string datetime_response { get; set; }
        public string call_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string ringtime { get; set; }
        public string link_down_record { get; set; }
        public string status { get; set; }
        public string error_code { get; set; }
        public string error_desc { get; set; }
        public string log_callcenter { get; set; } //them để lưu log gọi callcenter 22/07/2016
    }
    public class CasecCallInfor
    {        
        public string agent_code { get; set; }
        public string station_id { get; set; }
        public string mobile_phone { get; set; }
        public string datetime_response { get; set; }
        public string call_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string ringtime { get; set; }
        public string link_down_record { get; set; }
        public string status { get; set; }        
    }
    public class CasecAccount 
    {
        public int contactId { get; set; }
        public string casecAccount { get; set; }
        public string passWord { get; set; }
        public string tenHV { get; set; }
    }
    public class InterviewInfo
    {
        public string AccountTVTS { get; set; }
        public int hocVienID { get; set; }
        public string hoTenHocVien { get; set; }
        public string dienThoaiHV { get; set; }
        public DateTime ngayHenPV { get; set; }
        public int kieuGV { get; set; }
        public int levelTester { get; set; }
        public string ghiChuLichHen { get; set; }
        public int khungGioID { get; set; }
    }

    public class ChangeInterviewInfo
    {
        public string AccountTVTS { get; set; }
        public int hocVienID { get; set; }
        public string hoTenHocVien { get; set; }
        public string dienThoaiHV { get; set; }
        public DateTime ngayHenPV { get; set; }
        public int kieuGV { get; set; }
        public int levelTester { get; set; }
        public string ghiChuLichHen { get; set; }
        public int khungGioID { get; set; }
    }

    public class CancelInterview
    {
        public int hocVienID { get; set; }
    }

    public class SB100
    {
        public int hocVienId;
        public int kieuHocPhiId;
        public string TVTS;
        public string hoTenHocVien;
        public string dienThoai;

    }

    public class SB100Topica
    {
        public int hocVienId;
        public int kieuHocPhiId;
        public DateTime? ngayTest;
        public string TVTS;
        public string tenHocVien;
        public string mobile;
        public double S1;
        public double S2;
        public double S3;
        public double S4;
    }

    public class Result
    {
        public int Code { get; set; }// 1: co loi; 0: thanh cong
        public object Tag { get; set; }
        public string Description { get; set; }
    }

}
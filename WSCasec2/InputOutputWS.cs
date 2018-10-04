using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCasec
{
    public class InputOutputWS
    {

    }
    public class UpdateCasecMark {         
        public int contactId { get; set; }
        public string casecAccount { get; set; }
        public int tuVung { get; set; }
        public int nguPhap { get; set; }
        public int ngheHieu { get; set; }
        public int chinhTa { get; set; }
        public int toeic { get; set; }
        public int levelCasec { get; set; }
        public int tongDiem { get; set; }
        public DateTime ngayThi { get; set; }
    }

    public class UpdateTopicaMark
    {
        public string userName { get; set; }
        public int tuVung { get; set; }
        public int dienDat { get; set; }
        public int ngheHieu { get; set; }
        public int chinhTa { get; set; }
        public int level { get; set; }
    }


    public class UpdateInterviewMark {        
        public int contactId { get; set; }
        public int smooth { get; set; }
        public int vocabulary { get; set; }
        public int grammar { get; set; }
        public int rythm { get; set; }
        public float speaking { get; set; }
        public int levelSpeaking { get; set; }
        public string Notes { get; set; } // Moi them
        public string agent_user { get; set; }
    }

    public class ChangeInterview
    {
        public int StatusInterviewId { get; set; }
        public int ContactId { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public int TimeSlotId { get; set; } 
        public int TeacherTypeId { get; set; } 
        public string Notes { get; set; } // Doi lich pv cu 20-11-2014, 8h30-9h30: notes of Chuyen Mon

        //update
        public string UserId { get; set; }
        public string NotesCM { get; set; }
        public string LevelTesterId { get; set; }
    }
    public class UpdateLinkSB100
    {
        public int contactId { get; set; }
        public string SB100 { get; set; }
    }

    public class UpdateLinkSB100Topica
    {
        public int contactId { get; set; }
        public string SB100Topica { get; set; }
    }
    public class UpdateCasecCallInfo {
        public int CallHistoryId { get; set; }
        public int ContactId { get; set; }
        public string AgentCode { get; set; }
        public string StationId { get; set; }
        public string MobilePhone { get; set; }
        public DateTime CallTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RingTime { get; set; }
        public string LinkRecord { get; set; }
        public string CallCenterInfo { get; set; }
        public string Duration { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public int StatusUpDate { get; set; } // 
        public int CallType { get; set; } // CallId
    }  
    public class UpdateStatusInterview {
        public int ContactId { get; set; }
        //public int UserId { get; set; }
        //public DateTime AppointmentDate { get; set; }
        //public int TimeSlotId { get; set; }
        //public string Notes { get; set; }
        public int StatusInterviewId { get; set; }
        //public int TeacherTypeId { get; set; }
        //public int CasecAccountId { get; set; }
        public string Notes { get; set; }
    }

    public class UserInfomation
    {
        public int UserId;
        public string UserName;
        public string FullName;
        public string Mobile;
        public string Email;
    }

    public class ContactDulicateAutosale
    {
        public string FullName;
        public string Email;
        public string Mobile;
        public int Level;
        public string StatusCare;
        public string StatusMap;
        public string Consultant;
        public DateTime? CallConsulantDate;
        public int StatusCareId;
    }
}
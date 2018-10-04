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
        public int tuVung { get; set; }
        public int nguPhap { get; set; }
        public int ngheHieu { get; set; }
        public int chinhTa { get; set; }
        public int toeic { get; set; }
        public int levelCasec { get; set; }
    }    
    //{"contactId":"123","tuVung":"80","nguPhap":"80","ngheHieu":"80","chinhTa":"80","toeic":"80"}

    public class UpdateInterviewMark{        
        public int contactId { get; set; }
        public int smooth { get; set; }
        public int vocabulary { get; set; }
        public int grammar { get; set; }
        public int rythm { get; set; }
        public int speaking { get; set; }
    }
    //{"contactId":"123","smooth":"80","vocabulary":"80","grammer":"80","rythm":"80","speaking":"80"}
    public class UpdateLinkSB100
    {
        public int contactId { get; set; }
        public string SB100 { get; set; }
    }
    //{"contactId":"123","SB100":"google.com.vn"}
    public class UpdateCasecCallInfo {
        //{"CallHistoryId":"321","contactId":"123","AgentCode":"12","StationId":"1006","MobilePhone":"1683710568","ResponseTime":"2014-11-16",
        //  "StartTime":"2014-11-16 16:45:36","EndTime":"2014-11-16 16:48:36","RingTime":"2014-11-16 16:50:36","LinkRecord":"google.com"
        //,"CallCenterInfo":"ngon, hoc thoi", "Duration":"133","ErrorCode":"12","ErrorDesc":"14","StatusUpdate":"1","CallType":"1"}
        public int CallHistoryId { get; set; }
        public int ContactId { get; set; }
        public string AgentCode { get; set; }
        public string StationId { get; set; }
        public string MobilePhone { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RingTime { get; set; }
        public string LinkRecord { get; set; }
        public string CallCenterInfo { get; set; }
        public string Duration { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public int StatusUpDate { get; set; }
        public int CallType { get; set; }
    }    
    
}
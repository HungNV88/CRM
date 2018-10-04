using System;
using System.Xml.Serialization;

namespace TamoCRM.Domain.CallHistories
{
    [Serializable, XmlRoot("CallInfor")]
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
    }

    [Serializable, XmlRoot("CallInfor")]
    public class CallInforUpdate
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
        public int campaign_id { get; set; }
    }
}

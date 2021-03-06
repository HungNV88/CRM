using System;

namespace TamoCRM.Domain.CallHistories
{
    public class CallHistoryInfo
    {
        public int CallHistoryId { get; set; }

        public int LevelId { get; set; }

        public int ContactId { get; set; }

        public int StatusCareId { get; set; }

        public int StatusMapId { get; set; }

        private DateTime? _callTime;
        public DateTime? CallTime
        {
            get { return _callTime; }
            set
            {
                if (value == _callTime) return;

                _callTime = value;
                CallTimeTime = _callTime;
                if (_callTime.HasValue)
                    CallTimeAmPm = _callTime.Value.Hour >= 12 ? "Chiều" : "Sáng";
                else
                    CallTimeAmPm = string.Empty;
            }
        }
        public DateTime? CallTimeTime { get; set; }
        public string CallTimeAmPm { get; set; }

        public int CallTimeLength { get; set; }

        public string CallCenterInfo { get; set; }

        private DateTime? _recallTime;
        public DateTime? RecallTime
        {
            get { return _recallTime; }
            set
            {
                if (value == _recallTime) return;

                _recallTime = value;
                RecallTimeTime = _recallTime;
                if (_recallTime.HasValue)
                    RecallTimeAmPm = _recallTime.Value.Hour >= 12 ? "Chiều" : "Sáng";
                else
                    RecallTimeAmPm = string.Empty;
            }
        }
        public DateTime? RecallTimeTime { get; set; }
        public string RecallTimeAmPm { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LinkRecord { get; set; }

        public string MessageCode { get; set; }

        public string AgentCode { get; set; }

        public string StationId { get; set; }

        public string MobilePhone { get; set; }

        public DateTime? ResponseTime { get; set; }

        public string CallId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Duration { get; set; }

        public string RingTime { get; set; }

        public string Status { get; set; }

        public string ErrorCode { get; set; }

        public string UserName { get; set; }

        public string ErrorDesc { get; set; }

        public int StatusUpdate { get; set; }

        public int CallType { get; set; }

        public string ContactName { get; set; }

        public int CallAmount { get; set; }

        /// <summary>
        /// Use enum StatusMapUsedBy
        /// </summary>
        public int UserLogType { get; set; }

        public bool Deleted { get; set; }

        public int HandoverHistoryId { get; set; }

        public int QualityId { get; set; }
        public string LogCallCenter { get; set; } //Them ngay 22/07/2016: luu lai log khi goi api CallCenter

        public double CreatedDate_TimeStamp { get; set; }
        public double StartTime_TimeStamp { get; set; }
        public double EndTime_TimeStamp { get; set; }
    }
}


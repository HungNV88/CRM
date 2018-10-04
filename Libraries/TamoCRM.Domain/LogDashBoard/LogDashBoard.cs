using System;

namespace TamoCRM.Domain.LogDashBoard
{
    public class LogDashBoard
    {
        public int STT { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public string Runtime { get; set; }
        public int Session { get; set; }
        public string CallId { get; set; }
        public int CallHistoryId { get; set; }
    }
}
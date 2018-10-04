using System;

namespace TamoCRM.Domain.Logs
{
    public class TmpLogServiceInfo
    {
        public int Status { get; set; }
        public int CallType { get; set; }
        public DateTime? Time { get; set; }
        public string Description { get; set; }
    }

    //public class LogDashBoard
    //{
    //    public int STT { get; set; }
    //    public DateTime? Date { get; set; }
    //    public string Time { get; set; }
    //    public int ContactId { get; set; }
    //    public string Name { get; set; }
    //    public int CreatedBy { get; set; }
    //    public string Runtime { get; set; }
    //    public int Session { get; set; }
    //    public string CallId { get; set; }
    //    public int CallHistoryId { get; set; }

    //}
}


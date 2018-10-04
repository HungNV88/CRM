using System;

namespace TamoCRM.Domain.Activity
{
    public class ActivityLogInfo
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int FunctionType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ActivityLogInfo()
        {
            CreatedDate = DateTime.Now;
        }
    }
}

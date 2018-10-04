using System;

namespace TamoCRM.Domain.Activity
{
    public class ActivityObjectChangeInfo : BaseClassInfo
    {
        public int ActivityId { get; set; }
        public int ObjectId { get; set; }
        public int ObjectType { get; set; }
        public int PropertyType { get; set; }
        public int? PropertyValueInt { get; set; }
        public string PropertyValueString { get; set; }
        public DateTime? PropertyValueDateTime { get; set; }

        public ActivityObjectChangeInfo()
        {
            PropertyValueInt = null;
            ChangedDate = DateTime.Now;
            PropertyValueString = null;
            PropertyValueDateTime = null;
        }
    }
}

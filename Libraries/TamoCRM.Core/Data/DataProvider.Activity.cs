using System;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        // ActivityLogs
        public abstract int ActivityLogs_Insert(int functionId, int functionType, int createdBy, DateTime createdDate);

        // ActivityObjectChanges
        public abstract int ActivityObjectChanges_Insert(int activityId, int objectType, int objectId, int propertyType, int? propertyValueInt, string propertyValueString, DateTime? propertyValueDateTime, DateTime changedDate); 
    }
}

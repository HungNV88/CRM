using System;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int ActivityLogs_Insert(int functionId, int functionType, int createdBy, DateTime createdDate)
        {
            if (functionId == 0) return 0;
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("ActivityLogs_Insert"), functionId, functionType, createdBy, createdDate);
        }

        public override int ActivityObjectChanges_Insert(int activityId, int objectType, int objectId, int propertyType, int? propertyValueInt, string propertyValueString, DateTime? propertyValueDateTime, DateTime changedDate)
        {
            if (activityId == 0 || objectId == 0) return 0;
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("ActivityObjectChanges_Insert"), activityId, objectType, objectId, propertyType, propertyValueInt, propertyValueString, propertyValueDateTime, changedDate);
        }
    }
}


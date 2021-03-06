using TamoCRM.Core.Data;
using TamoCRM.Domain.Activity;

namespace TamoCRM.Services.Activity
{
    public class ActivityObjectChangeRepository
    {
        public static int Create(ActivityObjectChangeInfo info)
        {
            return DataProvider.Instance().ActivityObjectChanges_Insert(info.ActivityId, info.ObjectType, info.ObjectId, info.PropertyType, info.PropertyValueInt, info.PropertyValueString, info.PropertyValueDateTime, info.ChangedDate);
        }
    }        
}

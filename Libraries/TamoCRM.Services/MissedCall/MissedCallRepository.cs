using System.Collections.Generic;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.MissedCall;

namespace TamoCRM.Services.MissedCall
{
    public class MissedCallRepository
    {
        public static int Create(MissedCallInfo info)
        {
            return DataProvider.Instance().MissedCall_Insert(info.UserId, info.ContactId, info.PhoneNumber, info.OldCallId, info.AgentCode, info.MissedCallTime, info.CreatedTime, info.Status);
        }
        public static void UpdateStatus(int contactId, int status)
        {
            DataProvider.Instance().MissedCall_UpdateStatus(contactId, status);
        }
        public static List<MissedCallInfo> GetAll(int userId, int status)
        {
            return ObjectHelper.FillCollection<MissedCallInfo>(DataProvider.Instance().MissedCall_GetAll(userId, status));
        }
    }        
}

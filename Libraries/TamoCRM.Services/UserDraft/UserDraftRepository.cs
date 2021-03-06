using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Data;
using TamoCRM.Domain.UserDraft;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.UserDraft
{
    public class UserDraftRepository
    {
        public static int Create(UserDraftInfo info)
        {
            return DataProvider.Instance().UserDraft_Insert( info.UserId, info.BranchId, info.IsDraftCollabortor, info.IsDraftConsultant, info.CreatedDate);
        }
        public static void Update(UserDraftInfo info)
        {
            DataProvider.Instance().UserDraft_Update( info.Id, info.UserId, info.BranchId, info.IsDraftCollabortor, info.IsDraftConsultant,info.CreatedDate);
        }
        public static void Delete(int id)
        {
            DataProvider.Instance().UserDraft_Delete(id);
        }
        public static UserDraftInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<UserDraftInfo>(DataProvider.Instance().UserDraft_GetInfo(id));
        }
        public static List<UserDraftInfo> GetAll()
        {
            return ObjectHelper.FillCollection<UserDraftInfo>(DataProvider.Instance().UserDraft_GetAll());
        }
        public static List<UserDraftInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillUserDraftCollection(DataProvider.Instance().UserDraft_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<UserDraftInfo> FillUserDraftCollection(IDataReader reader, out int totalRecords)
        {
            List<UserDraftInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<UserDraftInfo>(reader, false);

                reader.NextResult();
                if (reader.Read()) totalRecords = reader[0].ToInt32();
            }
            finally
            {
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }

    }        
}

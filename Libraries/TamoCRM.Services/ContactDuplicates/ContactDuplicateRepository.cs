using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactDuplicates;

namespace TamoCRM.Services.ContactDuplicates
{
    public class ContactDuplicateRepository
    {
        public static int Update(ContactDuplicateInfo info)
        {
            return DataProvider.Instance().ContactDuplicate_Update(info.ContactId, info.SourceTypeId, info.Status, info.ImportId, info.DuplicateInfo, info.CreatedDate, info.IsNotyfiDuplicate);
        }

        public static int CheckExitsNotify(int ContactId)
        {
            return DataProvider.Instance().ContactDuplicate_CheckExitsNotify(ContactId);
        }

        public static int UpdateIsNotify(int ContactId)
        {
            return DataProvider.Instance().ContactDuplicate_UpdateIsNotify(ContactId);
        }

        public static int Create(ContactDuplicateInfo info)
        {
            return DataProvider.Instance().ContactDuplicate_Insert(info.ContactId, info.SourceTypeId, info.Status, info.ImportId, info.DuplicateInfo, info.CreatedDate, info.IsNotyfiDuplicate);
        }
    }        
}

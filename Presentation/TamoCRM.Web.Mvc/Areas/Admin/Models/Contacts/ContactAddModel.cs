using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactAddModel
    {
        public string Birthday { get; set; }
        public int SourceTypeMapId { get; set; }
        public int? ChannelId { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ContactLevelInfo ContactLevelInfo { get; set; }

        public ContactAddModel()
        {
            ContactInfo = new ContactInfo
                          {
                              Fullname = string.Empty,
                              Mobile1 = string.Empty,
                              Mobile2 = string.Empty,
                              Email = string.Empty,
                              Email2 = string.Empty,
                              Birthday = null,
                              Address = string.Empty,
                              Notes = string.Empty,
                              ProductSellId = 0,
                          };
        }
    }
}
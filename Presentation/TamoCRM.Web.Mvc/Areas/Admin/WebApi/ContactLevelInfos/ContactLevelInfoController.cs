using System.Collections.Generic;
using System.Web.Http;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.ContactLevelInfos
{
    public class ContactLevelInfoController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<ContactLevelInfo> Get()
        {
            return ContactLevelInfoRepository.GetAll();
        }

        // GET api/<controller>
        public ContactLevelInfoListModel Get(int collectorId, int sourceTypeId, int levelId, int channelId, int statusId, int statusMapId, int branchId, int page, int rows)
        {
            int totalRecords;
            var model = new ContactLevelInfoListModel
                            {
                                Rows = ContactLevelInfoRepository.Search(collectorId, sourceTypeId, levelId, channelId, statusId, statusMapId, branchId, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords/rows) + 1,
                                Records = rows
                            };
            return model;
        }

        
    }
}


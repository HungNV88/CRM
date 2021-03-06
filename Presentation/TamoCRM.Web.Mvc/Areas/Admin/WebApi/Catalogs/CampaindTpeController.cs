using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Services.Catalogs;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Catalogs;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Catalogs
{
    public class CampaindTpeController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<CampaindTpeInfo> Get()
        {
            return CatalogRepository.GetAll<CampaindTpeInfo>();
        }

        // GET api/<controller>
        public CatalogListModel<CampaindTpeInfo> Get(int page, int rows)
        {
            int totalRecords;
            var model = new CatalogListModel<CampaindTpeInfo>
                        {
                            Page = page,
                            Records = rows,
                            Rows = CatalogRepository.Search<CampaindTpeInfo>(string.Empty, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                        };
            return model;
        }

        // GET api/<controller>/5
        public CampaindTpeInfo Get(int id)
        {
            return CatalogRepository.GetInfo<CampaindTpeInfo>(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = form.Get("Id").Split(',')[0].ToInt32();
            if (string.IsNullOrEmpty(operation)) return retVal;

            CampaindTpeInfo info;
            switch (operation)
            {
                case "edit":
                    info = CatalogRepository.GetInfo<CampaindTpeInfo>(id);
                    if (info != null)
                    {
                        info.Name = form.Get("Name");
                        CatalogRepository.Update(info);
                    }
                    break;
                case "add":
                    info = new CampaindTpeInfo { Name = form.Get("Name") };
                    CatalogRepository.Create(info);
                    break;
                case "del":
                    CatalogRepository.Delete<CampaindTpeInfo>(id);
                    break;
            }
            StoreData.ReloadData<CampaindTpeInfo>();
            return retVal;
        }
    }
}


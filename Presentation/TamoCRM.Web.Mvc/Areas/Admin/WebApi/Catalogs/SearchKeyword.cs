using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Services.Catalogs;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Catalogs;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Catalogs
{
    public class SearchKeywordController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<SearchKeywordInfo> Get()
        {
            return CatalogRepository.GetAll<SearchKeywordInfo>();
        }

        // GET api/<controller>
        public CatalogListModel<SearchKeywordInfo> Get(int page, int rows)
        {
            int totalRecords;
            var model = new CatalogListModel<SearchKeywordInfo>
                        {
                            Page = page,
                            Records = rows,
                            Rows = CatalogRepository.Search<SearchKeywordInfo>(string.Empty, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                        };
            return model;
        }

        // GET api/<controller>/5
        public SearchKeywordInfo Get(int id)
        {
            return CatalogRepository.GetInfo<SearchKeywordInfo>(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = form.Get("Id").Split(',')[0].ToInt32();
            if (string.IsNullOrEmpty(operation)) return retVal;

            SearchKeywordInfo info;
            switch (operation)
            {
                case "edit":
                    info = CatalogRepository.GetInfo<SearchKeywordInfo>(id);
                    if (info != null)
                    {
                        info.Name = form.Get("Name");
                        CatalogRepository.Update(info);
                    }
                    break;
                case "add":
                    info = new SearchKeywordInfo { Name = form.Get("Name") };
                    CatalogRepository.Create(info);
                    break;
                case "del":
                    CatalogRepository.Delete<SearchKeywordInfo>(id);
                    break;
            }
            StoreData.ReloadData<SearchKeywordInfo>();
            return retVal;
        }
    }
}


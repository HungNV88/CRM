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
    public class ContainerController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<ContainerInfo> Get()
        {
            return CatalogRepository.GetAll<ContainerInfo>();
        }

        // GET api/<controller>
        public CatalogListModel<ContainerInfo> Get(int page, int rows)
        {
            int totalRecords;
            var model = new CatalogListModel<ContainerInfo>
                        {
                            Page = page,
                            Records = rows,
                            Rows = CatalogRepository.Search<ContainerInfo>(string.Empty, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                        };
            return model;
        }

        // GET api/<controller>/5
        public ContainerInfo Get(int id)
        {
            return CatalogRepository.GetInfo<ContainerInfo>(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = form.Get("Id").Split(',')[0].ToInt32();
            if (string.IsNullOrEmpty(operation)) return retVal;

            ContainerInfo info;
            switch (operation)
            {
                case "edit":
                    info = CatalogRepository.GetInfo<ContainerInfo>(id);
                    if (info != null)
                    {
                        info.Name = form.Get("Name");
                        CatalogRepository.Update(info);
                    }
                    break;
                case "add":
                    info = new ContainerInfo { Name = form.Get("Name") };
                    CatalogRepository.Create(info);
                    break;
                case "del":
                    CatalogRepository.Delete<ContainerInfo>(id);
                    break;
            }
            StoreData.ReloadData<ContainerInfo>();
            return retVal;
        }
    }
}


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
    public class PackageController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<PackageInfo> Get()
        {
            return CatalogRepository.GetAll<PackageInfo>();
        }

        // GET api/<controller>
        public CatalogListModel<PackageInfo> Get(int page, int rows)
        {
            int totalRecords;
            var model = new CatalogListModel<PackageInfo>
                        {
                            Page = page,
                            Records = rows,
                            Rows = CatalogRepository.Search<PackageInfo>(string.Empty, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                        };
            return model;
        }

        // GET api/<controller>/5
        public PackageInfo Get(int id)
        {
            return CatalogRepository.GetInfo<PackageInfo>(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = form.Get("Id").Split(',')[0].ToInt32();
            if (string.IsNullOrEmpty(operation)) return retVal;

            PackageInfo info;
            switch (operation)
            {
                case "edit":
                    info = CatalogRepository.GetInfo<PackageInfo>(id);
                    if (info != null)
                    {
                        info.Name = form.Get("Name");
                        CatalogRepository.Update(info);
                    }
                    break;
                case "add":
                    info = new PackageInfo { Name = form.Get("Name") };
                    CatalogRepository.Create(info);
                    break;
                case "del":
                    CatalogRepository.Delete<PackageInfo>(id);
                    break;
            }
            StoreData.ReloadData<PackageInfo>();
            return retVal;
        }
    }
}


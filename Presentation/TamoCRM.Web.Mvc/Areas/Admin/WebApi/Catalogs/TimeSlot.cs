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
    public class TimeSlotController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<TimeSlotInfo> Get()
        {
            return CatalogRepository.GetAll<TimeSlotInfo>();
        }

        // GET api/<controller>
        public CatalogListModel<TimeSlotInfo> Get(int page, int rows)
        {
            int totalRecords;
            var model = new CatalogListModel<TimeSlotInfo>
                        {
                            Page = page,
                            Records = rows,
                            Rows = CatalogRepository.Search<TimeSlotInfo>(string.Empty, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                        };
            return model;
        }

        // GET api/<controller>/5
        public TimeSlotInfo Get(int id)
        {
            return CatalogRepository.GetInfo<TimeSlotInfo>(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = form.Get("Id").Split(',')[0].ToInt32();
            if (string.IsNullOrEmpty(operation)) return retVal;

            TimeSlotInfo info;
            switch (operation)
            {
                case "edit":
                    info = CatalogRepository.GetInfo<TimeSlotInfo>(id);
                    if (info != null)
                    {
                        info.Name = form.Get("Name");
                        info.OrderTime = form.Get("OrderTime").ToInt32();
                        CatalogRepository.Update(info);
                    }
                    break;
                case "add":
                    info = new TimeSlotInfo
                           {
                               Name = form.Get("Name"), 
                               OrderTime = form.Get("OrderTime").ToInt32()
                           };
                    CatalogRepository.Create(info);
                    break;
                case "del":
                    CatalogRepository.Delete<TimeSlotInfo>(id);
                    break;
            }
            StoreData.ReloadData<TimeSlotInfo>();
            return retVal;
        }
    }
}


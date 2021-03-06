using System.Collections.Generic;
using System.Web.Http;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Services.Catalogs;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.StatusCare
{
    public class StatusCareController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<StatusCareInfo> Get()
        {
            return CatalogRepository.GetAll<StatusCareInfo>();
        }
    }
}


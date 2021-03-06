using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Services.StatusMap;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.StatusMap
{
    public class StatusMapController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<StatusMapInfo> Get()
        {
            return StatusMapRepository.GetAll();
        }

        [HttpGet]
        public IEnumerable<StatusMapInfo> GetByStatusCareId(int statusCareId, int employeeTypeId)
        {
            var list = StatusMapRepository.GetAll() ?? new List<StatusMapInfo>();

            list = list
                .Where(c => c.StatusCareId == statusCareId)
                .Where(c => c.StatusMapType == employeeTypeId)
                .ToList();
            list.Insert(0, new StatusMapInfo
                               {
                                   Id = 0,
                                   Name = "Tất cả",
                               });
            return list;
        }

        [HttpGet]
        public IEnumerable<StatusMapInfo> GetByStatusCareAndLevel(int statusCareId, string levelIds, int employeeTypeId)
        {
            var list = StatusMapRepository.GetAll() ?? new List<StatusMapInfo>();
            levelIds = levelIds.IsStringNullOrEmpty() ? "0" : levelIds;
            var arrLevelId = levelIds.Split(',').Select(c => c.ToInt32()).ToList();
            arrLevelId.RemoveAll(c => c.Equals(0));
            
            list = list
                .Where(c => c.StatusCareId == statusCareId)
                .Where(c => c.StatusMapType == employeeTypeId)
                .ToList();
            if (!arrLevelId.IsNullOrEmpty())
                list = list.Where(c => arrLevelId.Any(p => p == c.LevelId)).ToList();
            list.Insert(0, new StatusMapInfo
            {
                Id = 0,
                Name = "Tất cả",
            });
            return list;
        }
    }
}


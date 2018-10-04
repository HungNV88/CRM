using System.Collections.Generic;
using System.Web.Http;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Functions
{
    public class FunctionController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<FunctionInfo> Get(int roleId, int branchId)
        {
            return FunctionRepository.GetByRoleAndBranch(roleId, branchId);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
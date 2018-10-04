using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamoCRM.Domain.Recharge;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.GetPackage
{
    public class GetPackageController : CustomApiController
    {
        [HttpGet]
        public string GetCodePackageAdvisor(string package_product, string package_type, string package_cost, string package_unit)
        {
            string username = "admin";
            string password = "admin";

            var client = new RestClient("http://market.local.topmito.edu.vn/api/");
            client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request_create = new RestRequest("packageAPI/crmpackage", Method.GET);
            request_create.AddHeader("Accept", "application/json");

            request_create.AddParameter("key", "vcIXD3cK8LchElyTWPq96fIOOwkfnK");
            request_create.AddParameter("package_product", package_product);
            request_create.AddParameter("package_type", package_type); 
            request_create.AddParameter("package_cost", package_cost);
            request_create.AddParameter("package_unit", package_unit);

            request_create.JsonSerializer.ContentType = "application/json; charset=utf-8";

            IRestResponse response_create = client.Execute(request_create);
            var content_create = response_create.Content;
            var input = JsonConvert.DeserializeObject<RechargeInfo>(content_create);

            return input.data;
        }
    }
}

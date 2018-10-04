using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Domain.Recharge;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.AdvisorCreate
{
    public class AdvisorsCreateController : CustomApiController
    {
        [HttpPost]
        public void CreateContacts(FormDataCollection form)
        {
            string username = "admin";
            string password = "admin";

            var code = form.Get("code").ToString();
            var phone = form.Get("phone").ToString();
            var email = form.Get("email").ToString();
            var fullname = form.Get("fullname").ToString();
            var package_want_to_learn = form.Get("package_want_to_learn").ToString();
            var level_crm = form.Get("level_crm").ToString();
            var deposit_need = form.Get("deposit_need").ToString();
            var actually_paid = form.Get("actually_paid").ToString();
            var technical_test = form.Get("technical_test").ToString();
            var tvts_id = form.Get("tvts_id").ToString();
            var tvts_name = form.Get("tvts_name").ToString();
            var transaction_reason = form.Get("transaction_reason").ToString();
            var sb100 = form.Get("sb100").ToString();          
            

            var client = new RestClient("http://advisor.local.topicanative.edu.vn/");
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("advisor_api/create_new_student_info", Method.POST);

            request.AddHeader("Accept", "application/json");

            request.AddParameter("key", "SSeKfm7RXCJZxnFUleFsPf63o2ymZ93fWuCmvC34");

            request.AddParameter("contact_id", code);
            request.AddParameter("phone", phone);
            request.AddParameter("email", email);
            request.AddParameter("full_name", fullname);
            request.AddParameter("package_want_to_learn", package_want_to_learn);
            request.AddParameter("lang", "vi");
            request.AddParameter("note", "");
            request.AddParameter("level_crm", level_crm);
            request.AddParameter("deposit_need", deposit_need);
            request.AddParameter("actually_paid", actually_paid);
            request.AddParameter("technical_test", technical_test);
            request.AddParameter("tvts_id", tvts_id);
            request.AddParameter("tvts_name", tvts_name);
            request.AddParameter("actually_paid", actually_paid);
            request.AddParameter("transaction_reason", transaction_reason);
            request.AddParameter("sb100", sb100);


            request.JsonSerializer.ContentType = "application/json; charset=utf-8";
            IRestResponse response = client.Execute(request);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Domain.TopicaAccounts;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using System.IO;
using System.Runtime.Serialization.Json;
using TamoCRM.Services.TopicaAccounts;
using TamoCRM.Services.ContactLevelInfos;
using RestSharp;
using Newtonsoft.Json;
using TamoCRM.Web.Framework.Controllers;
using System.Configuration;
using RestSharp.Authenticators;
/// <summary>
/// Author: ThangHQ 
/// Created Date: 31/08/2016 - ThangHQ - Tao file
/// Updated Date: 31/10/2016 - ThangHQ - Comment Source Code
/// Note: Viet cac API lien quan den viec lay tai khoan tu he thong Trac Nghiem Topica
/// </summary>
namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Topica
{
    
    public class TopicaAccountController : CustomApiController
    {
        private const string URL = "http://tracnghiem.topicanative.edu.vn?";
        private const string token = "9552ae063e7aeedacce24353657f9c11";
        private const string function = "local_create_uservn";

        /// <summary>
        /// Get thong tin tai khoan tu he thong trac nghiem topica
        /// </summary>
        /// <param name="form">Chua thong tin: contactId</param>
        /// <returns>Tra ra danh sach thong tin tai khoan trac nghiem topica theo contactID</returns>
        [HttpPost]
        public List<TopicaAccountInfo> Provide(FormDataCollection form)
        {
           
            var contactId = form.Get("contactId").ToInt32();
            string linkTopicaSystem = ConfigurationManager.AppSettings["TopicaEnglishSystem"].ToString();
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            //string a = "http://tracnghiem.topicanative.edu.vn/webservice/rest/server.php?wstoken="+ token +"&wsfunction="+ function + "&moodlewsrestformat=json&contactid=" + contactId;
            // invoke the REST method
            byte[] data = client.DownloadData(
                   linkTopicaSystem + "?wstoken=" + token +"&wsfunction="+ function + "&moodlewsrestformat=json&contactid=" + contactId);
            
            // put the downloaded data in a memory stream
            MemoryStream ms = new MemoryStream();
            ms = new MemoryStream(data);
            // deserialize from json
            DataContractJsonSerializer ser =
                   new DataContractJsonSerializer(typeof(TopicaAccountInfo));
            TopicaAccountInfo result = ser.ReadObject(ms) as TopicaAccountInfo;
            
            var list = TopicaAccountRepository.UpdateTopicaAccount(contactId, result.username, result.password);
            ContactLevelInfoRepository.UpdateHasTopicaAccount(contactId, true);

            if (!list.IsNullOrEmpty())
            {
                foreach(var item in list)
                {
                    item.DateTimeString = item.DateTime.ToString("dd/MM/yyyy");
                    item.CreatedDateString = item.CreatedDate.ToString("dd/MM/yyyy");
                    item.StatusString = ObjectExtensions.GetEnumDescription((StatusTopicaType)item.StatusTopicaAccountId);
                }
                return list;
            }

            return null;
        }

        public List<TopicaAccountInfo> GetAllByContactId(int contactId)
        {
            var list = TopicaAccountRepository.GetAllByContactId(contactId);
            if (!list.IsNullOrEmpty())
            {
                foreach (var item in list)
                {
                    item.DateTimeString = item.DateTime.ToString("dd/MM/yyyy");
                    item.CreatedDateString = item.CreatedDate.ToString("dd/MM/yyyy");
                    item.StatusString = ObjectExtensions.GetEnumDescription((StatusTopicaType)item.StatusTopicaAccountId);
                }
                return list;
            }
            return null;
        }

        //Test thu xem co goi api ko ty xoa sau
        [HttpPost]
        public string Advisor(FormDataCollection form)
        {
            string username = "crm";
            string password = "crm.topica.";
            int ContactId = form.Get("ContactId").ToInt32();
            string UserName = form.Get("UserName").ToString();
            string UserPhone = form.Get("UserPhone").ToString();
            string UserEmail = form.Get("UserEmail").ToString();

            // Neu email trong thi thay bang so dien thoai
            if(UserEmail.IsStringNullOrEmpty())
            {
                UserEmail = form.Get("UserPhone").ToString();
            }

            var client = new RestClient("http://core.payment.local.topmito.edu.vn/");
            client.Authenticator = new HttpBasicAuthenticator(username, password);         

            var request_create = new RestRequest("User/Add", Method.POST);
            request_create.AddHeader("Accept", "application/json");

            request_create.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
            request_create.AddParameter("ContactId", ContactId);
            request_create.AddParameter("UserName", UserName); //lay email
            request_create.AddParameter("UserPhone", UserPhone);
            request_create.AddParameter("UserEmail", UserEmail); //neu ko có email thi thay phone

            request_create.JsonSerializer.ContentType = "application/json; charset=utf-8";
            //request.RequestFormat = DataFormat.Json;
            IRestResponse response_create = client.Execute(request_create);

            var content_create = response_create.Content;

            return content_create;
        }

        [HttpPost]
        public TopicaCasec CheckSumTopicaCasec(FormDataCollection form)
        {
            var checktime = form.Get("checktime");
            var check_Time = checktime.ToDateTime("ddMMyyyy");   
            var diem = TopicaAccountRepository.GetCountTopicaCasec(check_Time);                
            return diem;
        }


    }
}

using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
///summary
///created by thanhtp2 at 17/11/2016
///Tao file checkpoint để sử dụng để đo thời gian chạy các hàm trong hệ thống
///sumary
namespace TamoCRM.Core
{
    public class CheckPointApi
    {
        public static void createCheckPoint(string currentUser, string function, string data)
        {
            //lay ra username và pasword từ file web.config
            string userName = System.Configuration.ConfigurationManager.AppSettings["CheckPointUserName"];
            string passWord = System.Configuration.ConfigurationManager.AppSettings["CheckPointPassword"];
            string dateTime = TimeStampConvert.ConvertToUnixTime(DateTime.Now) + "";

            //khởi tạo restclient để tạo request
            var client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["CheckPointApi"]);
            client.Authenticator = new HttpBasicAuthenticator(userName, passWord);
            var request = new RestRequest("contact_collection_api/webservice", Method.POST);

            //add các parameter vào request
            request.AddHeader("Accept", "application/json");

            request.AddParameter("key", System.Configuration.ConfigurationManager.AppSettings["CheckPointKey"]);

            request.AddParameter("user_id", currentUser);
            request.AddParameter("time_in", dateTime);
            request.AddParameter("ip_address", "");
            request.AddParameter("function", function);
            request.AddParameter("data", data);
            request.AddParameter("system", System.Configuration.ConfigurationManager.AppSettings["CheckPointSystemName"]);

            request.JsonSerializer.ContentType = "application/json; charset=utf-8";
            IRestResponse responseStart = client.Execute(request);
        }

        public void CheckPointNew(string currentUser, string function, string data, long duration)
        {
            //lay ra username và pasword từ file web.config
            //string userName = System.Configuration.ConfigurationManager.AppSettings["CheckPointUserName"];
            //string passWord = System.Configuration.ConfigurationManager.AppSettings["CheckPointPassword"];
            //long dateTime = TimeStampConvert.ConvertToUnixTime(DateTime.Now);

            ////khởi tạo restclient để tạo request
            //var client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["CheckPointApi"]);
            //client.Authenticator = new HttpBasicAuthenticator(userName, passWord);
            //var request = new RestRequest("contact_collection_api/webservice", Method.POST);
            
            ////add các parameter vào request
            //request.AddHeader("Accept", "application/json");

            //request.AddParameter("key", System.Configuration.ConfigurationManager.AppSettings["CheckPointKey"]);
            //request.AddParameter("user_id", currentUser);
            //request.AddParameter("time_in", dateTime);
            //request.AddParameter("ip_address", "");
            //request.AddParameter("function", function);
            //request.AddParameter("data", data);
            //request.AddParameter("duration", duration.ToString());
            //request.AddParameter("system", System.Configuration.ConfigurationManager.AppSettings["CheckPointSystemName"]);

            //request.JsonSerializer.ContentType = "application/json; charset=utf-8";
            //IRestResponse responseStart = client.Execute(request);
        }

    }
}

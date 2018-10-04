using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Web.Framework
{
    public class LogTracking
    {
        public static void CreateCheckPoint(string systemName, string functional, string checkPointName, string userName, DateTime logTime, string session_name, int type)
        {
            //try
            //{
            //    string URL = ConfigurationManager.AppSettings["LogTrackingUrl"];
            //    string urlParameters = "?";
            //    urlParameters += "systemName=" + systemName
            //                   + "&functional=" + functional
            //                   + "&checkPointName=" + checkPointName
            //                   + "&userName=" + userName
            //                   + "&logTime=" + logTime
            //                   + "&information=" + session_name
            //                   + "&type=" + type;
            //    HttpClient httpClient = new HttpClient();
            //    httpClient.BaseAddress = new Uri(URL);
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = httpClient.GetAsync(urlParameters).Result;
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e);
            //}

        }
    }
}

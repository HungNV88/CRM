using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Domain.TopicaAccounts;
using TamoCRM.Domain.Recharge;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using System.IO;
using System.Runtime.Serialization.Json;
using TamoCRM.Services.TopicaAccounts;
using TamoCRM.Services.ContactLevelInfos;
using RestSharp;
using Newtonsoft.Json;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Services.MoneyLogs;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Recharge
{
    public class RechargeController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        //[HttpPost]
        //public string Advisor(FormDataCollection form)
        //{
        //    var user = UserContext.GetCurrentUser();
            
        //    string username = "crm";
        //    string password = "crm.topica.";

        //    int ContactId = Convert.ToInt32(form.Get("ContactId").ToString());
        //    string Code = form.Get("Code").ToString();
        //    string UserName = "";
        //    UserName = form.Get("UserName").ToString();
        //    string UserPhone = "";
        //    UserPhone = form.Get("UserPhone").ToString();
        //    string UserEmail = form.Get("UserEmail").ToString();
        //    string Value = form.Get("Value").ToString();
        //    string Reason = form.Get("Reason").ToString();
        //    DateTime? DateCollection = form.Get("DateCollection").ToDateTime();
        //    int TypeAppointment = form.Get("TypeAppointment").ToInt32();
        //    string sTypeAppointment = form.Get("TypeAppointment").ToString();
        //    int Dia_Phuong = form.Get("DiaPhuong").ToInt32();
        //    string sDiaPhuong = form.Get("DiaPhuong").ToString();
        //    int PricePackage = form.Get("PricePackage").ToInt32();
        //    string PackageWantToLearn = "day la goi hoc phi nhung chua co du lieu"; //goi API lay goi hoc ve tu advisor

        //    int TienNop = Value.ToInt32(); //TienNop

        //    if (UserEmail.IsStringNullOrEmpty())
        //    {
        //        UserEmail = form.Get("UserPhone").ToString(); //Neu Email trong thi thay bang so dien thoai
        //    }
         
        //    var content = "";

        //    //Lưu record log nạp tiền vào data CRM
        //    var logmoney = new LogsMoneyInfo
        //    {
        //        TienHVNop = Value.ToInt32(),
        //        TienBanGoi = PricePackage,
        //        NoteChuyenKhoan = Reason,
        //        NguoiTao = user.UserName,
        //        NgayThucThu = DateCollection,
        //        KieuThanhToan = TypeAppointment,
        //        DiaPhuong = Dia_Phuong,
        //        CreateDate = DateTime.Now,
        //        ContactId = ContactId,
        //        Code = Code,
        //        ChuDuToan = 1 //Ghi tam da
        //    };

        //    MoneyLogsRepository.Create(logmoney, null);

        //    //Nạp tiền lên payment
            

        //    int TienDaDong = TienNop + form.Get("FeeEdu").ToInt32();

        //    double min_money = (PricePackage / 100 * 70); // Neu 

        //    if (TienDaDong >= min_money)
        //    {
        //        BanGiaoHocVien(Code, UserPhone, UserEmail, UserName, "Thoa thich", sDiaPhuong, Value, "Truc tiep", Reason);
        //    }
        //    else
        //    {
        //        var client = new RestClient("http://core.payment.local.topmito.edu.vn/");
        //        client.Authenticator = new HttpBasicAuthenticator(username, password);

        //        var request_create = new RestRequest("User/Add", Method.POST);
        //        request_create.AddHeader("Accept", "application/json");

        //        request_create.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
        //        request_create.AddParameter("ContactId", ContactId);
        //        request_create.AddParameter("UserName", UserName); //lay email
        //        request_create.AddParameter("UserPhone", UserPhone);
        //        request_create.AddParameter("UserEmail", UserEmail); //neu ko có email thi thay phone

        //        request_create.JsonSerializer.ContentType = "application/json; charset=utf-8";

        //        IRestResponse response_create = client.Execute(request_create);

        //        var content_create = response_create.Content;
        //        var input = JsonConvert.DeserializeObject<RechargeInfo>(content_create);

        //        if (input.status == "True" || (input.status == "False" && input.msg == "CONTACTID_ALREADY_EXIST"))
        //        {
        //            var request = new RestRequest("Balance/Deposit", Method.POST);
        //            request.AddHeader("Accept", "application/json");

        //            request.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
        //            request.AddParameter("ContactId", ContactId);
        //            request.AddParameter("UserName", UserName); //lay email
        //            request.AddParameter("UserPhone", UserPhone);
        //            request.AddParameter("UserEmail", UserEmail); //neu ko có email thi thay phone
        //            request.AddParameter("Value", Value);
        //            request.AddParameter("Reason", Reason);
        //            request.AddParameter("TransactionBy", "CRM_BANKING");

        //            request.JsonSerializer.ContentType = "application/json; charset=utf-8";
        //            //request.RequestFormat = DataFormat.Json;
        //            IRestResponse response = client.Execute(request);
                    
        //            //content = response_create.Content;
        //        }
        //    }
        //    return "";
        //}

        //protected void BanGiaoHocVien(string code, string phone, string email, string fullname, string package_want_to_learn, string lang, string actually_paid, string pay_menthod, string note)
        //{
        //    string username = "admin";
        //    string password = "admin";
        //    var client = new RestClient("http://advisor.local.topicanative.edu.vn/");
        //    client.Authenticator = new HttpBasicAuthenticator(username, password);

        //    var request = new RestRequest("advisor_api/create_new_student", Method.POST);//create_new_student_info

        //    request.AddHeader("Accept", "application/json");

        //    request.AddParameter("key", "SSeKfm7RXCJZxnFUleFsPf63o2ymZ93fWuCmvC34");
        //    request.AddParameter("contact_id", code);
        //    request.AddParameter("phone", phone);
        //    request.AddParameter("email", email);
        //    request.AddParameter("full_name", fullname); //neu ko có email thi thay phone
        //    request.AddParameter("package_want_to_learn", package_want_to_learn);
        //    request.AddParameter("lang", lang); //noi dung chuyen khoan
        //    request.AddParameter("actually_paid", actually_paid);
        //    request.AddParameter("pay_menthod", pay_menthod); // ngay thuc thu do tvts nhap
        //    request.AddParameter("note", note); // bao gom TVTSID, ContactId, so tien, lang ...

        //    request.JsonSerializer.ContentType = "application/json; charset=utf-8";
        //    //request.RequestFormat = DataFormat.Json;
        //    IRestResponse response = client.Execute(request);

        //    var content = response.Content;
            
        //}
    }
}
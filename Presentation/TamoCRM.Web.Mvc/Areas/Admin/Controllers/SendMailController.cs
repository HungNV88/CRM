using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.TestResults;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Email;
using TamoCRM.Services.TestResults;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Email;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class SendMailController : AdminController
    {
        //
        // GET: /Admin/SendMail/ 
        public ActionResult Index()
        {
            var model = new ContactModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ContactModel model)
        {// Hiện tại không dùng
            if (ModelState.IsValid)
            {
                const string smtpUserName = "manhpq@topica.edu.vn";
                const string smtpPassword = "135606899";
                const string smptHost = "smtp.gmail.com";
                const int smtpPort = 25;

                const string emailTo = "manhpq18@gmail.com";
                var subject = model.Subject;
                var body = string.Format("Bạn vừa nhận được liên hệ từ: <b>{0}</b><br/>Email:{1}<br/>Nội dung:<br/>{2}",
                    model.UserName, model.Email, model.Message);

                var service = new EmailService();

                var kq = service.Send(smtpUserName, smtpPassword, smptHost, smtpPort, emailTo, subject, body);
                ModelState.AddModelError("", kq ? "Cảm ơn bạn đã liên hệ với Topmito" : "Gửi email thất bại, vui lòng gửi lại.");
            }
            return View(model);
        }

        public ActionResult EditMail()
        {
            return View();
        }
        public ActionResult ListMail()
        {
            return View();
        }
        public ActionResult Edit(int Id)
        {
            var info = EmailRepository.GetEmailInfo(Id);
            var model = new MailModel
            {
                TypeEmail = info.EmailType,
                Subject = info.Subject,
                Body = info.Content,
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase Picture1, HttpPostedFileBase Picture2, HttpPostedFileBase Picture3)
        {
            var info1 = Picture1;
            var info2 = Picture2;
            var info3 = Picture3;
            // Verify that the user selected a file
            
            if (Picture1 != null && Picture1.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(Picture1.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/FileAttach"), fileName);
                Picture1.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
               
            return View();
        }
        public ActionResult SendEmailWithAttachment(string mailTo, string mailTo2,int contactid, int type)
        {
            var email = string.Empty;
            if (!mailTo.IsStringNullOrEmpty()) email += mailTo;
            if (!mailTo2.IsStringNullOrEmpty()) email += ";" + mailTo2;
            var model = new MailModel
                        {
                            To = email, 
                            TypeEmail = type,  
                            ContactId = contactid
                        };
            var info = EmailRepository.GetEmailTemplate(type);
            model.Subject = info.Subject;
            model.Body = info.Content;
            var user = UserContext.GetCurrentUser();
           
            if (user != null && !user.SignEmailSend.IsStringNullOrEmpty())
                model.Body += "<br />" + user.SignEmailSend;
            if (type == 4) {
                string chuoi = "";
                chuoi = "<input type=\"checkbox\" name=\"anh_041\" value=\"\" checked=\"true\" disabled=\"disabled\">huongdantestphongvan04.png";
                chuoi += "</br>";
                chuoi += "<input type=\"checkbox\" name=\"docx_042\" value=\"\" checked=\"true\" disabled=\"disabled\">CASEC_Huongdandanhchothisinh04.docx";
                ViewBag.html4 = chuoi;   
            }
            else
            {
                ViewBag.html4 = "";
            }

            if (type == 7)
            {
                string chuoi = "";
                chuoi = "<input type=\"checkbox\" name=\"anh_071\" value=\"\" checked=\"true\" disabled=\"disabled\">huongdantestphongvan07.png";
                chuoi += "</br>";
                chuoi += "<input type=\"checkbox\" name=\"docx_072\" value=\"\" checked=\"true\" disabled=\"disabled\">Huongdansudungbaithilythuyet.doc";
                ViewBag.html7 = chuoi;   
            }
            else
            {
                ViewBag.html7 = "";
            }
            
            if (type == 6)
            {
                string sb;
                sb = "<input type=\"checkbox\" name=\"\" value=\"\" checked=\"true\" disabled=\"disabled\">File lộ trình học tập SB100";
                ViewBag.html6 = sb;
            }
            else
            {
                ViewBag.html6 = "";
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmailWithAttachment(MailModel objModelMail, HttpPostedFileBase fileUploader
            , HttpPostedFileBase fileUploader2, HttpPostedFileBase fileUploader3, HttpPostedFileBase fileUploader4, HttpPostedFileBase fileUploader5)
        {
            if (ModelState.IsValid)
            {
                    var user = UserContext.GetCurrentUser();
                    var emailSend = user.EmailSend;

                //var passwordSend = SecurityHelper.Decrypt(user.PasswordSend);
                var passwordSendtext = "198uR6IM2naAocEN07w/9g==";
            var   passwordSend = SecurityHelper.Decrypt(passwordSendtext);
                //var type = objModelMail.TypeEmail;
                //type = 4;             
                var emails = objModelMail.To.Split(';');

                    foreach (var emailTo in emails)
                    {
                        using (var mail = new MailMessage(emailSend, emailTo))
                        {
                            mail.Subject = objModelMail.Subject;
                            mail.Body = objModelMail.Body;
                            int type_email = objModelMail.TypeEmail;
                            int id = objModelMail.ContactId;
                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }
                        if (fileUploader2 != null)
                        {
                            string fileName = Path.GetFileName(fileUploader2.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader2.InputStream, fileName));
                        }
                        if (fileUploader3 != null)
                        {
                            string fileName = Path.GetFileName(fileUploader3.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader3.InputStream, fileName));
                        }
                        if (fileUploader4 != null)
                        {
                            string fileName = Path.GetFileName(fileUploader4.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader4.InputStream, fileName));
                        }
                        if (fileUploader5 != null)
                        {
                            string fileName = Path.GetFileName(fileUploader5.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader5.InputStream, fileName));
                        }

                        if (type_email == 4)
                        {
                            try
                            {
                                string path = Server.MapPath("~/FileAttach/huongdantestphongvan04.png");
                                string fileName = Path.GetFileName(path);
                                //string fileName = Path.GetFileName(@"C:\FileAttach\huongdantestphongvan04.png");
                                FileStream fileStream = new FileStream(path, FileMode.Open);
                                mail.Attachments.Add(new Attachment(fileStream, fileName));

                                string path2 = Server.MapPath("~/FileAttach/CASEC_Huongdandanhchothisinh4.docx");
                                string fileName2 = Path.GetFileName(path2);
                                //string fileName2 = Path.GetFileName(@"C:\FileAttach\CASEC_Huongdandanhchothisinh4.docx");
                                FileStream fileStream2 = new FileStream(path2, FileMode.Open);
                                mail.Attachments.Add(new Attachment(fileStream2, fileName2));
                            }
                            catch { }
                        }

                        if (type_email == 7)
                        {
                            try
                            {
                                string path = Server.MapPath("~/FileAttach/huongdantestphongvan07.png");
                                string fileName = Path.GetFileName(path);
                                FileStream fileStream = new FileStream(path, FileMode.Open);
                                mail.Attachments.Add(new Attachment(fileStream, fileName));

                                string path2 = Server.MapPath("~/FileAttach/Huongdansudungbaithilythuyet7.docx");
                                string fileName2 = Path.GetFileName(path2);
                                FileStream fileStream2 = new FileStream(path2, FileMode.Open);
                                mail.Attachments.Add(new Attachment(fileStream2, fileName2));
                            }
                            catch { }
                        }

                        if (type_email == 6)
                        {
                            TestResultLinkSb100Info contactLevelInfos = new TestResultLinkSb100Info();
                            contactLevelInfos = TestResultRepository.GetResultLinkSb100Curent(id);
                            string sb100_chuoi = "";
                            sb100_chuoi = contactLevelInfos.LinkSb100.ToString();
                            string fileName3 = Path.GetFileName(sb100_chuoi);
                            var request = (HttpWebRequest)WebRequest.Create(sb100_chuoi);
                            var response = request.GetResponse();
                            var responseStream = response.GetResponseStream();
                            mail.Attachments.Add(new Attachment(responseStream, fileName3));
                        }
                        mail.IsBodyHtml = true;
                            mail.BodyEncoding = Encoding.UTF8;
                            var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true };
                            //{ Host = "smtp.gmail.com", EnableSsl = true };
                            var networkCredential = new NetworkCredential(emailSend, passwordSend);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = networkCredential;
                            smtp.Timeout = 3600000;
                            smtp.Port = 587;
                            smtp.Send(mail);
                            ViewBag.Message = "Sent";
                        }
                    }
                    return View("SendEmailWithAttachment", objModelMail);
                }
            return View();
        }
    }
}




//const string SMTP_USERNAME = "AKIAJWJOJOSKUTYYYRRA";  // Replace with your SMTP username. 
//const string SMTP_PASSWORD = "ApoxxGZvthNbfIs40eCJvnB6ZJGbTSdHKWp9oR+opvov";
//const string HOST = "email-smtp.us-east-1.amazonaws.com";
//const int PORT = 587;

//var user = UserContext.GetCurrentUser();
//var emailSend = user.EmailSend;
//var passwordSend = SecurityHelper.Decrypt(user.PasswordSend);
////var type = objModelMail.TypeEmail;
////type = 4;             
//var emails = objModelMail.To.Split(';');

//foreach (var emailTo in emails)
//{
//    using (var message = new MailMessage(emailSend, emailTo))
//    {
//        var credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
//        var sender = new MailAddress(emailSend, "Người gửi");
//        var recipientTo = new MailAddress(emailTo, "Người nhận");
//        var subject = objModelMail.Subject;
//        string content = objModelMail.Body;
//        int type_email = objModelMail.TypeEmail;
//        int id = objModelMail.ContactId;

//        var htmlView = AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html);
//        var txtView = AlternateView.CreateAlternateViewFromString("This is a plain text message.", Encoding.UTF8, MediaTypeNames.Text.Plain);

//        //var message = new MailMessage();
//        message.Subject = subject;
//        message.From = sender;
//        message.To.Add(recipientTo);
//        message.AlternateViews.Add(txtView);
//        message.AlternateViews.Add(htmlView);
//        //message.CC.Add(emailSend);
//        message.Bcc.Add(emailSend);

//        if (fileUploader != null)
//        {
//            string fileName = Path.GetFileName(fileUploader.FileName);
//            message.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
//        }
//        if (fileUploader2 != null)
//        {
//            string fileName = Path.GetFileName(fileUploader2.FileName);
//            message.Attachments.Add(new Attachment(fileUploader2.InputStream, fileName));
//        }
//        if (fileUploader3 != null)
//        {
//            string fileName = Path.GetFileName(fileUploader3.FileName);
//            message.Attachments.Add(new Attachment(fileUploader3.InputStream, fileName));
//        }
//        if (fileUploader4 != null)
//        {
//            string fileName = Path.GetFileName(fileUploader4.FileName);
//            message.Attachments.Add(new Attachment(fileUploader4.InputStream, fileName));
//        }
//        if (fileUploader5 != null)
//        {
//            string fileName = Path.GetFileName(fileUploader5.FileName);
//            message.Attachments.Add(new Attachment(fileUploader5.InputStream, fileName));
//        }

//        if (type_email == 4)
//        {
//            try
//            {
//                string path = Server.MapPath("~/FileAttach/huongdantestphongvan04.png");
//                string fileName = Path.GetFileName(path);
//                //string fileName = Path.GetFileName(@"C:\FileAttach\huongdantestphongvan04.png");
//                FileStream fileStream = new FileStream(path, FileMode.Open);
//                message.Attachments.Add(new Attachment(fileStream, fileName));

//                string path2 = Server.MapPath("~/FileAttach/CASEC_Huongdandanhchothisinh4.docx");
//                string fileName2 = Path.GetFileName(path2);
//                //string fileName2 = Path.GetFileName(@"C:\FileAttach\CASEC_Huongdandanhchothisinh4.docx");
//                FileStream fileStream2 = new FileStream(path2, FileMode.Open);
//                message.Attachments.Add(new Attachment(fileStream2, fileName2));
//            }
//            catch { }
//        }

//        if (type_email == 7)
//        {
//            try
//            {
//                string path = Server.MapPath("~/FileAttach/huongdantestphongvan07.png");
//                string fileName = Path.GetFileName(path);
//                FileStream fileStream = new FileStream(path, FileMode.Open);
//                message.Attachments.Add(new Attachment(fileStream, fileName));

//                string path2 = Server.MapPath("~/FileAttach/Huongdansudungbaithilythuyet7.docx");
//                string fileName2 = Path.GetFileName(path2);
//                FileStream fileStream2 = new FileStream(path2, FileMode.Open);
//                message.Attachments.Add(new Attachment(fileStream2, fileName2));
//            }
//            catch { }
//        }

//        if (type_email == 6)
//        {
//            TestResultLinkSb100Info contactLevelInfos = new TestResultLinkSb100Info();
//            contactLevelInfos = TestResultRepository.GetResultLinkSb100Curent(id);
//            string sb100_chuoi = "";
//            sb100_chuoi = contactLevelInfos.LinkSb100.ToString();
//            string fileName3 = Path.GetFileName(sb100_chuoi);
//            var request = (HttpWebRequest)WebRequest.Create(sb100_chuoi);
//            var response = request.GetResponse();
//            var responseStream = response.GetResponseStream();
//            message.Attachments.Add(new Attachment(responseStream, fileName3));
//        }

//        using (var client = new SmtpClient(HOST, PORT))
//        {
//            client.Credentials = credentials;
//            client.EnableSsl = true;
//            client.Send(message);
//            ViewBag.Message = "Sent";
//        }
//    }
//    return View("SendEmailWithAttachment", objModelMail);
//}

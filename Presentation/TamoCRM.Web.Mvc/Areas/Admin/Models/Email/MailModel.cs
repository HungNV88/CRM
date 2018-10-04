using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.EmailInfo;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Email
{
    public class MailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public int TypeEmail { get; set; }
        public int Id { get; set; }
        //[AllowHtml]
        //public string Content { get; set; }
        //public int EmailType { get; set; }
        //public int Id { get; set; }
        public HttpPostedFileBase Picture1 { get; set; }
        public HttpPostedFileBase Picture2 { get; set; }
        public HttpPostedFileBase Picture3 { get; set; }
        public int ContactId { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<EmailInfo> Rows { get; set; }
        public int UserData { get; set; }
    }
}
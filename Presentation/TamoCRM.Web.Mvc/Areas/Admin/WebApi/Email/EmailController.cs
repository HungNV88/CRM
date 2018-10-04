using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactDeleted;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Phones;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.Activity;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Services.Email;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Email;
using TamoCRM.Domain.EmailInfo;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Email
{
    public class EmailController : CustomApiController
    {
        //public string CreateTemplate(string subject, int typeEmail, string content)
        //{
        //    var res = string.Empty;
        //    EmailRepository.Create(subject,typeEmail,content);
        //    return res;
        //}

        public string CreateTemplate(FormDataCollection form)
        {
            var res = string.Empty;            
            var subject = form.Get("Subject");
            int typeEmail = Int32.Parse(form.Get("TypeEmail"));
            var content = form.Get("Content");
            
            var entity = new EmailInfo
            {
                Content = content,
                Subject = subject,
                EmailType = typeEmail,
            };

            EmailRepository.Create(entity);
            return res;
        }

        public string EditEmailTemplate(FormDataCollection form)
        {
            var rel = string.Empty;
            var id = Int32.Parse(form.Get("Id"));
            var subject = form.Get("Subject");
            var typeEmail = Int32.Parse(form.Get("TypeEmail"));
            var content = form.Get("Content");

            var entity = new EmailInfo
            {
                Id = id,
                Content = content,
                Subject = subject,
                EmailType = typeEmail,
            };
            EmailRepository.Edit(entity);
            
            return rel;
        }

        [HttpGet]
        public MailModel GetAll()
        {
            var model = new MailModel
            {
                Rows = EmailRepository.GetAll(),
                
            };
            return model;
        }

    }
}

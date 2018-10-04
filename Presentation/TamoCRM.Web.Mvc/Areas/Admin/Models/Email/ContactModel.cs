using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Email
{
    public class ContactModel
    {
        [Display(Name = "Tên của bạn")]
        [Required(ErrorMessage = "Tên không được trống")]
        public string UserName { get; set;}

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được trống")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email Không Đúng")]
        public string Email { get; set; }

        [Display(Name = "Chủ Đề")]
        [Required(ErrorMessage = "Chủ đề không được trống")]
        public string Subject { get; set; }

        [Display(Name = "Nội dung")]        
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }
}
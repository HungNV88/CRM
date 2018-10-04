using System.Web.Mvc;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Users
{
    public class EmailSettingModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailSend { get; set; }
        public string PasswordSend { get; set; }
        [AllowHtml]
        public string SignEmailSend { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
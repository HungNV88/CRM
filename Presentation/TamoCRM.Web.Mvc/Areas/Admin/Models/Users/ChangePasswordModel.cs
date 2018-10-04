using TamoCRM.Domain.UserRole;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Users
{
    public class ChangePasswordModel
    {
        public string Password { get; set; }
        public UserInfo UserInfo { get; set; }
        public string OldPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
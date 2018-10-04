using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TamoCRM.Core.Commons.Utilities
{
    public class CookieHelper
    {
        public static void SetCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            HttpContext.Current.Response.SetCookie(cookie);
        }
        public static string GetCookie(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null) return cookie.Value;
            return string.Empty;
        }
    }
}

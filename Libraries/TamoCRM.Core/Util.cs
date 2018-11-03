using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Extensions;

namespace TamoCRM.Core
{
    public class Util
    {
        public static bool IsDigitsOnly(string str)
        {
            foreach (var c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        public static bool ValidateEmail(string email)
        {
            return ValidateFormat(email, Constant.EmailFormat);
        }
        public static bool ValidateMobile(string mobile)
        {
            // Check đầu số di động mạng trong nước
            var prefix = mobile.Substring(0,2);
            var index = Array.IndexOf(Constant.PREFIX_PHONE, prefix);
            if(index > -1)
            {
                if (mobile.Length == 9 && IsDigitsOnly(mobile))
                {
                    return true;
                }
            }
            // Không phải đầu số di động mạng trong nước, có thể là thuê bao nước ngoài, cần check thêm.....
            else
            {
                if (IsDigitsOnly(mobile))
                {
                    return true;
                }
            }
            return false;
        }
        public static string CleanAlphabetAndFirstZero(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            input = input.Replace("O", "0");
            input = input.Replace("o", "0");
            var arr = input.Where(char.IsNumber).ToArray();
            var retVal = new string(arr);
            // Nếu số điện thoại bắt đầu bằng 0, remove 0 khỏi số điện thoại để check duplicate
            if (retVal.StartsWith("0"))
            {
                retVal = retVal.Substring(1);
            }
            // Nếu số điện thoại bắt đầu bằng 84, remove 84 khỏi số điện thoại để check duplicate
            if (retVal.StartsWith("84") && retVal.Length > 9)
            {
                retVal = retVal.Substring(2);
            }
            // Nếu số điện thoại bắt đầu bằng 0, remove 0 khỏi số điện thoại để check duplicate
            if (retVal.StartsWith("0"))
            {
                retVal = retVal.Substring(1);
            }
            // Lấy đầu số để check số cũ và convert sang đầu số mới trong trường hợp nhập phone với đầu số cũ của các nhà mạng
            var prefix = retVal.Substring(0, 3);
            switch (prefix)
            {
                // Check đầu số cũ mạng Viettel, sẽ chuyển đổi sang đầu số mới
                case Constant.VIETTEL_162:
                    retVal = String.Concat(Constant.VIETTEL_32, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_163:
                    retVal = String.Concat(Constant.VIETTEL_33, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_164:
                    retVal = String.Concat(Constant.VIETTEL_34, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_165:
                    retVal = String.Concat(Constant.VIETTEL_35, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_166:
                    retVal = String.Concat(Constant.VIETTEL_36, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_167:
                    retVal = String.Concat(Constant.VIETTEL_37, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_168:
                    retVal = String.Concat(Constant.VIETTEL_38, retVal.Substring(3));
                    break;
                case Constant.VIETTEL_169:
                    retVal = String.Concat(Constant.VIETTEL_39, retVal.Substring(3));
                    break;
                // Check đầu số cũ mạng Mobile, sẽ chuyển đổi sang đầu số mới
                case Constant.MOBILE_120:
                    retVal = String.Concat(Constant.MOBILE_70, retVal.Substring(3));
                    break;
                case Constant.MOBILE_121:
                    retVal = String.Concat(Constant.MOBILE_79, retVal.Substring(3));
                    break;
                case Constant.MOBILE_122:
                    retVal = String.Concat(Constant.MOBILE_77, retVal.Substring(3));
                    break;
                case Constant.MOBILE_126:
                    retVal = String.Concat(Constant.MOBILE_76, retVal.Substring(3));
                    break;
                case Constant.MOBILE_128:
                    retVal = String.Concat(Constant.MOBILE_78, retVal.Substring(3));
                    break;
                // Check đầu số cũ mạng Vina, sẽ chuyển đổi sang đầu số mới
                case Constant.VINA_123:
                    retVal = String.Concat(Constant.VINA_83, retVal.Substring(3));
                    break;
                case Constant.VINA_124:
                    retVal = String.Concat(Constant.VINA_84, retVal.Substring(3));
                    break;
                case Constant.VINA_125:
                    retVal = String.Concat(Constant.VINA_85, retVal.Substring(3));
                    break;
                case Constant.VINA_127:
                    retVal = String.Concat(Constant.VINA_81, retVal.Substring(3));
                    break;
                case Constant.VINA_129:
                    retVal = String.Concat(Constant.VINA_82, retVal.Substring(3));
                    break;
                // Check đầu số cũ mạng Vietnammobile, sẽ chuyển đổi sang đầu số mới
                case Constant.VIETNAM_186:
                    retVal = String.Concat(Constant.VIETNAM_56, retVal.Substring(3));
                    break;
                case Constant.VIETNAM_188:
                    retVal = String.Concat(Constant.VIETNAM_58, retVal.Substring(3));
                    break;
                // Check đầu số cũ mạng GTel, sẽ chuyển đổi sang đầu số mới
                case Constant.GTEL_199:
                    retVal = String.Concat(Constant.GTEL_59, retVal.Substring(3));
                    break;
            }
            return retVal;
        }
        public static bool ValidateFormat(string input, string format)
        {
            var r = new Regex(format, RegexOptions.Compiled);
            return r.IsMatch(input);
        }

        public static SelectList SelectedList<T>(bool existAll = false)
        {
            var types = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
                types.Add((int)item, ObjectExtensions.GetEnumDescription((T)item));
            if (existAll) types.Add(0, "Tất cả");
            var selectList = new SelectList(types.Select(c => new {Id = c.Key, Name = c.Value}).OrderBy(c => c.Id), "Id", "Name");
            return selectList;
        }

        public static SelectList SelectedListWithNone<T>(bool existAll = false)
        {
            var types = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
                types.Add((int)item, ObjectExtensions.GetEnumDescription((T)item));
            if (existAll) types.Add(0, "Không có");
            var selectList = new SelectList(types.Select(c => new { Id = c.Key, Name = c.Value }).OrderBy(c => c.Id), "Id", "Name");
            return selectList;
        }

        public static SelectList SelectedList<T>(bool existAll, string allValue)
        {
            var types = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
                types.Add((int)item, ObjectExtensions.GetEnumDescription((T)item));
            if (existAll) types.Add(0, allValue);
            var selectList = new SelectList(types.Select(c => new { Id = c.Key, Name = c.Value }).OrderBy(c => c.Id), "Id", "Name");
            return selectList;
        }

        public static SelectList SelectedListExclude<T>(bool existAll = false, params int[] arrayInts)
        {
            var types = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (arrayInts.Any(c => (int)item == c)) continue;
                types.Add((int)item, ObjectExtensions.GetEnumDescription((T)item));
            }
            if (existAll) types.Add(0, "Tất cả");
            return new SelectList(types.Select(c => new { Id = c.Key, Name = c.Value }).OrderBy(c => c.Id), "Id", "Name");
        }

        public static SelectList SelectedListInclude<T>(bool existAll = false, params int[] arrayInts)
        {
            var types = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (!arrayInts.Any(c => (int)item == c)) continue;
                types.Add((int)item, ObjectExtensions.GetEnumDescription((T)item));
            }
            if (existAll) types.Add(0, "Tất cả");
            return new SelectList(types.Select(c => new { Id = c.Key, Name = c.Value }).OrderBy(c => c.Id), "Id", "Name");
        }

        public static string BuildLinkEmail(string mailTo, string mailTo2, int contactid, EmailTemplateType type)
        {
            var email = string.Empty;
            if (!mailTo.IsStringNullOrEmpty()) email += mailTo;
            if (!mailTo2.IsStringNullOrEmpty()) email += ";" + mailTo2;
            return "/admin/sendmail/SendEmailWithAttachment/?mailTo=" + email + "&contactid=" + contactid + "&type=" + (int)type;
        }
    }
}

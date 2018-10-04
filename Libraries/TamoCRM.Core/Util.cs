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
            if (mobile.Length > 8 && mobile.Length < 11)
            {
                if (IsDigitsOnly(mobile)) return true;
            }
            return false;
        }
        public static string CleanAlphabetAndFirstZero(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            input = input.Replace("O", "0");
            input = input.Replace("o", "0");
            var arr = input.Where(char.IsNumber).ToArray();
            var retVal = new string(arr);
            if (retVal.StartsWith("0")) retVal = retVal.Substring(1);
            if (retVal.StartsWith("84")) retVal = retVal.Substring(2);
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

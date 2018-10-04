using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Serialization;

namespace TamoCRM.Core.Commons.Extensions
{
    public static class ObjectExtensions
    {
        public static decimal ToDecimal(this object source)
        {
            if (source.IsStringNullOrEmpty()) return 0;

            decimal value;
            return decimal.TryParse(source.ToString(), out value) ? value : 0;
        }

        public static float ToFloat(this object source)
        {
            if (source.IsStringNullOrEmpty()) return 0;

            float value;
            return float.TryParse(source.ToString(), out value) ? value : 0;
        }

        public static int ToInt32(this object source)
        {
            if (source.IsStringNullOrEmpty()) return 0;

            int value;
            return int.TryParse(source.ToString(), out value) ? value : 0;
        }

        public static long ToInt64(this object source)
        {
            if (source.IsStringNullOrEmpty()) return 0;

            long value;
            return long.TryParse(source.ToString(), out value) ? value : 0;
        }

        public static byte ToByte(this object source)
        {
            if (source.IsStringNullOrEmpty()) return 0;

            byte value;
            return byte.TryParse(source.ToString(), out value) ? value : Convert.ToByte(0);
        }

        public static bool IsInteger(this object obj)
        {
            int outObj;
            return int.TryParse(obj.ToString(), out outObj);
        }

        public static bool IsIntegerNull(this object obj)
        {
            try
            {
                if (obj == null) return true;
                if (obj.IsInteger() && obj.ToString().Trim() == "") return true;
                if (obj.IsInteger() && obj.ToString().Trim() == "0") return true;
                if (obj.IsInteger() && obj.ToString().Trim() == "-1") return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsStringNullOrEmpty(this object obj)
        {
            return obj == null ||
                   obj.ToString().Length == 0 ||
                   obj.ToString().Trim().Length == 0;
        }

        private static bool IsDateTime(this object obj)
        {
            DateTime outObj;
            return DateTime.TryParse(obj.ToString(), out outObj);
        }

        public static bool IsDateTimeNull(this object obj)
        {
            try
            {
                if (obj == null) return true;
                if (obj.IsDateTime() && obj.ToString().Trim() == "") return true;
                if (obj.IsDateTime() && (DateTime)obj == default(DateTime)) return true;
                if (obj.IsDateTime() && Convert.ToDateTime(obj) == DateTime.MinValue) return true;
                if (obj.IsDateTime() && obj.ToString().IndexOf("1/1/1900") > -1) return true;
                if (obj.IsDateTime() && obj.ToString().IndexOf("01/01/1900") > -1) return true;
                if (obj.IsDateTime() && obj.ToString().IndexOf("1/1/1753") > -1) return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool ToBoolean(this object source)
        {
            if (source.IsStringNullOrEmpty()) return false;
            if (source.ToString() == "1") return true;

            bool value;
            return bool.TryParse(source.ToString(), out value) && value;
        }

        public static DateTime? ToDateTime(this object source, string format = "dd/MM/yyyy")
        {
            if (source is DateTime) return (DateTime) source;
            if (source.IsStringNullOrEmpty()) return null;
            try
            {
                if (format == "dd/MM/yyyy" && !source.ToString().Contains("/"))
                    format = "ddMMyyyy";
                return DateTime.ParseExact(source.ToString(), format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? ToDateTime(this object source, string[] formats)
        {
            DateTime datetime;
            if (source is DateTime) return (DateTime)source;
            if (source.IsStringNullOrEmpty()) return null;
            if (DateTime.TryParseExact(source.ToString().Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime))
                return datetime;
            return null;
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };
            return obj == null ? string.Empty : serializer.Serialize(obj);
        }

        public static T ToObject<T>(this string json)
        {
            return json.IsStringNullOrEmpty() ? default(T) : (new JavaScriptSerializer()).Deserialize<T>(json);
        }
    }
}
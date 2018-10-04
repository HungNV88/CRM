using System;
using System.Globalization;
using System.Threading;

namespace TamoCRM.Core.Commons.Utilities
{
    public class Null
    {
        
    }
	public class ConvertHelper
	{
		public static string FormatTimeVn(DateTime dt, string defaultText)
		{
			if (ToDateTime(dt) != new DateTime(1900, 1, 1))
				return dt.ToString("dd-mm-yy");
			else
				return defaultText;
		}

		public static int ToInt32(object obj)
		{
			int retVal;

			try
			{
				retVal = Convert.ToInt32(obj);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}

		public static int ToInt32(object obj, int defaultValue)
		{
			int retVal = defaultValue;

			try
			{
				retVal = Convert.ToInt32(obj);
			}
			catch
			{
				retVal = defaultValue;
			}

			return retVal;
		}

		public static string ToString(object obj)
		{
			string retVal;

			try
			{
				retVal = Convert.ToString(obj);
			}
			catch
			{
				retVal = String.Empty;
			}

			return retVal;
		}

		public static DateTime ToDateTime(object obj)
		{
			DateTime retVal;
			try
			{
				retVal = DateTime.Parse(obj.ToString());
			}
			catch
			{
				retVal = DateTime.Now;
			}

			return retVal;
		}

		public static DateTime ToDateTime(object obj, DateTime defaultValue)
		{
			DateTime retVal;
			try
			{
				retVal = Convert.ToDateTime(obj);
			}
			catch
			{
				retVal = DateTime.Now;
			}

			return retVal;
		}

		public static bool ToBoolean(object obj)
		{
			bool retVal;

			try
			{
				retVal = Convert.ToBoolean(obj);
			}
			catch
			{
				retVal = false;
			}

			return retVal;
		}

		public static double ToDouble(object obj)
		{
			double retVal;

			try
			{

			    retVal = Convert.ToDouble(obj);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}

		public static double ToDouble(object obj, double defaultValue)
		{
			double retVal;

			try
			{
				retVal = Convert.ToDouble(obj);
			}
			catch
			{
				retVal = defaultValue;
			}

			return retVal;
		}


        public static string ToDateTime()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
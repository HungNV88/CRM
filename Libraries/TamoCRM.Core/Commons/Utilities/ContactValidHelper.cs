using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TamoCRM.Core.Commons.Utilities
{
    public class ContactValidHelper
    {
        public static bool IsMobileValid(string input)
        {
            var retVal = false;
            if (String.IsNullOrEmpty(input)) return false;
            var r = new Regex("^[0-9]{9,10}$", RegexOptions.Compiled);
            retVal = r.IsMatch(input);
            return retVal;
        }
        public static bool IsValidEmail(string input)
        {
            var retVal = false;
            if (String.IsNullOrEmpty(input)) return false;
            var r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$", RegexOptions.Compiled);
            retVal = r.IsMatch(input);
            return retVal;
        }
    }
}

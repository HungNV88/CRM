using System.Text.RegularExpressions;

namespace TestSocketClient
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public const string MobileFormat = "^[0-9]{9,10}$";
        static void Main(string[] args)
        {
            
        }
        public static bool ValidateFormat(string input, string format)
        {
            var r = new Regex(format, RegexOptions.Compiled);
            return r.IsMatch(input);
        }  
    }
}

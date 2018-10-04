using System.Configuration;

namespace TamoCRM.ImportExcel.Library
{
    public class ImportConfig
    {
        public const string EmailFormat = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$";
        public const string MobileFormat = "^[0-9]{9,10}$";
        public const string HomePhoneFormat = "^[0-9]{9,10}$";
        public const int BulkImportCount = 1000;
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["LocalSql"].ConnectionString; }
        }
    }
}

namespace TamoCRM.Core
{
    public class Constant
    {
        public const int STATUSMAP_KHACVUNG = 9;
        public const int STATUSMAP_ACCEPT_L2 = 83;
        public const string EmailFormat = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$";
        public const int MaxRedisLoad = 100000;// so key load vao redis moi lan (phone, email)
        public const string NameSystem = ""; //ten he thong, se la tien to cong vao truoc moi dien thoai voi email de save vao redis
        public const int PortImportExcel = 9999;
        // Đầu số cũ mạng Viettel
        public const string VIETTEL_162 = "162";
        public const string VIETTEL_163 = "163";
        public const string VIETTEL_164 = "164";
        public const string VIETTEL_165 = "165";
        public const string VIETTEL_166 = "166";
        public const string VIETTEL_167 = "167";
        public const string VIETTEL_168 = "168";
        public const string VIETTEL_169 = "169";
        public const string VIETTEL_86 = "86";
        public const string VIETTEL_96 = "96";
        public const string VIETTEL_97 = "97";
        public const string VIETTEL_98 = "98";
        // Đầu số mới mạng Viettel
        public const string VIETTEL_32 = "32";
        public const string VIETTEL_33 = "33";
        public const string VIETTEL_34 = "34";
        public const string VIETTEL_35 = "35";
        public const string VIETTEL_36 = "36";
        public const string VIETTEL_37 = "37";
        public const string VIETTEL_38 = "38";
        public const string VIETTEL_39 = "39";
        // Đầu số cũ mạng Mobile
        public const string MOBILE_120 = "120";
        public const string MOBILE_121 = "121";
        public const string MOBILE_122 = "122";
        public const string MOBILE_126 = "126";
        public const string MOBILE_128 = "128";
        public const string MOBILE_90 = "90";
        public const string MOBILE_93 = "93";
        // Đầu số mới mạng Mobile
        public const string MOBILE_70 = "70";
        public const string MOBILE_79 = "79";
        public const string MOBILE_77 = "77";
        public const string MOBILE_76 = "76";
        public const string MOBILE_78 = "78";
        // Đầu số cũ mạng Vina
        public const string VINA_123 = "123";
        public const string VINA_124 = "124";
        public const string VINA_125 = "125";
        public const string VINA_127 = "127";
        public const string VINA_129 = "129";
        public const string VINA_91 = "91";
        public const string VINA_94 = "94";
        // Đầu số mới mạng Vina
        public const string VINA_83 = "83";
        public const string VINA_84 = "84";
        public const string VINA_85 = "85";
        public const string VINA_81 = "81";
        public const string VINA_82 = "82";
        // Đầu số cũ mạng VietNamMobile
        public const string VIETNAM_186 = "186";
        public const string VIETNAM_188 = "188";
        public const string VIETNAM_92 = "92";
        // Đầu số mới mạng VietNamMobile
        public const string VIETNAM_56 = "56";
        public const string VIETNAM_58 = "58";
        // Đầu số cũ mạng GTel
        public const string GTEL_199 = "199";
        public const string GTEL_99 = "99";
        // Đầu số mới mạng GTel
        public const string GTEL_59 = "59";

        public static string[] PREFIX_PHONE = new string[] {
            VIETTEL_86,
            VIETTEL_96,
            VIETTEL_97,
            VIETTEL_98,
            VIETTEL_32,
            VIETTEL_34,
            VIETTEL_35,
            VIETTEL_36,
            VIETTEL_37,
            VIETTEL_38,
            VIETTEL_39,
            MOBILE_90,
            MOBILE_93,
            MOBILE_70,
            MOBILE_79,
            MOBILE_77,
            MOBILE_76,
            MOBILE_78,
            VINA_91,
            VINA_94,
            VINA_83,
            VINA_84,
            VINA_85,
            VINA_81,
            VINA_82,
            VIETNAM_92,
            VIETNAM_56,
            VIETNAM_58,
            GTEL_99,
            GTEL_59
        };
    }
}

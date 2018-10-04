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
    }
}

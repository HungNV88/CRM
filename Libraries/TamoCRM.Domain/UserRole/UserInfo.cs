namespace TamoCRM.Domain.UserRole
{
    public class UserInfo : BaseClassInfo
    {
        public int UserID { get; set; }
        public int GroupId { get; set; }
        public int BranchId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public string StationId { get; set; }
        public string Description { get; set; }
        public string ConfirmPassword { get; set; }

        public int Norms { get; set; }
        public bool IsCollector { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsConsultant { get; set; }
        public bool IsCollaborator { get; set; }
        public int NormsConsultant { get; set; }
        public int NormsCollaborator { get; set; }
        public int GroupConsultantType { get; set; }
        public int GroupCollaboratorType { get; set; }

        public string EmailSend { get; set; }
        public string PasswordSend { get; set; }
        public string SignEmailSend { get; set; }

        public int CountContactDay { get; set; }

        public int CountContactCollaboratorDay { get; set; }

        //Thêm triển khai dự án tester 4h, triển khai với 70 TVTS. (true là 1 trong 70 TVTS đó) 
        public bool IsDATester { get; set; }

    }
}


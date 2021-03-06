namespace TamoCRM.Domain.Catalogs
{
    public class CampaindTpeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LandingPageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TemplateAdsInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchKeywordInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PackageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ContainerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserGroupInfo
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }

    public class UserBranchInfo
    {
        public int UserId { get; set; }
        public int BranchId { get; set; }
    }

    public class UserRoleInfo
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class PackageFeeEduInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }

    public class ProductInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TimeSlotInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderTime { get; set; }
    }

    public class TeacherTypeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StatusCareInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LevelTesterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FeeMoneyTypeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class QualityInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PackagePriceListed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Nation { get; set; }
        public int MoneyUnitId { get; set; }
        public string Value { get; set; }
        public int Poistion { get; set; }

    }
}


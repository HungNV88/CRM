using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void Catalog_Delete<T>(int id)
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CampaindTpe_Delete"), id);
            }
            if (type == typeof(ContainerInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Container_Delete"), id);
            }
            if (type == typeof(LandingPageInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LandingPage_Delete"), id);
            }
            if (type == typeof(PackageInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Package_Delete"), id);
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_Delete"), id);
            }
            if (type == typeof(ProductInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Product_Delete"), id);
            }
            if (type == typeof(SearchKeywordInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SearchKeyword_Delete"), id);
            }
            if (type == typeof(TeacherTypeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TeacherType_Delete"), id);
            }
            if (type == typeof(TemplateAdsInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TemplateAds_Delete"), id);
            }
            //if (type == typeof(TimeSlotInfo))
            //{
            //    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TimeSlot_Delete"), id);
            //}
            //if (type == typeof(StatusCareInfo))
            //{
            //    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StatusCare_Delete"), id);
            //}
            if (type == typeof(FeeMoneyTypeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("FeeMoneyType_Delete"), id);
            }
            if (type == typeof(QualityInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Quality_Delete"), id);
            }
        }
        public override IDataReader Catalog_SelectAll<T>()
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CampaindTpe_GetAll"));
            }
            if (type == typeof(ContainerInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Container_GetAll"));
            }
            if (type == typeof(LandingPageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("LandingPage_GetAll"));
            }
            if (type == typeof(PackageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Package_GetAll"));
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_GetAll"));
            }
            if (type == typeof(ProductInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Product_GetAll"));
            }
            if (type == typeof(SearchKeywordInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SearchKeyword_GetAll"));
            }
            if (type == typeof(TeacherTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TeacherType_GetAll"));
            }
            if (type == typeof(TemplateAdsInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TemplateAds_GetAll"));
            }
            if (type == typeof(TimeSlotInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TimeSlot_GetAll"));
            }
            if (type == typeof(StatusCareInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusCare_GetAll"));
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("FeeMoneyType_GetAll"));
            }
            if (type == typeof(QualityInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Quality_GetAll"));
            }
            if (type == typeof(PackagePriceListed))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Package_GetAll"));
            }

            return null;
        }
        public override IDataReader Catalog_SelectOne<T>(int id)
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CampaindTpe_GetInfo"), id);
            }
            if (type == typeof(ContainerInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Container_GetInfo"), id);
            }
            if (type == typeof(LandingPageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("LandingPage_GetInfo"), id);
            }
            if (type == typeof(PackageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Package_GetInfo"), id);
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_GetInfo"), id);
            }
            if (type == typeof(ProductInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Product_GetInfo"), id);
            }
            if (type == typeof(SearchKeywordInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SearchKeyword_GetInfo"), id);
            }
            if (type == typeof(TeacherTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TeacherType_GetInfo"), id);
            }
            if (type == typeof(TemplateAdsInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TemplateAds_GetInfo"), id);
            }
            if (type == typeof(TimeSlotInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TimeSlot_GetInfo"), id);
            }
            if (type == typeof(StatusCareInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusCare_GetInfo"), id);
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("FeeMoneyType_GetInfo"), id);
            }
            if (type == typeof(QualityInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Quality_GetInfo"), id);
            }
            return null;
        }
        public override int Catalog_Insert<T>(string name, object objOrder = null)
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CampaindTpe_Insert"), name);
            }
            if (type == typeof(ContainerInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Container_Insert"), name);
            }
            if (type == typeof(LandingPageInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("LandingPage_Insert"), name);
            }
            if (type == typeof(PackageInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Package_Insert"), name);
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_Insert"), name);
            }
            if (type == typeof(ProductInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Product_Insert"), name);
            }
            if (type == typeof(SearchKeywordInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("SearchKeyword_Insert"), name);
            }
            if (type == typeof(TeacherTypeInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TeacherType_Insert"), name);
            }
            if (type == typeof(TemplateAdsInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TemplateAds_Insert"), name);
            }
            if (type == typeof(TimeSlotInfo))
            {
                return objOrder != null
                    ? (int) SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TimeSlot_Insert"), name, objOrder)
                    : (int) SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TimeSlot_Insert"), name);
            }
            if (type == typeof(StatusCareInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("StatusCare_Insert"), name);
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("FeeMoneyType_Insert"), name);
            }
            if (type == typeof(QualityInfo))
            {
                return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Quality_Insert"), name);
            }
            return 0;
        }
        public override void Catalog_Update<T>(int id, string name, object objOrder = null)
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CampaindTpe_Update"), id, name);
            }
            if (type == typeof(ContainerInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Container_Update"), id, name);
            }
            if (type == typeof(LandingPageInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LandingPage_Update"), id, name);
            }
            if (type == typeof(PackageInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Package_Update"), id, name);
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_Update"), id, name);
            }
            if (type == typeof(ProductInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Product_Update"), id, name);
            }
            if (type == typeof(SearchKeywordInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SearchKeyword_Update"), id, name);
            }
            if (type == typeof(TeacherTypeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TeacherType_Update"), id, name);
            }
            if (type == typeof(TemplateAdsInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TemplateAds_Update"), id, name);
            }
            if (type == typeof(TimeSlotInfo))
            {
                if (objOrder != null)
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TimeSlot_Update"), id, name, objOrder);
                else 
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TimeSlot_Update"), id, name);
            }
            if (type == typeof(StatusCareInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StatusCare_Update"), id, name);
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("FeeMoneyType_Update"), id, name);
            }
            if (type == typeof(QualityInfo))
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Quality_Update"), id, name);
            }
        }
        public override IDataReader Catalog_Search<T>(string keyword, int pageIndex, int pageSize)
        {
            var type = typeof(T);
            if (type == typeof(CampaindTpeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CampaindTpe_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(ContainerInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Container_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(LandingPageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("LandingPage_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(PackageInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Package_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(PackageFeeEduInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("PackageFeeEdu_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(ProductInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Product_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(SearchKeywordInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SearchKeyword_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(TeacherTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TeacherType_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(TemplateAdsInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TemplateAds_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(TimeSlotInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TimeSlot_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(StatusCareInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusCare_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(FeeMoneyTypeInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("FeeMoneyType_Search"), keyword, pageIndex, pageSize);
            }
            if (type == typeof(QualityInfo))
            {
                return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Quality_Search"), keyword, pageIndex, pageSize);
            }
            return null;
        }

        public override IDataReader UserRole_SelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_UserRole_SelectAll"));
        }
        public override IDataReader UserGroup_SelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_UserInGroups_SelectAll"));
        }
        public override IDataReader UserBranch_SelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_UserBranch_SelectAll"));
        }
    }
}
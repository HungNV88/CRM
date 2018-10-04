using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Services.TemplateAd
{
    public static class TemplateAdsRepository
    {
        public static List<TemplateAdsInfo> GetAll()
        {
            return ObjectHelper.FillCollection<TemplateAdsInfo>(DataProvider.Instance().TemplateAds_GetAll());
        }
    }
}

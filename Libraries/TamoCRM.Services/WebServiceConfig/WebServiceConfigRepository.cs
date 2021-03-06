using System.Collections.Generic;
using TamoCRM.Core.Data;
using TamoCRM.Domain.WebServiceConfig;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.WebServiceConfig
{
    public class WebServiceConfigRepository
    {
        public static WebServiceConfigInfo GetInfo(int branchId, int type)
        {
            return ObjectHelper.FillObject<WebServiceConfigInfo>(DataProvider.Instance().WebServiceConfig_GetInfo(branchId, type));
        }
        public static void Update(WebServiceConfigInfo info)
        {
            DataProvider.Instance().WebServiceConfig_Update(info.Id, info.BranchId, info.Type, info.Value);
        }
        public static int Create(WebServiceConfigInfo info)
        {
            return DataProvider.Instance().WebServiceConfig_Insert(info.BranchId, info.Type, info.Value);
        }
        public static WebServiceConfigInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<WebServiceConfigInfo>(DataProvider.Instance().WebServiceConfig_GetInfo(id));
        }
        public static List<WebServiceConfigInfo> GetAll()
        {
            return ObjectHelper.FillCollection<WebServiceConfigInfo>(DataProvider.Instance().WebServiceConfig_GetAll());
        }
        public static void Delete(int id)
        {
            DataProvider.Instance().WebServiceConfig_Delete(id);
        }
    }
}

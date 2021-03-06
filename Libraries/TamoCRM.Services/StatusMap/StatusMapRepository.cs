using System.Collections.Generic;
using TamoCRM.Core.Data;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.StatusMap;

namespace TamoCRM.Services.StatusMap
{
    public class StatusMapRepository
    {
        public static StatusMapInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<StatusMapInfo>(DataProvider.Instance().StatusMap_GetInfo(id));
        }

        public static List<StatusMapInfo> GetAll()
        {
            return ObjectHelper.FillCollection<StatusMapInfo>(DataProvider.Instance().StatusMap_GetAll());
        }

        public static List<StatusMapInfo> GetAllByLevelId(int levelId)
        {
            return ObjectHelper.FillCollection<StatusMapInfo>(DataProvider.Instance().StatusMap_GetAll_ByLevelId(levelId));
        }
    }        
}

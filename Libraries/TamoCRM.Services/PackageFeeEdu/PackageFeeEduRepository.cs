using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.PackageFeeEdu
{
    public class PackageFeeEduRepository
    {
        public static List<PackageFeeEduInfo> GetAll()
        {
            return ObjectHelper.FillCollection<PackageFeeEduInfo>(DataProvider.Instance().PackageFeeEdu_GetAll());
        }
    }        
}

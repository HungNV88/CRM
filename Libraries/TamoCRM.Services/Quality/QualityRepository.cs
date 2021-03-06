using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.Quality
{
    public class QualityRepository
    {
        public static List<QualityInfo> GetAll()
        {
            return ObjectHelper.FillCollection<QualityInfo>(DataProvider.Instance().Quality_GetAll());
        }
    }        
}

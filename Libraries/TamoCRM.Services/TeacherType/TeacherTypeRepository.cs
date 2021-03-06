using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.TeacherType
{
    public class TeacherTypeRepository
    {
        public static List<TeacherTypeInfo> GetAll()
        {
            return ObjectHelper.FillCollection<TeacherTypeInfo>(DataProvider.Instance().TeacherType_GetAll());
        }

    }        
}

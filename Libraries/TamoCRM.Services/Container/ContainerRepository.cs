using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.Container
{
    public class ContainerRepository
    {
        public static List<ContainerInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ContainerInfo>(DataProvider.Instance().Container_GetAll());
        }

    }        
}

using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.Product
{
    public class ProductRepository
    {
        public static List<ProductInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ProductInfo>(DataProvider.Instance().Product_GetAll());
        }

    }        
}

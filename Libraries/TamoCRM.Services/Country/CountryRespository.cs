using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;

namespace TamoCRM.Services.Country
{
    public class CountryRespository
    {
        public static string GetValueWithId(int id)
        {
            return (string)DataProvider.Instance().Country_GetValueWithId(id);
        }
    }
}

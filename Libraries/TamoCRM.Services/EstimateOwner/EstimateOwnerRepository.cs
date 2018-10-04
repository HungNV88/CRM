using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;

namespace TamoCRM.Services.EstimateOwner
{
    public class EstimateOwnerRepository
    {
        public static string GetValueWithId(int id)
        {
           return (string)DataProvider.Instance().EstimateOwner_GetValueWithId(id);
        }
    }
}

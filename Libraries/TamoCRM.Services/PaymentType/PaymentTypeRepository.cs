using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;

namespace TamoCRM.Services.PaymentType
{
    public class PaymentTypeRepository
    {
        public static string GetValuePaymentTypeWithId(int id)
        {
            return (string)DataProvider.Instance().GetValuePaymentTypeWithId(id);
        }
    }
}

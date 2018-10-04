using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;

namespace TamoCRM.Services.Banks
{
    public class BankRepository
    {
        public static string GetBankWithId(int id)
        {
           return DataProvider.Instance().Bank_Get(id);
        }
    }
}

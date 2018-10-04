using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactLevelInfos;
using System.Data;
using System.Data.SqlClient;

namespace TamoCRM.Services.LogFixMoney
{
    public class LogFixMoneyRepository
    {
        public static void Create(LogFixedMoney log, SqlTransaction tran)
        {
            DataProvider.Instance().LogFixMoney_Insert(log.UserConsulantCreate, log.Notes, log.Description, log.CreateDate, tran);
        }
    }
}

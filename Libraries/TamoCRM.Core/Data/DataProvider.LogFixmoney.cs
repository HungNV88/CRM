using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void LogFixMoney_Insert(string userTVTS, string notes, string description, DateTime createDate, SqlTransaction tran);
    }
}

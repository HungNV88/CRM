using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Services.MoneyLogs;
using TamoCRM.Services;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.HistoryMoney
{
    public class HistoryMoneyController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }

        public List<LogsMoneyInfo> GetAllByContactId(int contactId)
        {
            var list = MoneyLogsRepository.GetAllByContactId(contactId);
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    item.sNgayThucThu = Convert.ToDateTime(item.NgayThucThu).ToString("dd/MM/yyyy");
                    item.sKieuThanhToan = ObjectExtensions.GetEnumDescription((AppointmentType)item.KieuThanhToan);
                }
                return list;
            }
            return null;
        }
    }
}
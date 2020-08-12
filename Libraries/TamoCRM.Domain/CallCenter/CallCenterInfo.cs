using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.CallCenter
{
    public class CallCenterInfo: BaseClassInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserFor { get; set; }
        public string EndPoint { get; set; }
        public int Port { get; set; }
    }
}

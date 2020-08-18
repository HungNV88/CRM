using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.CallCenter
{
    public class CallCenterExtensionInfo: BaseClassInfo
    {
        public int Id { get; set; }
        public int CallCenterId { get; set; }
        public string CallCenterName { get; set; }
        public string PhoneNumber { get; set; }
        public int Extension { get; set; }
        public string UserName { get; set; }
    }
}

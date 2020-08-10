using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.CallCenter
{
    class CallCenterExtension: BaseClassInfo
    {
        public int Id { get; set; }
        public int CallCenterId { get; set; }
        public int Extension { get; set; }
    }
}

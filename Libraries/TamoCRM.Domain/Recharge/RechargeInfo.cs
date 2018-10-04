using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.Recharge
{
    public class RechargeInfo
    {
        public string status { get; set; }
        public string status_code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }
   
    public class LogNapTien
    {
        public string status { get; set; }
        public int transaction_id { get; set; }
        public string transaction_reason { get; set; }
        public int transaction_type { get; set; }
        public int balance_transaction { get; set; }
        public int balance_affter { get; set; }
        public string user_phones { get; set; }
        public string user_email { get; set; }
        public string status_code { get; set; }
        public string msg { get; set; }
        
    }

    //"{\"state\":1,\"key_name\":\"contact_id\",\"record\":{\"contact_id\":\"20150129094182\",\"full_name\":\"Ph\\u00f9ng Th\\u1ecb Tuy\\u1ebft Thu\",\"package_want_to_learn\":\"Thoa thich\",\"lang\":\"1\",\"actually_paid\":\"9000000\",\"pay_menthod\":\"Truc tiep\",\"note\":\"nop het\",\"first_name\":\"Thu\",\"last_name\":\"Ph\\u00f9ng Th\\u1ecb Tuy\\u1ebft\",\"course_id\":2,\"user_name\":\"phungthu1089@gmail.com\",\"gender\":\"Nam\",\"payment_account_created\":\"SUCCESS\",\"time_created\":1432280540,\"creator_id\":0},\"msg\":\"\\u0110\\u00e3 th\\u00eam t\\u00e0i kho\\u1ea3n th\\u00e0nh c\\u00f4ng\"}"

    public class LogTaoHocVien 
    {
        public int state {get; set;}
        public string key_name {get; set;}
        public string msg { get; set; }
    }

    public class Record 
    {
        public string contact_id {get; set;}
    }
}

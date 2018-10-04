using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Call;

namespace TestCall
{
    class Program
    {
        static void Main(string[] args)
        {
            var phone = "0979830886";
            var station_id = "3200";
            var gencode = "hhhhh";
            Console.WriteLine("Start call ...");
            //Console.ReadLine();
            //var callInfor = HelpUtils.Call(phone, station_id, gencode);
            //Console.ReadLine();
            //var callInfor2 = HelpUtils.GetCall(callInfor.call_id, phone, station_id,gencode);
            var callInfor2 = HelpUtils.GetCall("1399347376", "0986034258", "3002", "thuypt");
            Console.WriteLine(callInfor2.link_down_record);
            Console.ReadLine();

        }
    }
}

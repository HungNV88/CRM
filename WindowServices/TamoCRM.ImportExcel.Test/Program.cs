using System;
using System.Net;
using TamoCRM.ImportExcel.Library;
using TamoCRM.Core;

namespace TamoCRM.ImportExcel.Test
{
    class Program
    {  
        static void Main(string[] args)
        {
            var server = new TCPServer(IPAddress.Any, Constant.PortImportExcel);
            server.StartServer();
            Console.WriteLine("Server is started");
            Console.WriteLine("Type 'stop' to stop server");
            Console.WriteLine("Type 'loadtoredis' to load all phone and email from database to redis");
            
            var msg = string.Empty;
            while (msg != "stop")
            {
                msg = Console.ReadLine();
                if (msg == "loadtoredis")
                {
                    Console.WriteLine("Please waiting...");
                    StaticData.LoadToRedis();
                    Console.WriteLine("Load successfully !");
                }
            }
            StaticData.ClearData();
            Console.WriteLine("Server is stopping...");
            Console.WriteLine("Server is stopped...");
            server.StopServer();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Services.ImportExcels;

namespace TamoCRM.ImportExcel.Library
{
    public class ImportProcessManager : IDisposable
    {
        private bool stopFlag;
        public int Port { get; set; }
        private readonly TcpListener tcpListener;
        private readonly Queue Messages = new Queue();

        public ImportProcessManager(int port)
        {
            Port = port;
            tcpListener = new TcpListener(IPAddress.Any, Port);

        }
        public void Start()
        {
            StartSocketServer();
            StartQueueImport();
        }
        public void Stop()
        {
            stopFlag = true;

        }
        private void StartQueueImport()
        {
            while (!stopFlag)
            {
                var importId = 0;
                Monitor.Enter(Messages.SyncRoot);
                if (Messages.Count > 0)
                {
                    importId = ConvertHelper.ToInt32(Messages.Dequeue());
                }
                Monitor.Exit(Messages.SyncRoot);
                var importInfo = ImportExcelRepository.GetInfo(importId);

                if (importInfo != null)
                {
                    var importProcess = new ImportProcess(importInfo);
                    var t = new Thread(importProcess.Start);
                    t.Start();
                    Console.WriteLine(importInfo.FilePath);
                }
                
                Thread.Sleep(10);
            }
        }
        private void StartSocketServer()
        {
            tcpListener.Start();
            Console.WriteLine("************This is Server program************");
            
            const int numberOfClientsYouNeedToConnect = 10;
            for (var i = 0; i < numberOfClientsYouNeedToConnect; i++)
            {
                var newThread = new Thread(Listen);
                newThread.Start();
            }
        }
        private void Listen()
        {
            var socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                Console.WriteLine("Client:" + socketForClient.RemoteEndPoint + " now connected to server.");
                var networkStream = new NetworkStream(socketForClient);
                var streamWriter = new System.IO.StreamWriter(networkStream);
                var streamReader = new System.IO.StreamReader(networkStream);

                while (!stopFlag)
                {
                    string theString = streamReader.ReadLine();
                    if (!string.IsNullOrEmpty(theString))
                    {
                        Monitor.Enter(Messages.SyncRoot);
                        Messages.Enqueue(theString);
                        Monitor.Exit(Messages.SyncRoot);
                    }
                    Thread.Sleep(10);
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();

            }
            socketForClient.Close();
            Console.WriteLine("Press any key to exit from server program");
            Console.ReadKey();
        }
        public void Dispose()
        {

        }
    }
}

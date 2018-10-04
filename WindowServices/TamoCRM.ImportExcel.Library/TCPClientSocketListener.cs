using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.ImportExcel.Library
{
    /// <summary>
    /// Summary description for TCPSocketListener.
    /// </summary>
    public class TCPSocketListener
    {
        public static String DEFAULT_FILE_STORE_LOC = "C:\\TCP\\";

        public enum STATE { FILE_NAME_READ, DATA_READ, FILE_CLOSED };

        /// <summary>
        /// Variables that are accessed by other classes indirectly.
        /// </summary>
        private bool m_stopClient;
        private Socket m_clientSocket;
        private bool m_markedForDeletion;
        private Thread m_clientListenerThread;

        private DateTime m_lastReceiveDateTime;
        private DateTime m_currentReceiveDateTime;

        /// <summary>
        /// Client Socket Listener Constructor.
        /// </summary>
        /// <param name="clientSocket"></param>
        public TCPSocketListener(Socket clientSocket)
        {
            m_clientSocket = clientSocket;
        }

        /// <summary>
        /// Client SocketListener Destructor.
        /// </summary>
        ~TCPSocketListener()
        {
            StopSocketListener();
        }

        /// <summary>
        /// Method that starts SocketListener Thread.
        /// </summary>
        public void StartSocketListener()
        {
            if (m_clientSocket == null) return;
            m_clientListenerThread = new Thread(SocketListenerThreadStart);
            m_clientListenerThread.Start();
        }
        private static void StartImport(object obj)
        {
            var process = (ImportProcess)(obj);
            process.Start();
        }
        /// <summary>
        /// Thread method that does the communication to the client. This 
        /// thread tries to receive from client and if client sends any data
        /// then parses it and again wait for the client data to come in a
        /// loop. The recieve is an indefinite time receive.
        /// </summary>
        private void SocketListenerThreadStart()
        {
            m_lastReceiveDateTime = DateTime.Now;
            m_currentReceiveDateTime = DateTime.Now;

            var t = new Timer(CheckClientCommInterval, null, 15000, 15000);

            while (!m_stopClient)
            {
                try
                {
                    var networkStream = new NetworkStream(m_clientSocket);
                    var streamReader = new StreamReader(networkStream);
                    var importId = ConvertHelper.ToInt32(streamReader.ReadLine());
                    var importInfo = ImportExcelRepository.GetInfo(importId);

                    if (importInfo == null) continue;
                    var importProcess = new ImportProcess(importInfo);
                    var thread = new Thread(StartImport);
                    thread.Start(importProcess);
                    //size = m_clientSocket.Receive(byteBuffer);
                    //m_currentReceiveDateTime = DateTime.Now;
                    //ParseReceiveBuffer(byteBuffer, size);
                    //Console.WriteLine(m_oneLineBuf.ToString());
                }
                catch
                {
                    m_stopClient = true;
                    m_markedForDeletion = true;
                }
            }
            t.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Method that stops Client SocketListening Thread.
        /// </summary>
        public void StopSocketListener()
        {
            if (m_clientSocket == null) return;
            m_stopClient = true;
            m_clientSocket.Close();

            // Wait for one second for the the thread to stop.
            m_clientListenerThread.Join(1000);

            // If still alive; Get rid of the thread.
            if (m_clientListenerThread.IsAlive)
            {
                m_clientListenerThread.Abort();
            }
            m_clientListenerThread = null;
            m_clientSocket = null;
            m_markedForDeletion = true;
        }

        /// <summary>
        /// Method that returns the state of this object i.e. whether this
        /// object is marked for deletion or not.
        /// </summary>
        /// <returns></returns>
        public bool IsMarkedForDeletion()
        {
            return m_markedForDeletion;
        }

        /// <summary>
        /// Method that checks whether there are any client calls for the
        /// last 15 seconds or not. If not this client SocketListener will
        /// be closed.
        /// </summary>
        /// <param name="o"></param>
        private void CheckClientCommInterval(object o)
        {
            if (m_lastReceiveDateTime.Equals(m_currentReceiveDateTime))
            {
                StopSocketListener();
            }
            else
            {
                m_lastReceiveDateTime = m_currentReceiveDateTime;
            }
        }
    }
}

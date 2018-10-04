using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using TamoCRM.ImportExcel.Library;

namespace TamoCRM.ImportExcel.WinService
{
    /// <summary>
    /// Class that will run as a Windows Service and its display name is
    /// TCP (Sabre Group Config Transfer Service) in Windows Services.
    /// This service basically start a server on service start 
    /// (on OnStart method) and shutdown the server on the servie stop 
    /// (on OnStop method).
    /// </summary>
    public partial class TCPService : System.ServiceProcess.ServiceBase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private TCPServer server = null;

        public TCPService()
        {
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
        }


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "TCPService";
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            // Create the Server Object ans Start it.
            server = new TCPServer();
            server.StartServer();
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            // Stop the Server. Release it.
            server.StopServer();
            server = null;
        }
    }
}

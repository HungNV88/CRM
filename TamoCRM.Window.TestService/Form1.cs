using System;
using System.Windows.Forms;

namespace TamoCRM.Window.TestService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //var callInfo = ObjectHelper.CreateObject<CallInfor>(txtXml.Text);
            var service = new ServiceMissCall.ServiceCRM();
            var obj = service.Incoming(txtXml.Text);
            if (obj != null)
            {
                MessageBox.Show(obj.Description);
            }
        }

        private void btnMissCall_Click(object sender, EventArgs e)
        {
            var service = new ServiceMissCall.ServiceCRM();
            var obj = service.MissCall(txtXml.Text);
            if (obj != null)
            {
                MessageBox.Show(obj.Description);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var service = new ServiceMissCall.ServiceCRM();
            var obj = service.UpdateConectStatus(txtXml.Text);
            if (obj != null) MessageBox.Show(obj.Description);
        }

        private void btnUpdateIncommingTCL_Click(object sender, EventArgs e)
        {
            var service = new ServiceMissCall.ServiceCRM();
            var obj = service.TCLCallInfoUpdate(txtXml.Text);
            if (obj != null) MessageBox.Show(obj.Description);
        }
    }
}

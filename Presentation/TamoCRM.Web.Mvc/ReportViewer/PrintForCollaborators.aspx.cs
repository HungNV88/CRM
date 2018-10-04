using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using TamoCRM.Services.Channels;

namespace TamoCRM.Web.Mvc.ReportViewer
{
    public partial class PrintForCollaborators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)return;

            //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsPrintContactForCollaborators",new DataTable()));
            //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsChannel", ChannelRepository.GetAll()));
            //ReportViewer1.LocalReport.Refresh();
        }
    }
}
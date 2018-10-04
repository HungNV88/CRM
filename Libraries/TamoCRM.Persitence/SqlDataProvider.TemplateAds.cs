using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader TemplateAds_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TemplateAds_GetAll"));
        }
    }
}

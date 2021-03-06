using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        //public override int EmailTemplate_Insert(string subject, int typeEmail, string content)
        //{
        //    return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("EmailTemplate_Insert"), subject, typeEmail, content);
        //}
        public override void EmailTemplate_Insert(string subject, int typeEmail, string content)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("EmailTemplate_Insert"), subject, typeEmail, content);

        }
        public override void EmailTemplate_Update(int Id, string subject, int typeEmail, string content)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("EmailTemplate_Update"), Id, subject, typeEmail, content);

        }
        public override IDataReader EmailTemplate_Filter(int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("EmailTemplate_GetAll"), pageIndex, pageSize);
        }
        public override IDataReader GetEmailTemplateId(int EmailTemplateId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetEmailTemplate"), EmailTemplateId);
        }
        public override IDataReader GetEmailInfo(int Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetEmailInfo"), Id);
        }
        public override IDataReader Email_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Email_GetAll"));
        }
    }
}


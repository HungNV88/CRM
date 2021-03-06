using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void EmailTemplate_Insert(string subject, int typeEmail, string content);
        public abstract void EmailTemplate_Update(int Id,string subject, int typeEmail, string content);
        public abstract IDataReader EmailTemplate_Filter(int pageIndex, int pageSize);
        public abstract IDataReader GetEmailTemplateId(int EmailTemplateId);
        public abstract IDataReader GetEmailInfo(int Id);
        public abstract IDataReader Email_GetAll();
    }
}


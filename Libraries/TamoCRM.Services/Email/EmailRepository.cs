using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.EmailInfo;

namespace TamoCRM.Services.Email
{
    public class EmailRepository
    {
        public static void Create(EmailInfo info)
        {
            DataProvider.Instance().EmailTemplate_Insert( info.Subject, info.EmailType, info.Content);            
        }
        public static void Edit(EmailInfo info)
        {
            DataProvider.Instance().EmailTemplate_Update(info.Id,info.Subject, info.EmailType, info.Content);
        }
        public static EmailInfo GetEmailTemplate(int EmailTemplateId)
        {            
            return ObjectHelper.FillObject<EmailInfo>(DataProvider.Instance().GetEmailTemplateId(EmailTemplateId));
        }
        public static EmailInfo GetEmailInfo(int Id)
        {
            return ObjectHelper.FillObject<EmailInfo>(DataProvider.Instance().GetEmailInfo(Id));
        }
        public static List<EmailInfo> FilterListEmail(int pageIndex, int pageSize, out int totalRecord)
        {
            return FillEmailCollection(DataProvider.Instance().EmailTemplate_Filter(pageIndex, pageSize), out totalRecord);
        }
        public static List<EmailInfo> GetAll()
        {
            return ObjectHelper.FillCollection<EmailInfo>(DataProvider.Instance().Email_GetAll());
        }

        private static List<EmailInfo> FillEmailCollection(IDataReader reader, out int totalRecords)
        {
            List<EmailInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<EmailInfo>(reader, false);

                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }

            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }       

    }        
}

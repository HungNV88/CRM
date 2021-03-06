using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Phones;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.Phones
{
    public class PhoneRepository
    {
        public static void Create(PhoneInfo info)
        {
            DataProvider.Instance().Phones_Insert( info.ContactId, info.PhoneType, info.PhoneNumber, info.IsPrimary);
        }
        public static void Update(PhoneInfo info)
        {
            DataProvider.Instance().Phones_Update(info.ContactId, info.PhoneType, info.PhoneNumber,info.IsPrimary);
        }
        public static void Delete(int phoneId)
        {
            DataProvider.Instance().Phones_Delete(phoneId);
        }
        public static List<PhoneInfo> GetAll()
        {
            return ObjectHelper.FillCollection<PhoneInfo>(DataProvider.Instance().Phones_GetAll());
        }
        public static List<PhoneInfo> GetAll_ForRedis()
        {
            return ObjectHelper.FillCollection<PhoneInfo>(DataProvider.Instance().Phones_GetAll_ForRedis());
        }
        public static List<PhoneInfo> GetByContact(int contactId)
        {
            return ObjectHelper.FillCollection<PhoneInfo>(DataProvider.Instance().Phones_GetByContact(contactId));
        }
        public static List<PhoneInfo> GetByContacts_Xml(string xml)
        {
            return ObjectHelper.FillCollection<PhoneInfo>(DataProvider.Instance().Phones_GetByContacts_Xml(xml));
        }
        public static List<PhoneInfo> GetByContacts(string contactIds)
        {
            return ObjectHelper.FillCollection<PhoneInfo>(DataProvider.Instance().Phones_GetByContacts(contactIds));
        }
        public static PhoneInfo GetByPhoneNumber(string phoneNumber)
        {
            return ObjectHelper.FillObject<PhoneInfo>(DataProvider.Instance().Phones_GetByPhoneNumber(phoneNumber));
        }
        public static List<PhoneInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillPhonCollection(DataProvider.Instance().Phones_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<PhoneInfo> FillPhonCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<PhoneInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new PhoneInfo();
					/*
                    
                    
                    info.PhoneId = ConvertHelper.ToInt32(reader["PhoneId"]);
                    
                    
                    
                    
                    info.ContactId = ConvertHelper.ToInt32(reader["ContactId"]);
                    
                    
                    
                    info.PhoneType = ConvertHelper.ToString(reader["PhoneType"]);
                    
                    
                    
                    
                    info.PhoneNumber = ConvertHelper.ToString(reader["PhoneNumber"]);
                    
                    
                    
                    
                    
                    info.IsPrimary = ConvertHelper.ToInt32(reader["IsPrimary"]);
                    
                    
                    
					*/
                    retVal.Add(info);
                }
                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }
                                
            }
            catch (Exception exc)
            {
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
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

using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.AppointmentInterviews;

namespace TamoCRM.Services.AppointmentInterview
{
    public class AppointmentInterviewRepository
    {
        public static int Create(AppointmentInterviewInfo info)
        {
            return DataProvider.Instance().AppointmentInterview_Insert(info.ContactId, info.UserId, info.AppointmentDate, info.TimeSlotId, info.Notes, info.NotesCM, info.StatusInterviewId, info.TeacherTypeId, info.CasecAccountId, info.LevelTesterId);
        }
        public static void Update(AppointmentInterviewInfo info)
        {
            DataProvider.Instance().AppointmentInterview_Update( info.ContactId, info.UserId, info.AppointmentDate, info.TimeSlotId, info.Notes, info.NotesCM, info.StatusInterviewId, info.TeacherTypeId,info.CasecAccountId);
        }
        public static void Delete(int contactId)
        {
            DataProvider.Instance().AppointmentInterview_Delete(contactId);
        }
        public static AppointmentInterviewInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<AppointmentInterviewInfo>(DataProvider.Instance().AppointmentInterview_GetInfo(id));
        }
        public static List<AppointmentInterviewInfo> GetAll()
        {
            return ObjectHelper.FillCollection<AppointmentInterviewInfo>(DataProvider.Instance().AppointmentInterview_GetAll());
        }
        public static List<AppointmentInterviewInfo> GetAllByContactId(int contactId)
        {
            return ObjectHelper.FillCollection<AppointmentInterviewInfo>(DataProvider.Instance().AppointmentInterview_GetAllByContactId(contactId));
        }
        public static List<AppointmentInterviewInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillAppointmentInterviewCollection(DataProvider.Instance().AppointmentInterview_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<AppointmentInterviewInfo> FillAppointmentInterviewCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<AppointmentInterviewInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new AppointmentInterviewInfo();
					/*
                    
                    
                    info.ContactId = ConvertHelper.ToInt32(reader["ContactId"]);
                    
                    
                    
                    
                    info.UserId = ConvertHelper.ToInt32(reader["UserId"]);
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    info.TimeSlotId = ConvertHelper.ToInt32(reader["TimeSlotId"]);
                    
                    
                    
                    info.Notes = ConvertHelper.ToString(reader["Notes"]);
                    
                    
                    
                    
                    
                    info.StatusInterviewId = ConvertHelper.ToInt32(reader["StatusInterviewId"]);
                    
                    
                    
                    
                    info.TeacherTypeId = ConvertHelper.ToInt32(reader["TeacherTypeId"]);
                    
                    
                    
                    
                    info.CasecAccountId = ConvertHelper.ToInt32(reader["CasecAccountId"]);
                    
                    
                    
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
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }

    }        
}

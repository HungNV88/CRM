using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int ContactTmps_Insert(int importId, string studentId, string fullname, string birth, string mobil1, string mobil2, string tel, string email, string registeredSchool, string registerdMajorId, string question, string enrollmentMark, string educationalLevel, string locationId, int errorStatus);
        public abstract void ContactTmps_Update( int contactId, int importId, string studentId, string fullname, string birth, string mobil1, string mobil2, string tel, string email, string registeredSchool, string registerdMajorId, string question, string enrollmentMark, string educationalLevel, string locationId,int errorStatus);
        public abstract void ContactTmps_Delete(int contactId);
        public abstract IDataReader ContactTmps_GetInfo(int contactId);
        public abstract IDataReader ContactTmps_GetAll();  
        public abstract IDataReader ContactTmps_Search(string keyword, int pageIndex, int pageSize);
        public abstract IDataReader ContactTmps_Search(int branchId, int collector, int channelId, int importId,DateTime? importDate, int pageIndex, int pageSize);
        
    }
}


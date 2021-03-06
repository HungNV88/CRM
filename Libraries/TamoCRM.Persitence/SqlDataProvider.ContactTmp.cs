using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int ContactTmps_Insert(int importId, string studentId, string fullname, string birth, string mobil1, string mobil2, string tel, string email, string registeredSchool, string registerdMajorId, string question, string enrollmentMark, string educationalLevel, string locationId, int errorStatus)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("ContactTmps_Insert"), importId, studentId, fullname, birth, mobil1, mobil2, tel, email, registeredSchool, registerdMajorId, question, enrollmentMark, educationalLevel, locationId, errorStatus);
        }

        public override void ContactTmps_Update( int contactId, int importId, string studentId, string fullname, string birth, string mobil1, string mobil2, string tel, string email, string registeredSchool, string registerdMajorId, string question, string enrollmentMark, string educationalLevel, string locationId,int errorStatus)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactTmps_Update"),  contactId, importId, studentId, fullname, birth, mobil1, mobil2, tel, email, registeredSchool, registerdMajorId, question, enrollmentMark, educationalLevel, locationId,errorStatus);
        }

        public override void ContactTmps_Delete(int contactId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ContactTmps_Delete"), contactId);
        }

        public override IDataReader ContactTmps_GetInfo(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactTmps_GetInfo"), contactId);
        }

        public override IDataReader ContactTmps_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactTmps_GetAll"));
        }

        public override IDataReader ContactTmps_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactTmps_Search"), keyword, pageIndex, pageSize);
        }

        public override IDataReader ContactTmps_Search(int branchId, int collector, int channelId, int importId,DateTime? importDate,int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactTmps_SearchByCollectorChannelImport"), branchId, collector, channelId, importId, importDate, pageIndex, pageSize);
        }
    }
}


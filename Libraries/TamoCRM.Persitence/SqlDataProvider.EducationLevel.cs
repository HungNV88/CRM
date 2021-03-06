using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int EducationLevels_Insert( string name, string description, int createdBy, DateTime createdDate)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("EducationLevels_Insert"),  name, description, createdBy, createdDate);
        }

        public override void EducationLevels_Update( int educationLevelId, string name, string description, int createdBy,DateTime createdDate)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("EducationLevels_Update"),  educationLevelId, name, description, createdBy,createdDate);
        }

        public override void EducationLevels_Delete(int educationLevelId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("EducationLevels_Delete"), educationLevelId);
        }

        public override IDataReader EducationLevels_GetInfo(int educationLevelId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("EducationLevels_GetInfo"), educationLevelId);
        }

        public override IDataReader EducationLevels_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("EducationLevels_GetAll"));
        }

        public override IDataReader EducationLevels_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("EducationLevels_Search"), keyword, pageIndex, pageSize);
        }
    }
}


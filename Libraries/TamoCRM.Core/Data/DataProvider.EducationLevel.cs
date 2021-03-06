using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int EducationLevels_Insert( string name, string description, int createdBy, DateTime createdDate);
        public abstract void EducationLevels_Update( int educationLevelId, string name, string description, int createdBy,DateTime createdDate);
        public abstract void EducationLevels_Delete(int educationLevelId);
        public abstract IDataReader EducationLevels_GetInfo(int educationLevelId);
        public abstract IDataReader EducationLevels_GetAll();  
        public abstract IDataReader EducationLevels_Search(string keyword, int pageIndex, int pageSize);
    }
}


using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader SourceTypes_GetAllTest();
        public abstract IDataReader SourceTypes_GetAll();
        public abstract void SourceTypes_Delete(int sourceTypeId);
        public abstract IDataReader SourceTypes_GetInfo(string code);
        public abstract IDataReader SourceTypes_GetInfo(int sourceTypeId);
        public abstract IDataReader SourceTypes_Search(string keyword, int pageIndex, int pageSize);
        public abstract int SourceTypes_Insert(string code, string name, string description, bool isCheckDuplicate, bool isCheckUpdate, bool isShowHotLine, bool isShowConsultant,  int createdBy, DateTime createdDate, int changedBy, DateTime changedDate);
        public abstract void SourceTypes_Update(int sourceTypeId, string code, string name, string description, bool isCheckDuplicate, bool isShowHotLine, bool isShowConsultant, bool isCheckUpdate, int createdBy, DateTime createdDate, int changedBy, DateTime changedDate);
    }
}


using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int ImportExcels_Insert( int userId, int typeId, int channelId, int collectorId, int branchId, int levelId, int status, string filePath, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount, DateTime importedDate, int importStatus);
        public abstract void ImportExcels_Update(int importId, int userId, int typeId, int channelId, int collectorId, int branchId, int levelId, int status, string filePath, int rowIndex, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount,DateTime importedDate, int importStatus);
        public abstract void ImportExcels_UpdateDuplicateContact(int importId, int rowIndex, int totalRow, int checkCount, int errorCount, int duplicateCount, int souceTypeId, int duplicateContactId);
        public abstract void ImportExcels_UpdateImportProgress(int importId, int rowIndex, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount, string code, string fullname, DateTime? birth, string email, string mobile1, string mobile2, string tel, string address, int gender, int typeId, int channelId, int levelId, int collectorId, int branchId, int statusId, int statusMapId, DateTime? registeredDate, string notes);
        public abstract void ImportExcels_UpdateImportProgressStatus(int importId, int status);
        public abstract void ImportExcels_Delete(int importId);
        public abstract IDataReader ImportExcels_GetInfo(int importId);
        public abstract IDataReader ImportExcels_GetAll(int? top = 10);
        public abstract IDataReader ImportExcels_GetAll_ByTypeId(int typeId);
        public abstract IDataReader ImportExcels_GetForBC01(int collectorId, DateTime dtFrom, DateTime dtTo);
        public abstract IDataReader ImportExcels_Search(string keyword, int pageIndex, int pageSize);
        public abstract IDataReader ImportExcels_Filter_ForCampain(int branchId, int sourceTypeId);      
        public abstract IDataReader ImportExcels_Filter_ForMolContainer(int branchId, int sourceTypeId);

    }
}


using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int ImportExcels_Insert(int userId, int typeId, int channelId, int collectorId, int branchId, int levelId, int status, string filePath, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount, DateTime importedDate, int importStatus)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("ImportExcels_Insert"),  userId, typeId, channelId, collectorId, branchId, levelId, status, filePath, totalRow, checkCount, errorCount, internalDuplicate, duplicateCount, importedDate, importStatus);
        }

        public override void ImportExcels_Update(int importId, int userId, int typeId, int channelId, int collectorId, int branchId, int levelId, int status, string filePath, int rowIndex, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount, DateTime importedDate, int importStatus)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ImportExcels_Update"),  importId, userId, typeId, channelId, collectorId, branchId, levelId, status, filePath, rowIndex, totalRow, checkCount, errorCount, internalDuplicate, duplicateCount,importedDate, importStatus);
        }

        public override void ImportExcels_UpdateDuplicateContact(int importId, int rowIndex, int totalRow, int checkCount, int errorCount, int duplicateCount, int sourceTypeId, int duplicateContactId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ImportExcels_UpdateDuplicateContact"), importId, rowIndex, totalRow, checkCount, errorCount, duplicateCount, sourceTypeId, duplicateContactId);
        }

        public override void ImportExcels_UpdateImportProgress(int importId, int rowIndex, int totalRow, int checkCount, int errorCount, int internalDuplicate, int duplicateCount, string code, string fullname, DateTime? birth, string email, string mobile1, string mobile2, string tel, string address, int gender, int typeId, int channelId, int levelId, int collectorId, int branchId, int statusId, int statusMapId, DateTime? registeredDate, string notes)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ImportExcels_UpdateImportProgress"), importId, rowIndex, totalRow, checkCount, errorCount, internalDuplicate, duplicateCount, code, fullname, birth, email, mobile1, mobile2, tel, address, gender, typeId, channelId, levelId, branchId, statusId, statusMapId, registeredDate, notes);
        }

        public override void ImportExcels_UpdateImportProgressStatus(int importId, int status)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ImportExcels_UpdateImportProgressStatus"), importId, status);
        }

        public override void ImportExcels_Delete(int importId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("ImportExcels_Delete"), importId);
        }

        public override IDataReader ImportExcels_GetInfo(int importId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_GetInfo"), importId);
        }

        public override IDataReader ImportExcels_GetAll(int? top = 10)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_GetAll"), top);
        }

        public override IDataReader ImportExcels_GetAll_ByTypeId(int typeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_GetAll_ByTypeId"), typeId);
        }

        public override IDataReader ImportExcels_GetForBC01(int collectorId, DateTime dtFrom, DateTime dtTo)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_GetForBC01"), collectorId, dtFrom, dtTo);
        }
        public override IDataReader ImportExcels_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_Search"), keyword, pageIndex, pageSize);
        }

        public override IDataReader ImportExcels_Filter_ForCampain(int branchId, int sourceTypeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_Filter_ForCampain"), branchId, sourceTypeId);
        }
        
        public override IDataReader ImportExcels_Filter_ForMolContainer(int branchId, int sourceTypeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ImportExcels_Filter_ForMolContainer"), branchId, sourceTypeId);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Contacts;
namespace TamoCRM.Services.ImportExcels
{
    public class ImportExcelRepository
    {
        public static int Create(ImportExcelInfo info)
        {
            return DataProvider.Instance().ImportExcels_Insert(info.UserId, info.TypeId, info.ChannelId, info.CollectorId, info.BranchId, info.LevelId, info.Status, info.FilePath, info.TotalRow, info.CheckCount, info.ErrorCount, info.InternalDuplicateCount, info.DuplicateCount, info.ImportedDate, info.ImportStatus);
        }
        public static void Update(ImportExcelInfo info)
        {
            DataProvider.Instance().ImportExcels_Update(info.ImportId, info.UserId, info.TypeId, info.ChannelId, info.CollectorId, info.BranchId, info.LevelId, info.Status, info.FilePath, info.RowIndex, info.TotalRow, info.CheckCount, info.ErrorCount, info.InternalDuplicateCount, info.DuplicateCount,info.ImportedDate, info.ImportStatus);
        }
        public static void UpdateDuplicateContact(ImportExcelInfo import, int duplicateContactId)
        {
            DataProvider.Instance().ImportExcels_UpdateDuplicateContact(import.ImportId, import.RowIndex, import.TotalRow, import.CheckCount, import.ErrorCount, import.DuplicateCount, import.TypeId, duplicateContactId);
        }
        public static void UpdateImportProgress(ImportExcelInfo import, ContactInfo contact, string mobile1, string mobile2, string tel)
        {
            if (contact == null)
            {
                Update(import);
            }
            else
            {
                DataProvider.Instance().ImportExcels_UpdateImportProgress(import.ImportId, import.RowIndex, import.TotalRow, import.CheckCount, import.ErrorCount, import.InternalDuplicateCount, import.DuplicateCount, contact.Code, contact.Fullname, contact.Birthday, contact.Email, mobile1, mobile2, tel, contact.Address, contact.Gender, contact.TypeId, contact.ChannelId, contact.LevelId, contact.CollectorId, contact.BranchId, contact.StatusId, contact.StatusCareConsultantId, contact.RegisteredDate, contact.Notes);
            }
        }
        public static void Delete(int importId)
        {
            DataProvider.Instance().ImportExcels_Delete(importId);
        }
        public static ImportExcelInfo GetInfo(int importId)
        {
            return ObjectHelper.FillObject<ImportExcelInfo>(DataProvider.Instance().ImportExcels_GetInfo(importId));
        }
        public static List<ImportExcelInfo> GetAll(int? top = 10)
        {
            return ObjectHelper.FillCollection<ImportExcelInfo>(DataProvider.Instance().ImportExcels_GetAll(top));
        }
        public static List<ImportExcelInfo> GetAllByTypeId(int typeId)
        {
            return ObjectHelper.FillCollection<ImportExcelInfo>(DataProvider.Instance().ImportExcels_GetAll_ByTypeId(typeId));
        }
        public static List<ImportExcelInfo> GetForBC01(int collectorId, DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ImportExcelInfo>(DataProvider.Instance().ImportExcels_GetForBC01(collectorId, from, to));
        }
        public static List<ImportExcelInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillImportExcelCollection(DataProvider.Instance().ImportExcels_Search(keyword, pageIndex, pageSize), out totalRecord);
        }

        public static List<ImportExcelInfo> FilterForCampain(int branchId, int sourceTypeId = 0)
        {
            return ObjectHelper.FillCollection<ImportExcelInfo>(DataProvider.Instance().ImportExcels_Filter_ForCampain(branchId, sourceTypeId));
        }
        
        public static List<ImportExcelInfo> FilterForMOLContainer(int branchId, int sourceTypeId = 0)
        {
            return ObjectHelper.FillCollection<ImportExcelInfo>(DataProvider.Instance().ImportExcels_Filter_ForMolContainer(branchId, sourceTypeId));
        }
        private static List<ImportExcelInfo> FillImportExcelCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<ImportExcelInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new ImportExcelInfo();
                    
                    info.ImportId = ConvertHelper.ToInt32(reader["ImportId"]);
                    info.UserId = ConvertHelper.ToInt32(reader["UserId"]);
                    info.TypeId = ConvertHelper.ToInt32(reader["TypeId"]);
                    info.ChannelId = ConvertHelper.ToInt32(reader["ChannelId"]);
                    info.CollectorId = ConvertHelper.ToInt32(reader["CollectorId"]);
                    info.BranchId = ConvertHelper.ToInt32(reader["BranchId"]);
                    info.Status = ConvertHelper.ToInt32(reader["Status"]);
                    info.FilePath = ConvertHelper.ToString(reader["FilePath"]);
                    info.TotalRow = ConvertHelper.ToInt32(reader["TotalRow"]);
                    info.CheckCount = ConvertHelper.ToInt32(reader["CheckCount"]);
                    info.ErrorCount = ConvertHelper.ToInt32(reader["ErrorCount"]);
                    info.DuplicateCount = ConvertHelper.ToInt32(reader["DuplicateCount"]);
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

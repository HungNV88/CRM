using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Caching;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Branches;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.Branches
{
    public class BranchRepository
    {
        public const string Redis_BranchKey = "Branches";
        public static int Create(BranchInfo info)
        {
            //var retVal = CachingProvider.Instance().Get()
            var retVal =  DataProvider.Instance().Branches_Insert( info.Code, info.Name, info.LocationID, info.Description, info.CreatedBy);
            return retVal;
        }
        public static void Update(BranchInfo info)
        {
            DataProvider.Instance().Branches_Update( info.BranchId, info.Code, info.Name, info.LocationID, info.Description, info.ChangedBy);
        }
        public static void Delete(int branchId,int deletedby)
        {
            DataProvider.Instance().Branches_Delete(branchId,deletedby);
        }
        public static BranchInfo GetInfo(int branchId)
        {
            return ObjectHelper.FillObject<BranchInfo>(DataProvider.Instance().Branches_GetInfo(branchId));
        }
        public static List<BranchInfo> GetAll()
        {
            return ObjectHelper.FillCollection<BranchInfo>(DataProvider.Instance().Branches_GetAll());
        }
        public static List<BranchInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillBranchCollection(DataProvider.Instance().Branches_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<BranchInfo> FillBranchCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<BranchInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new BranchInfo();
                    
                    info.BranchId = ConvertHelper.ToInt32(reader["BranchId"]);
                    
                    info.Code = ConvertHelper.ToString(reader["Code"]);
                    
                    info.Name = ConvertHelper.ToString(reader["Name"]);
                    
                    info.LocationID = ConvertHelper.ToInt32(reader["LocationID"]);
                    
                    info.Description = ConvertHelper.ToString(reader["Description"]);

                    info.Status = ConvertHelper.ToInt32(reader["Status"]);
                    
                    info.CreatedBy = ConvertHelper.ToInt32(reader["CreatedBy"]);
                    
                    info.ChangedBy = ConvertHelper.ToInt32(reader["ChangedBy"]);

                    info.CreatedDate = ConvertHelper.ToDateTime(reader["CreatedDate"]);

                    info.ChangedDate = ConvertHelper.ToDateTime(reader["ChangedDate"]);

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

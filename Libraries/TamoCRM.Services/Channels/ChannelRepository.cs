using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Channels;
using TamoCRM.Core.Commons.Utilities;
using System.Linq;
using TamoCRM.Core.Commons.Extensions;

namespace TamoCRM.Services.Channels
{
    public class ChannelRepository
    {
        public static int Create(ChannelInfo info)
        {
            return DataProvider.Instance().Channels_Insert(info.Name, info.Code, info.Description, info.SourceTypeId, info.CreatedBy);
        }
        public static void Update(ChannelInfo info)
        {
            DataProvider.Instance().Channels_Update(info.ChannelId, info.Name, info.Code, info.Description, info.SourceTypeId, info.ChangedBy);
        }
        public static void Delete(int channelId)
        {
            DataProvider.Instance().Channels_Delete(channelId);
        }
        public static ChannelInfo GetInfo(int channelId)
        {
            return ObjectHelper.FillObject<ChannelInfo>(DataProvider.Instance().Channels_GetInfo(channelId));
        }
        public static ChannelInfo GetInfo(string name)
        {
            return ObjectHelper.FillObject<ChannelInfo>(DataProvider.Instance().Channels_GetInfo(name));
        }
        public static List<ChannelInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_GetAll());
        }
        public static List<ChannelInfo> GetAvailables(int branchId)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_GetAvailables(branchId));
        }
        public static List<ChannelInfo> FilterCountForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_Filter_Count_ForDistributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds));
        }
        public static int FilterContactCountForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds, string channelIds, string schoolIds)
        {
            return DataProvider.Instance().Channels_Filter_ContactCount_ForDistributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds);
        }
        public static List<ChannelInfo> FilterCountForCollaborator(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_Filter_Count_ForCollaborator(branchId, sourceTypeId, statusId, fromDate, toDate));
        }
        public static List<ChannelInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecords)
        {
            var reader = DataProvider.Instance().Channels_Search(keyword, pageIndex, pageSize);
            var retVal = ObjectHelper.FillCollection<ChannelInfo>(reader, false);
            totalRecords = 0;
            try
            {

                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }

            }
            catch (Exception)
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
        public static List<ChannelInfo> FilterCountForHandover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_Filter_Count_ForHandover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, type, statusMapId, statusCareId));
        }
        
        //Thêm ngày 28/03/2016
        public static List<ChannelInfo> FilterCountForHandoverMOL(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_Filter_Count_ForHandoverMOL(branchId, typeIds, levelIds, importIds, statusIds, containerIds, type, statusMapId, statusCareId, fromRegisterDate, toRegisterDate));
        }

        public static List<ChannelInfo> FilterForCampain(int branchId, int sourceTypeId = 0)
        {
            return ObjectHelper.FillCollection<ChannelInfo>(DataProvider.Instance().Channels_Filter_ForCampain(branchId, sourceTypeId));
        }

        //Get Channel
        private static List<ChannelInfo> _Channels = new List<ChannelInfo>();
        public static int GetChannelId(string value, int typeId, int defaultId)
        {
            if (string.IsNullOrEmpty(value)) return defaultId;
            if (_Channels.IsNullOrEmpty()) _Channels = ChannelRepository.GetAll();
            var entity = _Channels
                .Where(c => c.SourceTypeId == typeId)
                .FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new ChannelInfo
                {
                    Name = value,
                    CreatedBy = 1,
                    ChangedBy = 1,
                    SourceTypeId = typeId,
                    Description = string.Empty,
                    CreatedDate = DateTime.Now,
                    ChangedDate = DateTime.Now,
                };
                var index = entity.Name.IndexOf(".");
                if (index > -1) entity.Code = entity.Name.Substring(0, index);

                entity.ChannelId = ChannelRepository.Create(entity);
                _Channels.Add(entity);
            }
            return entity.ChannelId;
        }
    }
}

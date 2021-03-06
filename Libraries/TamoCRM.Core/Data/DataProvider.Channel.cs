using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Channels_Insert( string name, string code, string description, int sourceTypeId, int createdBy);
        public abstract void Channels_Update(int channelId, string name, string code, string description, int sourceTypeId, int changedBy);
        public abstract void Channels_Delete(int channelId);
        public abstract IDataReader Channels_GetInfo(int channelId);
        public abstract IDataReader Channels_GetInfo(string name);
        public abstract IDataReader Channels_GetAll();
        public abstract IDataReader Channels_GetAvailables(int branchId);
        public abstract IDataReader Channels_Search(string keyword, int pageIndex, int pageSize);
        public abstract IDataReader Channels_Filter_Count_ForHandover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId);   
        public abstract IDataReader Channels_Filter_Count_ForHandoverMOL(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId, DateTime? fromRegisterDate, DateTime? toRegisterDate);
        public abstract IDataReader Channels_Filter_Count_ForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds);
        public abstract IDataReader Channels_Filter_Count_ForCollaborator(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate);
        public abstract int Channels_Filter_ContactCount_ForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds, string channelIds, string schoolIds);
        public abstract IDataReader Channels_Filter_ForCampain(int branchId, int sourceTypeId);
    }
}


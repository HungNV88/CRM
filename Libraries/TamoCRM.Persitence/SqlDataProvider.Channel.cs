using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Channels_Insert(string name, string code, string description, int sourceTypeId, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Channels_Insert"), name, code, description, sourceTypeId, createdBy);
        }

        public override void Channels_Update(int channelId, string name, string code, string description, int sourceTypeId, int changedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Channels_Update"), channelId, name, code, description, sourceTypeId, changedBy);
        }

        public override void Channels_Delete(int channelId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Channels_Delete"), channelId);
        }

        public override IDataReader Channels_GetInfo(int channelId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_GetInfo"), channelId);
        }

        public override IDataReader Channels_GetInfo(string name)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_GetInfoByName"), name);
        }

        public override IDataReader Channels_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_GetAll"));
        }

        public override IDataReader Channels_GetAvailables(int branchId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_GetAvailables"), branchId);
        }

        public override IDataReader Channels_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Search"), keyword, pageIndex, pageSize);
        }

        public override IDataReader Channels_Filter_Count_ForHandover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId)
        {
            switch (type)
            {
                case EmployeeType.All:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_All"), branchId, typeIds, levelIds, importIds, statusIds, containerIds);
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_Collaborator"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, statusMapId, statusCareId);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_Consultant"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, statusMapId, statusCareId);
            }
            return null;
        }

        //Thêm ngày 28/03/2016
        public override IDataReader Channels_Filter_Count_ForHandoverMOL(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, EmployeeType type, int statusMapId, int statusCareId, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            switch (type)
            {
                case EmployeeType.All:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_All_MOL"), branchId, typeIds, levelIds, importIds, statusIds, containerIds);
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_Collaborator_MOL"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, statusMapId, statusCareId, fromRegisterDate, toRegisterDate);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForHandover_Consultant_MOL_2"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, statusMapId, statusCareId, fromRegisterDate, toRegisterDate);
            }
            return null;
        }

        public override IDataReader Channels_Filter_Count_ForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForDistributor"), branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds);
        }

        public override IDataReader Channels_Filter_Count_ForCollaborator(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_Count_ForCollaborator"), branchId, sourceTypeId, statusId, fromDate, toDate);
        }

        public override int Channels_Filter_ContactCount_ForDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds, string channelIds, string schoolIds)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Channels_Filter_ContactCount_ForDistributor"), branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds);
        }

        public override IDataReader Channels_Filter_ForCampain(int branchId, int sourceTypeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Channels_Filter_ForCampain"), branchId, sourceTypeId);
        }
    }
}


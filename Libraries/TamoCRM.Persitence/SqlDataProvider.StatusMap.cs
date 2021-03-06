using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader StatusMap_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusMap_GetAll"));
        }

        public override IDataReader StatusMap_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusMap_GetInfo"), id);
        }

        public override IDataReader StatusMap_GetAll_ByLevelId(int levelId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("StatusMap_GetAll_ByLevelId"), levelId);
        }
    }
}


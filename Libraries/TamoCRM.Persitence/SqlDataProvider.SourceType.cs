using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int SourceTypes_Insert(string code, string name, string description, bool isCheckDuplicate, bool isCheckUpdate, bool isShowHotLine, bool isShowConsultant, int createdBy, DateTime createdDate, int changedBy, DateTime changedDate)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("SourceTypes_Insert"), code, name, description, isCheckDuplicate, isCheckUpdate, isShowHotLine, isShowConsultant, createdBy, createdDate, changedBy, changedDate);
        }

        public override void SourceTypes_Update(int sourceTypeId, string code, string name, string description, bool isCheckDuplicate, bool isShowHotLine, bool isShowConsultant, bool isCheckUpdate, int createdBy, DateTime createdDate, int changedBy, DateTime changedDate)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SourceTypes_Update"), sourceTypeId, code, name, description, isCheckDuplicate, isCheckUpdate, isShowHotLine, isShowConsultant, createdBy, createdDate, changedBy, changedDate);
        }

        public override void SourceTypes_Delete(int sourceTypeId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SourceTypes_Delete"), sourceTypeId);
        }

        public override IDataReader SourceTypes_GetInfo(int sourceTypeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SourceTypes_GetInfo"), sourceTypeId);
        }

        public override IDataReader SourceTypes_GetInfo(string code)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, "SELECT * FROM SourceTypes WHERE Code='" + code + "'");
        }

        public override IDataReader SourceTypes_GetAllTest()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SourceTypes_GetAllTest"));
        }
        public override IDataReader SourceTypes_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SourceTypes_GetAll"));
        }

        public override IDataReader SourceTypes_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("SourceTypes_Search"), keyword, pageIndex, pageSize);
        }
    }
}


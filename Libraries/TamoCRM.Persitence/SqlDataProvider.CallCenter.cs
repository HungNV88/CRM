using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;
using System;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        
        public override IDataReader CallCenter_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_GetAll"));
        }

        public override IDataReader CallCenter_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_Search"), keyword, pageIndex, pageSize);
        }

        public override int CallCenter_Insert(string name, string phoneNumber, string useFor, string endPoint, string token, int port, int createBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CallCenter_Insert"), name, phoneNumber, useFor, endPoint, token, port, createBy);
        }

        public override IDataReader CallCenter_Extension_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_Extension_GetAll"));
        }

        public override IDataReader CallCenter_Extension_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallCenter_Extension_Search"), keyword, pageIndex, pageSize);
        }

    }
}

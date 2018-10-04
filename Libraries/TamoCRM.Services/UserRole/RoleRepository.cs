using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.UserRole;

namespace TamoCRM.Services.UserRole
{
   public class RoleRepository
    {
       public static List<RoleInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
       {
           var reader = DataProvider.Instance().Role_Search(keyword, pageIndex, pageSize);
           var lst = ObjectHelper.FillCollection<RoleInfo>(reader,false);
           totalRecord = 0;
           try
           {

               reader.NextResult();
               //Get the total no of records from the second result
               if (reader.Read())
               {
                   totalRecord = Convert.ToInt32(reader[0]);
               }
           }
           catch (Exception ex)
           {
               
           }
           finally
           {
               ObjectHelper.CloseDataReader(reader,true);
           }
               return lst;
       }

       public static RoleInfo GetInfo(int id)
       {
           return ObjectHelper.FillObject<RoleInfo>(DataProvider.Instance().Role_GetInfo(id));
       }

       public static void Update(RoleInfo info)
       {
           DataProvider.Instance().Role_Update(info.RoleID, info.Name, info.Description, info.ChangedBy);
       }

       public static void Delete(int id, int deletedby)
       {
        DataProvider.Instance().Role_Delete(id,deletedby);   
       }

       public static void Insert(RoleInfo info)
       {
           info.RoleID= DataProvider.Instance().Role_Insert(info.Name, info.Description, info.CreatedBy);
       }

       public static IEnumerable<RoleInfo> GetAll()
       {
           return ObjectHelper.FillCollection<RoleInfo>(DataProvider.Instance().Role_GetAll());
       }

       public static List<RoleInfo> GetRoleOfUser(int userid)
       {
           return ObjectHelper.FillCollection<RoleInfo>(DataProvider.Instance().Role_GetRoleOfUser(userid));
       }

       //public static void ClearFunction(int roleid)
       //{
       //    DataProvider.Instance().Role_ClearFunction(roleid);
       //}

       //public static void SetFunction(int roleid, List<int> functionid,int createdby)
       //{
       //    foreach (var i in functionid)
       //    {
       //        DataProvider.Instance().Role_SetFunction(roleid,i,createdby);
       //    }
       //} 
    }
}

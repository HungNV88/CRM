using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Groups;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.Catalogs;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Groups;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Users
{
    public class UserController : CustomApiController //ApiController
    {
        // GET api/<controller>
        public IEnumerable<UserInfo> Get()
        {
            return UserRepository.GetAll();
        }

        public IEnumerable<UserInfo> GetByGroupId(int groupId)
        {
            var user = UserContext.GetCurrentUser();
            var users = StoreData.ListUser.IsNullOrEmpty() ? UserRepository.GetAll() : StoreData.ListUser;
            switch (user.GroupConsultantType)
            {
                case (int)GroupConsultantType.ManagerConsultant:
                case (int)GroupConsultantType.LeaderConsultant:
                    return groupId == 0 ? users : users.Where(c => c.GroupId == groupId).ToList();
                case (int)GroupConsultantType.Consultant:
                    return groupId == 0 ? users : users.Where(c => c.UserID == user.UserID).ToList();
            }

            if (!user.IsConsultant)
            {
                return groupId == 0 ? users : users.Where(c => c.GroupId == groupId).ToList();
            }
            return null;
        }

        public List<UserInfo> GetByGroupIds(string groupIds)
        {

            return null;
        }

        public IEnumerable<UserInfo> GetByBranchId(int branchId)
        {
            var groups = StoreData.ListGroup.IsNullOrEmpty() ? GroupRepository.GetAll() : StoreData.ListGroup;
            groups = branchId == 0 ? groups : groups.Where(c => c.BranchId == branchId).ToList();

            var user = UserContext.GetCurrentUser();
            var users = StoreData.ListUser.IsNullOrEmpty() ? UserRepository.GetAll() : StoreData.ListUser;
            switch (user.GroupConsultantType)
            {
                case (int)GroupConsultantType.ManagerConsultant:
                case (int)GroupConsultantType.LeaderConsultant:
                    return groups == null ? users : users.Where(c => groups.Any(p => p.GroupId == c.GroupId)).ToList();
                case (int)GroupConsultantType.Consultant:
                    return groups == null ? users : users.Where(c => c.UserID == user.UserID).ToList();
            }
            return null;
        }

        // GET api/<controller>
        public UserListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new UserListModel
                            {
                                Rows = UserRepository.Search(string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords / rows) + 1,
                                Records = rows
                            };
            return model;
        }

        // GET api/<controller>
        [HttpGet]
        public UserListModel Find(string user, int page, int rows)
        {
            if (string.IsNullOrEmpty(user)) user = string.Empty;
            int totalRecords;
            var model = new UserListModel
            {
                Rows = UserRepository.Search(user, page, rows, out totalRecords),
                Page = page,
                Total = (totalRecords / rows) + 1,
                Records = rows
            };
            return model;
        }

        // GET api/<controller>/5
        public UserInfo Get(int id)
        {
            return UserRepository.GetInfo(id);
        }

        [HttpGet]
        public List<UserDraftNormsInfo> GetDraft(int employeeTypeId)
        {
            return InitDraft(employeeTypeId);
        }

        [HttpGet]
        public List<UserDraftNormsInfo> GetDraftByDate(int employeeTypeId, string hanoverDate)
        {
            hanoverDate = hanoverDate.Replace("!", "/");
            DateTime dt = DateTime.ParseExact(hanoverDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return InitDraftByDate(employeeTypeId, dt);
        }

        [HttpGet]
        public List<UserDraftNormsInfo> DiviceDraft(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int diviceType, int employeeTypeId, int statusCareId, int statusMapId)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var userImportId = UserContext.GetCurrentUser().UserID;
            var listContact = ContactRepository.FilterHandover(9999999, branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId) ?? new List<ContactInfo>();
            return DiviceContactDraft(branchId, employeeTypeId, diviceType, listContact);
        }

        private List<UserDraftNormsInfo> InitDraft(int employeeTypeId)
        {
            var branchId = UserContext.GetDefaultBranch();

            // Get list draft
            var listContacts = ContactRepository.FilterDraft(branchId) ?? new List<ContactInfo>();

            // Get list ConsultantNomrs
            var listConsultantNomrs = GetNorms(0, employeeTypeId);

            var list = new List<UserDraftNormsInfo>();
            foreach (var item in listConsultantNomrs)
            {
                var entity = new UserDraftNormsInfo
                {
                    DraftNew = 0,
                    Id = item.UserID,
                    Norms = item.Norms,
                    FullName = item.FullName,
                    GroupName = item.GroupName,
                    Draft = employeeTypeId == (int)EmployeeType.Collaborator
                        ? listContacts.Count(c => c.UserCollaboratorId == item.UserID)
                        : listContacts.Count(c => c.UserConsultantId == item.UserID),
                    CountContactDay = employeeTypeId == (int)EmployeeType.Collaborator
                        ? item.CountContactCollaboratorDay
                        : item.CountContactDay
                };
                entity.Amount = entity.Draft + entity.DraftNew;
                list.Add(entity);
            }
            return list.Where(c => c.Norms > 0).OrderBy(c => c.GroupName).ToList();
        }

        private List<UserDraftNormsInfo> InitDraftByDate(int employeeTypeId, DateTime hanoverDate)
        {
            var branchId = UserContext.GetDefaultBranch();

            // Get list draft
            var listContacts = ContactRepository.FilterDraft(branchId) ?? new List<ContactInfo>();

            // Get list ConsultantNomrs
            var listConsultantNomrs = GetNormsByDate(0, employeeTypeId, hanoverDate);

            var list = new List<UserDraftNormsInfo>();
            foreach (var item in listConsultantNomrs)
            {
                var entity = new UserDraftNormsInfo
                {
                    DraftNew = 0,
                    Id = item.UserID,
                    Norms = item.Norms,
                    FullName = item.FullName,
                    GroupName = item.GroupName,
                    Draft = employeeTypeId == (int)EmployeeType.Collaborator
                        ? listContacts.Count(c => c.UserCollaboratorId == item.UserID)
                        : listContacts.Count(c => c.UserConsultantId == item.UserID),
                    //CountContactDay = item.CountContactDay
                    CountContactDay = employeeTypeId == (int)EmployeeType.Collaborator
                        ? item.CountContactCollaboratorDay
                        : item.CountContactDay
                };
                entity.Amount = entity.Draft + entity.DraftNew;
                list.Add(entity);
            }
            return list.Where(c => c.Norms > 0).OrderBy(c => c.GroupName).ToList();
        }

        public List<UserDraftNormsInfo> DiviceContactDraft(int branchId, int employeeTypeId, int diviceType, List<ContactInfo> listContact)
        {
            var listUserDrafts = InitDraft(employeeTypeId);

            var total = listContact.Count;
            var totalUser = listUserDrafts.Count;
            var totalNorms = listUserDrafts.Sum(c => c.Norms);
            foreach (var item in listUserDrafts.OrderBy(c => c.Norms))
            {
                switch (diviceType)
                {
                    case (int)DiviceType.SameChannel:
                        {
                            if (total > 0)
                            {
                                var divice = (int)Math.Ceiling((decimal)total / totalUser);
                                item.DraftNew = item.Norms - item.Draft < divice ? item.Norms - item.Draft : divice;
                            }
                            item.Amount = item.Draft + item.DraftNew;
                            total = total - item.DraftNew;
                            totalUser = totalUser - 1;
                        }
                        break;
                    //case (int)DiviceType.Same:
                    //    {
                    //        if (total > 0)
                    //        {
                    //            var divice = (int)Math.Ceiling((decimal)total / totalUser);
                    //            item.DraftNew = item.Norms - item.Draft < divice ? item.Norms - item.Draft : divice;
                    //        }
                    //        item.Amount = item.Draft + item.DraftNew;
                    //        total = total - item.DraftNew;
                    //        totalUser = totalUser - 1;
                    //    }
                    //    break;
                    //case (int)DiviceType.Ratio:
                    //    {
                    //        if (total > 0)
                    //        {
                    //            var divice = (int)Math.Ceiling(((decimal)total * item.Norms)/ totalNorms);
                    //            item.DraftNew = item.Norms - item.Draft < divice ? item.Norms - item.Draft : divice;
                    //        }
                    //        item.Amount = item.Draft + item.DraftNew;
                    //        total = total - item.DraftNew;
                    //        totalUser = totalUser - 1;
                    //        totalNorms = totalNorms - item.Norms;
                    //    }
                    //    break;
                    //case (int)DiviceType.Enough:
                    //    {
                    //        if (total > 0)
                    //        {
                    //            var divice = total;
                    //            item.DraftNew = item.Norms - item.Draft < divice ? item.Norms - item.Draft : divice;
                    //        }
                    //        item.Amount = item.Draft + item.DraftNew;
                    //        total = total - item.DraftNew;
                    //        totalUser = totalUser - 1;
                    //    }
                    //    break;
                }
            }
            return listUserDrafts;
        }

        // GET api/<controller>
        public List<UserInfo> GetNormsByBranchId(int branchId, int groupId, int employeeTypeId)
        {
            var userGroups = CatalogRepository.UserGroupGetAll() ?? new List<UserGroupInfo>();
            var userBranchs = CatalogRepository.UserBranchGetAll() ?? new List<UserBranchInfo>();

            var groups = new List<GroupInfo> { new GroupInfo { GroupId = groupId } };
            if (groupId == 0) groups = StoreData.ListGroup.Where(c => c.EmployeeTypeId == employeeTypeId).ToList();

            var userInBranchs = branchId == 0
                ? userBranchs.Select(c => c.UserId).Distinct().ToList()
                : userBranchs.Where(c => c.BranchId == branchId).Select(c => c.UserId).Distinct().ToList();
            var userInGroups = userGroups.Where(c => groups.Any(p => p.GroupId == c.GroupId)).Select(c => c.UserId).Distinct().ToList();
            StoreData.ReloadData<UserInfo>();
            var users = StoreData.ListUser;
            //var users = UserRepository.GetAll();
            users = users.Where(c => userInGroups.Any(p => p == c.UserID))
                .Where(c => groups.Any(p => p.GroupId == c.GroupId))
                .Where(c => userInBranchs.Any(p => p == c.UserID))
                .Where(c => c.Status == (int)StatusUserType.Nomal)
                .ToList();

            var result = new List<UserInfo>();
            foreach (var entity in users)
            {
                entity.Norms = employeeTypeId == (int)EmployeeType.Collaborator
                    ? entity.NormsCollaborator
                    : entity.NormsConsultant;
                if (result.Any(c => c.UserID == entity.UserID)) continue;
                result.Add(entity);
            }
            return result;
        }

        public List<UserInfo> GetNormsByDateByBranchId(int branchId, int groupId, int employeeTypeId, DateTime hanoverDate)
        {
            var userGroups = CatalogRepository.UserGroupGetAll() ?? new List<UserGroupInfo>();
            var userBranchs = CatalogRepository.UserBranchGetAll() ?? new List<UserBranchInfo>();

            var groups = new List<GroupInfo> { new GroupInfo { GroupId = groupId } };
            if (groupId == 0) groups = StoreData.ListGroup.Where(c => c.EmployeeTypeId == employeeTypeId).ToList();

            var userInBranchs = branchId == 0
                ? userBranchs.Select(c => c.UserId).Distinct().ToList()
                : userBranchs.Where(c => c.BranchId == branchId).Select(c => c.UserId).Distinct().ToList();
            var userInGroups = userGroups.Where(c => groups.Any(p => p.GroupId == c.GroupId)).Select(c => c.UserId).Distinct().ToList();
            var users = UserRepository.GetAll(hanoverDate);
            users = users.Where(c => userInGroups.Any(p => p == c.UserID))
                .Where(c => groups.Any(p => p.GroupId == c.GroupId))
                .Where(c => userInBranchs.Any(p => p == c.UserID))
                .Where(c => c.Status == (int)StatusUserType.Nomal)
                .ToList();

            var result = new List<UserInfo>();
            foreach (var entity in users)
            {
                entity.Norms = employeeTypeId == (int)EmployeeType.Collaborator
                    ? entity.NormsCollaborator
                    : entity.NormsConsultant;
                if (result.Any(c => c.UserID == entity.UserID)) continue;
                result.Add(entity);
            }
            return result;
        }

        public List<UserInfo> GetNorms(int groupId, int employeeTypeId)
        {
            var branchId = UserContext.GetDefaultBranch();
            return GetNormsByBranchId(branchId, groupId, employeeTypeId);
        }

        public List<UserInfo> GetNormsByDate(int groupId, int employeeTypeId, DateTime hanoverDate)
        {
            var branchId = UserContext.GetDefaultBranch();
            return GetNormsByDateByBranchId(branchId, groupId, employeeTypeId, hanoverDate);
        }

        // POST api/<controller>
        [HttpPost]
        public string EditNormsCollaborator(FormDataCollection form)
        {
            var retVal = string.Empty;
            var id = form.Get("UserID").ToInt32();
            var entity = UserRepository.GetInfo(id);
            if (entity != null)
            {
                entity.NormsCollaborator = form.Get("Norms").ToInt32();
                entity.ChangedBy = UserContext.GetCurrentUser().UserID;
                UserRepository.UpdateNorms(entity);
                StoreData.ReloadData<UserInfo>();
            }
            return retVal;
        }

        // POST api/<controller>
        [HttpPost]
        public string EditNormsConsultant(FormDataCollection form)
        {
            var retVal = string.Empty;
            var id = form.Get("UserID").ToInt32();
            var entity = UserRepository.GetInfo(id);
            if (entity != null)
            {
                entity.NormsConsultant = form.Get("Norms").ToInt32();
                entity.ChangedBy = UserContext.GetCurrentUser().UserID;
                UserRepository.UpdateNorms(entity);
                //StoreData.ReloadData<UserInfo>();
            }
            return retVal;
        }

        [HttpPost]
        public string LockUser(FormDataCollection form)
        {
            try
            {
                var id = form.Get("id").ToInt32();
                UserRepository.LockUser(id);
                StoreData.ReloadData<UserInfo>();
                return "Khóa tài khoản thành công";
            }
            catch
            {
                return "Hệ thống bị lỗi, khóa tài khoản không thành công.";
            }
        }

        [HttpPost]
        public string OpenUser(FormDataCollection form)
        {
            try
            {
                var id = form.Get("id").ToInt32();
                UserRepository.OpenUser(id);
                StoreData.ReloadData<UserInfo>();
                return "Mở tài khoản thành công";
            }
            catch
            {
                return "Hệ thống bị lỗi, mở tài khoản không thành công.";
            }
        }

        [HttpPost]
        public bool SetPassWord(FormDataCollection form)
        {
            //userName       

            bool check = false;
            string userName = form.Get("userName").ToString();
            List<UserInfo> list = StoreData.ListUser.IsNullOrEmpty() ? UserRepository.GetAll() : StoreData.ListUser;
            check = list.Any(x => x.UserName == userName);

            if (check)
            {
                UserRepository.SetPassWord(userName);
            }
            
            StoreData.ReloadData<UserInfo>();
            return check;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.Groups;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.Branches;
using TamoCRM.Services.Groups;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        //
        // GET: /Admin/Branch/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListPaging(string searchKey, int? page)
        {
            const int pagesize = 30;
            var pi = page == null ? 1 : page.Value;
            int totalrecord;
            var lstuser = UserRepository.Search(string.IsNullOrEmpty(searchKey) ? "" : searchKey, pi, pagesize, out totalrecord);
            var model = new UserListModel
            {
                Page = pi,
                Records = pagesize,
                Rows = lstuser,
                Total = totalrecord
            };
            return View(model);
        }

        public ActionResult Create()
        {
            // Groups
            var groups = StoreData.ListGroup ?? GroupRepository.GetAll();
            var model = new UserModel
                        {
                            UserInfo = new UserInfo(),
                            PostedRole = new PostedRole(),
                            PostedBranch = new PostedBranch(),
                            AllRole = RoleRepository.GetAll(),
                            AllBranch = BranchRepository.GetAll(),
                            SelectedRole = new Collection<RoleInfo>(),
                            SelectedBranch = new Collection<BranchInfo>(),

                            PostedGroupConsultant = new PostedGroup(),
                            PostedGroupConsultantType = new PostedGroupType(),
                            SelectedGroupConsultants = new Collection<GroupInfo>(),
                            SelectedGroupConsultantTypes = new Collection<GroupTypeInfo>(),
                            AllGroupConsultants = groups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Consultant),
                            AllGroupConsultantTypes = StoreData.ListGroupType.Where(c => c.EmployeeTypeId == (int)EmployeeType.Consultant),

                            PostedGroupCollaborator = new PostedGroup(),
                            PostedGroupCollaboratorType = new PostedGroupType(),
                            SelectedGroupCollaborators = new Collection<GroupInfo>(),
                            SelectedGroupCollaboratorTypes = new Collection<GroupTypeInfo>(),
                            AllGroupCollaborators = groups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Collaborator),
                            AllGroupCollaboratorTypes = StoreData.ListGroupType.Where(c => c.EmployeeTypeId == (int)EmployeeType.Collaborator),
                        };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            try
            {
                var currentUserId = UserContext.GetCurrentUser().UserID;
                //var info = UserRepository.GetInfo(model.UserInfo.UserName);
                var info = StoreData.ListUser.FirstOrDefault(c => c.UserName == model.UserInfo.UserName);
                if (model.UserInfo.StationId != null && model.UserInfo.StationId != string.Empty)
                {
                    var ip_phone = UserRepository.GetIPPhone(model.UserInfo.StationId);
                    if (ip_phone != null && ip_phone != 0)
                    {
                        ViewBag.Message = "Số ip phone đã có người sử dụng. Vui lòng nhập số khác ^_^";
                        return Create();
                    }
                }
                
                if (info == null)
                {
                    var user = new UserInfo
                    {
                        CreatedBy = currentUserId,
                        ChangedBy = currentUserId,
                        Email = model.UserInfo.Email,
                        Mobile = model.UserInfo.Mobile,
                        UserName = model.UserInfo.UserName,
                        FullName = model.UserInfo.FullName,
                        Password = model.UserInfo.Password,
                        StationId = model.UserInfo.StationId,
                        Description = model.UserInfo.Description,
                        IsCollector = model.UserInfo.IsCollector,
                        IsSuperAdmin = model.UserInfo.IsSuperAdmin,
                        IsConsultant = model.UserInfo.IsConsultant,
                        IsCollaborator = model.UserInfo.IsCollaborator,
                    };
                    var groupIds = new List<int>();

                    // IsCollaborator
                    if (user.IsCollaborator)
                    {
                        if (model.PostedGroupCollaboratorType != null && model.PostedGroupCollaboratorType.Id.Any())
                            user.GroupCollaboratorType = model.PostedGroupCollaboratorType.Id.First();
                        if (model.PostedGroupCollaborator != null) groupIds.AddRange(model.PostedGroupCollaborator.Id);
                        user.NormsCollaborator = model.UserInfo.NormsCollaborator;
                    }

                    // IsConsultant
                    if (user.IsConsultant)
                    {
                        if (model.PostedGroupConsultantType != null && model.PostedGroupConsultantType.Id.Any())
                            user.GroupConsultantType = model.PostedGroupConsultantType.Id.First();
                        if (model.PostedGroupConsultant != null) groupIds.AddRange(model.PostedGroupConsultant.Id);
                        user.NormsConsultant = model.UserInfo.NormsConsultant;
                    }

                    // RoleIds
                    var roleIds = model.PostedRole.Id.ToList();

                    // BranchIds
                    var branchIds = model.PostedBranch.Id.ToList();

                    // Inser User
                    UserRepository.Insert(string.Join(",", roleIds), string.Join(",", groupIds), string.Join(",", branchIds), user);
                    
                    StoreData.ReloadData<UserInfo>();
                    ViewBag.Message = "Tạo mới người dùng thành công";
                    return RedirectToAction("Edit", new { id = user.UserID });
                }
                ViewBag.Message = "Tên đăng nhập đã tồn tại, vui lòng chọn tên đăng nhập khác";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Cập nhật người dùng không thành công. - " + ex.Message.ToString();
            }
            return Create();
        }

        public ActionResult Edit(int id)
        {
            var userInfo = UserRepository.GetInfo(id);
            if (userInfo != null)
            {
                // Collaborator
                var selectedGroupCollaboratorType = new List<GroupTypeInfo>();
                var groupCollaboratorTypeInfo = StoreData.ListGroupType.FirstOrDefault(c => c.Id == userInfo.GroupCollaboratorType);
                if (groupCollaboratorTypeInfo != null)
                {
                    selectedGroupCollaboratorType.Add(new GroupTypeInfo
                                                      {
                                                          Id = userInfo.GroupCollaboratorType,
                                                          Name = groupCollaboratorTypeInfo.Name,
                                                      });
                }

                // Consultant
                var selectedGroupConsultantType = new List<GroupTypeInfo>();
                var groupConsultantTypeInfo = StoreData.ListGroupType.FirstOrDefault(c => c.Id == userInfo.GroupConsultantType);
                if (groupConsultantTypeInfo != null)
                {
                    selectedGroupConsultantType.Add(new GroupTypeInfo
                                              {
                                                  Id = userInfo.GroupConsultantType,
                                                  Name = groupConsultantTypeInfo.Name,
                                              });
                }

                // Groups
                var groups = StoreData.ListGroup ?? GroupRepository.GetAll();
                var selectedGroups = GroupRepository.GetByUser(id) ?? new List<GroupInfo>();
                var model = new UserModel
                                {
                                    UserInfo = userInfo,
                                    AllRole = RoleRepository.GetAll(),
                                    SelectedRole = RoleRepository.GetRoleOfUser(id),
                                    SelectedBranch = UserRepository.GetBranchOfUser(id),
                                    AllBranch = StoreData.ListBranch ?? BranchRepository.GetAll(),

                                    SelectedGroupCollaboratorTypes = selectedGroupCollaboratorType,
                                    AllGroupCollaborators = groups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Collaborator),
                                    SelectedGroupCollaborators = selectedGroups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Collaborator),
                                    AllGroupCollaboratorTypes = StoreData.ListGroupType.Where(c => c.EmployeeTypeId == (int)EmployeeType.Collaborator),

                                    SelectedGroupConsultantTypes = selectedGroupConsultantType,
                                    AllGroupConsultants = groups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Consultant),
                                    SelectedGroupConsultants = selectedGroups.Where(c => c.EmployeeTypeId == (int)EmployeeType.Consultant),
                                    AllGroupConsultantTypes = StoreData.ListGroupType.Where(c => c.EmployeeTypeId == (int)EmployeeType.Consultant),
                                };
                if (model.SelectedGroupConsultants != null && model.SelectedGroupConsultants.Any())
                    model.UserInfo.IsConsultant = true;
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(int id, UserModel model)
        {
            try
            {
                var currentUserId = UserContext.GetCurrentUser().UserID;
                //var user = UserRepository.GetInfo(id);
                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == id);
                if (user != null)
                {
                    user.ChangedBy = currentUserId;
                    user.Email = model.UserInfo.Email;
                    user.Mobile = model.UserInfo.Mobile;
                    user.FullName = model.UserInfo.FullName;
                    user.StationId = model.UserInfo.StationId;
                    user.Description = model.UserInfo.Description;
                    user.IsCollector = model.UserInfo.IsCollector;
                    user.IsConsultant = model.UserInfo.IsConsultant;
                    user.IsCollaborator = model.UserInfo.IsCollaborator;
                    user.NormsConsultant = model.UserInfo.NormsConsultant;
                    user.NormsCollaborator = model.UserInfo.NormsCollaborator;

                    // IsCollaborator
                    var groupIds = new List<int>();
                    if (user.IsCollaborator)
                    {
                        if (model.PostedGroupCollaboratorType != null && model.PostedGroupCollaboratorType.Id.Any())
                            user.GroupCollaboratorType = model.PostedGroupCollaboratorType.Id.First();
                        if (model.PostedGroupCollaborator != null) groupIds.AddRange(model.PostedGroupCollaborator.Id);
                        user.NormsCollaborator = model.UserInfo.NormsCollaborator;
                    }

                    // IsConsultant
                    if (user.IsConsultant)
                    {
                        if (model.PostedGroupConsultantType != null && model.PostedGroupConsultantType.Id.Any())
                            user.GroupConsultantType = model.PostedGroupConsultantType.Id.First();
                        if (model.PostedGroupConsultant != null) groupIds.AddRange(model.PostedGroupConsultant.Id);
                        user.NormsConsultant = model.UserInfo.NormsConsultant;
                    }

                    // RoleIds
                    var roleIds = model.PostedRole.Id.ToList();

                    // BranchIds
                    var branchIds = model.PostedBranch.Id.ToList();

                    // Update User
                    UserRepository.Update(string.Join(",", roleIds), string.Join(",", groupIds), string.Join(",", branchIds), user);
                }
                var user2 = StoreData.ListUser.FirstOrDefault(c => c.UserID == id);
                StoreData.ListUser.Remove(user2);
                StoreData.ListUser.Add(user);
                ViewBag.Message = "Cập nhật người dùng thành công";
                return Edit(id);
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Cập nhật người dùng không thành công: " + ex.Message.ToString();
                return Edit(id);
            }
        }

        public ActionResult ChangePassword()
        {
            var userInfo = UserContext.GetCurrentUser();
            if (userInfo != null)
            {
                var model = new ChangePasswordModel { UserInfo = userInfo };
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var currentUser = UserContext.GetCurrentUser();
            if (UserRepository.ValidateUser(model.UserInfo.UserName, SecurityHelper.GetMD5Hash(model.OldPassword)))
            {
                UserRepository.ChangePassword(currentUser.UserID, model.Password);
                ViewBag.Message = "Đổi mật khẩu mới thành công";
                return ChangePassword();
            }
            ViewBag.Message = "Đổi mật khẩu mới không thành công, mật khẩu cũ không đúng";
            return ChangePassword();
        }

        public ActionResult NormsCollaborator()
        {
            return View();
        }

        public ActionResult NormsConsultant()
        {
            return View();
        }

        public ActionResult EmailSetting()
        {
            var userInfo = UserContext.GetCurrentUser();
            if (userInfo != null)
            {
                userInfo = UserRepository.GetInfo(userInfo.UserID) ?? new UserInfo();
                var model = new EmailSettingModel
                            {
                                UserID = userInfo.UserID,
                                FullName = userInfo.FullName,
                                UserName = userInfo.UserName,
                                EmailSend = userInfo.EmailSend,
                                PasswordSend = userInfo.PasswordSend,
                                SignEmailSend = userInfo.SignEmailSend,
                                ConfirmPassword = userInfo.ConfirmPassword,
                            };
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EmailSetting(EmailSettingModel model)
        {
            try
            {
                UserRepository.SetEmailSend(model.UserID, model.EmailSend, model.PasswordSend, model.SignEmailSend);
                ViewBag.Message = "Thiết lập email thành công";
                StoreData.ReloadData<UserInfo>();
            }
            catch
            {
                ViewBag.Message = "Thiết lập email không thành công, vui lòng thử lại";
            }
            return RedirectToAction("EmailSetting");
        }

        public ActionResult PhoneSetting()
        {
            var userInfo = UserContext.GetCurrentUser();
            if(userInfo != null)
            {
                userInfo = UserRepository.GetInfo(userInfo.UserID) ?? new UserInfo();
                var model = new PhoneSettingModel
                                {
                                    UserID = userInfo.UserID,
                                    Phone = userInfo.Mobile,
                                    Email = userInfo.Email
                                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult PhoneSetting(PhoneSettingModel model)
        {
            try
            {

                UserRepository.SetPhone(model.UserID, model.Phone, model.Email);
                ViewBag.Message = "Thiết lập điện thoại thành công";
                StoreData.ReloadData<UserInfo>();
            }
            catch
            {
                ViewBag.Message = "Thiết lập điện thoại không thành công, vui lòng thử lại";
                StoreData.ReloadData<UserInfo>();
            }
            return RedirectToAction("PhoneSetting");
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        
    }
}
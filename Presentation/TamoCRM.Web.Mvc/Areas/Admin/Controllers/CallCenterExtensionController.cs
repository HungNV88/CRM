using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter;
using TamoCRM.Domain.CallCenter;
using TamoCRM.Services.CallCenter;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class CallCenterExtensionController : AdminController
    {
        //
        // GET: /Admin/CallCenter/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CallCenterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CallCenterModel model)
        {
            try
            {
                var currentUserId = UserContext.GetCurrentUser().UserID;
                ////var info = UserRepository.GetInfo(model.UserInfo.UserName);
                //var info = StoreData.ListUser.FirstOrDefault(c => c.UserName == model.UserInfo.UserName);
                //if (model.UserInfo.StationId != null && model.UserInfo.StationId != string.Empty)
                //{
                //    var ip_phone = UserRepository.GetIPPhone(model.UserInfo.StationId);
                //    if (ip_phone != null && ip_phone != 0)
                //    {
                //        ViewBag.Message = "Số ip phone đã có người sử dụng. Vui lòng nhập số khác ^_^";
                //        return Create();
                //    }
                //}

                var callCenter = new CallCenterInfo
                {
                    CreatedBy = currentUserId,
                    ChangedBy = currentUserId,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    UseFor = model.UseFor,
                    EndPoint = model.EndPoint,
                    Token = model.Token,
                    Port = model.Port
                };


                // Inser User
                //  UserRepository.Insert(string.Join(",", roleIds), string.Join(",", groupIds), string.Join(",", branchIds), user);
                CallCenterRepository.Insert(callCenter);

                ViewBag.Message = "Tạo mới call center thành công";
                //return RedirectToAction("Edit", new { id = user.UserID });
                //return Index();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo mới call center không thành công. - " + ex.Message.ToString();
            }
            return Create();
        }

    }
}

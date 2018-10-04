using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class AccessDeniedController : Controller
    {
        //
        // GET: /Admin/AccessDenied/

        public ActionResult Index()
        {
            ViewBag.Message = "Bạn không có quyền truy cập trang này, liên hệ admin để biết thêm thông tin!";
            return View();
        }

        //
        // GET: /Admin/AccessDenied/Details/5

        public ActionResult ErrorMissInfomation()
        {
            ViewBag.Message = "Thông tin truy vấn dữ liệu bị thiếu, liên hệ admin để biết thêm thông tin!";
            return View();
        }

        //
        // GET: /Admin/AccessDenied/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/AccessDenied/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/AccessDenied/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/AccessDenied/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/AccessDenied/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/AccessDenied/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

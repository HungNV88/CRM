using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Services.EducationLevels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class EducationLevelController : AdminController
    {
        //
        // GET: /Admin/Branch/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Branch/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Branch/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Branch/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                try
                {
					//fill business object
                    var info = new EducationLevelInfo();
					/*
                    
                    
                    info.EducationLevelId = collection["EducationLevelId"];
                    
                    
                    
                    info.Name = collection["Name"];
                    
                    
                    
                    
                    info.Description = collection["Description"];
                    
                    
                    
                    
                    
                    info.CreatedBy = collection["CreatedBy"];
                    
                    
                    
                    
                    
                    
                    
                                        
					*/
                    EducationLevelRepository.Create(info);
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Branch/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Branch/Edit/5

        [HttpPost]
        public ActionResult Edit(int? id, FormCollection forms)
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

        //
        // GET: /Admin/Branch/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Branch/Delete/5

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


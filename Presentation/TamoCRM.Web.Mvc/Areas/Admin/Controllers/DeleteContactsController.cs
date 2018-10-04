using Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Services.Contacts;
using TamoCRM.Domain.Contacts;


namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class DeleteContactsController : AdminController
    {
        //
        // GET: /Admin/DeleteContacts/

        public ActionResult DeleteImportFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteImportFile(FormCollection forms, HttpPostedFileBase excelfile)
        {

            if (excelfile.FileName.EndsWith(".xls") || excelfile.FileName.EndsWith(".xlsx"))
            {
                var now = DateTime.Now;
                var fullFileDir = Server.MapPath("/Uploads");
                var fullFilePath = Server.MapPath("/Uploads/") + string.Format("{0}_{1}_{2}_{3}_{4}_{5}_", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + excelfile.FileName;
                if (!Directory.Exists(fullFileDir)) Directory.CreateDirectory(fullFileDir);
                excelfile.SaveAs(fullFilePath);

                using (var stream = System.IO.File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var dr = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        string strPhones = "";
                        while (dr.Read())
                        {
                            var phone = dr[0] == null ? string.Empty : dr[0].ToString().Trim();
                            strPhones += phone;
                            strPhones += ",";
                        }
                        strPhones = strPhones.Remove(strPhones.LastIndexOf(','));
                        List<ContactInfo> lst_Id_Cts = new List<ContactInfo>();
                        lst_Id_Cts = ContactRepository.GetListContactsId(strPhones);

                        string strContactsID = "";
                        foreach(ContactInfo Id_Cts in lst_Id_Cts)
                        {
                            strContactsID += Id_Cts.Id;
                            strContactsID += ",";
                        }
                        strContactsID = strContactsID.Remove(strContactsID.LastIndexOf(','));
                        ContactRepository.Contacts_Delete_ImportFile(strContactsID);
                    }
                }
            }
            else
            {
                ViewBag.Message = "InvalidFileFormat";
            }
            return View();
        }

    }
}

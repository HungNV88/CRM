using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ImportExcels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.ImportExcels
{
    public class ImportExcelController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<ImportExcelInfo> Get()
        {
            return ImportExcelRepository.GetAll();
        }

        // GET api/<controller>
        public ImportExcelListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new ImportExcelListModel
                            {
                                Rows = ImportExcelRepository.Search(string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords/rows) + 1,
                                Records = rows
                            };
            return model;
        }

        // GET api/<controller>/5
        public ImportExcelModel Get(int id)
        {
            var info = ImportExcelRepository.GetInfo(id);
            var retVal = ImportExcelModel.FromInfo(info);
            if (retVal.ImportStatus.HasValue && retVal.ImportStatus == 1)
                //StoreData.LoadData(); 
                StoreData.LoadImportExcel();
            return retVal; 
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("ImportExcelId"));
            if (!string.IsNullOrEmpty(operation))
            {
                ImportExcelInfo info;
                switch (operation)
                {
                    case "edit":
                        info = ImportExcelRepository.GetInfo(id);
                        if (info != null)
                        {
						/*
							
							info.ImportId = form.Get("ImportId");
							
							info.UserId = form.Get("UserId");
							
							info.TypeId = form.Get("TypeId");
							
							info.ChannelId = form.Get("ChannelId");
							
							info.CollectorId = form.Get("CollectorId");
							
							info.BranchId = form.Get("BranchId");
							
							info.Status = form.Get("Status");
							
							info.FilePath = form.Get("FilePath");
							
							info.TotalRow = form.Get("TotalRow");
							
							info.CheckCount = form.Get("CheckCount");
							
							info.ErrorCount = form.Get("ErrorCount");
							
							info.DuplicateCount = form.Get("DuplicateCount");
							
							info.ImportedDate = form.Get("ImportedDate");
							                            
                            ImportExcelRepository.Update(info);
							*/
                        }
                        break;
                    case "add":
                        info = new ImportExcelInfo();
						/*
                        
						info.ImportId = form.Get("ImportId");
						
						info.UserId = form.Get("UserId");
						
						info.TypeId = form.Get("TypeId");
						
						info.ChannelId = form.Get("ChannelId");
						
						info.CollectorId = form.Get("CollectorId");
						
						info.BranchId = form.Get("BranchId");
						
						info.Status = form.Get("Status");
						
						info.FilePath = form.Get("FilePath");
						
						info.TotalRow = form.Get("TotalRow");
						
						info.CheckCount = form.Get("CheckCount");
						
						info.ErrorCount = form.Get("ErrorCount");
						
						info.DuplicateCount = form.Get("DuplicateCount");
						
						info.ImportedDate = form.Get("ImportedDate");
						                            
						*/
                        ImportExcelRepository.Create(info);
                        break;
                    case "del":
                        ImportExcelRepository.Delete(id);
                        break;
                }
            }
            return retVal;
        }
    }
}


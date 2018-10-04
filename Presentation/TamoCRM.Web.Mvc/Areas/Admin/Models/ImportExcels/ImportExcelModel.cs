using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Services.Channels;
using TamoCRM.Services.SourceTypes;
using TamoCRM.Services.Branches;
using TamoCRM.Services.Levels;
using TamoCRM.Services.Collectors;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ImportExcels
{
    public class ImportExcelModel
    {
        public DateTime ImportedDate { get; set; }

        public int? SourceTypeId { get; set; }
        public string SourceTypeName { get; set; }

        public int? ChannelId { get; set; }
        public string ChannelName { get; set; }

        public int? CollectorId { get; set; }
        public string CollectorName { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public int? LevelId { get; set; }
        public bool IsCorrectFormat { get; set; }
        public string Filename { get; set; }
        public int RowIndex { get; set; }
        public int TotalRow { get; set; }
        public int CheckCount { get; set; }
        public int ErrorCount { get; set; }
        public int DuplicateCount { get; set; }
        public int NoDuplicateCount { get; set; }
        public int Status { get; set; }
        public int StatusId { get; set; } // 18/02/2016 - them trang thai contact:  Moi (TVTS) hoac Moi (Loc)
        public int? ImportStatus { get; set; }

        public static ImportExcelModel FromInfo(ImportExcelInfo info)
        {
            ImportExcelModel retVal = new ImportExcelModel();
            retVal.Filename = Path.GetFileName(info.FilePath);
            retVal.IsCorrectFormat = retVal.Filename.ToLower().EndsWith(".xls") || retVal.Filename.ToLower().EndsWith(".xlsx");
            retVal.RowIndex = info.RowIndex;
            retVal.TotalRow = info.TotalRow;
            retVal.ErrorCount = info.ErrorCount;
            retVal.CheckCount = info.CheckCount;
            retVal.DuplicateCount = info.DuplicateCount + info.InternalDuplicateCount;
            retVal.NoDuplicateCount = retVal.TotalRow - retVal.ErrorCount - retVal.DuplicateCount;
            retVal.ImportedDate = info.ImportedDate;
            retVal.Status = info.Status;
            retVal.ImportStatus = info.ImportStatus;
            var sourceType = SourceTypeRepository.GetInfo(info.TypeId);
            if (sourceType != null) retVal.SourceTypeName = sourceType.Name;
            var channel = ChannelRepository.GetInfo(info.ChannelId);
            if (channel != null) retVal.ChannelName = channel.Name;
            var branch = BranchRepository.GetInfo(info.BranchId);
            if (branch != null) retVal.BranchName = branch.Name;
            var collector = CollectorRepository.GetInfo(info.CollectorId);
            if (collector != null) retVal.CollectorName = collector.Name;
            return retVal;
        }
    }
}

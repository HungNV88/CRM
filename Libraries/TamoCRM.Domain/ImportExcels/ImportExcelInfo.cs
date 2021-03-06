using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.ImportExcels
{
    public class ImportExcelInfo
    {
        public ImportExcelInfo()
        {
            
        }
	
		public int ImportId { get;set; }
	
		public int UserId { get;set; }
	
		public int TypeId { get;set; }
	
		public int ChannelId { get;set; }
	
		public int CollectorId { get;set; }
	
		public int BranchId { get;set; }

        public int LevelId { get; set; }

		public int Status { get;set; }
        public int StatusId { get; set; }
	
		public string FilePath { get;set; }

        public int RowIndex { get; set; }

		public int TotalRow { get;set; }
	
		public int CheckCount { get;set; }
	
		public int ErrorCount { get;set; }

        public int InternalDuplicateCount { get; set; }

		public int DuplicateCount { get;set; }
	
		public DateTime ImportedDate { get;set; }

        public int ImportStatus { get; set; }
	
    }
}


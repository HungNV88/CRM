using System;
using System.ComponentModel.DataAnnotations;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts
{
    public class ContactCreateModel
    {
        public string Fullname { get; set; }

        public string Birth { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int Gender { get; set; }

        public int TypeId { get; set; }

        public int ChannelId { get; set; }

        public int ContainerId { get; set; }

        public int LevelId { get; set; }

        public int CollectorId { get; set; }

        public int BranchId { get; set; }

        public int StatusId { get; set; }

        public int StatusIdDulicate { get; set; }

        public int StatusMapId { get; set; }
        public int StatusCareId { get; set; }
        
        public string Notes { get; set; }

        public DateTime ImportDate { get; set; }

        public int? ImportId { get; set; }

        [Required]
        public string Mobile1 { get; set; }
        
        public string Mobile2 { get; set; }
        
        [Required]
        public string Status { get; set; }

        public int ProductSellId { get; set; }

        public int ProductSoldId { get; set; }
    }
}
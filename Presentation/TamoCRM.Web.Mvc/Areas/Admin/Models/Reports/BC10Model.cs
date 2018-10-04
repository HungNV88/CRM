using System.Collections.Generic;
using System.Linq;
using TamoCRM.Domain.Contacts;
using TamoCRM.Services.Channels;
using TamoCRM.Web.Framework;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC10Model : BCModelContact
    {
        public string Collaborator { get; set; }
        public string EducationLevelName { get; set; }
        public string L1 { get; set; }
        public string L2 { get; set; }
        public string L3 { get; set; }
        public string L4 { get; set; }
        public string L5 { get; set; }
        public string L6 { get; set; }
        public string L7 { get; set; }
        public string L8 { get; set; }
        public string L9 { get; set; }

        public static BC10Model CreateBc10Model(ContactInfo info)
        {
            var objModel = Create<BC10Model>(info);

            // Collaborator
            var collaborator = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
            if (collaborator != null) objModel.Collaborator = collaborator.FullName;

            var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId);
            if (channel != null) objModel.Channel = channel.Name;
            objModel.Code = info.Code;
            
            return objModel;
        }

        public static List<BC10Model> CreateBc10Model(List<ContactInfo> infos)
        {
            var objModels = Create<BC10Model>(infos);

            foreach (var objModel in objModels)
            {
                var info = infos.FirstOrDefault(c => c.Id == objModel.Id);
                if (info == null) continue;
                
                // Collaborator
                var collaborator = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
                if (collaborator != null) objModel.Collaborator = collaborator.FullName;

                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId);
                if (channel != null) objModel.Channel = channel.Name;
            }

            return objModels;
        }
    }
}
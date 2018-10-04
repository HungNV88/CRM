using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Phones;
using TamoCRM.Services.StatusCare;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BCModelContact
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string Notes { get; set; }
        public string Major { get; set; }
        public string Mobile { get; set; }
        public string School { get; set; }
        public string Channel { get; set; }
        public string HaveJob { get; set; }
        public string Fullname { get; set; }
        public string Location { get; set; }
        public string MajorName { get; set; }
        public string StatusMap { get; set; }
        public string Consultant { get; set; }
        public string SchoolName { get; set; }
        public string StatusCare { get; set; }
        public string SchoolCare { get; set; }
        public string SchoolGrad { get; set; }
        public string SourceType { get; set; }
        public string HandoverDate { get; set; }
        public string HaveLearnTransfer { get; set; }
        public string HandoverDistributorDate { get; set; }

        public static T Create<T>(ContactInfo info) where T : BCModelContact
        {
            var objModel = Activator.CreateInstance(typeof (T)) as BCModelContact;
            if (objModel == null) return default(T);

            objModel.Id = info.Id;
            objModel.Code = info.Code;
            objModel.Email = info.Email;
            objModel.Location = info.Address;
            objModel.Fullname = info.Fullname;
            objModel.Level = Enum.GetName(typeof (LevelType), info.LevelId);
            objModel.SourceType = Enum.GetName(typeof (SourceType), info.TypeId);
            objModel.HandoverDate = info.HandoverConsultantDate != null
                ? info.HandoverConsultantDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
            objModel.HandoverDistributorDate = info.HandoverCollaboratorDate != null
                ? info.HandoverCollaboratorDate.Value.ToString("dd/MM/yyyy")
                : info.RegisteredDate != null ? info.RegisteredDate.Value.ToString("dd/MM/yyyy") : string.Empty;

            // Phone
            var phones = PhoneRepository.GetByContact(info.Id);
            if (phones != null)
            {
                var phoneEntity = phones.FirstOrDefault(phone => phone.IsPrimary > 0) ?? phones.FirstOrDefault();
                if (phoneEntity != null) objModel.Mobile = phoneEntity.PhoneNumber;
            }

            // StatusCare
            var statusCare = StatusCareRepository.GetAll().FirstOrDefault(c => c.Id == info.StatusCareConsultantId);
            if (statusCare != null) objModel.StatusCare = statusCare.Name;

            // StatusMap
            var statusMap = StatusMapRepository.GetAll().FirstOrDefault(c => c.Id == info.StatusMapConsultantId) ??
                            StatusMapRepository.GetInfo(info.StatusMapConsultantId);
            if (statusMap != null) objModel.StatusMap = statusMap.Name;

            // Notes
            if (info.CallInfoConsultant.IsStringNullOrEmpty())
            {
                var callHistories = CallHistoryRepository.GetAllByContactId(info.Id);
                if (!callHistories.IsNullOrEmpty()) objModel.Notes = callHistories.Last().CallCenterInfo;
            }
            else objModel.Notes = info.CallInfoConsultant;

            // Channel
            if (objModel.SourceType == Enum.GetName(typeof (SourceType), Core.SourceType.MO))
            {
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                              ChannelRepository.GetInfo(info.ChannelId);
                if (channel != null) objModel.Channel = channel.Name;
            }
            else if (objModel.SourceType == Enum.GetName(typeof (SourceType), Core.SourceType.CC))
            {
                if (info.UserCollaboratorId > 0)
                {
                    var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
                    if (user != null) objModel.Channel = user.FullName;
                }
            }
            else
            {
                if (info.ChannelId == 1)
                {
                    if (info.UserCollaboratorId > 0)
                    {
                        var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
                        if (user != null) objModel.Channel = user.FullName;
                    }
                }
                if (objModel.Channel.IsStringNullOrEmpty())
                {
                    var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                                  ChannelRepository.GetInfo(info.ChannelId);
                    if (channel != null) objModel.Channel = channel.Name;
                }
            }

            // Consultant
            var consultant = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserConsultantId) ??
                             UserRepository.GetInfo(info.UserConsultantId);
            if (consultant != null) objModel.Consultant = consultant.FullName;

            return (T) objModel;
        }

        public static List<T> Create<T>(List<ContactInfo> infos) where T : BCModelContact
        {
            var list = new List<T>();
            var listId = infos.Select(c => c.Id).Distinct().ToList();
            var listTdHistories =
                infos.Where(c => c.CallInfoConsultant.IsStringNullOrEmpty()).Select(c => c.Id).Distinct().ToList();
            var xs = new XmlSerializer(typeof (List<int>));
            var ms = new MemoryStream();
            xs.Serialize(ms, listId);
            var resultXML = Encoding.UTF8.GetString(ms.ToArray());
            var phones = PhoneRepository.GetByContacts_Xml(resultXML);
            var contactLevelInfos = ContactLevelInfoRepository.GetInfos_Xml(resultXML);

            xs = new XmlSerializer(typeof (List<int>));
            ms = new MemoryStream();
            xs.Serialize(ms, listTdHistories);
            resultXML = Encoding.UTF8.GetString(ms.ToArray());
            var callHistories = CallHistoryRepository.GetAllByContactId_Xml(resultXML);
            foreach (var info in infos)
            {
                var t = Create<T>(info, contactLevelInfos, phones, callHistories);
                list.Add(t);
            }
            return list;
        }

        private static T Create<T>(ContactInfo info, List<ContactLevelInfo> contactLevelInfos,
            List<PhoneInfo> phoneInfos, List<CallHistoryInfo> callHistoryInfos) where T : BCModelContact
        {
            var contactLevel = contactLevelInfos.FirstOrDefault(c => c.ContactId == info.Id) ?? new ContactLevelInfo();
            var objModel = Activator.CreateInstance(typeof (T)) as BCModelContact;
            if (objModel == null) return default(T);

           objModel.Id = info.Id;
            objModel.Code = info.Code;
           objModel.Email = info.Email;
            objModel.Location = info.Address;
          objModel.Fullname = info.Fullname;
            objModel.Level = Enum.GetName(typeof (LevelType), info.LevelId);
           objModel.SourceType = Enum.GetName(typeof (SourceType), info.TypeId);
          objModel.HandoverDate = info.HandoverConsultantDate != null
               ? info.HandoverConsultantDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
           objModel.HandoverDistributorDate = info.HandoverCollaboratorDate != null
                ? info.HandoverCollaboratorDate.Value.ToString("dd/MM/yyyy")
                : info.RegisteredDate != null ? info.RegisteredDate.Value.ToString("dd/MM/yyyy") : string.Empty;

            // Phone
            var phones = phoneInfos.Where(c => c.ContactId == info.Id).ToList();
          var phoneEntity = phones.FirstOrDefault(phone => phone.IsPrimary > 0) ?? phones.FirstOrDefault();
            if (phoneEntity != null) objModel.Mobile = phoneEntity.PhoneNumber;

            // StatusCare
            var statusCare = StatusCareRepository.GetAll().FirstOrDefault(c => c.Id == info.StatusCareConsultantId);
            if (statusCare != null) objModel.StatusCare = statusCare.Name;

            // StatusMap
            var statusMap = StatusMapRepository.GetInfo(info.StatusMapConsultantId);
            if (statusMap != null) objModel.StatusMap = statusMap.Name;

            // Notes
            if (info.CallInfoConsultant.IsStringNullOrEmpty())
            {
                var callHistories = callHistoryInfos.Where(c => c.ContactId == info.Id).ToList();
                if (!callHistories.IsNullOrEmpty()) objModel.Notes = callHistories.Last().CallCenterInfo;
            }
            else objModel.Notes = info.CallInfoConsultant;


            // Channel
            if (objModel.SourceType == Enum.GetName(typeof (SourceType), Core.SourceType.MO))
            {
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                              ChannelRepository.GetInfo(info.ChannelId);
                if (channel != null) objModel.Channel = channel.Name;
            }
            else if (objModel.SourceType == Enum.GetName(typeof (SourceType), Core.SourceType.CC))
            {
                if (info.UserCollaboratorId > 0)
                {
                    var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
                    if (user != null) objModel.Channel = user.FullName;
                }
                else
                {
                    if (info.ChannelId == 1)
                    {
                        if (info.UserCollaboratorId > 0)
                        {
                            var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId);
                            if (user != null) objModel.Channel = user.FullName;
                        }
                    }
                    if (objModel.Channel.IsStringNullOrEmpty())
                    {
                        var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                                      ChannelRepository.GetInfo(info.ChannelId);
                        if (channel != null) objModel.Channel = channel.Name;
                    }
                }

                // Consultant
                var consultant = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserConsultantId) ??
                                 UserRepository.GetInfo(info.UserConsultantId);
                if (consultant != null) objModel.Consultant = consultant.FullName;

                // HaveJob
                objModel.HaveJob = "Chưa";

                // HaveLearnTransfer
              
            }
            return (T)objModel;
        }
    }
}
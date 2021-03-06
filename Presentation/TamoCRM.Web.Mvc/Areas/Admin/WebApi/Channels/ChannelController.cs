using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Channels;
using TamoCRM.Services.Channels;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Channels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Channels
{
    public class ChannelController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<ChannelInfo> Get()
        {
            return ChannelRepository.GetAll();
        }

        // GET api/<controller>
        public ChannelListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new ChannelListModel
                            {
                                Rows = ChannelRepository.Search(string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords / rows) + 1,
                                Records = rows,
                                UserData = totalRecords
                            };
            return model;
        }

        // GET api/<controller>/5
        public ChannelInfo Get(int id)
        {
            return ChannelRepository.GetInfo(id);
        }

        [HttpPost]
        public ChannelInfo GetByCode(FormDataCollection form)
        {
            return ChannelRepository.GetInfo(form.Get("code"));
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("ChannelId"));
            if (!string.IsNullOrEmpty(operation))
            {
                ChannelInfo info;
                switch (operation)
                {
                    case "edit":
                        info = ChannelRepository.GetInfo(id);
                        if (info != null)
                        {
                            info.Name = form.Get("Name");
                            info.Code = form.Get("Code");
                            info.Description = form.Get("Description");
                            info.SourceTypeId = form.Get("SourceTypeId").ToInt32();
                            info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
                            ChannelRepository.Update(info);
                            retVal = "Cập nhật thành công";
                        }
                        break;
                    case "add":
                        info = new ChannelInfo
                                   {
                                       Name = form.Get("Name"),
                                       Code = form.Get("Code"),
                                       Description = form.Get("Description"),
                                       SourceTypeId = form.Get("SourceTypeId").ToInt32(),
                                       CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                                   };
                        ChannelRepository.Create(info);
                        retVal = "Thêm mới thành công";
                        break;
                    case "del":
                        ChannelRepository.Delete(id);
                        retVal = "Xoá thành công";
                        break;
                }
            }
            //StoreData.ListChannel = ChannelRepository.GetAll();
            return retVal;
        }

        [HttpGet]
        public List<ChannelInfo> FilterCountForDistributor(int sourceTypeId, int typeCc, string statusMapIds, string from, string to)
        {
            var branchId = UserContext.GetDefaultBranch();
            const int statusId = ((int)StatusType.New);
            var fromDate = from.ToDateTime() ?? DateTime.Now.Date;
            var toDate = to.ToDateTime() ?? DateTime.Now.Date; toDate = toDate.AddDays(1).AddSeconds(-1);

            var list = ChannelRepository.FilterCountForDistributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds)
                       ?? new List<ChannelInfo>();
            return list.Count > 0 ? list.OrderBy(c => c.Amount).ToList() : list;
        }

        [HttpGet]
        public int FilterContactCountForDistributor(int sourceTypeId, int typeCc, string statusMapIds, string from, string to, string channelIds, string schoolIds)
        {
            schoolIds = schoolIds.Trim(',');
            channelIds = channelIds.Trim(',');
            var branchId = UserContext.GetDefaultBranch();
            const int statusId = ((int)StatusType.New);
            var fromDate = from.ToDateTime() ?? DateTime.Now.Date;
            var toDate = to.ToDateTime() ?? DateTime.Now.Date; toDate = toDate.AddDays(1).AddSeconds(-1);
            return ChannelRepository.FilterContactCountForDistributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds);
        }

        [HttpGet]
        public List<ChannelInfo> FilterCountForCollaborator(int branchId, int sourceTypeId, int statusId, string from, string to)
        {
            var fromDate = string.IsNullOrEmpty(from)
                ? DateTime.Now.Date
                : DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var toDate = string.IsNullOrEmpty(to)
                ? DateTime.Now.Date.AddDays(1).AddSeconds(-1)
                : DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

            var list = ChannelRepository.FilterCountForCollaborator(branchId, sourceTypeId, statusId, fromDate, toDate) ?? new List<ChannelInfo>();
            return list.Count > 0 ? list.OrderBy(c => c.Amount).ToList() : list;
        }

        [HttpGet]
        public List<ChannelInfo> FilterCountForHandover(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, int employeeTypeId, int statusMapId, int statusCareId)
        {
            var type = (EmployeeType)employeeTypeId;
            var branchId = UserContext.GetDefaultBranch();
           
            var list = ChannelRepository.FilterCountForHandover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, type, statusMapId, statusCareId) ?? new List<ChannelInfo>();
            return list.Count > 0 ? list.OrderBy(c => c.Name).ToList() : list;
        }

        //Them ngay 28/03/2016 - Filter so luong contact theo kenh cho chuc nang "Kho Contact MOL"
        [HttpGet]
        public List<ChannelInfo> FilterCountForHandoverMOL(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, int employeeTypeId, int statusMapId, int statusCareId, string fromRegisterDate, string toRegisterDate)
        {
            var type = (EmployeeType)employeeTypeId;
            var branchId = UserContext.GetDefaultBranch();
            var fromRegisterDay = fromRegisterDate.ToDateTime("ddMMyyyy"); 
            var toRegisterDay = toRegisterDate.ToDateTime("ddMMyyyy");

            var list = ChannelRepository.FilterCountForHandoverMOL(branchId, typeIds, levelIds, importIds, statusIds, containerIds, type, statusMapId, statusCareId, fromRegisterDay, toRegisterDay) ?? new List<ChannelInfo>();
            return list.Count > 0 ? list.OrderBy(c => c.Name).ToList() : list;
        }
    }
}


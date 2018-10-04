using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TamoCRM.Core.Caching;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Channels;
using TamoCRM.Domain.SourceTypes;
using TamoCRM.Services.Catalogs;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.SourceTypes;
using StackExchange.Redis;
using TamoCRM.Core;

namespace TamoCRM.ImportExcel.Library
{
    public static class StaticData
    {
        public static void ClearData()
        {
            _Channels = null;
            _Packages = null;
            _TemplateAds = null;
            _CampaindTpes = null;
            _LandingPages = null;
            _SearchKeywords = null;
        }

        public static void LoadToRedis()
        {
            ConnectionMultiplexer Redis;
            Redis = ConnectionMultiplexer.Connect("localhost");
            
            //Lay DB REDIS
            var db_redis = Redis.GetDatabase();
            
            //Add all Phonenumber to redis
            IDictionary<string, string> list_phone = new Dictionary<string, string>();
            int dem_phone = 0;
            var lstPhones = PhoneRepository.GetAll_ForRedis();

            foreach (var phone in lstPhones.Where(c => !string.IsNullOrEmpty(c.PhoneNumber)))
            {
                if (dem_phone < Constant.MaxRedisLoad)
                {
                    list_phone.Add(Constant.NameSystem + phone.PhoneNumber, phone.ContactId.ToString());
                }
                dem_phone++;

                if (dem_phone == Constant.MaxRedisLoad)
                {
                    db_redis.StringSet(list_phone.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());
                    list_phone.Clear();
                    dem_phone = 0;
                }
            }

            //add cac phonenumber con lai
            db_redis.StringSet(list_phone.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());

            //Add all Email to redis
            IDictionary<string, string> list_Email = new Dictionary<string, string>();
            int dem_email = 0;
            var lstContacts = ContactRepository.GetAll_ForRedis();

            foreach (var info in lstContacts.Where(c => !string.IsNullOrEmpty(c.Email)))
            {
                if (dem_email < Constant.MaxRedisLoad)
                {
                    try {
                        list_Email.Add(Constant.NameSystem + info.Email, info.Id.ToString());
                    }
                    catch {
                        continue;
                    }
                    
                }
                dem_email++;

                if (dem_email == Constant.MaxRedisLoad)
                {
                    db_redis.StringSet(list_Email.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());
                    list_Email.Clear();
                    dem_email = 0;
                }
            }

            //add cac email 1 con lai
            db_redis.StringSet(list_Email.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());

            //Add Email 2
            list_Email.Clear();
            dem_email = 0;
            foreach (var info in lstContacts.Where(c => !string.IsNullOrEmpty(c.Email2)))
            {
                if (dem_email < Constant.MaxRedisLoad)
                {
                    list_Email.Add(Constant.NameSystem + info.Email2, info.Id.ToString());
                }
                dem_email++;

                if (dem_email == Constant.MaxRedisLoad)
                {
                    db_redis.StringSet(list_Email.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());
                    list_Email.Clear();
                    dem_email = 0;
                }
            }
            //add cac email 2 con lai
            db_redis.StringSet(list_Email.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());
        }

        public static void LoadToRedis(IDictionary<string, string> lstPhoneEmail)
        {
            ConnectionMultiplexer Redis;
            Redis = ConnectionMultiplexer.Connect("localhost");

            //Lay DB REDIS
            var db_redis = Redis.GetDatabase();
            db_redis.StringSet(lstPhoneEmail.Select(p => new KeyValuePair<RedisKey, RedisValue>((RedisKey)p.Key, p.Value)).ToArray());
            lstPhoneEmail.Clear();
        }

        private static List<ChannelInfo> _Channels = new List<ChannelInfo>();
        public static int GetChannelId(string value, int typeId, int defaultId)
        {
            if (string.IsNullOrEmpty(value)) return defaultId;
            if (_Channels.IsNullOrEmpty()) _Channels = ChannelRepository.GetAll();
            var entity = _Channels
                .Where(c => c.SourceTypeId == typeId)
                .FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));  
            if (entity == null)
            {
                entity = new ChannelInfo
                {
                    Name = value,
                    CreatedBy = 1,
                    ChangedBy = 1,
                    SourceTypeId = typeId,
                    Description = string.Empty,
                    CreatedDate = DateTime.Now,
                    ChangedDate = DateTime.Now,
                };
                var index = entity.Name.IndexOf(".");
                if (index > -1) entity.Code = entity.Name.Substring(0, index);

                entity.ChannelId = ChannelRepository.Create(entity);
                _Channels.Add(entity);
            }
            return entity.ChannelId;
        }

        private static List<CampaindTpeInfo> _CampaindTpes = new List<CampaindTpeInfo>();
        public static int GetCampaindTpeId(string value)
        {
            if (_CampaindTpes.IsNullOrEmpty()) _CampaindTpes = CatalogRepository.GetAll<CampaindTpeInfo>();
            var entity = _CampaindTpes.FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new CampaindTpeInfo { Name = value };
                entity.Id = CatalogRepository.Create(entity);
                _CampaindTpes.Add(entity);
            }
            return entity.Id;
        }

        private static List<LandingPageInfo> _LandingPages = new List<LandingPageInfo>();
        public static int GetLandingPageId(string value)
        {
            if (_LandingPages.IsNullOrEmpty()) _LandingPages = CatalogRepository.GetAll<LandingPageInfo>();
            var entity = _LandingPages.FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new LandingPageInfo { Name = value };
                entity.Id = CatalogRepository.Create(entity);
                _LandingPages.Add(entity);
            }
            return entity.Id;
        }

        private static List<TemplateAdsInfo> _TemplateAds = new List<TemplateAdsInfo>();
        public static int GetTemplateAdsId(string value)
        {
            if (_TemplateAds.IsNullOrEmpty()) _TemplateAds = CatalogRepository.GetAll<TemplateAdsInfo>();
            var entity = _TemplateAds.FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new TemplateAdsInfo { Name = value };
                entity.Id = CatalogRepository.Create(entity);
                _TemplateAds.Add(entity);
            }
            return entity.Id;
        }

        private static List<SearchKeywordInfo> _SearchKeywords = new List<SearchKeywordInfo>();
        public static int GetSearchKeywordId(string value)
        {
            if (_SearchKeywords.IsNullOrEmpty()) _SearchKeywords = CatalogRepository.GetAll<SearchKeywordInfo>();
            var entity = _SearchKeywords.FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new SearchKeywordInfo { Name = value };
                entity.Id = CatalogRepository.Create(entity);
                _SearchKeywords.Add(entity);
            }
            return entity.Id;
        }

        private static List<PackageInfo> _Packages = new List<PackageInfo>();
        public static int GetPackageId(string value)
        {
            if (_Packages.IsNullOrEmpty()) _Packages = CatalogRepository.GetAll<PackageInfo>();
            var entity = _Packages.FirstOrDefault(c => c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                entity = new PackageInfo { Name = value };
                entity.Id = CatalogRepository.Create(entity);
                _Packages.Add(entity);
            }
            return entity.Id;
        }

        private static List<SourceTypeInfo> _SourceTypes = new List<SourceTypeInfo>();
        public static List<SourceTypeInfo> GetSourceTypeCheckDuplicate()
        {
            if (_SourceTypes.IsNullOrEmpty()) _SourceTypes = SourceTypeRepository.GetAll();
            return _SourceTypes.Where(c => c.IsCheckDuplicate).ToList();
        }
        public static List<SourceTypeInfo> GetSourceTypeCheckUpdate()
        {
            if (_SourceTypes.IsNullOrEmpty()) _SourceTypes = SourceTypeRepository.GetAll();
            return _SourceTypes.Where(c => c.IsCheckUpdate).ToList();
        }
    }

}

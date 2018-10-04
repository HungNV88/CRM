using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Activity;
using TamoCRM.Domain.ContactDuplicates;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.Activity;
using TamoCRM.Services.ContactDuplicates;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Services.Phones;
using TamoCRM.Web.Framework.Users;
using System.Collections;
using TamoCRM.Core.Caching;

namespace TamoCRM.ImportExcel.Library
{
    public class ImportProcess : IDisposable
    {
        #region Fields
        private DataTable dtPhones;
        private DataTable dtContacts;
        private DataTable dtContactTmps;
        private DataTable dtContactLevels;
        private DataTable dtObjectChanges;
        private DataTable dtContactDuplicate;
        private DataTable dtLogContainerMOL;
        public ImportExcelInfo ImportInfo { get; set; }
        private readonly HashSet<string> internalPhoneAndEmails = new HashSet<string>();
        private readonly Dictionary<int, ContactDuplicateInfo> duplicateContactIds = new Dictionary<int, ContactDuplicateInfo>();

        IDictionary<string, string> list_phone_email_redis = new Dictionary<string, string>();
        #endregion

        public ImportProcess(ImportExcelInfo importInfo)
        {
            ImportInfo = importInfo;
            dtContacts = CreateContact();
            dtPhones = CreatePhoneTable();
            dtContactLevels = CreateContactLevel();
            dtContactTmps = CreateContactTmpTable();
            dtObjectChanges = CreateObjectChangeTable();
            dtContactDuplicate = CreateContactDuplicateTable();
            dtLogContainerMOL = CreateLogContainerMOL();
        }

        public void Start()
        {
            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("==============Begin Import=================");
            Console.WriteLine("Processing file: " + ImportInfo.FilePath);
            DoImport(ImportInfo.FilePath);
            watch.Stop();
            Console.WriteLine("Processed file: " + ImportInfo.FilePath + " in: " + watch.ElapsedMilliseconds);
            Console.WriteLine("==============End Import===================");
        }

        private void DoImport(string file)
        {
            try
            {
                //Create activity log
                var activity = new ActivityLogInfo
                                   {
                                       CreatedDate = DateTime.Now,
                                       CreatedBy = ImportInfo.UserId,
                                       FunctionId = (int)LogFunctionType.ImportExcel,
                                       FunctionType = (int)LogFunctionType.ImportExcel,
                                   };
                activity.Id = ActivityLogRepository.Create(activity);

                //End log
                using (var connection = new SqlConnection(ImportConfig.ConnectionString))
                {
                    connection.Open();

                    SqlTransaction transaction = null;
                    try
                    {
                        transaction = connection.BeginTransaction();
                        // Use one transaction to put all the data in the database
                        var watch = new Stopwatch();
                        watch.Start();
                        var contactIdentity = GetIdentity("Contacts", connection, transaction);
                        var contactDuplicateIdentity = GetIdentity("ContactDuplicates", connection, transaction);
                        using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
                        {
                            using (var dr = ExcelReaderFactory.CreateOpenXmlReader(stream))
                            {
                                //Log bat dau tu 
                                var rowIndex = 0;
                                while (dr.Read())
                                {
                                    if (rowIndex >= 1)
                                    {
                                        #region Read excel row
                                        var name = dr[1] == null ? string.Empty : dr[1].ToString().Trim();
                                        var email = dr[2] == null ? string.Empty : dr[2].ToString().Trim().ToLower();
                                        var mobile = dr[3] == null
                                            ? string.Empty
                                            : Util.CleanAlphabetAndFirstZero(dr[3].ToString().Trim());
                                        var address = dr[4] == null ? string.Empty : dr[4].ToString().Trim();
                                        DateTime? registeredDate;
                                        if (dr[5] == null) registeredDate = null;
                                        else
                                        {
                                            try
                                            {
                                                registeredDate = (DateTime)dr[5];
                                            }
                                            catch
                                            {
                                                registeredDate = dr[5].ToDateTime();
                                            }
                                        }
                                        var campaindTpe = dr[6] == null ? string.Empty : dr[6].ToString().Trim();
                                        var campaindTpeId = StaticData.GetCampaindTpeId(campaindTpe);

                                        var landingPage = dr[7] == null ? string.Empty : dr[7].ToString().Trim();
                                        var landingPageId = StaticData.GetLandingPageId(landingPage);

                                        var channel = dr[8] == null ? string.Empty : dr[8].ToString().Trim();
                                        var channelId = StaticData.GetChannelId(channel, ImportInfo.TypeId, ImportInfo.ChannelId);

                                        var templateAds = dr[9] == null ? string.Empty : dr[9].ToString().Trim();
                                        var templateAdsId = StaticData.GetTemplateAdsId(templateAds);

                                        var searchKeyword = dr[10] == null ? string.Empty : dr[10].ToString().Trim();
                                        var searchKeywordId = StaticData.GetTemplateAdsId(searchKeyword);

                                        var package = dr[11] == null ? string.Empty : dr[11].ToString().Trim();
                                        var packageId = StaticData.GetTemplateAdsId(package);

                                        var code = dr[12] == null ? string.Empty : dr[12].ToString().Trim();

                                        var contactIdDb = 0;
                                        var contactTmpStatus = 0;
                                        var contactInfoDb = string.Empty;
                                        #endregion

                                        #region validate row
                                        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(mobile) && string.IsNullOrEmpty(email))
                                            continue;
                                        ImportInfo.RowIndex++;
                                        ImportInfo.TotalRow++;

                                        //Contact validation
                                        var error = Validate(mobile, string.Empty, string.Empty, email);

                                        // haihm
                                        if (error == ContactError.EmailFormat)
                                        {
                                            email = null;
                                            error = ContactError.None;
                                        }
                                        if (error != ContactError.None)
                                        {
                                            ImportInfo.ErrorCount++;
                                            contactTmpStatus = (int)error;
                                        }
                                        else
                                        {
                                            //Check if contact is internal duplicate or not
                                            if (IsInternalDuplicate(mobile, string.Empty, string.Empty, email))
                                            {
                                                contactTmpStatus = (int)ContactError.InternalDuplicate;
                                                ImportInfo.InternalDuplicateCount++;
                                            }
                                            else
                                            {
                                                //If not internal duplicate, add to hashtable data
                                                //int check_db_idcts;
                                                if (!string.IsNullOrEmpty(email)) internalPhoneAndEmails.Add(email);
                                                if (!string.IsNullOrEmpty(mobile)) internalPhoneAndEmails.Add(mobile);
                                                //int check_dulicated;
                                                var check = CheckDuplicateProvider.Instance().IsDuplicate(mobile, string.Empty, string.Empty, email, string.Empty, out contactIdDb, out contactInfoDb);
                                                
                                                //check_db_idcts = ContactRepository.ContactIsDuplicate(mobile, string.Empty, string.Empty, email, string.Empty);
                                                //if (check_db_idcts != 0)
                                                //{
                                                //    check = true;
                                                //}
                                                //if (!check)
                                                //{
                                                //    check_dulicated = ContactRepository.ContactIsDuplicate(mobile, string.Empty, string.Empty, email, string.Empty);
                                                //    if (check_dulicated != 0) check = true;
                                                //}
                                                if (check)
                                                {
                                                    ImportInfo.DuplicateCount++;
                                                    contactTmpStatus = (int)ContactError.Duplicate;
                                                }
                                            }

                                        }
                                        ImportInfo.CheckCount++;
                                        #endregion

                                        switch (contactTmpStatus)
                                        {
                                            case (int)ContactError.None:
                                                {
                                                    #region create contact row
//                                                  
                                                    string sTypeId = "01"; //Nguon contact: MO, CC ... 01 la MO

                                                    if(ImportInfo.TypeId == 3)
                                                    {
                                                        sTypeId = "02";
                                                    }
                                                    else if(ImportInfo.TypeId == 4)
                                                    {
                                                        sTypeId = "01";
                                                    }
                                                    else if(ImportInfo.TypeId == 5)
                                                    {
                                                        sTypeId = "03";
                                                    }
                                                    else if(ImportInfo.TypeId == 6)
                                                    {
                                                        sTypeId = "04";
                                                    }
                                                    
                                                    else if (ImportInfo.TypeId == 8)
                                                    {
                                                        sTypeId = "05";
                                                    }
                                                    else if (ImportInfo.TypeId == 9)
                                                    {
                                                        sTypeId = "06";
                                                    }
                                                    else if (ImportInfo.TypeId == 10)
                                                    {
                                                        sTypeId = "07";
                                                    }
                                                    else if (ImportInfo.TypeId == 11)
                                                    {
                                                        sTypeId = "08";
                                                    }
                                                    else
                                                    {
                                                        sTypeId = "09";
                                                    }
                                                  

                                                    var contactId = contactIdentity + rowIndex;
                                                    var sCode = DateTime.Now.ToString("yyyyMMdd") + sTypeId + contactId.ToString();

                                                    var contactRow = dtContacts.NewRow();
                                                    contactRow["Gender"] = 0;
                                                    contactRow["Email"] = email;
                                                    contactRow["Id"] = contactId;
                                                    contactRow["Address"] = address;
                                                    contactRow["Fullname"] = name;
                                                    contactRow["ChannelId"] = channelId;
                                                    contactRow["PackageId"] = packageId;
                                                    contactRow["CreatedDate"] = DateTime.Now;
                                                    contactRow["TypeId"] = ImportInfo.TypeId;
                                                    contactRow["LevelId"] = ImportInfo.LevelId;
                                                    contactRow["CampaindTpeId"] = campaindTpeId;
                                                    contactRow["LandingPageId"] = landingPageId;
                                                    contactRow["TemplateAdsId"] = templateAdsId;
                                                    contactRow["BranchId"] = ImportInfo.BranchId;
                                                    contactRow["ImportId"] = ImportInfo.ImportId;
                                                    contactRow["StatusId"] = ImportInfo.Status; //(int)StatusType.New;
                                                    contactRow["RegisteredDate"] = registeredDate;
                                                    contactRow["UserImportId"] = ImportInfo.UserId;
                                                    contactRow["SearchKeywordId"] = searchKeywordId;
                                                    contactRow["ImportedDate"] = ImportInfo.ImportedDate;
                                                    contactRow["Code"] = code.IsStringNullOrEmpty() ? sCode : code;
                                                    dtContacts.Rows.Add(contactRow);
                                                    
                                                    #endregion

                                                    #region create phone row
                                                    if (!string.IsNullOrEmpty(mobile))
                                                    {
                                                        var phoneRow = dtPhones.NewRow();
                                                        phoneRow["ContactId"] = contactId;
                                                        phoneRow["PhoneType"] = (int)PhoneType.HomePhone;
                                                        phoneRow["PhoneNumber"] = mobile;
                                                        phoneRow["IsPrimary"] = 1;
                                                        dtPhones.Rows.Add(phoneRow);
                                                    }
                                                    #endregion

                                                    #region create contact level row
                                                    var contactLelveRow = dtContactLevels.NewRow();
                                                    contactLelveRow["ContactId"] = contactId;
                                                    dtContactLevels.Rows.Add(contactLelveRow);
                                                    #endregion

                                                    #region create object changes
                                                    foreach (DataColumn col in dtContacts.Columns)
                                                    {
                                                        if (col.ColumnName != "TypeId" &&
                                                            col.ColumnName != "LevelId" &&
                                                            col.ColumnName != "StatusId" &&
                                                            col.ColumnName != "ChannelId" &&
                                                            col.ColumnName != "BranchId")
                                                            continue;
                                                        var objChange = dtObjectChanges.NewRow();
                                                        objChange["ActivityId"] = activity.Id;
                                                        objChange["ObjectType"] = (int)LogObjectType.Contact;
                                                        objChange["ObjectId"] = contactId;
                                                        var propertyType = 0;
                                                        int? propertyValueInt = null;
                                                        string propertyValueString = null;
                                                        DateTime? propertyValueDateTime = null;
                                                        switch (col.ColumnName)
                                                        {
                                                            case "BranchId":
                                                                propertyType = (int)LogPropertyType.Contacts_Branch;
                                                                propertyValueInt = ImportInfo.BranchId;
                                                                break;
                                                            case "TypeId":
                                                                propertyType = (int)LogPropertyType.Contacts_Type;
                                                                propertyValueInt = ImportInfo.TypeId;
                                                                break;
                                                            case "LevelId":
                                                                propertyType = (int)LogPropertyType.Contacts_Level;
                                                                propertyValueInt = ImportInfo.LevelId;
                                                                break;
                                                            case "StatusId":
                                                                propertyType = (int)LogPropertyType.Contacts_Status;
                                                                propertyValueInt = ImportInfo.Status;
                                                                break;
                                                            case "ChannelId":
                                                                propertyType = (int)LogPropertyType.Contacts_Channel;
                                                                propertyValueInt = channelId;
                                                                break;
                                                        }
                                                        objChange["PropertyType"] = propertyType;
                                                        objChange["PropertyValueInt"] = propertyValueInt;
                                                        objChange["PropertyValueString"] = propertyValueString;
                                                        objChange["PropertyValueDateTime"] = propertyValueDateTime;
                                                        objChange["ChangedDate"] = DateTime.Now;
                                                        dtObjectChanges.Rows.Add(objChange);
                                                    }

                                                    //Them Mobile vs Email cua moi contact vao cache Redis

                                                    //CachingProvider.Instance().Set(mobile, contactId.ToString()); 
                                                    //CachingProvider.Instance().Set(email, contactId.ToString()); 

                                                    //list_phone_email_redis.Add(Constant.NameSystem + mobile, contactId.ToString());
                                                    //list_phone_email_redis.Add(Constant.NameSystem + email, contactId.ToString());
                                                    #endregion

                                                    //Nếu import vào kho MOL thì save log vào bảng LogContainerMOL
                                                    #region create log contact kho MOL
                                                    if(ImportInfo.Status == (int)StatusType.ContainerMOL)
                                                    {
                                                        var logContainerMOL = dtLogContainerMOL.NewRow();

                                                        logContainerMOL["CreatedDate"] = DateTime.Now;
                                                        logContainerMOL["StatusContainerType"] = (int)StatusContainerTypeMOL.ImportContainer;
                                                        logContainerMOL["StatusAddContact"] = (int)StatusAddContact.ImportContact;
                                                        logContainerMOL["StatusContactFirst"] = ImportInfo.Status;
                                                        logContainerMOL["StatusContactAfter"] = null;
                                                        logContainerMOL["ContactId"] = contactId; 
                                                        logContainerMOL["ChannelId"] = channelId;

                                                        dtLogContainerMOL.Rows.Add(logContainerMOL);
                                                    }
                                                    #endregion
                                                }
                                                break;
                                            case (int)ContactError.Duplicate:
                                                {
                                                    #region create contact duplicate
                                                    var sourceTypes = StaticData.GetSourceTypeCheckDuplicate();
                                                    if (!sourceTypes.IsNullOrEmpty() && sourceTypes.Any(c => c.SourceTypeId == ImportInfo.TypeId))
                                                    {
                                                        var contactDuplicateRow = dtContactDuplicate.NewRow();
                                                        var contact = ContactRepository.GetInfo(contactIdDb);
                                                        //update chuyen trang thai contact neu trung chuyen sang kho trung
                                                        ContactRepository.UpdateChangeStatusId(contactIdDb);
                                                        int idContactDuplicate = contactDuplicateIdentity + rowIndex;

                                                        contactDuplicateRow["Id"] = idContactDuplicate;
                                                        contactDuplicateRow["ContactId"] = contactIdDb;
                                                        contactDuplicateRow["SourceTypeId"] = ImportInfo.TypeId;
                                                        contactDuplicateRow["Status"]  = contact != null ? contact.StatusId: 0;
                                                        contactDuplicateRow["ImportId"] = ImportInfo.ImportId;
                                                        contactDuplicateRow["DuplicateInfo"] = contactInfoDb;
                                                        contactDuplicateRow["CreatedDate"] = DateTime.Now;
                                                        contactDuplicateRow["IsNotyfiDuplicate"] = true;

                                                        dtContactDuplicate.Rows.Add(contactDuplicateRow);
                                                    }
                                                    #endregion
                                                }
                                                break;
                                            default:
                                                {
                                                    #region create contact temp errors
                                                    var contactRow = dtContactTmps.NewRow();
                                                    var contactId = contactIdentity + rowIndex;
                                                    contactRow["Gender"] = 0;
                                                    contactRow["Email"] = email;
                                                    contactRow["Id"] = contactId;
                                                    contactRow["Fullname"] = name;
                                                    contactRow["Mobile1"] = mobile;
                                                    contactRow["Address"] = address;
                                                    contactRow["ChannelId"] = channelId;
                                                    contactRow["PackageId"] = packageId;
                                                    contactRow["CreatedDate"] = DateTime.Now;
                                                    contactRow["TypeId"] = ImportInfo.TypeId;
                                                    contactRow["LevelId"] = ImportInfo.LevelId;
                                                    contactRow["CampaindTpeId"] = campaindTpeId;
                                                    contactRow["LandingPageId"] = landingPageId;
                                                    contactRow["TemplateAdsId"] = templateAdsId;
                                                    contactRow["BranchId"] = ImportInfo.BranchId;
                                                    contactRow["ImportId"] = ImportInfo.ImportId;
                                                    contactRow["StatusId"] = (int)StatusType.New;
                                                    contactRow["RegisteredDate"] = registeredDate;
                                                    contactRow["UserImportId"] = ImportInfo.UserId;
                                                    contactRow["SearchKeywordId"] = searchKeywordId;
                                                    contactRow["ImportedDate"] = ImportInfo.ImportedDate;
                                                    contactRow["Code"] = code.IsStringNullOrEmpty() ? "TPE_" + contactId : code;
                                                    dtContactTmps.Rows.Add(contactRow);
                                                    #endregion
                                                }
                                                break;
                                        }
                                    }
                                    rowIndex++;
                                }
                            }
                        }

                        watch.Stop();
                        Console.WriteLine("Thoi gian read file: " + watch.ElapsedMilliseconds);
                        watch.Reset();
                        watch.Restart();

                        dtPhones = RemoveDuplicateRows(dtPhones, "PhoneNumber");
                        // Import
                        ContactBulkImport.ImportContactPhone(dtPhones, connection, transaction);
                        ContactBulkImport.ImportContact(dtContacts, connection, transaction);
                        ContactBulkImport.ImportContactLevel(dtContactLevels, connection, transaction);                   
                        ContactBulkImport.ImportObjectChanges(dtObjectChanges, connection, transaction);
                        ContactBulkImport.ImportContactTmp(dtContactTmps);
                        ContactBulkImport.ImportContactDuplicate(dtContactDuplicate, connection, transaction);
                        //Log table LogContainerMOL phuc vu bao cao xuat nhap kho MOL
                        ContactBulkImport.ImportLogContainerMOL(dtLogContainerMOL, connection, transaction);

                        watch.Stop();
                        Console.WriteLine("Thoi gian Import file: " + watch.ElapsedMilliseconds);
                        
                        Console.WriteLine("Bat dau commit");
                        transaction.Commit();
                        Console.WriteLine("Commit thanh cong");
                        ImportInfo.ImportStatus = 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (transaction != null)
                        {
                            Console.WriteLine("Commit ko thanh cong. Dang rollback lai");
                            ImportInfo.ImportStatus = 2;
                            transaction.Rollback(); // This will not reset IDENT_CURRENT
                        }
                    }
                    finally
                    {
                        // Update Duplicate
                        Console.WriteLine("Bat dau vao finally.");
                        UpdateDuplicate();
                        //ImportInfo.ImportStatus = 1; 
                        ImportExcelRepository.Update(ImportInfo);
                        
                        // Load redis
                        if(ImportInfo.ImportStatus == 1)
                        {
                            Console.WriteLine("Update xong trang thai ImportExcel. Bat dau load Redis.");
                            StaticData.LoadToRedis();
                            Console.WriteLine("Load xong redis");
                        }

                    }
                }

                dtPhones = null;
                dtContacts = null;
                dtContactTmps = null;
                dtContactLevels = null;
                dtObjectChanges = null;
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.ToString());
            }
        }

        //loại bỏ những phonenumber trùng trong dtPhones.
        private DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);
                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove dupliate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Utility
        public void Dispose()
        {
            ImportInfo = null;
        }
        private void UpdateDuplicate()
        {
            var sourceTypes = StaticData.GetSourceTypeCheckUpdate();
            if (duplicateContactIds == null || duplicateContactIds.Count == 0) return;
            if (sourceTypes.IsNullOrEmpty() || sourceTypes.All(c => c.SourceTypeId != ImportInfo.TypeId)) return;
   
            foreach (var item in duplicateContactIds.Values)
            {
                try
                {
                    var contact = ContactRepository.GetInfo(item.ContactId);
                    if (contact == null)
                    {
                        continue;
                    }

                    item.Status = contact.StatusId;
                    item.IsNotyfiDuplicate = true; //Bao notify trung contact
                    ContactDuplicateRepository.Update(item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
          
        }
        private static DataTable CreateContact()
        {
            var retVal = new DataTable("Contacts");
            retVal.Columns.Add("Id");
            retVal.Columns.Add("Code");
            retVal.Columns.Add("Fullname");
            retVal.Columns.Add("Address");
            retVal.Columns.Add("Birthday");
            retVal.Columns.Add("Gender");
            retVal.Columns.Add("Email");
            retVal.Columns.Add("Email2");
            retVal.Columns.Add("Notes");
            retVal.Columns.Add("RegisteredDate");
            retVal.Columns.Add("CreatedDate");
            retVal.Columns.Add("ImportedDate");
            retVal.Columns.Add("HandoverCollaboratorDate");
            retVal.Columns.Add("RecoveryCollaboratorDate");
            retVal.Columns.Add("AppointmentCollaboratorDate");
            retVal.Columns.Add("CallCollaboratorDate");
            retVal.Columns.Add("HandoverConsultantDate");
            retVal.Columns.Add("RecoveryConsultantDate");
            retVal.Columns.Add("AppointmentConsultantDate");
            retVal.Columns.Add("CallConsultantDate");
            retVal.Columns.Add("CollectorId");
            retVal.Columns.Add("TypeId");
            retVal.Columns.Add("ChannelId");
            retVal.Columns.Add("CampaindTpeId");
            retVal.Columns.Add("LandingPageId");
            retVal.Columns.Add("TemplateAdsId");
            retVal.Columns.Add("SearchKeywordId");
            retVal.Columns.Add("PackageId");
            retVal.Columns.Add("LevelId");
            retVal.Columns.Add("BranchId");
            retVal.Columns.Add("StatusId");
            retVal.Columns.Add("ImportId");
            retVal.Columns.Add("ContainerId");
            retVal.Columns.Add("ProductSellId");
            retVal.Columns.Add("ProductSoldId");
            retVal.Columns.Add("QualityId");
            retVal.Columns.Add("StatusMapCollaboratorId");
            retVal.Columns.Add("StatusCareCollaboratorId");
            retVal.Columns.Add("StatusMapConsultantId");
            retVal.Columns.Add("StatusCareConsultantId");
            retVal.Columns.Add("UserImportId");
            retVal.Columns.Add("UserErrorId");
            retVal.Columns.Add("UserHandoverCollaboratorId");
            retVal.Columns.Add("UserCollaboratorId");
            retVal.Columns.Add("UserRecoveryCollaboratorId");
            retVal.Columns.Add("UserHandoverConsultantId");
            retVal.Columns.Add("UserConsultantId");
            retVal.Columns.Add("UserRecoveryConsultantId");
            retVal.Columns.Add("DraftCollaborator");
            retVal.Columns.Add("DraftConsultant");
            retVal.Columns.Add("CallInfoCollaborator");
            retVal.Columns.Add("CallInfoConsultant");
            retVal.Columns.Add("HandoverHistoryCollaboratorId");
            retVal.Columns.Add("HandoverHistoryConsultantId");
            retVal.Columns.Add("CallCount");
            return retVal;
        }
        private static DataTable CreatePhoneTable()
        {
            var retVal = new DataTable("Phones");
            retVal.Columns.Add("ContactId");
            retVal.Columns.Add("PhoneType");
            retVal.Columns.Add("PhoneNumber");
            retVal.Columns.Add("IsPrimary");
            return retVal;
        }
        private static DataTable CreateContactLevel()
        {
            var retVal = new DataTable("ContactLevelInfos");
            retVal.Columns.Add("ContactId");
            retVal.Columns.Add("EducationSchoolName");
            retVal.Columns.Add("WantStudyEnglish");
            retVal.Columns.Add("HasAppointment");
            retVal.Columns.Add("AppointmentTime");
            retVal.Columns.Add("ApointmentType");
            retVal.Columns.Add("ApointmentNotes");
            retVal.Columns.Add("HasAppointmentInterview");
            retVal.Columns.Add("AppointmentInterviewId");
            retVal.Columns.Add("HasCasecAccount");
            retVal.Columns.Add("CasecAccountId");
            retVal.Columns.Add("HasPointTestCasec");
            retVal.Columns.Add("HasPointInterview");
            retVal.Columns.Add("HasSetSb100");
            retVal.Columns.Add("HasLinkSb100");
            retVal.Columns.Add("FeePackageType");
            retVal.Columns.Add("FeeTuitionType");
            retVal.Columns.Add("FeeMoneyType");
            retVal.Columns.Add("FeeEdu");
            retVal.Columns.Add("FeeEduYet");
            retVal.Columns.Add("FeeEduNotes");
            return retVal;
        }
        private static DataTable CreateContactTmpTable()
        {
            var retVal = new DataTable("ContactTmps");
            retVal.Columns.Add("Id");
            retVal.Columns.Add("Code");
            retVal.Columns.Add("Fullname");
            retVal.Columns.Add("Address");
            retVal.Columns.Add("Birthday");
            retVal.Columns.Add("Gender");
            retVal.Columns.Add("Email");
            retVal.Columns.Add("Email2");
            retVal.Columns.Add("Notes");
            retVal.Columns.Add("RegisteredDate");
            retVal.Columns.Add("CreatedDate");
            retVal.Columns.Add("ImportedDate");
            retVal.Columns.Add("Mobile1");
            retVal.Columns.Add("Mobile2");
            retVal.Columns.Add("Mobile3");
            retVal.Columns.Add("CollectorId");
            retVal.Columns.Add("TypeId");
            retVal.Columns.Add("ChannelId");
            retVal.Columns.Add("CampaindTpeId");
            retVal.Columns.Add("LandingPageId");
            retVal.Columns.Add("TemplateAdsId");
            retVal.Columns.Add("SearchKeywordId");
            retVal.Columns.Add("PackageId");
            retVal.Columns.Add("LevelId");
            retVal.Columns.Add("BranchId");
            retVal.Columns.Add("StatusId");
            retVal.Columns.Add("ImportId");
            retVal.Columns.Add("ContainerId");
            retVal.Columns.Add("UserImportId");
            return retVal;
        }
        private static DataTable CreateObjectChangeTable()
        {
            var retVal = new DataTable("ActivityObjectChanges");
            retVal.Columns.Add("ActivityId");
            retVal.Columns.Add("ObjectType");
            retVal.Columns.Add("ObjectId");
            retVal.Columns.Add("PropertyType");
            retVal.Columns.Add("PropertyValueInt");
            retVal.Columns.Add("PropertyValueString");
            retVal.Columns.Add("PropertyValueDateTime");
            retVal.Columns.Add("ChangedDate");
            return retVal;
        }

        private static DataTable CreateContactDuplicateTable()
        {
            var retVal = new DataTable("ContactDuplicates");
            retVal.Columns.Add("Id");
            retVal.Columns.Add("ContactId");
            retVal.Columns.Add("SourceTypeId");
            retVal.Columns.Add("Status");
            retVal.Columns.Add("ImportId");
            retVal.Columns.Add("DuplicateInfo");
            retVal.Columns.Add("CreatedDate");
            retVal.Columns.Add("IsNotyfiDuplicate");
            return retVal;
        }

        private static DataTable CreateLogContainerMOL()
        {
            var retVal = new DataTable("LogContainerMOL");
            retVal.Columns.Add("Id");
            retVal.Columns.Add("CreatedDate");
            retVal.Columns.Add("StatusContainerType");
            retVal.Columns.Add("StatusAddContact");
            retVal.Columns.Add("StatusContactFirst");
            retVal.Columns.Add("StatusContactAfter");
            retVal.Columns.Add("ContactId");
            retVal.Columns.Add("ChannelId");
            return retVal;
        }

        private static void UpdatePhone(int contactId, List<string> mobiles)
        {
            if (mobiles.IsNullOrEmpty()) return;
            var listPhones = PhoneRepository.GetByContact(contactId) ?? new List<PhoneInfo>();
            foreach (var mobile in mobiles
                .Where(c => !c.IsStringNullOrEmpty())
                .Where(c => !listPhones.Exists(p => p.PhoneNumber.Equals(c))))
            {
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.HomePhone))
                {
                    var phone = new PhoneInfo
                                    {
                                        IsPrimary = 0,
                                        PhoneNumber = mobile,
                                        ContactId = contactId,
                                        PhoneType = ((int)PhoneType.HomePhone).ToString(),
                                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.MobilePhone))
                {
                    var phone = new PhoneInfo
                    {
                        IsPrimary = 0,
                        PhoneNumber = mobile,
                        ContactId = contactId,
                        PhoneType = ((int)PhoneType.MobilePhone).ToString(),
                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.Tel))
                {
                    var phone = new PhoneInfo
                    {
                        IsPrimary = 0,
                        PhoneNumber = mobile,
                        ContactId = contactId,
                        PhoneType = ((int)PhoneType.Tel).ToString(),
                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
            }
        }
        private bool IsInternalDuplicate(string mobile1, string mobible2, string tel, string email)
        {
            if (!string.IsNullOrEmpty(mobile1) && internalPhoneAndEmails.Contains(mobile1)) return true;
            if (!string.IsNullOrEmpty(mobible2) && internalPhoneAndEmails.Contains(mobible2)) return true;
            if (!string.IsNullOrEmpty(tel) && internalPhoneAndEmails.Contains(tel)) return true;
            if (!string.IsNullOrEmpty(email) && internalPhoneAndEmails.Contains(email)) return true;
            return false;
        }
        private static ContactError Validate(string mobile1, string mobile2, string tel, string email)
        {
            if (string.IsNullOrEmpty(mobile1) || !Util.ValidateMobile(mobile1))
            {
                return ContactError.MobilePhoneFormat;
            }

            if (!string.IsNullOrEmpty(mobile2) && !Util.ValidateMobile(mobile2))
            {
                return ContactError.MobilePhoneFormat;
            }
            if (!string.IsNullOrEmpty(tel) && !Util.ValidateMobile(tel))
            {
                return ContactError.HomePhoneFormat;
            }
            if (!string.IsNullOrEmpty(email) && !Util.ValidateFormat(email, ImportConfig.EmailFormat))
            {
                return ContactError.EmailFormat;
            }
            return ContactError.None;
        }
        public int GetIdentity(string tableName, SqlConnection connection, SqlTransaction transaction)
        {
            int identity;

            // Get the last customer identity
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText = string.Format("SELECT IDENT_CURRENT('{0}')", tableName);
                sqlCommand.Transaction = transaction;
                identity = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }

            return identity;
        }
        #endregion

        #region Tao ContactID (code) 
        public string GetContactId()
        {
            string sContactId = "";
            
            return sContactId;
        }
        #endregion
    }
}

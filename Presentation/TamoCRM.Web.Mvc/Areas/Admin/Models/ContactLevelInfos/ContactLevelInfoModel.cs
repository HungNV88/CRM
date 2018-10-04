using System.IO;
using TamoCRM.Domain.AppointmentInterviews;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.TestResults;
using TamoCRM.Domain.TopicaAccounts;


namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos
{
    public class ContactLevelInfoModel
    {
        public string Birthday { get; set; }
        public int CallHistoryId { get; set; }
        public string RecallTime { get; set; }
        public string RecallTime24h { get; set; }
        public string Time24hWantToLearn { get; set; } //giờ muốn học
        public string AppointmentTime { get; set; }      
        public int HandoverHistoryId { get; set; }
        public string PhoneTVTS { get; set; }
        public string EmailTVTS { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ContactLevelInfo ContactLevelInfo { get; set; }
        public CasecAccountInfo CasecAccountInfo { get; set; }
        public TopicaAccountInfo TopicaAccountInfo { get; set; }
        public TestResultTopicaInfo TestResultTopicaInfo { get; set; }
        public TestResultCasecInfo TestResultCasecInfo { get; set; }
        public TestResultInterviewInfo TestResultInterviewInfo { get; set; }
        public TestResultLinkSb100Info TestResultLinkSb100Info { get; set; }
        public TestResultLinkSb100TopicaInfo TestResultLinkSb100TopicaInfo { get; set; }
        public AppointmentInterviewInfo AppointmentInterviewInfo { get; set; }
    }
}
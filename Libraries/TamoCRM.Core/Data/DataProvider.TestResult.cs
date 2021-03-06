using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader TestResult_GetInfo(int id);
        public abstract void TestResult_UpdateCasecMark(int contactId, int tuVung, int nguPhap, int ngheHieu, int chinhTa, int toeic, int levelCasec, int tongDiem, DateTime ngayThi);
        public abstract void TestResult_InsertCasecMark(int contactId, string casecAccount, int tuVung, int nguPhap, int ngheHieu, int chinhTa, int toeic, int levelCasec, int tongDiem, DateTime ngayThi);
        public abstract void TestResult_InsertTopicaMark(string userName, int tuVung, int dienDat, int ngheHieu, int chinhTa, int levelCasec);
        public abstract void TestResult_UpdateInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking);
        public abstract void TestResult_InsertInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking, string Notes);
        public abstract void TestResult_InsertInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking, string Notes, string useragent);
        public abstract void TestResult_UpdateLinkSB100(int contactId, string SB100);
        public abstract void TestResult_UpdateLinkSB100Topica(int contactId, string SB100Topica);
        public abstract IDataReader TestResult_TestHasMark(int contactId);
        public abstract IDataReader TestResult_GetAllByContactId(int contactId);
        public abstract IDataReader TestResult_Get_ResultCasec_Curent(int contactId);
        public abstract IDataReader TestResult_Get_ResultTopica_Curent(int contactId);
        public abstract IDataReader TestResult_GetAll_ResultCasec_ByContactId(int contactId);
        public abstract IDataReader TestResult_GetAll_ResultTopica_ByContactId(int contactId);
        public abstract IDataReader TestResult_Get_ResultInterview_Curent(int contactId);
        public abstract IDataReader TestResult_GetAll_ResultInterview_ByContactId(int contactId);

        public abstract IDataReader TestResult_Get_ResultLinkSb100_Curent(int contactId);
        public abstract IDataReader TestResult_GetAll_ResultLinkSb100_ByContactId(int contactId);

    }
}


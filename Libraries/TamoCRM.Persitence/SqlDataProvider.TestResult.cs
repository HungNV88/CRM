using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader TestResult_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetInfo"), id);
        }
        public override void TestResult_UpdateCasecMark(int contactId, int tuVung, int nguPhap, int ngheHieu, int chinhTa, int toeic, int levelCasec, int tongDiem, DateTime ngayThi)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_UpdateCasecMark"), contactId, tuVung, nguPhap, ngheHieu, chinhTa, toeic, levelCasec, tongDiem, ngayThi);
        }
        public override void TestResult_InsertCasecMark(int contactId, string casecAccount, int tuVung, int nguPhap, int ngheHieu, int chinhTa, int toeic, int levelCasec, int tongDiem, DateTime ngayThi)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_InsertCasecMark"), contactId, casecAccount, tuVung, nguPhap, ngheHieu, chinhTa, toeic, levelCasec, tongDiem, ngayThi);
        }
        public override void TestResult_InsertTopicaMark(string userName, int tuVung, int dienDat, int ngheHieu, int chinhTa, int levelCasec)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_InsertTopicaMark"), userName, tuVung, dienDat, ngheHieu, chinhTa, levelCasec);
        }
        public override void TestResult_UpdateInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_UpdateInterviewMark"), contactId, smooth, vocabulary, grammar, rythm, speaking, levelSpeaking);
        }
        public override void TestResult_InsertInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking, string Notes, string useragent)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_InsertInterviewMark"), contactId, smooth, vocabulary, grammar, rythm, speaking, levelSpeaking, Notes, useragent);
        }
        public override void TestResult_InsertInterviewMark(int contactId, int smooth, int vocabulary, int grammar, int rythm, float speaking, int levelSpeaking, string Notes)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_InsertInterviewMark"), contactId, smooth, vocabulary, grammar, rythm, speaking, levelSpeaking, Notes);
        }
        public override void TestResult_UpdateLinkSB100(int contactId, string SB100)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_UpdateLinkSB100"), contactId, SB100);
        }
        public override void TestResult_UpdateLinkSB100Topica(int contactId, string SB100Topica)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("TestResult_UpdateLinkSB100Topica"), contactId, SB100Topica);
        }
        public override IDataReader TestResult_TestHasMark(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_TestHasMark"), contactId);
        }
        public override IDataReader TestResult_GetAllByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetAllByContactId"), contactId);
        }
        
        public override IDataReader TestResult_Get_ResultCasec_Curent(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_Get_ResultCasec_Curent"), contactId);
        }

        public override IDataReader TestResult_Get_ResultTopica_Curent(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_Get_ResultTopica_Curent"), contactId);
        }
        public override IDataReader TestResult_GetAll_ResultCasec_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetAll_ResultCasec_ByContactId"), contactId);
        }

        public override IDataReader TestResult_GetAll_ResultTopica_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetAll_ResultTopica_ByContactId"), contactId);
        }
        public override IDataReader TestResult_Get_ResultInterview_Curent(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_Get_ResultInterview_Curent"), contactId);
        }
        public override IDataReader TestResult_GetAll_ResultInterview_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetAll_ResultInterview_ByContactId"), contactId);
        }

        public override IDataReader TestResult_Get_ResultLinkSb100_Curent(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_Get_ResultLinkSb100_Curent"), contactId);
        }
        public override IDataReader TestResult_GetAll_ResultLinkSb100_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TestResult_GetAll_ResultLinkSb100_ByContactId"), contactId);
        }
    }
}


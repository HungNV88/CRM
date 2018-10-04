using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.ContactLevelInfos
{
    public class ContactInfoMoney
    {
        public string  ContactId { get; set; }
        public string  UserEmail { get; set; }
        public string  UserPhone { get; set; }
        public string  UserName { get; set; }
        public string  Value { get; set; }
        public string  TransactionBy { get; set; }
        public string  OtherInfo { get; set; }
        public string  Reason { get; set; }
        public string Time { get; set; }
        public bool  DisableWarning { get; set; }     
                
    }

    public class ContactInfoMoneyTranfer
    {
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string Value { get; set; }
        public string Reason { get; set; }
        public string OtherInfo { get; set; }
        public bool DisableWarning { get; set; }
        public string TransactionBy { get; set; }
        public string Time { get; set; }

    }

    public class ContactInfoMoneyBalance
    {
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string Value { get; set; }
        public string Reason { get; set; }
        public string BillNumber { get; set; }      
        public string Type { get; set; }  
        public string OtherInfo { get; set; }
        public bool DisableWarning { get; set; }
        public string Time { get; set; }
        public string TransactionBy { get; set; }    
    }

    public class AllDealMoney
    {
        public List<ContactInfoMoney> pricing { get; set; }
        public List<ContactInfoMoney> deposit { get; set; }
        public List<ContactInfoMoneyTranfer> transfer { get; set; }
        public List<ContactInfoMoneyBalance> balance { get; set; }
    }


    public class ContactFixedMoney
    {
        public string Name { get; set; }
        public int Level { get; set; }      
        public string Mobile { get; set; }
        public string UserConsulant { get; set; }
        public double FeeEdu { get; set; } 
        public double FeeEduChange { get; set; }
        public double FeeReturn { get; set; }
        public string Email { get; set; }
        public string Code { get; set; } 
        public int ContactId { get; set; } //Id contacts CRM
    }

    public class LogFixedMoney
    {
        public string UserConsulantCreate { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; } //json luu thong tin giao dich nap tien
        public DateTime CreateDate { get; set; }      
    }

   public class ReturnApiCallFixedMoney
   {
       public string status { get; set; }
       public string status_code { get; set; }
       public string msg { get; set; }     
       public string core_notify_user { get; set; }      
   }
}

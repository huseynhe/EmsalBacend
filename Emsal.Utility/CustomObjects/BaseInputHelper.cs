using Emsal.Utility.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.Utility.CustomObjects
{
   public static class BaseInputHelper
    {
       public static string GetIpOrEmpty(this String ip)
       {
           string ipNumber = ip == null ? "0" : ip;
           return ipNumber;
       }

       public static string GetTransactionId(this Int64 transactionId)
       {
           string _transactionId = transactionId == 0 ? IOUtil.GetFunctionRequestID() : transactionId.ToString(); 
           return _transactionId;
       }
    }
}

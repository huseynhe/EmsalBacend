using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CodeObjects
{
   public class BaseOutput
    {
       public BaseOutput( bool _status,int _resulCode, string _resultMessage, string _viewString)
       {
           this.Status = _status;
           this.ResultCode = _resulCode;
           this.ResultMessage = _resultMessage;
           this.ViewString = _viewString;
       }
       public BaseOutput() { }
        public bool Status { get; set; }
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string ViewString { get; set; }
    }
}

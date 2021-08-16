using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Utility
{
    public class MException : Exception

    {
        public MException(string _Message, MExceptionTypes _MExceptionType, int _Code = 0)
        {
            MMessage = _Message;
            MType = _MExceptionType;
            MCode = _Code;
        }
        public MExceptionTypes MType { get;  set; }
        public string MMessage { get; set; }
        public int MCode { get; set; }

    }


   public enum MExceptionTypes { 
Unauthorized,
Fail,
NotFound

    }
}

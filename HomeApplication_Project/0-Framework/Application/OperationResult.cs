using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceded { get; set; }
        public string Message { get; set; }
        public OperationResult()
        {
            IsSucceded = false;
        }

        public OperationResult Succeded(string message = "Operation Succeeded.")
        {
            IsSucceded = true;
            Message = message;
            return this;
        }
        public OperationResult Failed(string message = "Operation Failed!")
        {
            IsSucceded = false;
            Message = message;
            return this;
        }
    }
}

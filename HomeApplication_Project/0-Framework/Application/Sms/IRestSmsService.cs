using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application.Sms
{
    public interface IRestSmsService
    {
        public string Send(string MessageText, string Mobile);
        public string FastSend(string Name, string Code, string Mobile);
    }
}

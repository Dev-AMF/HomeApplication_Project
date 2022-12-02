using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application.Sms
{
    public interface ISmsService
    {
        void Send(string number, string message);
    }
}

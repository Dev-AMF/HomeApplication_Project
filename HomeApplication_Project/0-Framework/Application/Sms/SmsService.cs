//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using IPE.SmsIrRestful;
//using System.Linq;
//using IPE.SmsIrRestful.TPL.NetCore;

//namespace _0_Framework.Application.Sms
//{
//    public class SmsService : ISmsService
//    {
//        private readonly IConfiguration _configuration;

//        public SmsService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void Send(string number, string message)
//        {
//            var token = GetToken();
//            var lines = new SmsLine().GetSmsLines(token);
//            if (lines == null) return;

//            var line = lines.SMSLines.Last().LineNumber.ToString();
//            var data = new MessageSendObject
//            {
//                Messages = new List<string>
//                    {message}.ToArray(),
//                MobileNumbers = new List<string> { number }.ToArray(),
//                LineNumber = line,
//                SendDateTime = DateTime.Now,
//                CanContinueInCaseOfError = true
//            };
//            var messageSendResponseObject =
//                new MessageSend().Send(token, data);

//            if (messageSendResponseObject.IsSuccessful) return;

//            line = lines.SMSLines.First().LineNumber.ToString();
//            data.LineNumber = line;
//            new MessageSend().Send(token, data);
//        }

//        private string GetToken()
//        {
//            var ApiKey = _configuration.GetSection("SmsSecrets")["ApiKey"];
//            var SecretKey = _configuration.GetSection("SmsSecrets")["SecretKey"];
            
//            var tokenService = new Token();
//            var token =  tokenService.GetToken(ApiKey, SecretKey);

//            return token;
//        }
//    }
//}

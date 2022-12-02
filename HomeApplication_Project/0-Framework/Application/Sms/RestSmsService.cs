using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace _0_Framework.Application.Sms
{
    public class RestSmsService : IRestSmsService
    {
        private readonly IConfiguration _configuration;

        public RestSmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FastSend(string Name, string Code, string Mobile)
        {
            var _apiKey = _configuration.GetSection("SmsSecrets")["ApiKey"];
            var _template = _configuration.GetSection("SmsSecrets")["TemplateId"];

            FastRequestBody requestBody = new FastRequestBody
            {
                mobile = long.Parse(Mobile).ToString(),
                templateId = long.Parse(_template),
                parameters = new List<FastRequestBody.Parameter>()
                {
                    new FastRequestBody.Parameter{ name = "NAME", value = Name },
                    new FastRequestBody.Parameter{ name = "CODE", value = Code },
                }

            };

            string _reruestBody = JsonConvert.SerializeObject(requestBody, Formatting.Indented);

            var client = new RestClient("https://api.sms.ir/v1/send/verify");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("X-API-KEY", _apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", _reruestBody, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response.Content;
        }



        public string Send(string MessageText, string Mobile)
        {
            var _apiKey = _configuration.GetSection("SmsSecrets")["ApiKey"];
            var _lineNumber = _configuration.GetSection("SmsSecrets")["LineNumber"];
            //var _reruestBody = @"{" + "\r\n\"lineNumber: " + (_lineNumber) + ",\r\n\"messageText\": \"" + (MessageText) + "\",\r\n\"mobiles\": [\r\n\"" + (Mobile) + "\"]\r\n}";

            RequestBody requestBody = new RequestBody
            {
                lineNumber = long.Parse(_lineNumber),
                messageText = MessageText,
                mobiles = new string[1] { Mobile }
            };

            string _reruestBody = JsonConvert.SerializeObject(requestBody, Formatting.Indented);

            var client = new RestClient("https://api.sms.ir/v1/send/bulk");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("X-API-KEY", _apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", _reruestBody, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response.Content;
        }


        class RequestBody
        {
            public long lineNumber { get; set; }
            public string messageText { get; set; }
            public string[] mobiles { get; set; }
        }
        class FastRequestBody
        {
            public string mobile { get; set; }
            public long templateId { get; set; }
            public ICollection<Parameter> parameters { get; set; }

            internal class Parameter
            {
                public string name { get; set; }
                public string value { get; set; }
            }
        }

    }
}

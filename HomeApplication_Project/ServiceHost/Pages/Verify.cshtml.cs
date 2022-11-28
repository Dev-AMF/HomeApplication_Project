using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parbad;

namespace ServiceHost.Pages
{
    public class VerifyModel : PageModel
    {
        public IPaymentVerifyResult VerifyResult { get; set; }

        public void OnGet(IPaymentVerifyResult verifyResult)
        {
            VerifyResult = verifyResult;   
        }
    }
}

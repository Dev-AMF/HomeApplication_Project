using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    //[BindProperties]
    public class UsersModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }

        [TempData]
        public string RegisterMessage { get; set; }

        [BindProperty]  
        public CreateAccount CreateAccountViewModel { get; set; }

        private readonly IAccountApplication _accountApplication;

        public UsersModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            CreateAccountViewModel = new CreateAccount();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSucceded)
                return RedirectToPage("/Index");

            LoginMessage = result.Message;
            return RedirectToPage("/Users");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        
        public IActionResult OnPostRegister()
        {
            var result = _accountApplication.Register(CreateAccountViewModel);
            RegisterMessage = result.Message;
            return RedirectToPage("/Users");
        }
    }
}

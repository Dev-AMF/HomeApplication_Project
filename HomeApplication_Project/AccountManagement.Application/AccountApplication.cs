using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IRoleRepository roleRepository, IPasswordHasher passwordHasher,IAccountRepository accountRepository,
                                  IAuthHelper authHelper,IFileUploader fileUploader)
                                  
        {
            _authHelper = authHelper;
            _fileUploader = fileUploader;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);

            _accountRepository.Save();
            return operation.Succeded();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(AT => AT.Username == command.Username || AT.MobileNo == command.MobileNo))
                return operation.Failed(ApplicationMessages.RecordAlreadyExists);


            var password = _passwordHasher.Hash(command.Password);
            var picturePath = _fileUploader.Upload(command.ProfilePhoto,command.MobileNo ,"ProfilePhotos");

            var account = new Account(command.Fullname, command.Username, password, command.MobileNo, command.RoleId, picturePath);
            _accountRepository.Create(account);

            _accountRepository.Save();
            return operation.Succeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(AT => (AT.Username == command.Username || AT.MobileNo == command.MobileNo) && AT.Id != command.Id))
                return operation.Failed(ApplicationMessages.RecordAlreadyExists);

            if (command.ProfilePhoto == null)
            {
                account.Edit(command.Fullname, command.Username, command.MobileNo, command.RoleId, command.ProfilePhotoPath);
            }
            else
            {
                var picturePath = _fileUploader.Upload(command.ProfilePhoto, command.MobileNo, "ProfilePhotos");
                account.Edit(command.Fullname, command.Username, command.MobileNo, command.RoleId, picturePath);
            }

            _accountRepository.Save();
            return operation.Succeded();
        }

        public AccountViewModel GetAccountBy(int id)
        {
            var account = _accountRepository.Get(id);
            return new AccountViewModel()
            {
                Fullname = account.Fullname,
                MobileNo = account.MobileNo,

            };
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public EditAccount GetDetails(int id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);

            if (account == null)
            { 
                return operation.Failed(ApplicationMessages.WrongUserOrPass); //Deos Not Exists
            }
            else
            {
                (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

                if (result.Verified)
                {
                    var permissions = _roleRepository.Get(account.RoleId)
                                                     .Permissions
                                                     .Select(P => P.Code)
                                                     .ToList();

                    var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username, account.MobileNo, 
                                                          permissions);

                    _authHelper.Signin(authViewModel);

                    return operation.Succeded();
                }
            }
                return operation.Failed(ApplicationMessages.WrongUserOrPass); //Wrong User Pass
            
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public OperationResult Register(CreateAccount command)
        {
            command.RoleId = 3; // Website's User
            return Create(command);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    
    }
}

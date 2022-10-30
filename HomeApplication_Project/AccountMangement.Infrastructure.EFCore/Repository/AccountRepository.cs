using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountMangement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<int, Account>, IAccountRepository
    {
        private readonly At_HomeApplicationAccountContext _context;

        public AccountRepository(At_HomeApplicationAccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public EditAccount GetDetails(int id)
        {
            return _context.Accounts.Select(AT => new EditAccount
            {
                Id = AT.Id,
                Fullname = AT.Fullname,
                MobileNo = AT.MobileNo,
                RoleId = AT.RoleId,
                Username = AT.Username,
                ProfilePhotoPath = AT.ProfilePhoto,
            }).FirstOrDefault(AT => AT.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                                .Include(AT => AT.Role)
                                .Select(AT => new AccountViewModel
                                {
                                    Id = AT.Id,
                                    Fullname = AT.Fullname,
                                    MobileNo = AT.MobileNo,
                                    ProfilePhoto = AT.ProfilePhoto,
                                    RoleName = AT.Role.Name,
                                    RoleId = AT.RoleId,
                                    Username = AT.Username,
                                    CreationDate = AT.CreationDate.ToFarsi()
                                });

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
                query = query.Where(AT => AT.Fullname.Contains(searchModel.Fullname));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(AT => AT.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.MobileNo))
                query = query.Where(AT => AT.MobileNo.Contains(searchModel.MobileNo));

            if (searchModel.RoleId > 0)
                query = query.Where(AT => AT.RoleId == searchModel.RoleId);

            return query.OrderByDescending(AT => AT.Id).ToList();
        }
    }
}

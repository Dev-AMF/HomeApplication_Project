using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;
using System.Linq;

namespace AccountMangement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<int, Role>, IRoleRepository
    {
        private readonly At_HomeApplicationAccountContext _context;

        public RoleRepository(At_HomeApplicationAccountContext accountContext) : base(accountContext)
        {
            _context = accountContext;
        }

        public EditRole GetDetails(int id)
        {
            return _context.Roles
                           .Select(R => new EditRole
                           {
                               Id = R.Id,
                               Name = R.Name

                           }).FirstOrDefault(R => R.Id == id);
        }

        public List<RoleViewModel> List()
        {
            return _context.Roles
                           .Select(R => new RoleViewModel
                           {
                               Id = R.Id,
                               Name = R.Name,
                               CreationDate = R.CreationDate.ToFarsi()
                           }).ToList();
        }
    }
}

using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
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
            var role = _context.Roles
                            .Select(R => new EditRole
                            {
                                Id = R.Id,
                                Name = R.Name,
                                PermissionDtos = MapPermissions(R.Permissions)

                            }).AsNoTracking()
                            .FirstOrDefault(R => R.Id == id);

            role.PermissionIds = role.PermissionDtos.Select(Pdt => Pdt.Code).ToList();

            return role;
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

        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(P => new PermissionDto(P.Code, P.Name))
                              .ToList();
        }
    }
}

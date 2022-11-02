using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Role
{
    public class EditRole : CreateRole
    {
        public int Id { get; set; }
        public List<PermissionDto> PermissionDtos { get; set; }

        public EditRole()
        {
            PermissionIds = new List<int>();
        }
    }

}

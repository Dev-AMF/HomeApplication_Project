using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _repository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.RecordAlreadyExists);

            var role = new Role(command.Name, new List<Permission>());
            _repository.Create(role);
            _repository.Save();
            
            return operation.Succeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _repository.Get(command.Id);
            if (role == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.RecordAlreadyExists);

            var permissions = new List<Permission>();
            command.PermissionIds.ForEach(code => permissions.Add(new Permission(code)));

            role.Edit(command.Name, permissions);
            _repository.Save();

            return operation.Succeded();
        }

        public EditRole GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<RoleViewModel> List()
        {
            return _repository.List();
        }
    }
}

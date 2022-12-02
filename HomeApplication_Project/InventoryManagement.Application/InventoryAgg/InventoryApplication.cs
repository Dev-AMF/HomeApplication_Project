using _0_Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.InventoryAgg
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _repository;
        private readonly IAuthHelper _authHelper;

        public InventoryApplication(IInventoryRepository repository, IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
        {

            var result = new OperationResult();

            if (_repository.Exists(I => I.ProductId == command.ProductId))
            {
                result.Failed(ApplicationMessages.RecordAlreadyExistsNonArgument);
            }
            else
            {
                var inventory = new Inventory(command.ProductId, command.UnitPrice);

                _repository.Create(inventory);
                _repository.Save();

                result.Succeded();
            }
            return result;
        }

        public OperationResult Decrease(DecreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _repository.Get(command.InventoryId);

            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var operatorId = _authHelper.CurrentAccountId();
            inventory.Decrease(command.Count, operatorId, command.Description, 0);
            
            _repository.Save();
            return operation.Succeded();
        }

        public OperationResult Decrease(List<DecreaseInventory> command)
        {

            var operation = new OperationResult();
            var operatorId = _authHelper.CurrentAccountId();


            foreach (var item in command)
            {
                var inventory = _repository.GetBy(item.ProductId);
                inventory.Decrease(item.Count, operatorId, item.Description, item.OrderId);
            }

            _repository.Save();

            return operation.Succeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var result = new OperationResult();
            var inventory = _repository.Get(command.Id);

            if (inventory != null)
            {
                result.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_repository.Exists(I => I.ProductId == command.ProductId &&
                                   I.Id != command.Id))// 
            {
                result.Failed(ApplicationMessages.RecordAlreadyExistsNonArgument);
            }
            else
            {
                inventory.Edit(command.ProductId, command.UnitPrice);

                _repository.Save();
                result.Succeded();
            }
            return result;
        }

        public EditInventory GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationsLog(int inventoryId)
        {
            return _repository.GetOperationsLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _repository.Get(command.InventoryId);
            
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var operatorId = _authHelper.CurrentAccountId();

            inventory.Increase(command.Count, operatorId, command.Description);
            
            _repository.Save();
            return operation.Succeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}

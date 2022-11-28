using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountMangement.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<int, Inventory>, IInventoryRepository
    {
        private readonly At_HomeApplicationInventoryContext _context;
        private readonly At_HomeApplicationContext _maincontext;
        private readonly At_HomeApplicationAccountContext _accountContext;

        public InventoryRepository(At_HomeApplicationInventoryContext context, At_HomeApplicationContext maincontext,
            At_HomeApplicationAccountContext accountContext) : base(context)
        {
            _context = context;
            _maincontext = maincontext;
            _accountContext = accountContext;
        }

        public Inventory GetBy(int productId)
        {
            return _context.Inventory.FirstOrDefault(I => I.ProductId == productId);
        }

        public EditInventory GetDetails(int id)
        {
            return _context.Inventory.Select(I => new EditInventory 
                   { 
                       Id = I.Id,
                       ProductId = I.ProductId,
                       UnitPrice = I.UnitPrice
                   }).FirstOrDefault(I => I.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationsLog(int inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(AC => new { AC.Id, AC.Fullname }).ToList();
            var inventory = _context.Inventory.FirstOrDefault(I=> I.Id == inventoryId);
            var operations = inventory.Operations.Select(I=> new InventoryOperationViewModel
            {
                Id = I.Id,
                Count = I.Count,
                CurrentCount = I.CurrentCount,
                Description = I.Description,
                Operation = I.Operation,
                OperationDate = I.OperationDate.ToFarsi(),
                OperatorId = I.OperatorId,
                OrderId = I.OrderId
            })
            .OrderByDescending(I => I.Id)
            .ToList();

            foreach (var item in operations)
            {
                item.Operator = accounts.FirstOrDefault(AC => AC.Id == item.OperatorId)?.Fullname;
            }

            return operations;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _maincontext.Products.Select(P => new { P.Id, P.Name }).ToList();

            var query = _context.Inventory
                .Select(I => new InventoryViewModel
                {
                    Id = I.Id,
                    UnitPrice = I.UnitPrice,
                    InStock = I.InStock,
                    ProductId = I.ProductId,
                    CurrentCount = I.CalculateCurrentCount(),
                    CreationDate = I.CreationDate.ToFarsi()
                });

            if (searchModel.ProductId > 0)
                query.Where(I => I.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query.Where(I => !I.InStock);

            var results = query.ToList();

            results.ForEach(query => query.ProductName = products.FirstOrDefault(P => P.Id == query.ProductId)?.Name);


            return results;
        }
    }
}

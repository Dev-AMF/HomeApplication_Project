using _0_Framework.Infrastructure;
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

        public InventoryRepository(At_HomeApplicationInventoryContext context, At_HomeApplicationContext maincontext) : base(context)
        {
            _context = context;
            _maincontext = maincontext;
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

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var inventory = _context.Inventory;
            var products = _maincontext.Products;


            var result = inventory.Join(products,
                          query => query.ProductId,
                          products => products.Id,
                          (query, products) => new InventoryViewModel
                          {
                              Id = query.Id,
                              UnitPrice = query.UnitPrice,
                              InStock = query.InStock,
                              ProductId = query.ProductId,
                              ProductName = products.Name,
                              CurrentCount = query.CalculateCurrentCount()
                          });

            if (searchModel.ProductId > 0)
                result.Where( I => I.ProductId == searchModel.ProductId );

            if (! searchModel.InStock)
                result.Where(I => ! I.InStock);


            return result.ToList();
        }
    }
}

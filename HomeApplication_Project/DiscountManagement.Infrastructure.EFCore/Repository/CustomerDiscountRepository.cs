using _0_Framework.Infrastructure;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerAgg;
using DiscountManagement.Domain.CustomerAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<int, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly At_HomeApplicationDiscountContext _context;
        private readonly At_HomeApplicationContext _ShopContext;
        public CustomerDiscountRepository(At_HomeApplicationDiscountContext context, At_HomeApplicationContext shopContext) : base(context)
        {
            _context = context;
            _ShopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts
            .Select(CD => new EditCustomerDiscount
            {
                Id = CD.Id,
                ProductId = CD.ProductId,
                DiscountRate = CD.DiscountRate,
                StartDate = CD.StartDate.ToString(),
                EndDate = CD.EndDate.ToString(),
                Description = CD.Description

            }).FirstOrDefault(CD=> CD.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _ShopContext.Products.Select(P => new { P.Id, P.Name }).ToList();

            var query = _context.CustomerDiscounts
                .Select(CD => new CustomerDiscountViewModel
            {
                Id = CD.Id,
                DiscountRate = CD.DiscountRate,
                EndDate = CD.EndDate.ToFarsi(),
                StartDate = CD.StartDate.ToFarsi(),
                ProductId = CD.ProductId,
                Description = CD.Description,
                CreationDate = CD.CreationDate.ToFarsi()
                }); 

            if (searchModel.ProductId > 0)
            {
                query.Where(CD => CD.ProductId == searchModel.ProductId);
            }
            if (String.IsNullOrEmpty(searchModel.StartDate))
            {
                query.Where(CD => CD.StartDate.ToGeorgianDateTime() < searchModel.StartDate.ToGeorgianDateTime() );
            }
            if (String.IsNullOrEmpty(searchModel.EndDate))
            {
                query.Where(CD => CD.EndDate.ToGeorgianDateTime() > searchModel.EndDate.ToGeorgianDateTime() );
            }

            var results = query.ToList();
            
            results.ForEach( query => query.ProductName = products.FirstOrDefault(P => P.Id == query.ProductId)?.Name );


            return results;
        }
    }
}

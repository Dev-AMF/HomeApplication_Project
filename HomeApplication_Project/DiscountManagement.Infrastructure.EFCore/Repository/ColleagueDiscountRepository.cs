using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueAgg;
using DiscountManagement.Domain.ColleagueAgg;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<int, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly At_HomeApplicationDiscountContext _context;
        private readonly At_HomeApplicationContext _shopContext;

        public ColleagueDiscountRepository(At_HomeApplicationDiscountContext context, At_HomeApplicationContext shopContext) :base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(int id)
        {
            return _context.ColleagueDiscounts.Select(COD => new EditColleagueDiscount
            {
                Id = COD.Id,
                DiscountRate = COD.DiscountRate,
                ProductId = COD.ProductId
            }).FirstOrDefault(COD => COD.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(P => new { P.Id, P.Name }).ToList();

            var query = _context.ColleagueDiscounts.Select(COD => new ColleagueDiscountViewModel
            {
                Id = COD.Id,
                CreationDate = COD.CreationDate.ToFarsi(),
                DiscountRate = COD.DiscountRate,
                ProductId = COD.ProductId,
                IsRemoved = COD.IsRemoved
            });

            if (searchModel.ProductId > 0)
                query = query.Where(COD => COD.ProductId == searchModel.ProductId);

            var results = query.ToList();

            results.ForEach(query => query.ProductName = products.FirstOrDefault(P => P.Id == query.ProductId)?.Name);


            return results;
        }
    }
}

using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Product;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly At_HomeApplicationContext _context;
        private readonly At_HomeApplicationInventoryContext _inventoryContext;
        private readonly At_HomeApplicationDiscountContext _discountContext;

        public ProductQuery(At_HomeApplicationContext context, At_HomeApplicationInventoryContext inventoryContext, At_HomeApplicationDiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestProductsBy(int count)
        {
            var inventory = _inventoryContext.Inventory.Select(I => new { I.ProductId, I.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                            .Where(CD => CD.StartDate < DateTime.Now && CD.EndDate > DateTime.Now)//Active Discounts
                            .Select(I => new { I.ProductId, I.DiscountRate , I.EndDate})
                            .ToList();

            var Pqm = _context.Products
                           .Include(P => P.Picture)
                           .Include(P => P.Metas)
                           .Include(P => P.Category)
                           .Select(P => new ProductQueryModel
                           {
                               Id = P.Id,
                               Name = P.Name,
                               Picture = P.Picture.Path,
                               PictureAlt = P.Picture.Alt,
                               PictureTitle = P.Picture.Title,
                               Slug = P.Metas.Slug,
                               CategoryName = P.Category.Name,

                           }).OrderByDescending(Pqm => Pqm.Id )
                           .Take(count)
                           .ToList();


            foreach (var item in Pqm)
            {
                if (inventory.FirstOrDefault(I => I.ProductId == item.Id) != null)
                {
                    var UnitPrice = inventory.FirstOrDefault(I => I.ProductId == item.Id).UnitPrice;
                    item.Price = UnitPrice.ToMoney();

                    if (discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id) != null)
                    {
                        var itemDiscount = discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id);
                        item.DiscountRate = itemDiscount.DiscountRate;

                        item.HasDiscount = item.DiscountRate > 0;

                        item.DiscountExpireDate = itemDiscount.EndDate.ToDiscountFormat();

                        var DiscountAmount = Math.Round((item.DiscountRate * UnitPrice) / 100);

                        item.PriceWithDiscount = (UnitPrice - DiscountAmount).ToMoney();
                    }
                }
            }

            return Pqm;
        }


        public List<ProductQueryModel> GetProductsBy(int categoryId)
        {
            var inventory = _inventoryContext.Inventory.Select(I => new { I.ProductId, I.UnitPrice }).ToList();
            
            var discounts = _discountContext.CustomerDiscounts
                            .Where(CD => CD.StartDate < DateTime.Now && CD.EndDate > DateTime.Now)//Active Discounts
                            .Select(I => new { I.ProductId, I.DiscountRate, I.EndDate })
                            .ToList();

            var Pqm = _context.Products
                           .Include(P => P.Picture)
                           .Include(P => P.Metas)
                           .Include(P => P.Category)
                           .Where(P => P.CategoryId == categoryId)
                           .Select(P => new ProductQueryModel
                           {
                               Id = P.Id,
                               Name = P.Name,
                               Picture = P.Picture.Path,
                               PictureAlt = P.Picture.Alt,
                               PictureTitle = P.Picture.Title,
                               Slug = P.Metas.Slug,
                               CategoryName = P.Category.Name

                           }).ToList();


            foreach (var item in Pqm)
            {
                if (inventory.FirstOrDefault(I => I.ProductId == item.Id) != null)
                {
                    var UnitPrice = inventory.FirstOrDefault(I => I.ProductId == item.Id).UnitPrice;
                    item.Price = UnitPrice.ToMoney();

                    if (discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id) != null)
                    {
                         var itemDiscount =  discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id);
                        item.DiscountRate = itemDiscount.DiscountRate;

                        item.HasDiscount = item.DiscountRate > 0;

                        item.DiscountExpireDate = itemDiscount.EndDate.ToDiscountFormat();

                        var DiscountAmount = Math.Round((item.DiscountRate * UnitPrice) / 100);

                        item.PriceWithDiscount = (UnitPrice - DiscountAmount).ToMoney();
                    }
                }   
            }

            return Pqm;
        }


        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(I => new { I.ProductId, I.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                            .Where(CD => CD.StartDate < DateTime.Now && CD.EndDate > DateTime.Now)//Active Discounts
                            .Select(I => new { I.ProductId, I.DiscountRate, I.EndDate })
                            .ToList();

            var Pqm = _context.Products
                           .Include(P => P.Picture)
                           .Include(P => P.Metas)
                           .Include(P => P.Category)
                           .Include(P => P.Category.Metas)
                           .Select(P => new ProductQueryModel
                           {
                               Id = P.Id,
                               Name = P.Name,
                               Picture = P.Picture.Path,
                               PictureAlt = P.Picture.Alt,
                               PictureTitle = P.Picture.Title,
                               Slug = P.Metas.Slug,
                               CategoryName = P.Category.Name,
                               CategorySlug = P.Category.Metas.Slug,
                               ShortDescription = P.ShortDescription

                           }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                Pqm = Pqm.Where(Pqm => Pqm.Name.Contains(value) || Pqm.ShortDescription.Contains(value));

            var ListedPqm = Pqm.OrderByDescending(Pqm => Pqm.Id).ToList(); 

            foreach (var item in ListedPqm)
            {
                if (inventory.FirstOrDefault(I => I.ProductId == item.Id) != null)
                {
                    var UnitPrice = inventory.FirstOrDefault(I => I.ProductId == item.Id).UnitPrice;
                    item.Price = UnitPrice.ToMoney();

                    if (discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id) != null)
                    {
                        var itemDiscount = discounts.FirstOrDefault(Cd => Cd.ProductId == item.Id);
                        item.DiscountRate = itemDiscount.DiscountRate;

                        item.HasDiscount = item.DiscountRate > 0;

                        item.DiscountExpireDate = itemDiscount.EndDate.ToDiscountFormat();

                        var DiscountAmount = Math.Round((item.DiscountRate * UnitPrice) / 100);

                        item.PriceWithDiscount = (UnitPrice - DiscountAmount).ToMoney();
                    }
                }
            }

            return ListedPqm;
        }

        public List<ProductQueryModel> GetProducts()
        {
            return GetLatestProductsBy(int.MaxValue);
        }
    }
}

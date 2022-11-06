using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Comment;
using Query.Contracts.Product;
using Query.Contracts.ProductPictureSlider;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductAgg;
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
        private readonly IProductPictureSliderQuery _sliderQuery;
        private readonly IProductRepository _repository;
        private readonly ICommentQuery _commentQuery;

        public ProductQuery(At_HomeApplicationContext context,
                            At_HomeApplicationInventoryContext inventoryContext,
                            At_HomeApplicationDiscountContext discountContext,
                            IProductPictureSliderQuery sliderQuery,
                            IProductRepository repository,
                            ICommentQuery commentQuery )
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _sliderQuery = sliderQuery;
            _commentQuery = commentQuery;
            _repository = repository;
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

        public ProductQueryModel GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(I => new { I.ProductId, I.UnitPrice, I.InStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                            .Where(CD => CD.StartDate < DateTime.Now && CD.EndDate > DateTime.Now)//Active Discounts
                            .Select(I => new { I.ProductId, I.DiscountRate, I.EndDate })
                            .ToList();

            var Pqm = _context.Products
                           //.Include(P => P.Picture)
                           //.Include(P => P.Metas)
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
                               CategorySlug = P.Category.Metas.Slug,
                               Code = P.Code,
                               Description = P.Description,
                               Keywords = P.Metas.Keywords,
                               MetaDescription = P.Metas.MetaDescription,
                               ShortDescription = P.ShortDescription,

                           }).FirstOrDefault(Pqm => Pqm.Slug == slug);
                            

            if (Pqm == null)
                return new ProductQueryModel();

            Pqm.PictursSlider = _sliderQuery.GetPicturesSliderByProduct(Pqm.Id);
            Pqm.Comments = _commentQuery.GetCommentsByProduct(Pqm.Id);

            var productInventory = inventory.FirstOrDefault(I => I.ProductId == Pqm.Id);

                if (productInventory != null)
                {
                    var UnitPrice = productInventory.UnitPrice;
                    Pqm.IsInStock = productInventory.InStock;

                
                    Pqm.Price = UnitPrice.ToMoney();
                    Pqm.NumericPrice = UnitPrice;
                    

                if (discounts.FirstOrDefault(Cd => Cd.ProductId == Pqm.Id) != null)
                    {
                        var PqmDiscount = discounts.FirstOrDefault(Cd => Cd.ProductId == Pqm.Id);
                        Pqm.DiscountRate = PqmDiscount.DiscountRate;

                        Pqm.HasDiscount = Pqm.DiscountRate > 0;

                        Pqm.DiscountExpireDate = PqmDiscount.EndDate.ToDiscountFormat();

                        var DiscountAmount = Math.Round((Pqm.DiscountRate * UnitPrice) / 100);

                        Pqm.PriceWithDiscount = (UnitPrice - DiscountAmount).ToMoney();
                    }
                }

            return Pqm;
        }


        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory.ToList();

            foreach (var cartItem in cartItems.Where(cartItem =>
                inventory.Any(I => I.ProductId == cartItem.Id && I.InStock)))
            {
                var itemInventory = inventory.Find(I => I.ProductId == cartItem.Id);
                cartItem.IsInStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
            }

            return cartItems;
        }

        public string GetSlugBy(int id)
        {
            return _repository.GetSlugBy(id);
        }
    }
}

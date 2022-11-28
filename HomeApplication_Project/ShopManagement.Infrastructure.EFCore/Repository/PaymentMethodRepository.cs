using _0_Framework.Infrastructure;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class PaymentMethodRepository : RepositoryBase<int, PaymentMethod>, IPaymentMethodRepository
    {
        private readonly At_HomeApplicationContext _context;

        public PaymentMethodRepository(At_HomeApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

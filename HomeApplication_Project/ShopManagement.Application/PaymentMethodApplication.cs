using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class PaymentMethodApplication : IPaymentMethodApplication<int , PaymentMethod>
    {
        private readonly IPaymentMethodRepository _repository;
        public PaymentMethodApplication(IPaymentMethodRepository repository)
        {
            _repository = repository;
        }

        public PaymentMethod GetBy(int id)
        {
            return _repository.Get(id);
        }

        public List<PaymentMethod> GetAll()
        {
            return _repository.GetAll();
        }
    }
}

using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace ShopManagement.Application.Contracts.Order
{
    public interface IPaymentMethodApplication <Tkey, TEntity> where TEntity : EntityBase
    {
        public TEntity GetBy(Tkey id);

        public List<TEntity> GetAll();
    }
}

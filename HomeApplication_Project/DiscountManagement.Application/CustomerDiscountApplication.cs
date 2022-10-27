using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerAgg;
using DiscountManagement.Domain.CustomerAgg;
using System;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _repository;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var result = new OperationResult();

            if (_repository.Exists(CD => CD.ProductId ==  command.ProductId && CD.DiscountRate == command.DiscountRate))
            {
                result.Failed(ApplicationMessages.RecordAlreadyExistsNonArgument);
            }
            else
            {
                var discount = new CustomerDiscount(command.ProductId, command.DiscountRate, 
                                                    command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), 
                                                    command.Description);
                _repository.Create(discount);
                _repository.Save();

                result.Succeded();
            }
            return result;
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var result = new OperationResult();
            var discount = _repository.Get(command.Id);

            if (discount != null)
            {
                result.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_repository.Exists(CD => CD.ProductId == command.ProductId && 
                                   CD.DiscountRate == command.DiscountRate && 
                                   CD.Id != command.Id))// ثبت یک کد تخفیف با درصد تکراری برای یک کالا 
            {
                result.Failed(ApplicationMessages.RecordAlreadyExistsNonArgument);
            }
            else
            {
                discount.Edit(command.ProductId, command.DiscountRate,command.StartDate.ToGeorgianDateTime(),
                              command.EndDate.ToGeorgianDateTime(), command.Description);

                _repository.Save();
                result.Succeded();
            }
            return result;
        }

        public EditCustomerDiscount GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}

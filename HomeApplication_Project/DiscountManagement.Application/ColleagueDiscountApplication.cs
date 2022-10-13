using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueAgg;
using DiscountManagement.Domain.ColleagueAgg;
using System;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _repository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {

            var result = new OperationResult();

            if (_repository.Exists(COD => COD.ProductId == command.ProductId && COD.DiscountRate == command.DiscountRate))
            {
                result.Failed(ApplicationMessage.RecordAlreadyExistsNonArgument);
            }
            else
            {
                var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

                _repository.Create(discount);
                _repository.Save();

                result.Succeded();
            }
            return result;
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var result = new OperationResult();
            var discount = _repository.Get(command.Id);

            if (discount != null)
            {
                result.Failed(ApplicationMessage.RecordNotFound);
            }
            if (_repository.Exists(COD => COD.ProductId == command.ProductId &&
                                   COD.DiscountRate == command.DiscountRate &&
                                   COD.Id != command.Id))// ثبت یک کد تخفیف با درصد تکراری برای یک کالا 
            {
                result.Failed(ApplicationMessage.RecordAlreadyExistsNonArgument);
            }
            else
            {
                discount.Edit(command.ProductId, command.DiscountRate);

                _repository.Save();
                result.Succeded();
            }
            return result;
        }

        public EditColleagueDiscount GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);
            
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            colleagueDiscount.Remove();

            _repository.Save();

            return operation.Succeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);

            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            colleagueDiscount.Restore();

            _repository.Save();

            return operation.Succeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}

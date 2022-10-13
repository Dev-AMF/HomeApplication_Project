using _0_Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts.ColleagueAgg
{
    public interface IColleagueDiscountApplication
    {

        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditColleagueDiscount GetDetails(int id);

        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}

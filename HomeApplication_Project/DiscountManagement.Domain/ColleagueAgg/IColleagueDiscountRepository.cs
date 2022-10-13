using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueAgg;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueAgg
{
    public interface IColleagueDiscountRepository : IRepository<int, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(int id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}

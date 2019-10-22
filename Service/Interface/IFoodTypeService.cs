using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public interface IFoodTypeService
    {
        Guid Add(FoodTypeViewModel foodTypeViewModel);
        void Delete(Guid id);
        FoodTypeViewModel GetById(Guid id);
        IList<FoodTypeViewModel> GetAll();
        Guid Update(Guid id, FoodTypeViewModel foodTypeViewModel);
    }
}

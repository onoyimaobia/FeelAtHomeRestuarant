using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public interface ICategoryTypeService
    {
        Guid Add(CategoryTypeViewModel categoryTypeViewModel);
        void Delete(Guid id);
        CategoryTypeViewModel GetById(Guid id);
        IList<CategoryTypeViewModel> GetAll();
        Guid Update(Guid id,CategoryTypeViewModel categoryTypeViewModel);
    }
}

using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface ICategoryTypeRepo
    {
        Guid Add(CategoryType categoryType);
        void Delete(CategoryType categoryType);
        CategoryType GetById(Guid id);
        IList<CategoryType> GetAll();
        Guid Update(CategoryType categoryType);
    }
}

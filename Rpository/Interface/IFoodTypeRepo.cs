using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
   public interface IFoodTypeRepo
    {
        Guid Add(FoodType foodType);
        void Delete(FoodType foodType);
        FoodType GetById(Guid id);
        IList<FoodType> GetAll();
        Guid Update(FoodType foodType);
    }
}

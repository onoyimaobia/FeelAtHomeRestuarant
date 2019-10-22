using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class FoodTypeRepo : IFoodTypeRepo
    {
        private readonly ApplicationDbContext _context;
        public FoodTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid Add(FoodType foodType)
        {
            
            _context.Add(foodType);
            _context.SaveChanges();
            return foodType.FoodTypeId;
        }

        public void Delete(FoodType foodType)
        {
            _context.Remove(foodType);
            _context.SaveChanges();
        }

        public IList<FoodType> GetAll()
        {
            return _context.FoodTypes.ToList();
        }

        public FoodType GetById(Guid id)
        {
            return _context.FoodTypes.SingleOrDefault(x => x.FoodTypeId == id);
        }

        public Guid Update(FoodType foodType)
        {
            _context.Update(foodType);
            _context.SaveChanges();
            return foodType.FoodTypeId;
        }

        
    }
}

using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class CategoryTypeRepository : ICategoryTypeRepo
    {
        private readonly ApplicationDbContext _context;
        public CategoryTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid Add(CategoryType categoryType)
        {
            _context.Add(categoryType);
            _context.SaveChanges();
            return categoryType.CategoryTypeId;
        }

        public void Delete(CategoryType categoryType)
        {
            _context.Remove(categoryType);
            _context.SaveChanges(); 
        }

        public IList<CategoryType> GetAll()
        {
           return  _context.CategoryTypes.ToList();
        }

        public CategoryType GetById(Guid id)
        {
            return _context.CategoryTypes.SingleOrDefault(x => x.CategoryTypeId == id);
        }

        public Guid Update(CategoryType categoryType)
        {
            _context.Update(categoryType);
            _context.SaveChanges();
            return categoryType.CategoryTypeId;
        }
    }
}

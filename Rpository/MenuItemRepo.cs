using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class MenuItemRepo : IMenuItemRepo
    { 
        private readonly ApplicationDbContext _context;
        public MenuItemRepo (ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid Add(MenuItem menuItem)
        {
            _context.Add(menuItem);
            _context.SaveChanges();
            return menuItem.MenuItemId;
        }

        public void Delete(MenuItem menuItem)
        {
            _context.Remove(menuItem);
            _context.SaveChanges();

        }

        public IList<MenuItem> GetAll()
        {
            return _context.MenuItems.Include(m => m.FoodType).Include(m => m.CategoryType).OrderBy(i => i.Name).ToList();
        }

        public MenuItem GetById(Guid id)
        {
            return _context.MenuItems.Include(m => m.FoodType).Include(m => m.CategoryType).SingleOrDefault(m => m.MenuItemId == id);
        }

        public Guid Update(MenuItem menuItem)
        {
            _context.Update(menuItem);
            _context.SaveChanges();
            return menuItem.MenuItemId;
        }
    }
}

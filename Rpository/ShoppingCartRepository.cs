using FeelAtHomeRestaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FeelAtHomeRestaurant.Rpository
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid Add(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            _context.SaveChanges();
            return shoppingCart.ShoppingCartId;
        }

        public void Delete(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public IList<ShoppingCart> GetAll(string userId)
        {
            var usercartList = _context.ShoppingCarts.Where(c => c.ApplicationUserId == userId).ToList();
            return usercartList;
        }

        public ShoppingCart GetById(Guid id)
        {
           var cartId = _context.ShoppingCarts.Where(c => c.ShoppingCartId == id).FirstOrDefault();
            return cartId;
        }
        public ShoppingCart AddCart(Guid id)
        {
            var addcart = _context.ShoppingCarts.Where(c => c.ShoppingCartId == id).FirstOrDefault();
            addcart.Count += 1;
            _context.ShoppingCarts.Update(addcart);
            _context.SaveChanges();
            return addcart;

        }
        public ShoppingCart RemoveCart(Guid id)
        {
            var removecart = _context.ShoppingCarts.Where(c => c.ShoppingCartId == id).FirstOrDefault();
            if(removecart.Count == 1)
            {
                _context.ShoppingCarts.Remove(removecart);
                _context.SaveChanges();
                //var countRemainingCart = _context.ShoppingCarts.Where(u => u.ApplicationUserId == removecart.ApplicationUserId).ToList().Count;
               
            }
            else
            {
                removecart.Count -= 1;
                _context.ShoppingCarts.Update(removecart);
                _context.SaveChanges();
            }
            return removecart;
        }
        public int GetUserId(string userId)
        {
            var Cart = _context.ShoppingCarts.Where(c => c.ApplicationUserId == userId).ToList().Count();
            return Cart;
        }
        public ShoppingCart CheckForExist(string userId, Guid menuItemId)
        {
          var Exist =  _context.ShoppingCarts.Where(c => c.ApplicationUserId == userId && c.MenuItemId == menuItemId).FirstOrDefault();
            return Exist;
        }
        public Guid Update(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            _context.SaveChanges();
            return shoppingCart.ShoppingCartId;
        }
        public MenuItem GetItemById(Guid id)
        {
            return _context.MenuItems.Include(m => m.FoodType).Include(m => m.CategoryType).SingleOrDefault(m => m.MenuItemId == id);
        }
    }
}

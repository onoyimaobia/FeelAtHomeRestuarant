using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface IShoppingCartRepository
    {
        Guid Add(ShoppingCart shoppingCart);
        void Delete(ShoppingCart shoppingCart);
        ShoppingCart GetById(Guid id);
        ShoppingCart AddCart(Guid id);
        ShoppingCart RemoveCart(Guid id);
        int GetUserId(string id);
        IList<ShoppingCart> GetAll(string userId);
        Guid Update(ShoppingCart shoppingCart);
        MenuItem GetItemById(Guid id);
        ShoppingCart CheckForExist(string userId, Guid menuItemId);
    }
}

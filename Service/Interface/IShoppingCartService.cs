using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public interface IShoppingCartService
    {
        Guid Add(ShoppingCartViewModel shoppingCartViewModel);
        void Delete(Guid id);
        ShoppingCartViewModel GetById(Guid id);
        IList<ShoppingCart> GetAll();
        Guid Update(ShoppingCartViewModel shoppingCartViewModel);
        ShoppingCartViewModel GetItemById(Guid id);
    }
}

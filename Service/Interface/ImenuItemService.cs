using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public interface IMenuItemService
    {
        Guid Add(MenuItemVM menuItemVM, IFormFile file);
        void Delete(Guid id);
        MenuItemVM GetById(Guid id);
        IList<MenuItem> GetAll();
        Guid Update(MenuItemVM menuItemVM, IFormFile file);
    }
}

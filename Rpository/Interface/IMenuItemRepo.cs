using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface IMenuItemRepo
    {
        Guid Add(MenuItem menuItem);
        void Delete(MenuItem menuItem);
        MenuItem GetById(Guid id);
        IList<MenuItem> GetAll();
        Guid Update(MenuItem menuItem);
    }
}

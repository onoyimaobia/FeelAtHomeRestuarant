using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
   public interface IOrderDetailService
    {
        Guid Add(CartDetailViewModel cartDetailViewModel);
    }
}

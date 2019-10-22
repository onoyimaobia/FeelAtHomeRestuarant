using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface ICartDetailRepository
    {
        Guid Add(OrderHeader orderHeader);
        OrderHeader GetById(Guid id, string userId);
        List<OrderHeader> GetAll(string userId);
        List<OrderHeader> GetAll();
        Guid Update(OrderHeader orderHeader);
    }
}

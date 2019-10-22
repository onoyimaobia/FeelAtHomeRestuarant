using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface IOrderDetailRepository
    {
        Guid Add(OrderDetail orderDetail);
        IList<OrderDetail> GetAll(Guid id);
        OrderDetail GetById(Guid id);
    }
}

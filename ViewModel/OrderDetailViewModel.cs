using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}

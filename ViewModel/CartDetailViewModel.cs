using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.ViewModel
{
    public class CartDetailViewModel
    {
        public List<ShoppingCart> Listcarts { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}

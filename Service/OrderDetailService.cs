using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ApplicationDbContext _context;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository,
            IShoppingCartRepository shoppingCartRepository, ApplicationDbContext context)
        {
            _orderDetailRepository = orderDetailRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _context = context;

        }
        public Guid Add(CartDetailViewModel cartDetailViewModel)
        {
            
            cartDetailViewModel.Listcarts = _shoppingCartRepository.GetAll(cartDetailViewModel.OrderHeader.UserId).ToList();
            foreach(var item in cartDetailViewModel.Listcarts)
            {
                item.MenuItem =  _shoppingCartRepository.GetItemById(item.MenuItemId);
                var orderdetail = new OrderDetail
                {
                    MenuItemId = item.MenuItemId,
                    OrderDetailId = Guid.NewGuid(),
                    OrderHeaderId = cartDetailViewModel.OrderHeader.OrderHeaderId,
                    Name = item.MenuItem.Name,
                    Description = item.MenuItem.Description,
                    Price = item.MenuItem.Price,
                    Count = item.Count
                };
                var Items =  _orderDetailRepository.Add(orderdetail);
            }
            _context.ShoppingCarts.RemoveRange(cartDetailViewModel.Listcarts);
            _context.SaveChanges();
            return cartDetailViewModel.OrderHeader.OrderHeaderId;


        }

       
    }
}

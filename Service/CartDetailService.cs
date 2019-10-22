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
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        public  CartDetailService(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }
        public Guid Add(CartDetailViewModel cartDetailViewModel)
        {
            var orderheardeentity = new OrderHeader
            {
                OrderHeaderId = Guid.NewGuid(),
                Orderdate = DateTime.Now,
                UserId = cartDetailViewModel.OrderHeader.UserId,
                Status = Utility.SD.StatusSubmitted,
                OrderTotal = cartDetailViewModel.OrderHeader.OrderTotal,
                Comment = cartDetailViewModel.OrderHeader.Comment,
                PickUpTime = cartDetailViewModel.OrderHeader.PickUpTime,
            
            };
            return _cartDetailRepository.Add(orderheardeentity);
        }

        public Guid Update(CartDetailViewModel shoppingCartViewModel)
        {
            throw new NotImplementedException();
        }
    }
}

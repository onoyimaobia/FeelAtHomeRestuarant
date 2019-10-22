using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid Add(OrderDetail orderDetail)
        {
            _context.OrderDetail.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail.OrderDetailId;
        }

        public IList<OrderDetail> GetAll(Guid id)
        {
            return _context.OrderDetail.Where(o => o.OrderHeaderId == id).ToList();
        }

        public OrderDetail GetById(Guid id)
        {
            return _context.OrderDetail.Where(o => o.OrderDetailId == id).SingleOrDefault();
        }
    }
}

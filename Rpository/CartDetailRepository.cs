using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public CartDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid Add(OrderHeader orderHeader)
        {
            _context.OrderHeader.Add(orderHeader);
            _context.SaveChanges();
            return orderHeader.OrderHeaderId;
              
        }

        public List<OrderHeader> GetAll(string userId)
        {
            if (userId.Contains("-"))
            {
                return _context.OrderHeader.Where(u => u.UserId == userId).OrderBy(u => u.Orderdate).ToList();
            }
            return _context.OrderHeader.Where(u => u.UserId == userId).OrderBy(u => u.Orderdate).ToList();
        }
        public List<OrderHeader> GetAll()
        {
            return _context.OrderHeader.Where(u => u.Status != SD.StatusCompleted && u.Status != SD.StatusReady && u.Status != SD.StatusCancelled).OrderByDescending(u => u.PickUpTime).ToList();
        }

        public OrderHeader GetById(Guid id, string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return _context.OrderHeader.Where(o => o.OrderHeaderId == id).FirstOrDefault();
            }
            return _context.OrderHeader.Where(o => o.OrderHeaderId == id && o.UserId == userId).FirstOrDefault();
        }

        public Guid Update(OrderHeader orderHeader)
        {
            orderHeader.OrderHeaderId = orderHeader.OrderHeaderId;
            orderHeader.OrderTotal = orderHeader.OrderTotal;
            orderHeader.PickUpTime = orderHeader.PickUpTime;
            orderHeader.Status = orderHeader.Status;
            orderHeader.UserId = orderHeader.UserId;
            orderHeader.Orderdate = orderHeader.Orderdate;
            orderHeader.Comment = orderHeader.Comment;
            _context.OrderHeader.Update(orderHeader);
            _context.SaveChanges();
            return orderHeader.OrderHeaderId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.Utility;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeelAtHomeRestaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartService _shoppingCart;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ApplicationDbContext _db;
        public OrderController(IMenuItemService imenuItemService,
            IHttpContextAccessor httpContextAccessor,
            IShoppingCartService shoppingCartService,
            IShoppingCartRepository shoppingCartRepository,
            IOrderDetailRepository orderDetailRepository,
            ICartDetailRepository cartDetailRepository,
            ApplicationDbContext db)
        {
            _shoppingCart = shoppingCartService;
            _shoppingCartRepository = shoppingCartRepository;
            _cartDetailRepository = cartDetailRepository;
            _orderDetailRepository = orderDetailRepository;
            _db = db;
        }
        // GET: Order
        public ActionResult OrderConfirmations(Guid id)
        {
            //id = Guid.Parse("48016809-a251-4472-a136-59efa17eaff5");
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
            {
                OrderHeader = _cartDetailRepository.GetById(id, claim.Value),
                OrderDetail = _orderDetailRepository.GetAll(id).ToList()
            };
            return View(orderDetailViewModel);
        }

        // GET: Order/Details/5
        public ActionResult OrderHistory(int id = 0)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            List<OrderDetailViewModel> orderDetailViewModel = new List<OrderDetailViewModel>();
            List<OrderHeader> OrderHeaderList = _cartDetailRepository.GetAll(claim.Value);
            if (id == 0 && OrderHeaderList.Count > 4)
            {
                OrderHeaderList = OrderHeaderList.Take(5).ToList();
            }
            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailViewModel IndividaulOrderHeader = new OrderDetailViewModel();
                IndividaulOrderHeader.OrderHeader = item;
                IndividaulOrderHeader.OrderDetail = _orderDetailRepository.GetAll(item.OrderHeaderId).ToList();
                orderDetailViewModel.Add(IndividaulOrderHeader);
            }
            ViewBag.OrderDetailVM = orderDetailViewModel;
            return View(orderDetailViewModel);
        }

        // GET: Order/Create
        public ActionResult ManageOrder()
        {
            List<OrderDetailViewModel> orderDetailViewModel = new List<OrderDetailViewModel>();
            List<OrderHeader> OrderHeaderList = _cartDetailRepository.GetAll();

            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailViewModel individual = new OrderDetailViewModel();
                individual.OrderDetail = _orderDetailRepository.GetAll(item.OrderHeaderId).ToList();
                individual.OrderHeader = item;

                orderDetailViewModel.Add(individual);
            }
            return View(orderDetailViewModel);
        }

        public ActionResult OrderPickUpDetails(Guid orderId)
        {
            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel();
            orderDetailViewModel.OrderHeader = _cartDetailRepository.GetById(orderId, "");
            orderDetailViewModel.OrderHeader.ApplicationUser = _db.Users.Where(u => u.Id == orderDetailViewModel.OrderHeader.UserId).FirstOrDefault();
            orderDetailViewModel.OrderDetail = _orderDetailRepository.GetAll(orderDetailViewModel.OrderHeader.OrderHeaderId).ToList();
            return View(orderDetailViewModel);
        }

        public ActionResult ConfirmOrderPickUp(int orderId)
        {
            OrderHeader orderHeader = _db.OrderHeader.Find(orderId);
            orderHeader.Status = SD.StatusCompleted;
            _db.SaveChanges();
            return RedirectToAction("ManageOrder");
        }
        // GET: Order/Edit/5
        public ActionResult OrderPrepare(Guid orderId)
        {
            OrderHeader orderHeader = _cartDetailRepository.GetById(orderId, "");
            orderHeader.Status = SD.StatusInProcess;
            var update = _cartDetailRepository.Update(orderHeader);

            return RedirectToAction("ManageOrder");
        }

        public ActionResult OrderReady(Guid orderId)
        {
            OrderHeader orderHeader = _cartDetailRepository.GetById(orderId, "");
            orderHeader.Status = SD.StatusReady;
            var update = _cartDetailRepository.Update(orderHeader);
            return RedirectToAction("ManageOrder");
        }

        public ActionResult OrderCancel(Guid orderId)
        {
            OrderHeader orderHeader = _cartDetailRepository.GetById(orderId, "");
            orderHeader.Status = SD.StatusCancelled;
            var update = _cartDetailRepository.Update(orderHeader);
            return RedirectToAction("ManageOrder");
        }

        // GET: Order/Delete/5
        public ActionResult OrderPickup(string option = null, string search = null)
        {
            List<OrderDetailViewModel> orderDetailViewModel = new List<OrderDetailViewModel>();
            if (search != null)
            {
                var user = new ApplicationUser();
                List<OrderHeader> OrderHeaderList = new List<OrderHeader>();
                if (option == "orderName")
                {
                    OrderHeaderList = _cartDetailRepository.GetAll(search);
                }
                else
                {
                    if (option == "email")
                    {
                        user = _db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower())).FirstOrDefault();
                    }
                    else
                    {
                        if (option == "phone")
                        {
                            user = _db.Users.Where(u => u.PhoneNumber.ToLower().Contains(search.ToLower())).FirstOrDefault();
                        }
                        else
                        {
                            if (option == "name")
                            {
                                user = _db.Users.Where(u => u.FirstName.ToLower().Contains(search.ToLower()) || u.LastName.ToLower().Contains(search.ToLower())).FirstOrDefault();
                            }
                        }
                    }
                }
                if (user != null || OrderHeaderList.Count > 0)
                {
                    if (OrderHeaderList.Count == 0)
                    {
                        OrderHeaderList = _db.OrderHeader.Where(o => o.UserId == user.Id).OrderByDescending(o => o.PickUpTime).ToList();
                    }
                    foreach (OrderHeader item in OrderHeaderList)
                    {
                        OrderDetailViewModel individual = new OrderDetailViewModel();
                        individual.OrderDetail = _orderDetailRepository.GetAll(item.OrderHeaderId).ToList();
                        individual.OrderHeader = item;

                        orderDetailViewModel.Add(individual);
                    }

                }
            }
            else
            {
                //If there is no search criteria
                List<OrderHeader> OrderHeaderList = _db.OrderHeader.Where(u => u.Status == SD.StatusReady).OrderByDescending(u => u.PickUpTime).ToList();

                foreach (OrderHeader item in OrderHeaderList)
                {
                    OrderDetailViewModel individual = new OrderDetailViewModel();
                    individual.OrderDetail = _orderDetailRepository.GetAll(item.OrderHeaderId).ToList();
                    individual.OrderHeader = item;

                    orderDetailViewModel.Add(individual);
                }
            }
            return View(orderDetailViewModel);
        }
    }
}
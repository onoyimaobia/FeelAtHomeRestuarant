using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeelAtHomeRestaurant.Controllers
{
    [Authorize]
    public class ShoppingcartController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeservice;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IFoodTypeService _foodTypeservice;
        private readonly IMenuItemRepo _menuItemRepo;
        private readonly IMenuItemService _imenuItemService;
        private readonly IShoppingCartService _shoppingCart;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartDetailService _cartDetailService;
        private readonly IOrderDetailService _orderDetailService;
        public ShoppingcartController(IMenuItemService imenuItemService,
            IFoodTypeService foodTypeservice,
            ICategoryTypeService categoryTypeservice,
            IHttpContextAccessor httpContextAccessor,
            IShoppingCartService shoppingCartService,
            IShoppingCartRepository shoppingCartRepository,
            IMenuItemRepo menuItemRepo, IOrderDetailService orderDetailService,
            ICartDetailService cartDetailService)
        {
            _imenuItemService = imenuItemService;
            _categoryTypeservice = categoryTypeservice;
            _foodTypeservice = foodTypeservice;
            _shoppingCart = shoppingCartService;
            _shoppingCartRepository = shoppingCartRepository;
            _menuItemRepo = menuItemRepo;
            _cartDetailService = cartDetailService;
            _orderDetailService = orderDetailService;
        }
        // GET: Shoppingcart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CartDetails()
        {
            CartDetailViewModel cartDetailViewModel = new CartDetailViewModel()
            {
                OrderHeader = new OrderHeader()
            };
            cartDetailViewModel.OrderHeader.OrderTotal = 0;
            // get the current user
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            // get user total, cart count
            var cartCount = _shoppingCartRepository.GetAll(claim.Value);
            if (cartCount != null)
            {
                cartDetailViewModel.Listcarts = cartCount.ToList();
            }
            foreach(var list in cartDetailViewModel.Listcarts)
            {
                list.MenuItem = _menuItemRepo.GetById(list.MenuItemId);
                cartDetailViewModel.OrderHeader.OrderTotal = cartDetailViewModel.OrderHeader.OrderTotal + (list.MenuItem.Price * list.Count);
                if(list.MenuItem.Description.Length > 100)
                {
                    list.MenuItem.Description = list.MenuItem.Description.Substring(0, 99) + "....";
                }
            }
            cartDetailViewModel.OrderHeader.PickUpTime = DateTime.Now;

            return View(cartDetailViewModel);
        }
       [BindProperty]
       public ShoppingCartViewModel shoppingCartViewModel { get; set; }
       
        // GET: Shoppingcart/Details/5
        public ActionResult AddCart(Guid cartId)
        {
            try
            {
                _shoppingCartRepository.AddCart(cartId);
                return RedirectToAction("CartDetails");
            }
            catch(Exception ex)
            {
                return RedirectToAction("CartDetails");
            }
        }
        public ActionResult RemoveCart(Guid cartId)
        {
            try
            {
                var removeCart =  _shoppingCartRepository.RemoveCart(cartId);
                var countRemainingCart = _shoppingCartRepository.GetUserId(removeCart.ApplicationUserId);
                HttpContext.Session.SetInt32("CartCount", countRemainingCart);
                return RedirectToAction("CartDetails");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CartDetails");
            }
        }

        // GET: Shoppingcart/Create
        public ActionResult Create(Guid id)
        {
            var ItemDetails = _shoppingCart.GetItemById(id);
            if (ItemDetails != null)
            {
                ViewBag.MenuItem = ItemDetails;

                return View(ItemDetails);
            }
            else
            {
                TempData["ErrorStatus"] = "Menu Item Does not exist";
                return RedirectToAction("Index");
            }
        }

        // POST: Shoppingcart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingCartViewModel shoppingCartViewModel)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = _httpContextAccessor.HttpContext.User.Identity.Name;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            try
            {
                if (ModelState.IsValid)
                {
                    shoppingCartViewModel.ApplicationUserId = claim.Value;
                    _shoppingCart.Add(shoppingCartViewModel);
                    // get user total, cart count
                    var count = _shoppingCartRepository.GetUserId(claim.Value);
                    // add session to shopping cart
                    HttpContext.Session.SetInt32("CartCount", count);
                   TempData["statusMessage"]  = "Item Added to Cart";
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Shoppingcart/Edit/5
        public ActionResult Edit(Guid id)
        {
            try
            {
                var ItemDetails = _imenuItemService.GetById(id);
                var menuItemVmEntity = Mapper.Map<MenuItem>(ItemDetails);
                if (ItemDetails != null)
                {
                    shoppingCartViewModel = new ShoppingCartViewModel()
                    {
                        MenuItemId = ItemDetails.MenuItemId,
                        MenuItem = menuItemVmEntity
                    };
                    //ViewBag.MenuItem = ItemDetails;

                    return View(shoppingCartViewModel);
                }
                else
                {
                    TempData["ErrorStatus"] = "Menu Item Does not exist";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorStatus"] = "Menu Item Does not exist";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Shoppingcart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderHeader(CartDetailViewModel cartDetailViewModel)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = cartDetailViewModel.OrderHeader.UserId = claim.Value;
                var addOrderheader = _cartDetailService.Add(cartDetailViewModel);
                cartDetailViewModel.OrderHeader.OrderHeaderId = addOrderheader;
                _orderDetailService.Add(cartDetailViewModel);
                HttpContext.Session.SetInt32("CartCount", 0);

                //cartDetailViewModel.Listcarts = _shoppingCartRepository.GetAll(claim.Value).ToList();
                // TODO: Add update logic here
                //return RedirectToAction("Index", "Home", new { id = orderHeader.Id });
                return RedirectToAction("OrderConfirmations", "Order", new { id = addOrderheader });
            }
            catch
            {
                return View();
            }
        }

        // GET: Shoppingcart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shoppingcart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
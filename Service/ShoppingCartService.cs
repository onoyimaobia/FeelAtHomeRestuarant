using AutoMapper;
using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FeelAtHomeRestaurant.Service
{
    public class ShoppingCartService: IShoppingCartService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IFileProvider fileProvider;
        //private readonly IHostingEnvironment hostingEnvironment;
        public ShoppingCartService(IHttpContextAccessor httpContextAccessor,IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            //_userManager = userManager;
            //_signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            //fileProvider = fileprovider;
            //hostingEnvironment = env;
        }

        public Guid Add(ShoppingCartViewModel shoppingCartViewModel)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var menuitemId = shoppingCartViewModel.MenuItemId;
            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var check = _shoppingCartRepository.CheckForExist(shoppingCartViewModel.ApplicationUserId, menuitemId);
            
            if(check == null)
            {
                var entity = new ShoppingCart
                {
                    ShoppingCartId = Guid.NewGuid(),
                    ApplicationUserId = shoppingCartViewModel.ApplicationUserId,
                    MenuItemId = shoppingCartViewModel.MenuItemId,
                    Count = shoppingCartViewModel.Count,
                    CreatedAt = DateTime.Now

                };
                return _shoppingCartRepository.Add(entity);
            }
            else
            {
                //shoppingCartViewModel.ApplicationUserId = userId;
                //shoppingCartViewModel.CreatedAt = DateTime.Now;
                //shoppingCartViewModel.ShoppingCartId = getshoppingcartId;
                var getshoppingcartId = check.ShoppingCartId;
                var entity = Mapper.Map<ShoppingCart>(shoppingCartViewModel);
                var addedcartcount = _shoppingCartRepository.GetById(check.ShoppingCartId);
                entity.Count = entity.Count + addedcartcount.Count;
                return _shoppingCartRepository.Update(entity);
            }
       
            
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<ShoppingCart> GetAll()
        {
            throw new NotImplementedException();
        }

        public ShoppingCartViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartViewModel GetItemById(Guid id)
        {
            var entity = _shoppingCartRepository.GetItemById(id);
            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel
            {
                MenuItem = entity,
            };
            //var menuItemVmEntity = Mapper.Map<ShoppingCartViewModel>(entity);
            return shoppingCartViewModel;
        
        }

        public Guid Update(ShoppingCartViewModel shoppingCartViewModel)
        {
            throw new NotImplementedException();
        }
    }
}

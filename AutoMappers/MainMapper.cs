using AutoMapper;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.AutoMappers
{
    public class MainMapper : Profile
    {
        public MainMapper()
        { 
        // Add as many of these lines as you need to map your objects
        //Users is destination while Viewmodel is our source
            CreateMap<CategoryTypeViewModel, CategoryType>();
            CreateMap<CategoryType, CategoryTypeViewModel>().ForMember(x => x.CategoryTypeId, y => y.UseDestinationValue());
            CreateMap<FoodTypeViewModel, FoodType>();
            CreateMap<FoodType, FoodTypeViewModel>().ForMember(x => x.FoodTypeId, y => y.UseDestinationValue());
            CreateMap<MenuItemVM, MenuItem>();
            CreateMap<MenuItem, MenuItemVM>().ForMember(x => x.MenuItemId, y => y.UseDestinationValue());
            CreateMap<ShoppingCartViewModel, ShoppingCart>();
       
            //CreateMap<MenuItem, MenuItemVM>().ForMember(x => x.MenuItemId, y => y.UseDestinationValue()); 

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using FeelAtHomeRestaurant.Utility;

namespace FeelAtHomeRestaurant.Controllers
{
    [Authorize(Policy = SD.AdminEndUser)]
    public class MenuItemController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeservice;
        
        private readonly IFoodTypeService _foodTypeservice;
        
        private readonly IMenuItemService _imenuItemService;
        public MenuItemController(IMenuItemService imenuItemService, IFoodTypeService foodTypeservice, ICategoryTypeService categoryTypeservice)
        {
            _imenuItemService = imenuItemService;
            _categoryTypeservice = categoryTypeservice;
            _foodTypeservice = foodTypeservice;
        }
        // GET: MenuItem
        public ActionResult Index()
        {
            if (TempData["SuccessStatus"] != null)
            {
                ViewBag.UpdateStatus = TempData["SuccessStatus"].ToString();
            }
            if (TempData["ErrorStatus"] != null)
            {
                ViewBag.ErrorStatus = TempData["ErrorStatus"];
            }

            IList<MenuItem> MenuItems = new List<MenuItem>();
            MenuItems = _imenuItemService.GetAll();
            //ViewBag.MenuItems = _imenuItemService.GetAll();
           

            return View(MenuItems);
        }

        // GET: MenuItem/Details/5
        public ActionResult Details(Guid id)
        {
            var ItemDetails = _imenuItemService.GetById(id);
            if (ItemDetails != null)
            {
                   ViewBag.MenuItem = ItemDetails;

                return View(ItemDetails);
            }
            else
            {
                TempData["ErrorStatus"] = "Menu Item Does not exist";
                return RedirectToAction(nameof(Index));
            }
        }
        public MenuItemVM MenuItem { get; set; }
        // GET: MenuItem/Create
        public ActionResult Create()
        {
            
            if (TempData["ErrorStatus"] != null)
            {
                ViewBag.ErrorStatus = TempData["ErrorStatus"].ToString();
            }
            var foodTypeViewModel = _foodTypeservice.GetAll().ToList();
            var categoryTypeViewModel = _categoryTypeservice.GetAll().ToList();

            var FoodType = Mapper.Map<IEnumerable<FoodType>>(foodTypeViewModel);
            var CategoryType = Mapper.Map<IEnumerable<CategoryType>>(categoryTypeViewModel);

            ViewBag.Categories = CategoryType;
            ViewBag.Foodtypes = FoodType;
            return View();
        }

        // POST: MenuItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItemVM menuItemVM, IFormFile file)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    if(file != null) {
                    // TODO: Add insert logic here
                        var menuItem = _imenuItemService.Add(menuItemVM, file);
                        if(menuItem != null)
                        {
                            TempData["SuccessStatus"] = "MenuItem Created Successfully";
                        }
                        else
                        {
                            TempData["ErrorStatus"] = "MenuItem Created UnSuccessfully, check the data you are passing";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Error"] = "File Cannot be empty";
                        return RedirectToAction(nameof(Index));
                    }
                }
                

            }
            catch(Exception ex)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MenuItem/Edit/5
        public ActionResult Edit(Guid id)
        {
            var ItemToEdit = _imenuItemService.GetById(id);
            if (ItemToEdit != null)
            {
                var foodTypeViewModel = _foodTypeservice.GetAll().ToList();
                var categoryTypeViewModel = _categoryTypeservice.GetAll().ToList();

                var FoodType = Mapper.Map<IEnumerable<FoodType>>(foodTypeViewModel);
                var CategoryType = Mapper.Map<IEnumerable<CategoryType>>(categoryTypeViewModel);

                ViewBag.Categories = CategoryType;
                ViewBag.Foodtypes = FoodType;
                ViewBag.FoodType = ItemToEdit;

                return View(ItemToEdit);
            }
            else
            {
                TempData["ErrorStatus"] = "Menu Item Does not exist";
                return RedirectToAction(nameof(Index));
            }
           
        }

        // POST: MenuItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuItemVM menuItemVM, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                        // TODO: Add insert logic here
                        var menuItem = _imenuItemService.Update(menuItemVM, file);
                        if (menuItem != null)
                        {
                            TempData["SuccessStatus"] = "MenuItem Updated Successfully";
                        }
                        else
                        {
                            TempData["ErrorStatus"] = "MenuItem was Updated Successfuly, check the data you are passing";
                            return View();
                        }
                    
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: MenuItem/Delete/5
        public ActionResult Delete(Guid id)
        {
            var DeleteMenuItem = _imenuItemService.GetById(id);
            ViewBag.Delete = DeleteMenuItem;
            return View(DeleteMenuItem);
        }

        // POST: MenuItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id,MenuItemVM  menuItemVM)
        {
            try
            {
                
                    _imenuItemService.Delete(id);
                    
                    
                    TempData["SuccessStatus"] = "Item Deleted Successfully";
                    
                // TODO: Add delete logic here

                
            }
            catch
            {
                TempData["ErrorStatus"] = "Menu Item Does not exist";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
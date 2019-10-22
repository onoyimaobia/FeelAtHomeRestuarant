using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.Utility;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeelAtHomeRestaurant.Controllers
{
    [Authorize(Policy = SD.AdminEndUser)]
    public class FoodTypeController : Controller
    {
        private readonly IFoodTypeService _foodTypeservice;
        public FoodTypeController(IFoodTypeService foodTypeservice)
        {
            _foodTypeservice = foodTypeservice;
        }
        string Message = "";
        // GET: FoodType
        public ActionResult Index()
        {
            List<FoodTypeViewModel> allCategory = new List<FoodTypeViewModel>();
            allCategory = _foodTypeservice.GetAll().OrderBy(c => c.Name).ToList();
            ViewBag.foodTypeViewModel = allCategory;
            return View();
        }

        // GET: FoodType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FoodType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodTypeViewModel foodTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _foodTypeservice.Add(foodTypeViewModel);
                }
                else
                {
                    return View();
                }
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: FoodType/Edit/5
        public ActionResult Edit(Guid id)
        {
            try
            {
                var ItemToEdit = _foodTypeservice.GetById(id);
                if (ItemToEdit != null)
                {
                    ViewBag.FoodType = ItemToEdit;
                    return View();
                }
                else
                {
                    return Redirect("Index");
                }
            }
            catch(Exception ex)
            {
                return Redirect("Index");
            }
         
        }

        // POST: FoodType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [FromForm] FoodTypeViewModel entity)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _foodTypeservice.Update(id, entity);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FoodType/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id != null)
            {
                try
                {
                    var FoodType = _foodTypeservice.GetById(id);
                    if (FoodType != null)
                    {
                        ViewBag.FoodType = FoodType;
                        return View();
                    }
                    else
                    {
                        return Redirect("Index");
                    }

                }
                catch
                {
                    return Redirect("Index");
                }
            }
            else
            {
                return Redirect("Index");
            }
        }

        // POST: FoodType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FoodTypeViewModel foodTypeViewModel)
        {
            try
            {

                _foodTypeservice.Delete(id);
                Message = "Item Deleted Successfully";
                TempData["SuccefulMessage"] = Message;
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                Message = "Error Occured. check ";
                TempData["Error"] = Message;
                return View();
            }
        }
    }
}
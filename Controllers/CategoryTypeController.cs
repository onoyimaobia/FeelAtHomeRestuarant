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
    public class CategoryTypeController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeservice;
        public CategoryTypeController(ICategoryTypeService categoryTypeservice)
        {
            _categoryTypeservice = categoryTypeservice;
        }
        string Message = "";
        //bind
        [BindProperty]
        public CategoryTypeViewModel categoryTypeViewModel {get; set;}
        // GET: CategoryType
        public IActionResult Index()
        { if(TempData["SuccefulMessage"] != null)
            {
                TempData["SuccefulMessage"] = ViewBag.SuceesfulMessage;
            }
            List<CategoryTypeViewModel> allCategory = new List<CategoryTypeViewModel>();
            allCategory = _categoryTypeservice.GetAll().OrderBy(c=>c.DisplayOrder).ToList();
            ViewBag.categoryViewModel = allCategory;
            return View();
        }

        // GET: CategoryType/Details/5
        public ActionResult Details(Guid id)
        {
            if (id != null)
            {
                try
                {
                    var category = _categoryTypeservice.GetById(id);
                    if (category != null)
                    {
                        ViewBag.Category = category;
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

        // GET: CategoryType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryTypeViewModel categoryTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryTypeservice.Add(categoryTypeViewModel);
                }
                else
                {
                    return View();
                }
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryType/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id != null)
            {
                try
                {
                    var category = _categoryTypeservice.GetById(id);
                    if(category != null)
                    {
                        ViewBag.Category = category;
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

        // POST: CategoryType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [FromForm] CategoryTypeViewModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryTypeservice.Update(id, entity);
                }
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        // GET: CategoryType/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id != null)
            {
                try
                {
                    var category = _categoryTypeservice.GetById(id);
                    if (category != null)
                    {
                        ViewBag.Category = category;
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

        // POST: CategoryType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(Guid id, CategoryTypeViewModel categoryTypeViewModel)
        {
            try
            {

                _categoryTypeservice.Delete(id);
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
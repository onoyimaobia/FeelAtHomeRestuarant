using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeelAtHomeRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Service.Interface;
using Newtonsoft.Json;

namespace FeelAtHomeRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;
        private readonly ICategoryTypeService _categoryTypeservice;

        private readonly IFoodTypeService _foodTypeservice;

        private readonly IMenuItemService _imenuItemService;
       
        public HomeController(IHostingEnvironment hostingEnvironment, ApplicationDbContext db, IMenuItemService imenuItemService, IFoodTypeService foodTypeservice, ICategoryTypeService categoryTypeservice)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
            _categoryTypeservice = categoryTypeservice;
            _foodTypeservice = foodTypeservice;
            _imenuItemService = imenuItemService;
        }
        public IActionResult Index()
        {
            if(TempData["statusMessage"] != null)
            {
                ViewBag.statusMessage = TempData["statusMessage"];
            }
            IList<MenuItem> MenuItems = new List<MenuItem>();
            MenuItems = _imenuItemService.GetAll();
            return View(MenuItems);
        }

        public JsonResult Privacy(int pageIndex, int pageSize)
        {
            if(pageSize == 0)
            {
                pageSize = 5;
            }
            System.Threading.Thread.Sleep(4000);
            var sa = new JsonSerializerSettings();
            var data = (from c in _db.MenuItems
                         orderby c.Name ascending
                         select c)
                         .Skip(pageIndex * pageSize)
                         .Take(pageSize);
            return Json(data.ToList());
            //return View();
        }
        [HttpGet]
        public ActionResult UploadImageSlider()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImageSlider(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                string folderName = "ImageSlider";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        FileInfo fi = new FileInfo(item.FileName);
                        var newFilename = Guid.NewGuid() + "_" + String.Format("{0:d}",
                                     (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, newFilename);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        var pathToSave = @"/ImageSlider/" + newFilename;
                        var imagesliderentity = new ImageSlider
                        {
                           ImageSliderId = Guid.NewGuid(),
                           ImageUrl = pathToSave
                        };
                        _db.ImageSliders.Add(imagesliderentity);
                    }
                    _db.SaveChanges();
                }
                return this.Content("Success");
            }
            return this.Content("Fail");
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

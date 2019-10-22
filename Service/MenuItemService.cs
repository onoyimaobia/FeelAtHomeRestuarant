using FeelAtHomeRestaurant.Rpository;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeelAtHomeRestaurant.Models;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using System.IO;
using System.Net.Http.Headers;

namespace FeelAtHomeRestaurant.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepo _menuItemRepo;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;
        public MenuItemService(IMenuItemRepo menuItemRepo, IFileProvider fileprovider, IHostingEnvironment env)
        {
            _menuItemRepo = menuItemRepo;
            fileProvider = fileprovider;
            hostingEnvironment = env;
        }
        public Guid Add(MenuItemVM menuItemVM, IFormFile file)
        {
            try
            {
                menuItemVM.Image = "string";
                menuItemVM.MenuItemId =  Guid.NewGuid();
                var entity = Mapper.Map<MenuItem>(menuItemVM);
                var saveItem = _menuItemRepo.Add(entity);
                if (saveItem != null)
                {
                    if (file != null)
                    {
                        // Create a File Info 
                        FileInfo fi = new FileInfo(file.FileName);
                        if(fi.Extension == ".mp4"){
                            string folderName = "MenuItemVideo";
                            string webRootPath = hostingEnvironment.WebRootPath;
                            string newPath = Path.Combine(webRootPath, folderName);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            if (file.Length > 0)
                            {
                                
                                var newFilename = Guid.NewGuid() + "_" + String.Format("{0:d}",
                                             (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                string fullPath = Path.Combine(newPath, newFilename);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                var pathToSave = @"/MenuItemVideo/" + newFilename;
                                entity.Image = pathToSave;
                                _menuItemRepo.Update(entity);
                            }
                        }
                        else
                        {
                            var newFilename = entity.MenuItemId + "_" + String.Format("{0:d}",
                                                                 (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                            var webPath = hostingEnvironment.WebRootPath;
                            var path = Path.Combine("", webPath + @"\MenuItemImages\" + newFilename);

                            // IMPORTANT: The pathToSave variable will be save on the column in the database
                            var pathToSave = @"/MenuItemImages/" + newFilename;

                            // This stream the physical file to the allocate wwwroot/ImageFiles folder
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyToAsync(stream);
                            }
                            entity.Image = pathToSave;
                            _menuItemRepo.Update(entity);
                        }
                        // This code creates a unique file name to prevent duplications
                        // stored at the file location
                       
                       
                    }
                }
                return saveItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Delete(Guid id)
        { try
            {
                var entityToDelete = _menuItemRepo.GetById(id);
                var image = entityToDelete.Image;
                //remove image from path
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                
                _menuItemRepo.Delete(entityToDelete);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<MenuItem> GetAll()
        {
            var allMenuItem = _menuItemRepo.GetAll().ToList();
            //List<MenuItemVM> MenuItemVMs = new List<MenuItemVM>();
            //foreach (MenuItem menuItem in allMenuItem)
            //{
            //    var Item = Mapper.Map<MenuItemVM>(allMenuItem);
            //    MenuItemVMs.Add(Item);
            //}
            return allMenuItem;
        }

        public MenuItemVM GetById(Guid id)
        {
            var entity = _menuItemRepo.GetById(id);
            var menuItemVmEntity = Mapper.Map<MenuItemVM>(entity);
            return menuItemVmEntity;
        }

        public Guid Update(MenuItemVM menuItemVM, IFormFile file)
        {
            if(file != null)
            {
                var webPath = hostingEnvironment.WebRootPath;
                //delete file
                var getfilepath = _menuItemRepo.GetById(menuItemVM.MenuItemId);
                var image = getfilepath.Image;
                var rootfolder  = Path.Combine("", webPath + @"\MenuItemImages\" + image);
                if (System.IO.File.Exists(Path.Combine(rootfolder)))
                {
                    System.IO.File.Delete(Path.Combine(rootfolder));
                }
                // Create a File Info 
                FileInfo fi = new FileInfo(file.FileName);
                // This code creates a unique file name to prevent duplications
                // stored at the file location
                var newFilename = menuItemVM.MenuItemId + "_" + String.Format("{0:d}",
                              (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                
                var path = Path.Combine("", webPath + @"\MenuItemImages\" + newFilename);

                // IMPORTANT: The pathToSave variable will be save on the column in the database
                var pathToSave = @"/MenuItemImages/" + newFilename;

                // This stream the physical file to the allocate wwwroot/ImageFiles folder
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                menuItemVM.Image = pathToSave;
                var entity = Mapper.Map<MenuItem>(menuItemVM);
                return _menuItemRepo.Update(entity);

            }
            else
            {
                var entity = Mapper.Map<MenuItem>(menuItemVM);
                return _menuItemRepo.Update(entity);
            }
            
        }
    }
}

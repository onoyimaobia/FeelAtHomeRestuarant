using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.ViewModel;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private readonly ICategoryTypeRepo _categoryTypeRepo;
        public CategoryTypeService(ICategoryTypeRepo categoryTypeRepo)
        {
            _categoryTypeRepo = categoryTypeRepo;
        }
        public Guid Add(CategoryTypeViewModel categoryTypeViewModel)
        {
            var entity = new CategoryType
            {
                CategoryTypeId = new Guid(),

                Name = categoryTypeViewModel.Name,
                DisplayOrder = categoryTypeViewModel.DisplayOrder
            };
            return _categoryTypeRepo.Add(entity);
        }

        public void Delete(Guid id)
        {
            var itemToDelete = _categoryTypeRepo.GetById(id);
            _categoryTypeRepo.Delete(itemToDelete);
        }

        public IList<CategoryTypeViewModel> GetAll()
        {
            var categories = _categoryTypeRepo.GetAll().ToList();
            var categoryViewModel = new List<CategoryTypeViewModel>();
            foreach (CategoryType categoryType in categories)
            {
                var categoryViewModels = Mapper.Map<CategoryTypeViewModel>(categoryType);
               
               categoryViewModel.Add(categoryViewModels);
            }

            return categoryViewModel;
        }

        public CategoryTypeViewModel GetById(Guid id)
        {
            var category = _categoryTypeRepo.GetById(id);
            var categoryviewmodel = Mapper.Map<CategoryTypeViewModel>(category);
            return categoryviewmodel;
        }

        public Guid Update(Guid id,CategoryTypeViewModel categoryTypeViewModel)
        {
            var entity = Mapper.Map<CategoryType>(categoryTypeViewModel);
            return _categoryTypeRepo.Update(entity);
        }
    }
}

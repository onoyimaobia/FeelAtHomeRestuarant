using AutoMapper;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly IFoodTypeRepo _foodTypeRepo;
        public FoodTypeService(IFoodTypeRepo foodTypeRepo)
        {
            _foodTypeRepo = foodTypeRepo;
        }
        public Guid Add(FoodTypeViewModel foodTypeViewModel)
        {
            var entity = Mapper.Map<FoodType>(foodTypeViewModel);
            //var entity = new FoodType
            //{
            //    FoodTypeId = new Guid(),
            //    Name = foodTypeViewModel.Name
            //};
            return _foodTypeRepo.Add(entity);
        }

        public void Delete(Guid id)
        {
            var ItemToDelete = _foodTypeRepo.GetById(id);
            _foodTypeRepo.Delete(ItemToDelete);
        }

        public IList<FoodTypeViewModel> GetAll()
        {
            var foodtypes = _foodTypeRepo.GetAll().ToList();
            var foodViewModel = new List<FoodTypeViewModel>();
            foreach (FoodType foodType in foodtypes)
            {
                var foodViewModels = Mapper.Map<FoodTypeViewModel>(foodType);

                foodViewModel.Add(foodViewModels);
            }

            return foodViewModel;
        }

        public FoodTypeViewModel GetById(Guid id)
        {
            var ItemToEdit = _foodTypeRepo.GetById(id);
            var foodTypeViewModel = Mapper.Map<FoodTypeViewModel>(ItemToEdit);
            return foodTypeViewModel;
        }

        public Guid Update(Guid id, FoodTypeViewModel foodTypeViewModel)
        {
            var entity = Mapper.Map<FoodType>(foodTypeViewModel);
            return _foodTypeRepo.Update(entity);
        }
    }
}

using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.ViewModel
{
    public class MenuItemViewModel
    {
        public Guid MenuItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Spicyness { get; set; }
        public enum Spicy { Mild = 0, Moderate = 1, High = 2 }
        [Range(1, int.MaxValue, ErrorMessage = "Price Should not be less than 1#")]
        public double Price { get; set; }
        [Display(Name = "Category Type")]
        public Guid CategoryTypeId { get; set; }
        [ForeignKey("CategoryTypeId")]
        public virtual CategoryType CategoryType { get; set; }
        public Guid FoodTypeId { get; set; }
        [Display(Name = "Food Type")]
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
        
    }
}

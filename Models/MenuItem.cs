using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class MenuItem
    {
        public Guid MenuItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Image { get; set; }
        [Required]
        public string Spicyness { get; set; }
        public enum Spicy { Mild= 0, Moderate=1, High= 2 }
        [Range(1, int.MaxValue, ErrorMessage ="Price Should not be less than 1#")]
        public double Price { get; set; }
        [Display(Name ="Category Type")]
        public Guid CategoryTypeId { get; set; }
        [ForeignKey("CategoryTypeId")]
        public virtual CategoryType CategoryType { get; set; }
        [Display(Name = "Food Type")]
        public Guid FoodTypeId { get; set; }
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

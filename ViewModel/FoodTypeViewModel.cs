using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.ViewModel
{
    public class FoodTypeViewModel
    {
        [Key]
        public Guid FoodTypeId { get; set; }
        public string Name { get; set; }
    }
}

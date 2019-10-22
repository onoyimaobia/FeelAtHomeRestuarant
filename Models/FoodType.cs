using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class FoodType
    {
        public Guid FoodTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

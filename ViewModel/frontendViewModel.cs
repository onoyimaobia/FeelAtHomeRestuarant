using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.ViewModel
{
    public class FrontendViewModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<CategoryType> CategoryTypes { get; set; }

    }
}

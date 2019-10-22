using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> Items, Guid SelectedValues)
        {
            return from Item in Items
                   select new SelectListItem
                   {
                       Text = Item.GetPropertyValue("Name"),
                       Value = Item.GetPropertyValue("Id"),
                       Selected = Item.GetPropertyValue("Id").Equals(SelectedValues.ToString())
                   };
        }
    }
}

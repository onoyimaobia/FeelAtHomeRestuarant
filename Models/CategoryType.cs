using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class CategoryType
    {
        [Required]
        public Guid CategoryTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

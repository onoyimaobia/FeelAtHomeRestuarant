using FeelAtHomeRestaurant.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class OrderHeader
    {
        [Required]
        public Guid OrderHeaderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime Orderdate { get; set; }
        [Required]
        public Double OrderTotal { get; set; }
        [Required]
        public DateTime PickUpTime { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }

    }
}

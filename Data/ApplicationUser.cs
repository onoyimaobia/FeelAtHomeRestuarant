﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName;} }
    }
}

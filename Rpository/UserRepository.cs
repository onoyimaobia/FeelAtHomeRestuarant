using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Rpository.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string GetUserProfilePicture(string userName)
        {
            return _dbContext.Users.Where(u => u.UserName == userName).First().ProfilePic;
        }
    }
}

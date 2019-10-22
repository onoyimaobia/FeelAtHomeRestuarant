using FeelAtHomeRestaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface IUserRepository
    {
        string GetUserProfilePicture(string userName);
    }
}

using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Rpository.Interface
{
    public interface IEmailSenderRepository
    {
        EmailCredentials GetEmailSender(string emailSender);
    }
}

using FeelAtHomeRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service.Interface
{
    public interface IEmailSenderService
    {
         EmailCredentials GetEmailSender(string emailSender);
        
    }
}

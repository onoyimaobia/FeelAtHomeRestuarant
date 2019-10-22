using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Service
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailSenderRepository _emailSenderRepository;
        public EmailSenderService(IEmailSenderRepository emailSenderRepository)
        {
            _emailSenderRepository = emailSenderRepository;
        }
        public EmailCredentials GetEmailSender(string emailSender)
        {
            try
            {
                var myItem = _emailSenderRepository.GetEmailSender(emailSender);
                if (myItem == null) { return null; }
                return myItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

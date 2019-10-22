using FeelAtHomeRestaurant.Data;
using FeelAtHomeRestaurant.Models;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FeelAtHomeRestaurant.Rpository
{
    public class EmailSenderRepository: IEmailSenderRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly IEmailSenderRepository _emailSenderRepository;
        public EmailSenderRepository(ApplicationDbContext context)
        {
            _context = context;
            //_emailSenderRepository = emailSenderRepository;
        }

        public EmailCredentials GetEmailSender(string emailSender)
        {
            try
            {
                var myItem = _context.EmailCredentials.FirstOrDefault(y => y.SenderEmail == emailSender);
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

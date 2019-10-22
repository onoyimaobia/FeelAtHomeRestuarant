using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class EmailCredentials
    {
        public Guid EmailCredentialsId { get; set; }

        public string EmailType { get; set; }

        public string SenderEmail { get; set; }

        public string SenderPassword { get; set; }

        public string SMTPHost { get; set; }

        public int SMTPPort { get; set; }
    }
}

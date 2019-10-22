using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelAtHomeRestaurant.Models
{
    public class IndividualButtonPartial
    {
        public string Page { get; set; }
        public string Controller { get; set; }
        public string FontAwesome { get; set; }
        public string ButtonType { get; set; }
        public Guid Id { get; set; }

        public string ActionParameter {
            get {
                if(Id !=null && Id.ToString().Count() > 0)
                {
                    return Id.ToString();
                }
                return null;
            }
        }
    }
}

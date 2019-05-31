using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models.ViewModels
{
    public class MapServiceViewModel
    {
        public ApplicationUser User { get; set; }

        public string DeparturePlace { get; set; }

        public string ArrivalPlace { get; set; }

        public string IntermediatePlace { get; set; } ///TODO: Should be a List of intermediate points
    }
}

using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models.ViewModels
{
    public class GraphMapViewModel
    {
        public GraphMap GraphMap { get; set; }

        public IEnumerable<IntercityLink> Edges { get; set; }
    }
}

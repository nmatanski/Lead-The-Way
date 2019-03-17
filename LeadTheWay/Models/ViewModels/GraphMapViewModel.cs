using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using System.Collections.Generic;

namespace LeadTheWay.Models.ViewModels
{
    public class GraphMapViewModel
    {
        public GraphMap GraphMap { get; set; }

        public IEnumerable<IntercityLink> Edges { get; set; }
    }
}

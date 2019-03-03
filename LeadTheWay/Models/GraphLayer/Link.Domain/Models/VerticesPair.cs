using LeadTheWay.GraphLayer.Vertex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Link.Domain.Models
{
    public class VerticesPair
    {
        public int FirstNodeId { get; set; }

        public int RelatedNodeId { get; set; }
    }
}

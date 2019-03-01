using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Vertex.Domain.Models
{
    public class TransportVertex : IVertex
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

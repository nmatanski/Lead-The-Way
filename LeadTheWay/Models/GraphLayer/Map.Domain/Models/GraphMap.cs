using LeadTheWay.Models.GraphLayer.Map.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Map.Domain.Models
{
    public class GraphMap : IGraph
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GraphString { get; set; }
    }
}

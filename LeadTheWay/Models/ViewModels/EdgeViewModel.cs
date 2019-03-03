using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Vertex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models.ViewModels
{
    public class EdgeViewModel
    {
        public IntercityLink Edge { get; set; }

        public IEnumerable<TransportVertex> Nodes { get; set; }

        /// <summary>
        /// true = one direction from first to second; false = bidirectional (undirected; graph theory)
        /// </summary>
        public bool IsDirected { get; set; }

        public TimeSpan DurationSpan { get => TimeSpan.FromMinutes(Duration); }

        public int Duration { get; set; }
    }
}

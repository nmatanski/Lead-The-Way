using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Vertex.Domain
{
    public interface IVertex
    {
        string Name { get; set; }

        string Description { get; set; }
    }
}

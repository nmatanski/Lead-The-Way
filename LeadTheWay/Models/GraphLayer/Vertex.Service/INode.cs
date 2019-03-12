using LeadTheWay.GraphLayer.Vertex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Vertex.Service
{
    interface INode : IVertex
    {
        void Reset();
    }
}

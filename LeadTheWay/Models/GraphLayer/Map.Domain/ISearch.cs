using LeadTheWay.GraphLayer.Vertex.Service;
using System.Collections.Generic;

namespace LeadTheWay.GraphLayer.Map.Domain.Search
{
    public interface ISearch
    {
        bool HasPath(string start, string end, List<Node> intermediateNodes = null);
    }
}
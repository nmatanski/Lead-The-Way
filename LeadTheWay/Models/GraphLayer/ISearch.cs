using System.Collections.Generic;

namespace LeadTheWay.Models.GraphLayer
{
    public interface ISearch
    {
        bool HasPath(string start, string end, List<Node> intermediateNodes = null);
    }
}
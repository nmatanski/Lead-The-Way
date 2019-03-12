using LeadTheWay.GraphLayer.Map.Domain.Search;
using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.GraphLayer.Map.Service.Search.Services.Utility;
using LeadTheWay.GraphLayer.Vertex.Service;
using System;
using System.Collections.Generic;

namespace LeadTheWay.GraphLayer.Map.Service.Search.Services
{
    public class PathWithIntermediatePointsSearch
    {
        public Graph Graph { get; set; }

        public string Path { get; internal set; }
        public string Result { get; internal set; }


        public PathWithIntermediatePointsSearch(Graph graph)
        {
            Graph = graph;
        }

        private static string Search(ISearch s, string start, string end)
        {
            if (s.HasPath(start, end))
            {
                return "HAVE A PATH!!!";
            }
            else
            {
                return "DOES NOT HAVE A PATH!!!";
            }
        }

        public string Search(string start, string end, List<Node> intermediatePoints)
        {
            intermediatePoints.Insert(0, Graph.GetNodeByName(start));
            intermediatePoints.Add(Graph.GetNodeByName(end));
            string result = "";
            for (int i = 0; i < intermediatePoints.Count - 1; i++)
            {
                result = Search(new CheapestPathSearch(Graph), intermediatePoints[i].Name, intermediatePoints[i + 1].Name);
                Path = GraphUtil.Path + Path;
                if (i == intermediatePoints.Count - 2)
                {
                    Result = result;
                    return result;
                }
            }

            return "-1"; // няма да се достигне никога
        }
    }
}
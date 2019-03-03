using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Link.Service;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using LeadTheWay.GraphLayer.Map.Domain.Search;
using LeadTheWay.GraphLayer.Map.Service.Search.Services;
using LeadTheWay.GraphLayer.Vertex.Service;
using LeadTheWay.Models.GraphLayer.Map.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Map.Service
{
    public class Graph : IGraph
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GraphString { get; set; }


        public Dictionary<string, Node> Map { get; set; } = new Dictionary<string, Node>(); ///TODO: set map.add(GraphString.ToGraph())

        public static string Path { get; set; }
        public bool Err { get; private set; }


        public void AddNode(Node node)
        {
            Map[node.Name] = node;
        }

        public void AddEdge(string start, string end, double length, TimeSpan duration, double price, byte serviceClass, Timetable timetable)
        {
            if (Map.ContainsKey(start) && Map.ContainsKey(end))
            {
                Edge edge = new Edge(Map[start], Map[end], length, duration, price, serviceClass, timetable); //type
                edge.FirstNode = Map[start]; ///TODO: test
                Map[start].Edges.Add(edge);
                Err = false;
            }
            else Err = true;
        }

        public void AddBidirectionalEdge(string start, string end, double length, TimeSpan duration, double price, byte serviceClass, Timetable timetable)
        {
            AddEdge(start, end, length, duration, price, serviceClass, timetable);
            AddEdge(end, start, length, duration, price, serviceClass, timetable);
        }

        public void ResetAll()
        {
            foreach (Node node in Map.Values)
                node.Reset();
        }

        public bool ContainsNode(string name)
        {
            return Map.ContainsKey(name);
        }

        public Node GetNodeByName(string name)
        {
            return Map[name];
        }

        public static string Search(ISearch s, string start, string end)
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

        public static string Search(PathWithIntermediatePointsSearch s, string start, string end, List<Node> intermediatePoints)
        {
            s.Search(start, end, intermediatePoints);
            Path = s.Path;
            return s.Result;
        }

        //public static string ToString(GraphMap graphMap, List<string> edgeHistory) //string edges
        //{
        //    //string nodes = "";
        //    /////TODO:
        //    ////foreach (KeyValuePair<string, Node> node in Map)
        //    ////    nodes += '#' + node.Value.Name + ',' + node.Value.Weight + ',' + node.Value.Coordinates[0] + ',' + node.Value.Coordinates[1] + "#\n";

        //    //return nodes + edges;

        //    var ns = "";
        //    foreach (var kvp in graphMap.Graph.Map)
        //    {
        //        ns += $"*{kvp.Key}({kvp.Value.Description})";
        //    }
        //    var es = "";
        //    foreach (var item in edgeHistory)
        //    {
        //        var tempEdge = await db.IntercityLinks.Where(e => e.EdgeString == item).FirstOrDefaultAsync();
        //        es += $"#{item}({tempEdge.Length}, {tempEdge.DurationTicks}, {tempEdge.Price}, {tempEdge.ServiceClass})";
        //    }
        //    var gstring = ns + es;
        //}
    }
}
    
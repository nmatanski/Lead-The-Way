using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models.GraphLayer
{
    public class Graph
    {
        public Dictionary<string, Node> Map { get; set; } = new Dictionary<string, Node>();

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

        public string ToString(string edges)
        {
            string nodes = "";
            ///TODO:
            //foreach (KeyValuePair<string, Node> node in Map)
            //    nodes += '#' + node.Value.Name + ',' + node.Value.Weight + ',' + node.Value.Coordinates[0] + ',' + node.Value.Coordinates[1] + "#\n";

            return nodes + edges;
        }
    }
}
    
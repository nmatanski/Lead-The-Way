using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.GraphLayer.Link.Service;

namespace LeadTheWay.GraphLayer.Vertex.Service
{
    public class Node : INode
    {
        public string Name { get; set; } //feature

        public string Description { get; set; } //feature 

        //public double Weight { get; set; } // calculated based on coords; unimplemented

        public List<Edge> Edges { get; set; }

        public List<Node> Childs { get; set; } = new List<Node>();

        public bool IsTested { get; set; }

        public bool IsExpanded { get; set; }

        public int Depth { get; set; }

        public Node Parent { get; set; }

        public double Cost { get; set; }

        public double CurrentLength { get; set; }


        public Node(string name, string description = null) //int? x = null, int? y = null
        {
            //Coordinates = new int[2];
            //Coordinates[0] = x.GetValueOrDefault(0);
            //Coordinates[1] = y.GetValueOrDefault(0);
            Name = name;
            Description = description;
            Parent = null;
            Edges = new List<Edge>(); ///TODO: TEST
        }


        public void Reset()
        {
            Parent = null;
            IsTested = false;
            IsExpanded = false;
            Depth = 0;
            Cost = 0;
            CurrentLength = 0;
        }
    }
}

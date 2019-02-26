using System.Collections.Generic;

namespace LeadTheWay.Models.GraphLayer
{
    internal class CheapestPathSearch : ISearch
    {
        private Graph graph;


        public CheapestPathSearch(Graph graph)
        {
            this.graph = graph;
        }


        public bool HasPath(string start, string end, List<Node> intermediateNodes = null)
        {
            if (!graph.ContainsNode(start) || !graph.ContainsNode(end))
            {
                return false;
            }

            graph.ResetAll();

            List<Node> list = new List<Node>();
            Node temp;
            list.Add(graph.GetNodeByName(start));
            //graph.GetNodeByName(start).CurrentLength = 0; // ?

            while (list.Count > 0)
            {
                temp = list[0];

                GraphUtil.PrintNodeInfo(temp, true);

                list.RemoveAt(0);
                temp.IsTested = true;

                if (!temp.IsExpanded)
                {
                    foreach (Edge edge in temp.Edges)
                    {
                        Node relatedNode = edge.RelatedNode;
                        GraphUtil.SetParentCost(temp, edge);
                        //relatedNode.Depth = temp.Depth + 1; // ?
                        list.Insert(0, relatedNode);
                    }
                    temp.IsExpanded = true;
                }
            }

            if (graph.GetNodeByName(end).Parent == null)
            {
                return false;
            }
            else
            {
                GraphUtil.ResultCheapest += "Cost: " + graph.GetNodeByName(end).Cost + '\n';
                Node startNode = graph.GetNodeByName(start);
                Node goalNode = graph.GetNodeByName(end);
                GraphUtil.Path = "";
                GraphUtil.PrintPath(startNode, goalNode, true);

                return true;
            }
        }
    }
}
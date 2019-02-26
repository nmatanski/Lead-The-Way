using System;
using System.Collections.Generic;

namespace LeadTheWay.Models.GraphLayer
{
    public static class GraphUtil
    {
        public static string Path { get; set; }

        public static string ResultCheapest { get; set; } = "";
        public static string ResultGreedy { get; set; } = "";

        public static void PrintNodeInfo(Node node, bool algorithm)
        {
            string parentName;
            if (node.Parent == null)
                parentName = "No parent";
            else parentName = node.Parent.Name;

            if (algorithm)
                ResultCheapest += "Node Name: " + node.Name + ", Cost: " + node.Cost + ", Parent Name: " + parentName + ", Current Length: " + node.CurrentLength + ", Depth: " + node.Depth + '\n';
            else ResultGreedy += "Node Name: " + node.Name + ", Cost: " + node.Cost + ", Parent Name: " + parentName + ", Current Length: " + node.CurrentLength + ", Depth: " + node.Depth + '\n';
        }

        public static void SetParentCost(Node parent, Edge edgeChild)
        {
            double newCost = NewCost(parent, edgeChild);
            Node relatedNode = edgeChild.RelatedNode;
            if (relatedNode.Parent == null || relatedNode.Cost > newCost || relatedNode.Cost == newCost && relatedNode.CurrentLength > parent.CurrentLength + edgeChild.Length)
            {
                relatedNode.Cost = newCost;
                relatedNode.Parent = parent;
                relatedNode.CurrentLength = parent.CurrentLength + edgeChild.Length;
            }
        }

        public static double NewCost(Node parent, Edge edgeChild)
        {
            double cost = parent.Cost;
            //switch (edgeChild.Type)
            //{
            //    case 0:
            //        cost += 3;
            //        if (edgeChild.Length > 10)
            //        {
            //            int temp = (int)(Math.Ceiling(edgeChild.Length) - 10);
            //            cost += 0.5 * temp;
            //        }
            //        break;
            //    case 1:
            //        if (edgeChild.Length > 5)
            //        {
            //            int temp = (edgeChild.Length % 5 == 0 && edgeChild.Length % 10 > 0) ? (int)(Math.Floor(edgeChild.Length / 10.0)) : (int)(Math.Round(edgeChild.Length / 10.0)); // така при 15, 25, 35, ... няма да се заплаща следващ започнат км
            //            cost += 2 * temp;
            //        }
            //        break;
            //}
            return cost;
        }

        //public static void SetParentCostXY(Node parent, Edge edgeChild)
        //{
        //    double newCost = NewCost(parent, edgeChild);
        //    Node relatedNode = edgeChild.RelatedNode;
        //    CalculateDistance(parent, relatedNode);
        //    if (relatedNode.Parent == null || relatedNode.Weight > parent.Weight || relatedNode.Weight == parent.Weight && relatedNode.Cost > newCost || relatedNode.Weight == parent.Weight && relatedNode.Cost == newCost && relatedNode.CurrentLength > parent.CurrentLength + edgeChild.Length)
        //    {
        //        relatedNode.Cost = newCost;
        //        relatedNode.Parent = parent;
        //        relatedNode.CurrentLength = parent.CurrentLength + edgeChild.Length;
        //    }
        //}

        //public static void CalculateDistance(Node start, Node end)
        //{
        //    start.Weight = Math.Sqrt(Math.Pow(start.Coordinates[0] - end.Coordinates[0], 2) + Math.Pow(start.Coordinates[1] - end.Coordinates[1], 2));
        //}

        //public static List<Node> SortListByDistance(List<Node> list, Node node)
        //{
        //    bool isNotAdded = true;
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (node.Weight < list[i].Weight) //fix the distance and not weight = calculated distance by coords
        //        {
        //            list.Insert(i, node);
        //            isNotAdded = false;
        //            break;
        //        }
        //    }
        //    if (isNotAdded)
        //    {
        //        list.Add(node);
        //    }
        //    return list;
        //}

        public static void PrintPath(Node stopNode, Node goalNode, bool algorithm)
        {
            if (string.Compare(goalNode.Name, stopNode.Name) != 0)
            {
                if (algorithm)
                    ResultCheapest += goalNode.Name + "<-";
                else ResultGreedy += goalNode.Name + "<-";
                Path += goalNode.Name + "<-";
                PrintPath(stopNode, goalNode.Parent, algorithm);
            }
            else
            {
                if (algorithm)
                    ResultCheapest += stopNode.Name + "|\n";
                else ResultGreedy += stopNode.Name + "|\n";
                Path += stopNode.Name + "|\n";
                return;
            }
        }
    }
}

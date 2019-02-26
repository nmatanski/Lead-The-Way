using System;

namespace LeadTheWay.Models.GraphLayer
{
    [Serializable]
    public class Edge
    {
        public Node RelatedNode { get; set; } //feature

        public Node FirstNode { get; set; } //feature; the algorithm works without it

        public double Length { get; set; } //feature

        //public byte Type { get; set; }

        public TimeSpan Duration { get; set; } //feature

        public double Price { get; set; } //feature

        public byte ServiceClass { get; set; } //feature; 1-5 stars

        public Timetable Timetable { get; set; } //feature


        //public Edge(Node firstNode, Node relatedNode, double? length) //byte type
        //{
        //    RelatedNode = relatedNode;
        //    Length = length.GetValueOrDefault(0);
        //    //Type = type;
        //}

        public Edge(Node firstNode, Node relatedNode, double? length, TimeSpan duration, double price, byte serviceClass, Timetable timetable)
        {
            RelatedNode = relatedNode;
            Length = length.GetValueOrDefault(0);
            Duration = duration;
            Price = price;
            ServiceClass = serviceClass;
            Timetable = timetable;
        }
    }
}
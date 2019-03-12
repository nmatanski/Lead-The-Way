using LeadTheWay.GraphLayer.Link.Domain;
using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Vertex.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Link.Service
{
    public class Edge : IEdge
    {
        public string EdgeString { get; set; } //FirstNode, RelatedNode - format

        public double Length { get; set; } //feature

        //public TimeSpan Duration { get; set; } //feature
        public long DurationTicks { get; set; }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromTicks(DurationTicks); }
            set { DurationTicks = value.Ticks; }
        }

        public double Price { get; set; } //feature

        public byte ServiceClass { get; set; } //feature; 1-5 stars

        //public Timetable Timetable { get; set; } //feature
        public string TimetableString { get; set; }
        public Timetable Timetable { get; set; } ///TODO: to make it like the Duration from Ticks


        public Node RelatedNode { get; set; } //feature

        public Node FirstNode { get; set; } //feature; the algorithm works without it




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

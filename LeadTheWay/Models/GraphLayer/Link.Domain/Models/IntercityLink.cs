using LeadTheWay.GraphLayer.Vertex.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Link.Domain.Models
{
    public class IntercityLink : IEdge
    {
        public int Id { get; set; }

        public string EdgeString { get; set; } //FirstNode, RelatedNode - format

        [NotMapped]
        public VerticesPair NodesPair { get; set; }

        public double Length { get; set; } //feature

        //public TimeSpan Duration { get; set; } //feature
        [Column(TypeName = "bigint")]
        public long DurationTicks { get; set; }

        public double Price { get; set; } //feature

        public byte ServiceClass { get; set; } //feature; 1-5 stars

        //public Timetable Timetable { get; set; } //feature
        public string TimetableString { get; set; }
    }
}

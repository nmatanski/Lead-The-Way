using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadTheWay.GraphLayer.Link.Domain.Models
{
    public class IntercityLink : IEdge
    {
        public int Id { get; set; }

        public string EdgeString { get; set; } //FirstNode, RelatedNode - format

        [Required]
        [Range(1, double.MaxValue)]
        public double Length { get; set; } //feature

        //public TimeSpan Duration { get; set; } //feature
        [Column(TypeName = "bigint")]
        public long DurationTicks { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Price { get; set; } //feature

        [Required]
        [Range(1, 5)]
        public byte ServiceClass { get; set; } //feature; 1-5 stars

        //public Timetable Timetable { get; set; } //feature
        public string TimetableString { get; set; }

        [NotMapped]
        public VerticesPair NodesPair { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace LeadTheWay.GraphLayer.Link.Domain
{
    public interface IEdge
    {
        string EdgeString { get; set; } //FirstNode, RelatedNode - format

        double Length { get; set; } //feature

        //public TimeSpan Duration { get; set; } //feature
        [Column(TypeName = "bigint")]
        long DurationTicks { get; set; }

        double Price { get; set; } //feature

        byte ServiceClass { get; set; } //feature; 1-5 stars

        //public Timetable Timetable { get; set; } //feature
        string TimetableString { get; set; }
    }
}

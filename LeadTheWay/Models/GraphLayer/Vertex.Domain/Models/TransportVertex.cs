using System.ComponentModel.DataAnnotations;

namespace LeadTheWay.GraphLayer.Vertex.Domain.Models
{
    public class TransportVertex : IVertex
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

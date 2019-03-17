using System.ComponentModel.DataAnnotations;

namespace LeadTheWay.Areas.Admin.Models.DTOs
{
    public class SimpleGraph
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}

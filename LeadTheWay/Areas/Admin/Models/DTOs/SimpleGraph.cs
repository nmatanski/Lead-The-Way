using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

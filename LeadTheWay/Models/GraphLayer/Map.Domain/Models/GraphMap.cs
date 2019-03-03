using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.Models.GraphLayer.Map.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Map.Domain.Models
{
    public class GraphMap : IGraph
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string GraphString { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public string NodeHistoryString { get; set; }

        [NotMapped]
        public List<string> NodeHistory { get; set; }

        public string EdgeHistoryString { get; set; }

        [NotMapped]
        public List<string> EdgeHistory { get; set; }

        [NotMapped]
        public Graph Graph { get; set; }

        [NotMapped]
        public int CurrentEdgeIdToAdd { get; set; }
    }
}

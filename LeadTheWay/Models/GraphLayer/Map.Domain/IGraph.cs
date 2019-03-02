using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models.GraphLayer.Map.Domain
{
    public interface IGraph
    {
        int Id { get; set; }

        string Name { get; set; }

        string GraphString { get; set; }
    }
}

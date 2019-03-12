using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.GraphLayer.Link.Domain.Models
{
    public class Timetable
    {
        public List<TimeSpan> DepartureHoursFromStraightDirection { get; set; }

        public List<TimeSpan> DepartureHoursFromOppositeDirection { get; set; }
    }
}

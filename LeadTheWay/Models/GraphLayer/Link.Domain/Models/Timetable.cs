using System;
using System.Collections.Generic;

namespace LeadTheWay.GraphLayer.Link.Domain.Models
{
    public class Timetable
    {
        public List<TimeSpan> DepartureHoursFromStraightDirection { get; set; }

        public List<TimeSpan> DepartureHoursFromOppositeDirection { get; set; }
    }
}

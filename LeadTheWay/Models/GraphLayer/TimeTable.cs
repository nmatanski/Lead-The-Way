using System;
using System.Collections.Generic;

namespace LeadTheWay.Models.GraphLayer
{
    public class Timetable
    {
        public List<TimeSpan> DepartureHoursFromStraightDirection { get; set; }

        public List<TimeSpan> DepartureHoursFromOppositeDirection { get; set; }
    }
}
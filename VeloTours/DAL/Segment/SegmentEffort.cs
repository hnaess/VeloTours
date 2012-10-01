using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.DAL.Segment
{
    public class SegmentEffort : IComparable<SegmentEffort>
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int ElapsedTime { get; set; }
        public DateTime Start { get; set; }

        #region IComparable<Employee> Members

        public int CompareTo(SegmentEffort other)
        {
            if (this.ElapsedTime > other.ElapsedTime) return 1;
            else if (this.ElapsedTime < other.ElapsedTime) return -1;
            else return 0;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeloTours.DAL.Segment
{
    public struct EffortUpdateStatus
    {
        public int riders;
        public int ridden;
        public EffortUpdateStatus(int noRiders, int noRidden)
        {
            riders = noRiders;
            ridden = noRidden;
        }
    }
}

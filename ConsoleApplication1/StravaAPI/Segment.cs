using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.StravaAPI
{
    public class Segment
    {
        public int id { get; set; }
        public string name { get; set; }
        public double distance { get; set; }
        public double elevationGain { get; set; }
        public double elevationHigh { get; set; }
        public double elevationLow { get; set; }
        public double averageGrade { get; set; }
        public string climbCategory { get; set; }
    }

    public class RootObject
    {
        public Segment segment { get; set; }
    }
}

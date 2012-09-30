using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stravan;
using Stravan.Json;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        // http://www.geekzone.co.nz/VisualStudio/7906
        // https://stravasite-main.pbworks.com/w/page/51754151/Strava%20REST%20API%20Method%3A%20segments%20show
        // http://json2csharp.com/


        // http://app.strava.com/api/v1/segments/229781/efforts


        static void Main(string[] args)
        {
            var SegmentId = 1637189;

            GetSegmentRides(SegmentId);
         }

        private static List<SegmentEffort> GetSegmentRides(int SegmentId)
        {
            StravaWebClient cli = new StravaWebClient();
            SegmentService serv = new SegmentService(cli);

            serv.Show(SegmentId);

            List<SegmentEffort> rides = new List<SegmentEffort>();
            SegmentEfforts segmentEfforts = null;
            int offset = 0;
            do
            {
                segmentEfforts = serv.Efforts(SegmentId, offset: offset);
            } while (AddEfforts(ref rides, ref segmentEfforts, ref offset));

            rides.Sort();

            return rides;
        }

        private static void SegmentInfo(int SegmentId)
        {

        }

        private static bool AddEfforts(ref List<SegmentEffort> rides, ref SegmentEfforts segmentEfforts, ref int offset)
        {
            if (segmentEfforts.Efforts.Count == 0)
                return false;
            
            foreach (Effort effort in segmentEfforts.Efforts)
            {
                offset++;

                int elapsedTime = effort.ElapsedTime;
                int id = effort.Id;
                int athleteId = effort.Athlete.Id;
                DateTime startTime = Convert.ToDateTime(effort.StartDate);

                var ride = new SegmentEffort() { ElapsedTime = elapsedTime, Id = id, Start = startTime, AthleteId = athleteId };
                rides.Add(ride);
                //Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", i, effort.Athlete.Name, effort.ElapsedTime, effort.StartDate.ToString()));
            }
            return true;
        }
    }

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

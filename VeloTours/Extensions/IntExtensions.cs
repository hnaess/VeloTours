using System;

namespace VeloTours.Models.Extensions
{
    public static partial class EnumerableStats
    {
        public static String ToTime(this System.Nullable<Int32> secs)
        {
            TimeSpan t = TimeSpan.FromSeconds(secs ?? 0);

            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);

            return answer;
        }
    }
}

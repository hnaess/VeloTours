using System;

namespace VeloTours.Models.Extensions
{
    public static partial class IntExtensions
    {
        public static String ToTime(this int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            if (t.TotalMinutes > 0)
                return string.Format("{0:D2}:{1:D2}", (int)t.TotalMinutes, t.Seconds);
            else if (t.Seconds > 0)
                return string.Format("{0:D2}", t.Seconds);
            else
                return String.Empty;
                   
        }

        public static String ToTimeStandard(this int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            if (t.TotalDays > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", (int)t.TotalDays, t.Hours, t.Minutes, t.Seconds);
            else if (t.Hours > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            else if (t.Minutes > 0)
                return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            else
                return string.Format("{0:D2}", t.Seconds);
        }
    }
}

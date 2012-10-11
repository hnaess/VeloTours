using System;

namespace VeloTours.Models.Extensions
{
    public static partial class IntExtensions
    {
        public static String ToTime(this int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            if (t.Days > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", t.Days, t.Hours, t.Minutes, t.Seconds);
            else if (t.Hours > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            else if (t.Minutes > 0)
                return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            else
                return string.Format("{0:D2}", t.Seconds);

            //if (t.Days > 0)
            //    return string.Format("{0:D2}d:{1:D2}h:{2:D2}m:{3:D2}s", t.Days, t.Hours, t.Minutes, t.Seconds);
            //else if (t.Hours > 0)
            //    return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
            //else if (t.Minutes > 0)
            //    return string.Format("{0:D2}m:{1:D2}s", t.Minutes, t.Seconds);
            //else 
            //    return string.Format("{0:D2}s", t.Minutes, t.Seconds);
        }
    }
}

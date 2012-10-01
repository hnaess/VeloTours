using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace VeloTours.DAL
{
    public class Utils
    {
        internal static CultureInfo SetStravaCultureAndReturnCurrentCulture()
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;

            var stravaCulture = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = stravaCulture;

            return originalCulture;
        }
    }
}
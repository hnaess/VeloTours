using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;

namespace VeloTours.DAL
{
    public abstract class RideUpdate
    {
        protected TourModelContainer db = new TourModelContainer();

        #region Singletons

        protected StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }

        #endregion

        public abstract void Update();
    }
}
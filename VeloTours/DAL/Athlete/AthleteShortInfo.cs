using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.DAL.Athlete
{
    public class AthleteShortInfo
    {
        public enum DbState { Unknown, New, Update, Saved }
        
        //public int ID { get; set; }
        public string Name { get; set; }
        public DbState State { get; set; }
    }
}
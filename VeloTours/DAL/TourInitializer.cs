using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VeloTours.Models;

namespace VeloTours.DAL
{
    public class TourInitializer : DropCreateDatabaseIfModelChanges<TourModelContainer>
    {
        protected override void Seed(TourModelContainer context)
        {
            InitDB.Init(context);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;
using VeloTours.DAL;

namespace VeloTours.DAL.IntilizeDB.Norway
{
    internal class SegmentData
    {
        internal static void Baerum(TourModelContainer context, Models.Region region)
        {
            var segmentAreas = new List<SegmentArea>
            {
                InitDB.NewSegmentArea(context, region, null, "Østerås-Eiksmarka"),
                InitDB.NewSegmentArea(context, region, null, "Hosle nord"),
                InitDB.NewSegmentArea(context, region, null, "Voll"),
                InitDB.NewSegmentArea(context, region, null, "Grav"),
                InitDB.NewSegmentArea(context, region, null, "Hosle sør"),
                InitDB.NewSegmentArea(context, region, null, "Jar"),
                InitDB.NewSegmentArea(context, region, null, "Lysaker"),
                InitDB.NewSegmentArea(context, region, null, "Snarøya"),
                InitDB.NewSegmentArea(context, region, null, "Stabekk"),
                InitDB.NewSegmentArea(context, region, null, "Høvik"),
                InitDB.NewSegmentArea(context, region, null, "Løkeberg-Blommenholm"),
                InitDB.NewSegmentArea(context, region, null, "Haslum"),
                ////InitDB.NewSegmentArea(context, region, null, "Østre Bærumsmarka"),
                InitDB.NewSegmentArea(context, region, null, "Sandvika-Valler"),
                InitDB.NewSegmentArea(context, region, null, "Jong"),
                InitDB.NewSegmentArea(context, region, null, "Kirkerud-Sollihøgda"),
                //InitDB.NewSegmentArea(context, region, null, "Slependen-Tanum"),
                //InitDB.NewSegmentArea(context, region, null, "Dønski-Rud"),
                //InitDB.NewSegmentArea(context, region, null, "Kolsås"),
                //InitDB.NewSegmentArea(context, region, null, "Rykkinn"),
                //InitDB.NewSegmentArea(context, region, null, "Bærums Verk"),
                InitDB.NewSegmentArea(context, region, null, "Lommedalen"),
                // øst-vest, nord/syd
                InitDB.NewSegmentArea(context, region, null, "Krogskogen", SegmentArea.AreaTypeEnum.MountainBike),
                InitDB.NewSegmentArea(context, region, null, "Bærumsmarka", SegmentArea.AreaTypeEnum.MountainBike),
                InitDB.NewSegmentArea(context, region, null, "Vestmarka", SegmentArea.AreaTypeEnum.MountainBike),
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            InitArea(context, region, "Slependen-Tanum", new List<int>() { 1355742, 1108129, 1489331, 1128542 });
            InitArea(context, region, "Kolsås", new List<int>() { 1433727, 1225116, 2173825 });
            InitArea(context, region, "Dønski/Rud", new List<int>() { 1518560, 1596245, 1353132 });
            InitArea(context, region, "Rykkinn", new List<int>() { 1637189, 1382813, 1524861, 1354941, 1242445 });
            InitArea(context, region, "Bærums Verk", new List<int>() { 730928, 1797698 });
        }

        private static void InitArea(TourModelContainer context, Models.Region region, string name, List<int> segmentList)
        {
            var segments = new List<Models.Segment>();
            for (int i = 0; i < segmentList.Count; i++ )
            {
                segments.Add(InitDB.NewSegment(context, segmentList[i], name + " #" + i));
            }
            segments.ForEach(s => context.Segments.Add(s));

            context.SegmentAreas.Add(InitDB.NewSegmentArea(context, region, segments, name));
            context.SaveChanges();
        }


        internal static void Asker(TourModelContainer context, Models.Region region)
        {
            var segmentAreas = new List<SegmentArea>
            {
                InitDB.NewSegmentArea(context, region, null, "Nesøya"),
                InitDB.NewSegmentArea(context, region, null, "Nesbru"),
                InitDB.NewSegmentArea(context, region, null, "Billingstad"),
                InitDB.NewSegmentArea(context, region, null, "Hvalstad"),
                InitDB.NewSegmentArea(context, region, null, "Skaugum"),
                InitDB.NewSegmentArea(context, region, null, "Sem"),
                InitDB.NewSegmentArea(context, region, null, "Syverstad"),
                InitDB.NewSegmentArea(context, region, null, "Fusdal"),
                InitDB.NewSegmentArea(context, region, null, "Sentrum"),
                InitDB.NewSegmentArea(context, region, null, "Drengsrud"),
                InitDB.NewSegmentArea(context, region, null, "Vettre"),
                InitDB.NewSegmentArea(context, region, null, "Borgen"),
                InitDB.NewSegmentArea(context, region, null, "Blakstad"),
                InitDB.NewSegmentArea(context, region, null, "Vollen"),
                InitDB.NewSegmentArea(context, region, null, "Heggedal"),
                InitDB.NewSegmentArea(context, region, null, "Solberg"),
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }

        internal static void Nesodden(TourModelContainer context, Models.Region region)
        {
            var segmentAreas = new List<Models.SegmentArea>
            {
                InitDB.NewSegmentArea(context, region, null, "Berger"), 
                InitDB.NewSegmentArea(context, region, null, "Tangen"), 
                InitDB.NewSegmentArea(context, region, null, "Fjellstrand"), 
                InitDB.NewSegmentArea(context, region, null, "Jaer"), 
                InitDB.NewSegmentArea(context, region, null, "Myklerud"), 
                InitDB.NewSegmentArea(context, region, null, "Bjørnemyr"), 
                InitDB.NewSegmentArea(context, region, null, "Kolbotn"), 
                InitDB.NewSegmentArea(context, region, null, "Tårnåsen"), 
                InitDB.NewSegmentArea(context, region, null, "Sofiemyr"), 
                InitDB.NewSegmentArea(context, region, null, "Greverud"), 
                InitDB.NewSegmentArea(context, region, null, "Svartskog"), 
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }

        internal static void OsloKlatreKonge(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                InitDB.NewSegment(context, 632847, "Kongsveien"),
                InitDB.NewSegment(context, 1942901, "Klatringen til Tryvann fra Gressbanen"),
                InitDB.NewSegment(context, 660072, "Grefsenkollen"),
            };
            segments.ForEach(s => context.Segments.Add(s));

            var segmentAreas = new List<SegmentArea>
            {
                InitDB.NewSegmentArea(context, region, segments, "Oslo klatrekonge"),

                InitDB.NewSegmentArea(context, region, null, "Gamle Oslo"),
                InitDB.NewSegmentArea(context, region, null, "Grünerløkka"),
                InitDB.NewSegmentArea(context, region, null, "Sagene"),
                InitDB.NewSegmentArea(context, region, null, "Hanshaugen"),
                InitDB.NewSegmentArea(context, region, null, "Frogner"),
                InitDB.NewSegmentArea(context, region, null, "Ullern"),
                InitDB.NewSegmentArea(context, region, null, "Vestre Aker"),
                InitDB.NewSegmentArea(context, region, null, "Nordre Aker"),
                InitDB.NewSegmentArea(context, region, null, "Bjerke"),
                InitDB.NewSegmentArea(context, region, null, "Grorud"),
                InitDB.NewSegmentArea(context, region, null, "Stovner"),
                InitDB.NewSegmentArea(context, region, null, "Alna"),
                InitDB.NewSegmentArea(context, region, null, "Østensjø"),
                InitDB.NewSegmentArea(context, region, null, "Nordstrand"),
                InitDB.NewSegmentArea(context, region, null, "Søndre Nordstrand"),
                InitDB.NewSegmentArea(context, region, null, "Sentrum"),

                InitDB.NewSegmentArea(context, region, null, "Lillomarka", SegmentArea.AreaTypeEnum.MountainBike),
                InitDB.NewSegmentArea(context, region, null, "Nordmarka syd", SegmentArea.AreaTypeEnum.MountainBike),
                InitDB.NewSegmentArea(context, region, null, "Nordmarka nord", SegmentArea.AreaTypeEnum.MountainBike),
                InitDB.NewSegmentArea(context, region, null, "Østmarka", SegmentArea.AreaTypeEnum.MountainBike),
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }
    }
}
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
                InitDB.NewSegmentArea(context, region, null, "Østre Bærumsmarka"),
                InitDB.NewSegmentArea(context, region, null, "Sandvika-Valler"),
                InitDB.NewSegmentArea(context, region, null, "Jong"),
                InitDB.NewSegmentArea(context, region, null, "Slependen-Tanum"),
                //InitDB.NewSegmentArea(context, region, null, "Dønski-Rud"),
                //InitDB.NewSegmentArea(context, region, null, "Kolsås"),
                //InitDB.NewSegmentArea(context, region, null, "Rykkinn"),
                InitDB.NewSegmentArea(context, region, null, "Kirkerud-Sollihøgda"),
                //InitDB.NewSegmentArea(context, region, null, "Bærums Verk"),
                InitDB.NewSegmentArea(context, region, null, "Lommedalen"),
                InitDB.NewSegmentArea(context, region, null, "Krogskogen", isMountainBike: true),
                InitDB.NewSegmentArea(context, region, null, "Bærumsmarka", isMountainBike: true),
                InitDB.NewSegmentArea(context, region, null, "Vestmarka", isMountainBike: true),
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            InitDonskiRud(context, region);
            InitKolsos(context, region);
            InitRykkinn(context, region);
            InitBVerk(context, region);
        }

        private static void InitKolsos(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                InitDB.NewSegment(context, 1433727, "Alpe de Kolsåslia"),
                InitDB.NewSegment(context, 1225116, "Brynsveien (Bærumsvn. - Kolsås)"),
                InitDB.NewSegment(context, 2173825, "Brynsveien-spurten"),
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SegmentAreas.Add(InitDB.NewSegmentArea(context, region, segments, "Kolsås"));
            context.SaveChanges();
        }

        private static void InitDonskiRud(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                InitDB.NewSegment(context, 1518560, "Bærumsveien (Brynsvn. - Johs. Haugeruds vei)"),
                InitDB.NewSegment(context, 1596245, "Sogneprest M.K. - Rudsveien"),
                InitDB.NewSegment(context, 1353132, "Rud industriområde"),
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SegmentAreas.Add(InitDB.NewSegmentArea(context, region, segments, "Dønski-Rud"));
            context.SaveChanges();
        }

        private static void InitBVerk(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                InitDB.NewSegment(context, 730928, "Bærums Verk - Belset"),
                InitDB.NewSegment(context, 1797698, "Steinshøgda Vest"),
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SegmentAreas.Add( InitDB.NewSegmentArea(context, region, segments, "Bærums Verk") );
            context.SaveChanges();
        }

        private static void InitRykkinn(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                InitDB.NewSegment(context, 1637189, "Nybrua - Bryn kirke"),
                InitDB.NewSegment(context, 1382813, "Rykkinn round (counter clockwise)"),
                InitDB.NewSegment(context, 1524861, "Paal Bergs vei"),
                InitDB.NewSegment(context, 1354941, "Belset-strekket"),
                InitDB.NewSegment(context, 1242445, "Gamle Lommedalsvei (Brynsvn - Lommedalsveien)"),
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SegmentAreas.Add(InitDB.NewSegmentArea(context, region, segments, "Rykkinn"));
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

                InitDB.NewSegmentArea(context, region, null, "Lillomarka", isMountainBike: true),
                InitDB.NewSegmentArea(context, region, null, "Nordmarka syd", isMountainBike: true),
                InitDB.NewSegmentArea(context, region, null, "Nordmarka nord", isMountainBike: true),
                InitDB.NewSegmentArea(context, region, null, "Østmarka", isMountainBike: true),
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }
    }
}
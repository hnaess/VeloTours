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
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Østerås-Eiksmarka", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Hosle nord", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Voll", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Grav", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Hosle sør", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Jar", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Lysaker", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Snarøya", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Stabekk", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Høvik", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Løkeberg-Blommenholm", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Haslum", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Østre Bærumsmarka", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Sandvika-Valler", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Jong", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Slependen-Tanum", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Dønski-Rud", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Kolsås", },
                //new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Rykkinn", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Kirkerud-Sollihøgda", },
                //new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Bærums Verk", },
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Lommedalen", },
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            InitRykkinn(context, region);
            InitBVerk(context, region);
        }

        private static void InitBVerk(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                new Models.Segment { SegmentID = 730928, Name = "Bærums Verk - Belset", LastUpdated = new DateTime(1980, 1, 1), },
                new Models.Segment { SegmentID = 1797698, Name = "Steinshøgda Vest", LastUpdated = new DateTime(1980, 1, 1), },
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SaveChanges();

            AddSegmentArea(context, region, "Bærums Verk", segments);
        }

        private static void InitRykkinn(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                new Models.Segment { SegmentID = 1637189, Name = "Nybrua - Bryn kirke", LastUpdated = new DateTime(1980, 1, 1), },
                new Models.Segment { SegmentID = 1382813, Name = "Rykkinn round (counter clockwise)", LastUpdated = new DateTime(1980, 1, 1), },
                new Models.Segment { SegmentID = 1524861, Name = "Paal Bergs vei", LastUpdated = new DateTime(1980, 1, 1), },
                new Models.Segment { SegmentID = 1354941, Name = "Belset-strekket", LastUpdated = new DateTime(1980, 1, 1), },
                new Models.Segment { SegmentID = 1242445, Name = "Gamle Lommedalsvei (Brynsvn - Lommedalsveien)", LastUpdated = new DateTime(1980, 1, 1), },
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SaveChanges();

            AddSegmentArea(context, region, "Rykkinn", segments);
        }

        internal static void Asker(TourModelContainer context, Models.Region region)
        {
            var segmentAreas = new List<SegmentArea>
            {
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Nesøya", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Nesbru", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Billingstad", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Hvalstad", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Skaugum", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Sem", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Syverstad", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Fusdal", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Sentrum", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Drengsrud", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Vettre", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Borgen", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Blakstad", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Vollen", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Heggedal", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Solberg", },
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }

        internal static void Nesodden(TourModelContainer context, Models.Region region)
        {
            var segmentAreas = new List<Models.SegmentArea>
            {
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Berger", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Tangen", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Fjellstrand", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Jaer", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Myklerud", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Bjørnemyr", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Kolbotn", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Tårnåsen", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Sofiemyr", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Greverud", },
                new Models.SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = "Svartskog", },
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }


        internal static void OsloKlatreKonge(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Models.Segment>
            {
                new Models.Segment { 
                    SegmentID = 632847, 
                    Name = "Kongsveien", 
                    GradeType = 1,
                    Distance = 5.6,
                    //ElevDifference = 372,
                    AvgGrade = 6.7,
                    NoRiders = 627,
                    NoRidden = 2952,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                },
                new Models.Segment { 
                    SegmentID = 1942901,
                    Name = "Klatringen til Tryvann fra Gressbanen", 
                    Distance = 1.8,
                    //ElevDifference = 122,
                    AvgGrade = 6.7,
                    NoRiders = 767,
                    NoRidden = 8132,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                },
                new Models.Segment { 
                    SegmentID = 660072,
                    Name = "Grefsenkollen", 
                    //ElevDifference = 91,
                    Distance = 2.0,
                    AvgGrade = 4.6,
                    NoRiders = 705,
                    NoRidden = 4872,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                }
            };
            segments.ForEach(s => context.Segments.Add(s));

            var segmentAreas = new List<SegmentArea>
            {
                new SegmentArea { 
                    Name = "Oslo klatrekonge",
                    Region = region,
                    Segments = segments,
                    Distance = 9.4,
                    //ElevDifference = 585,
                    AvgGrade = 6.25,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                }
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            UpdateResultForOsloKlatekonge();
        }

        private static void UpdateResultForOsloKlatekonge()
        {
            //throw new NotImplementedException();
        }

        private static void AddSegmentArea(TourModelContainer context, Models.Region region, string name, List<Models.Segment> segments)
        {
            var segmentAreas = new List<SegmentArea>
            {
                new SegmentArea { Region = region, LastUpdated = DateTime.Now, Name = name, Segments = segments },
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();
        }
    }
}
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stravan;
using Stravan.Json;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        // http://www.geekzone.co.nz/VisualStudio/7906
        // https://stravasite-main.pbworks.com/w/page/51754151/Strava%20REST%20API%20Method%3A%20segments%20show
        // http://json2csharp.com/


        // http://app.strava.com/api/v1/segments/229781/efforts
        // http://app.strava.com/api/v1/segments/229781/efforts?offset=2

        
        static void Main(string[] args)
        {
            var SegmentId = 1637189;

            StravaWebClient cli = new StravaWebClient();
            SegmentService serv = new SegmentService(cli);
            //Segment segment = serv.Show(SegmentId);
            
            int i = 0;
            do
            {
                SegmentEfforts segmentEfforts = serv.Efforts(SegmentId);
                PrintEffort(segmentEfforts, ref i);
                serv.Index(serv.

            } while (i % 50 == 0);
         }

        private static void PrintEffort(SegmentEfforts segmentEfforts, ref int i)
        {
            foreach (Effort effort in segmentEfforts.Efforts)
            {
                i++;
                Athlete athlete = effort.Athlete;
                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", i, effort.Athlete.Name, effort.ElapsedTime, effort.StartDate.ToString()));
            }
        }


        void test1()
        {
            System.Net.WebClient proxy = new System.Net.WebClient();
            string uri = "http://app.strava.com/api/v1/segments/1637189";

            byte[] resultJsonAsString = proxy.DownloadData(new Uri(uri));
            string result = System.Text.Encoding.UTF8.GetString(resultJsonAsString);

            JObject jResult = JObject.Parse(result);
            IList<JToken> jResults = jResult["segment"].Children().ToList();

            List<ConsoleApplication1.StravaAPI.Segment> stravaSegments = new List<ConsoleApplication1.StravaAPI.Segment>();
            //foreach(JToken stravaSegment in stravaSegments)
            //{

            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202017
{
    class Program
    {
        static Dictionary<long, Dictionary<long, Dictionary<long, string>>> map = new Dictionary<long, Dictionary<long, Dictionary<long, string>>>(); 

        static void Main(string[] args)
        {
            var initLines = File.ReadAllLines("input17.txt").ToList();
            map[0] = new Dictionary<long, Dictionary<long, string>>();
            for (int y = 0; y < initLines.Count; y++)
            {
                map[0][y] = new Dictionary<long, string>();
                for (int x = 0; x < initLines.First().Length; x++)
                {
                    map[0][y][x] = initLines[y][x].ToString();
                }
            }

            int step = 1;
            while(true)
            {
                long minz = map.Keys.Min();
                long maxz = map.Keys.Max();

                var zps = map.Values.SelectMany(zp => zp).ToList();
                long miny = zps.Select(kvp => kvp.Key).Min();
                long maxy = zps.Select(kvp => kvp.Key).Max();

                var yps = zps.Select(kvp => kvp.Value).SelectMany(yp => yp).ToList();
                long minx = yps.Select(kvp => kvp.Key).Min();
                long maxx = yps.Select(kvp => kvp.Key).Max();

                var nextMap = 


                step++;
            }

            Console.WriteLine("Hello World!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202016
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<(int from, int to)>> rules = new Dictionary<string, List<(int from, int to)>>();
            var lines = File.ReadAllLines("input16.txt");
            foreach(var l in lines)
            {
                if(l == "")
                {
                    break;
                }

                var rs = l.Split(":");
                rules.Add(rs[0], new List<(int from, int to)>());

                var vs = rs[1].Split(" or ");
                var vs0s = vs[0].Split("-");
                var vs1s = vs[1].Split("-");

                rules[rs[0]].Add((int.Parse(vs0s[0]), int.Parse(vs0s[1])));
                rules[rs[0]].Add((int.Parse(vs1s[0]), int.Parse(vs1s[1])));
            }

            var myTicket = lines.SkipWhile(l => l != "your ticket:").Skip(1).First();

            var nearByTickets = lines.SkipWhile(l => l != "nearby tickets:").Skip(1).ToList();

            long ret1 = 0;
            var allValues = rules.Values.SelectMany(v => v).ToList();

            var validTickets = new List<List<int>>();

            foreach (var t in nearByTickets)
            {
                var tvalues = t.Split(",").Select(v => int.Parse(v)).ToList();
                bool valid = true;
                foreach(var tv in tvalues)
                {
                    if(!allValues.Any(r=>tv >= r.from && tv <= r.to))
                    {
                        ret1 += tv;
                        valid = false;
                    }
                }
                if(valid)
                {
                    validTickets.Add(tvalues);
                }
            }

            Dictionary<int, List<string>> possibleMeanings = new Dictionary<int, List<string>>();
            for(int i = 0; i<myTicket.Split(",").Length;i++)
            {
                possibleMeanings.Add(i, new List<string>());
                possibleMeanings[i].AddRange(rules.Keys);
            }

            while (possibleMeanings.Values.Any(ms => ms.Count > 1))
            {
                foreach (var t in validTickets)
                {
                    for (int i = 0; i < t.Count; i++)
                    {
                        var pns = rules.Where(r => r.Value.Any(rv => t[i] <= rv.to && t[i] >= rv.from)).Select(r => r.Key).ToList();
                        possibleMeanings[i] = possibleMeanings[i].Intersect(pns).ToList();
                    }
                }

                foreach(var o in possibleMeanings.Where(ms => ms.Value.Count == 1))
                {
                    foreach(var pms in possibleMeanings)
                    {
                        if (pms.Key != o.Key)
                        {
                            pms.Value.Remove(o.Value.Single());
                        }
                    }
                }
            }

            long ret2 = 1;
            var myteicketvalues = myTicket.Split(",").Select(v => int.Parse(v)).ToList();

            foreach (var m in possibleMeanings.Where(ms =>ms.Value.Single().StartsWith("departure")))
            {
                ret2 *= myteicketvalues[m.Key];
            }

            Console.WriteLine("Hello World!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day7
{
    class Rule
    {
        public string Color { get; set; }
        public List<Tuple<string, int>> Contains { get; set; }
        public int Bags
        {
            get
            {
                return 1 + Contains.Sum(c => c.Item2 * Program.rules.Single(r => r.Color == c.Item1).Bags);
            }
        }
    }

    class Program
    {
        public static List<Rule> rules = new List<Rule>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input7.txt");
            foreach(var line in lines)
            {
                var ps = line.Split(" bags contain ");
                var rule = new Rule { Color = ps[0].Trim(), Contains = new List<Tuple<string, int>>() };
                var contains = ps[1].Split(", ");
                foreach(var c in contains)
                {
                    var cc = c.TrimEnd('.').Replace(" bags", "").Replace(" bag", "");
                    if (cc != "no other")
                    {
                        var a = int.Parse(cc.Substring(0, 1));
                        var ccc = cc.Substring(2);
                        rule.Contains.Add(new Tuple<string, int>(ccc, a));
                    }
                }
                rules.Add(rule);
            }
            
            List<Rule> ars = rules.Where(r => r.Contains.Any(t => t.Item1 == "shiny gold")).ToList();
            List<string> colors = new List<string>();
            while(ars.Any())
            {
                var ri = ars.First();
                ars = ars.Skip(1).ToList();
                colors.Add(ri.Color);
                var nrs = rules.Where(r => r.Contains.Any(t => t.Item1 == ri.Color)).ToList();
                ars = ars.Union(nrs).ToList();
            }

            var x = colors.Distinct().Count();

            var shgr = rules.Single(r => r.Color == "shiny gold");
            int ret2 = shgr.Bags;

            Console.WriteLine("Hello World!");
        }
    }
}

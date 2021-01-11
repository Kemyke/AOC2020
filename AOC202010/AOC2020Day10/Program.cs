using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var joltages = File.ReadAllLines("input10.txt").Select(l => int.Parse(l)).OrderBy(j => j).ToList();
            joltages.Insert(0, 0);
            joltages.Add(joltages.Last() + 3);
            var diffs = joltages.Skip(1).Select(j => j - joltages.ElementAt(joltages.IndexOf(j) - 1)).ToList();
            var ret1 = diffs.Where(d => d == 1).Count() * diffs.Where(d => d == 3).Count();

            var oneLengths = string.Join("", diffs.Select(d => d.ToString())).Split('3').Select(s => s.Length).Where(d=>d > 1).ToList();
            
            List<int> series = new List<int> { 1, 2, 4, 7 };
            //for (int i = 1; i < 100; i++)
            //{
            //    series.Add(series[series.Count - 1] + series[series.Count - 2] + series[series.Count - 3]);
            //}

            var ret2 = oneLengths.Select(n => (long)series[(int)n - 1]).Aggregate((a, b) => a * b);
        }
    }
}

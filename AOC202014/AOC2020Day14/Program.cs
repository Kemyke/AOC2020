using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day14
{
    class Program
    {
        static void GenerateMasks(List<string> masks)
        {
            if(masks.First().Contains("X"))
            {
                var oms = masks.ToList();
                masks.Clear();
                foreach(var om in oms)
                {
                    var idx = om.IndexOf("X");
                    var mm = om.Remove(idx, 1);
                    masks.Add(mm.Insert(idx, "0"));
                    masks.Add(mm.Insert(idx, "1"));
                }

                GenerateMasks(masks);
            }
        }

        static void Main(string[] args)
        {
            long mask1 = 1;
            long mask0 = 0;
            Dictionary<long, long> memory = new Dictionary<long, long>(); 

            var lines = File.ReadAllLines("input14.txt");
            foreach (var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    var m = line.Replace("mask = ", "");
                    mask1 = Convert.ToInt64(m.Replace("X", "0"), 2);
                    mask0 = Convert.ToInt64(m.Replace("X", "1"), 2);
                }
                else
                {
                    var l = line.Replace("mem[", "");
                    long addr = long.Parse(l.Substring(0, l.IndexOf("]")));
                    long value = long.Parse(l.Substring(l.IndexOf("=") + 1));

                    value = value | mask1;
                    value = value & mask0;

                    memory[addr] = value;
                }
            }

            var ret1 = memory.Values.Sum();

            memory = new Dictionary<long, long>();
            List<string> masks = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    var mask = line.Replace("mask = ", "");
                    mask1 = Convert.ToInt64(mask.Replace("X", "0"), 2);
                    var maskY = mask.Replace("1", "0");
                    maskY = maskY.Replace("0", "Y");

                    masks = new List<string>() { maskY };
                    GenerateMasks(masks);

                }
                else
                {
                    var l = line.Replace("mem[", "");
                    long addr = long.Parse(l.Substring(0, l.IndexOf("]")));
                    long value = long.Parse(l.Substring(l.IndexOf("=") + 1));

                    addr = addr | mask1;

                    foreach(var m in masks)
                    {
                        var mask1p = Convert.ToInt64(m.Replace("Y", "0"), 2);
                        var mask0p = Convert.ToInt64(m.Replace("Y", "1"), 2);

                        addr = addr | mask1p;
                        addr = addr & mask0p;

                        memory[addr] = value;
                    }
                }
            }

            var ret2 = memory.Values.Sum();
            Console.WriteLine("Hello World!");
        }
    }
}

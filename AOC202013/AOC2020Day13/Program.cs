using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day13
{
    class Program
    {
        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input13.txt");
            var target = int.Parse(lines[0]);
            List<long> schedules = lines[1].Split(",").Where(s => s != "x").Select(s => long.Parse(s)).ToList();
            long minEtd = int.MaxValue;
            long selectedBus = -1;

            foreach(var bus in schedules)
            {
                var etd = (target / bus) * bus + bus;
                if(etd < minEtd)
                {
                    minEtd = etd;
                    selectedBus = bus;
                }
            }

            long ret = (minEtd - target) * selectedBus;

            List<(long n, long p)> nums = new List<(long n, long p)>();
            int i = 0;
            foreach (var l in lines[1].Split(","))
            {
                if (l != "x")
                {
                    nums.Add((long.Parse(l), ((long.Parse(l) - i) + 5 * long.Parse(l))% long.Parse(l)));
                }

                i++;
            }
             

            

            long step = nums.First().n;
            long t = 0;
            foreach (var num in nums.Skip(1))
            {
                for (long dt = 0; dt < long.MaxValue; dt += step)
                {
                    if ((t + dt) % num.n == num.p)
                    {
                        t += dt;
                        step = lcm(step, num.n);
                        break;
                    }
                }
            }



            
            int n = 0;
            for (long z = 0; z < 100000000; z += 17)
            {
                if(z%13 == 11)
                {
                    Console.WriteLine($"13: {n}");
                    //102
                    //6
                    //19
                    //32
                }
                if(z%19 == 16)
                {
                    Console.WriteLine($"19: {n}");
                    //187
                    //11
                    //30
                    //49
                }
                if (n==66)
                {

                }
                if (z % 19 == 16)
                    if (z % 13 == 11)
                    {

                    }
                        n++;
            }





            var nf = nums.First().n;            
            long max = nums.Select(a => a.n).Aggregate((a, b) => a * b);
            for (long j = 100000000000000; j < max; j += nf)
            {
                if(nums.All(c=> j % c.n == c.p))
                {

                }                
            }

            Console.WriteLine("Hello World!");
        }
    }
}

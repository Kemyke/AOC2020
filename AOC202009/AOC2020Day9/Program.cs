using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day9
{
    class Program
    {
        public static Queue<long> numbers = new Queue<long>();

        static bool IsValid(long num)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    var a = numbers.ElementAt(i);
                    var b = numbers.ElementAt(j);
                    if (a != b && a + b == num)
                    {
                        return true;
                    }
                }
            return false;
        }

        static void Main(string[] args)
        {
            //int preamble = 25;
            var lines = File.ReadAllLines("input9.txt").Select(l => long.Parse(l)).ToList();
            //foreach (var line in lines)
            //{
            //    if (numbers.Count < preamble)
            //    {
            //        numbers.Enqueue(line);
            //        continue;
            //    }
            //    if (IsValid(line))
            //    {
            //        numbers.Enqueue(line);
            //        numbers.Dequeue();
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            var cn = 29221323;
            for (int ss = 2; ss < 1000; ss++)
            {
                int setSize = ss;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines.Skip(i).Take(setSize).Sum() == cn)
                    {
                        var min = lines.Skip(i).Take(setSize).Min();
                        var max = lines.Skip(i).Take(setSize).Max();
                        var ret = min + max;
                        break;
                    }
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}

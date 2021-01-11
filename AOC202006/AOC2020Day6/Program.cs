using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            int ret = 0;
            var lines = File.ReadAllLines("input6.txt");
            List<char> answers = new List<char>();
            int gn = 0;
            foreach(var line in lines)
            {
                if(line == "")
                {
                    ret += answers.Distinct().Select(c => answers.Where(cc => cc == c).Count()).Where(n => n == gn).Count();
                    answers.Clear();
                    gn = 0;
                    continue;
                }
                foreach(var ch in line)
                {
                    answers.Add(ch);
                }
                gn++;
            }
            ret += answers.Distinct().Select(c => answers.Where(cc => cc == c).Count()).Where(n => n == gn).Count();

            Console.WriteLine("Hello World!");
        }
    }
}

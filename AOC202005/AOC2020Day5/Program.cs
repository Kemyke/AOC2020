using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var passes = File.ReadAllLines("input5.txt");
            int ret = 0;
            List<int> ids = new List<int>();
            foreach(var pass in passes)
            {
                var rowdef = pass.Substring(0, 7);
                var cmndef = pass.Substring(7, 3);

                int min = 1;
                int max = 128;
                int interval = 64;
                foreach(var ch in rowdef)
                {
                    if(ch == 'F')
                    {
                        max -= interval; ;
                    }
                    else
                    {
                        min += interval; ;
                    }
                    interval /= 2;
                }
                if(min != max)
                {

                }

                int rowid = max - 1;

                min = 1;
                max = 8;
                interval = 4;
                foreach (var ch in cmndef)
                {
                    if (ch == 'L')
                    {
                        max -= interval; ;
                    }
                    else
                    {
                        min += interval; ;
                    }
                    interval /= 2;
                }
                if (min != max)
                {

                }
                int cmnid = max - 1;

                int id = 8 * rowid + cmnid;
                if (id > ret)
                {
                    ret = id;
                }
                ids.Add(id);
            }

            int gmin = ids.Min();

            for(int i = gmin; i < ret; i++)
            {
                if(!ids.Contains(i))
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}

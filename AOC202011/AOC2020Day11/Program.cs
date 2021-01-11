using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day11
{
    class Program
    {                
        static int OccupiedSeatsAround((int x, int y) point, List<string> map)
        {
            int ret = 0;
            List<(int x, int y)> ns = new List<(int x, int y)>();
            ns.Add((point.x, point.y - 1));
            ns.Add((point.x, point.y + 1));
            ns.Add((point.x - 1, point.y));
            ns.Add((point.x + 1, point.y));
            
            ns.Add( (point.x - 1, point.y - 1));
            ns.Add( (point.x + 1, point.y - 1));
            ns.Add( (point.x - 1, point.y + 1));
            ns.Add((point.x + 1, point.y + 1));

            ret = ns.Where(p => p.x >= 0 && p.y >= 0 && p.x < map.First().Length && p.y < map.Count).Select(p => map[p.y][p.x]).Where(c => c == '#').Count();


            return ret;
        }

        static int OccupiedSeatsVisible((int x, int y) point, List<string> map)
        {
            int ret = 0;
            var maxx = map.First().Length - 1;
            var maxy = map.Count - 1;
            List<(int x, int y)> ns = new List<(int x, int y)>();
            for(int dy = point.y - 1; dy >= 0; dy--)
            {
                var np = map[dy][point.x];
                if(np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            for (int dy = point.y + 1; dy <= maxy; dy++)
            {
                var np = map[dy][point.x];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            for (int dx = point.x - 1; dx >= 0; dx--)
            {
                var np = map[point.y][dx];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            for (int dx = point.x + 1; dx <= maxx; dx++)
            {
                var np = map[point.y][dx];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            int ul = Math.Min(point.x, point.y);
            for (int i = 1; i <= ul; i++)
            {
                var np = map[point.y - i][point.x - i];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            int ur = Math.Min(maxx - point.x, point.y);
            for (int i = 1; i <= ur; i++)
            {
                var np = map[point.y - i][point.x + i];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            int dl = Math.Min(point.x, maxy - point.y);
            for (int i = 1; i <= dl; i++)
            {
                var np = map[point.y + i][point.x - i];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            int dr = Math.Min(maxx - point.x, maxy - point.y);
            for (int i = 1; i <= dr; i++)
            {
                var np = map[point.y + i][point.x + i];
                if (np == '#')
                {
                    ret++;
                    break;
                }
                else if (np == 'L')
                {
                    break;
                }
            }

            return ret;
        }


        static void Main(string[] args)
        {
            List<string> map = File.ReadAllLines("input11.txt").ToList();
            foreach (var l in map)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            while (true)
            {
                bool changed = false;
                List<string> newState = map.ToList();
                for (int y = 0; y < map.Count; y++)
                {
                    for (int x = 0; x < map.First().Length; x++)
                    {
                        if(y == 9 && x == 8)
                        {

                        }
                        var cp = map[y][x];
                        if (cp != '.')
                        {
                            var onc = OccupiedSeatsVisible((x, y), map);
                            if (cp == 'L' && onc == 0)
                            {
                                newState[y] = newState[y].Remove(x, 1).Insert(x, "#");
                                changed = true;
                            }
                            if(cp == '#' && onc >= 5)
                            {
                                newState[y] = newState[y].Remove(x, 1).Insert(x, "L");
                                changed = true;
                            }
                        }
                    }
                }

                if(!changed)
                {
                    break;
                }
                changed = false;
                map = newState;

                foreach (var l in map)
                    Console.WriteLine(l);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                //Console.ReadLine();
            }

            var ret = map.Select(l => l.Where(c => c == '#').Count()).Sum();
            Console.WriteLine("Hello World!");
        }
    }
}

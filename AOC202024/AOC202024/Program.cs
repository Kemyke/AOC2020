using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202024
{
    class Program
    {
        class Hexa
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int FlipNum { get; set; } = 0;

            public int BlackNeighbours()
            {
                var ns = GetNeighbours();
                List<Hexa> mns = new List<Hexa>();
                var ret = 0;
                foreach(var n in ns)
                {
                    if(map.TryGetValue(n.Y, out var xs))
                    {
                        if(xs.TryGetValue(n.X, out var h))
                        {
                            if(h.FlipNum % 2 == 1)
                            {
                                ret++;
                            }
                        }
                    }
                }
                return ret;
            }

            public List<Hexa> GetNeighbours()
            {
                List<Hexa> ret = new List<Hexa>();
                ret.Add(new Hexa { X = X + 1, Y = Y });
                ret.Add(new Hexa { X = Y % 2 == 0 ? X : X + 1, Y = Y - 1 });
                ret.Add(new Hexa { X = Y % 2 == 0 ? X - 1 : X, Y = Y - 1 });
                ret.Add(new Hexa { X = X - 1, Y = Y });
                ret.Add(new Hexa { X = Y % 2 == 0 ? X - 1 : X, Y = Y + 1 });
                ret.Add(new Hexa { X = Y % 2 == 0 ? X : X + 1, Y = Y + 1 });
                return ret;
            }

            public Hexa GetNeighbour(string n)
            {
                //e, se, sw, w, nw, and ne
                switch (n)
                {
                    case "e":
                        return new Hexa { X = X + 1, Y = Y };
                    case "se":
                        return new Hexa { X = Y % 2 == 0 ? X : X + 1, Y = Y - 1 };
                    case "sw":
                        return new Hexa { X = Y % 2 == 0 ? X - 1 : X, Y = Y - 1 };
                    case "w":
                        return new Hexa { X = X - 1, Y = Y };
                    case "nw":
                        return new Hexa { X = Y % 2 == 0 ? X - 1 : X, Y = Y + 1 };
                    case "ne":
                        return new Hexa { X = Y % 2 == 0 ? X : X + 1, Y = Y + 1 };
                    default:
                        throw new Exception();
                }
            }
        }

        //static List<Hexa> map = new List<Hexa>();
        static Dictionary<int, Dictionary<int, Hexa>> map = new Dictionary<int, Dictionary<int, Hexa>>();

        static void Main(string[] args)
        {
            Hexa reference = new Hexa { X = 0, Y = 0 };
            var lines = File.ReadAllLines("input24.txt");
            foreach (var line in lines)
            {
                var curr = reference;
                for (int i = 0; i < line.Length; i++)
                {
                    string n;
                    if (line[i] == 's' || line[i] == 'n')
                    {
                        n = line.Substring(i, 2);
                        i++;
                    }
                    else
                    {
                        n = line.Substring(i, 1);
                    }
                    curr = curr.GetNeighbour(n);
                }

                Dictionary<int, Hexa> xs;
                if (!map.TryGetValue(curr.Y, out xs))
                {
                    xs = new Dictionary<int, Hexa>();
                    map.Add(curr.Y, xs);
                }
                if (!xs.ContainsKey(curr.X))
                {
                    xs.Add(curr.X, new Hexa { X = curr.X, Y = curr.Y });
                }

                map[curr.Y][curr.X].FlipNum++;
            }

            var ret1 = map.Values.SelectMany(y => y.Values).Where(h => (h.FlipNum % 2) == 1).Count();

            for (int i = 0; i < 100; i++)
            {
                var newMap = new Dictionary<int, Dictionary<int, Hexa>>();
                var ys = map.Keys.ToList();
                var xs = map.Values.SelectMany(k=>k.Keys).ToList();
                int minx = xs.Min() - 1;
                int maxx = xs.Max() + 1;
                int miny = ys.Min() - 1;
                int maxy = ys.Max() + 1;

                for(int y = miny; y <= maxy; y++)
                {
                    newMap.Add(y, new Dictionary<int, Hexa>());
                    for (int x = minx; x <= maxx; x++)
                    {
                        int originalFlipNum = 0;
                        if(map.TryGetValue(y, out var ixs))
                        {
                            if(ixs.TryGetValue(x, out var ih))
                            {
                                originalFlipNum = ih.FlipNum;
                            }
                        }

                        var curr = new Hexa { X = x, Y = y, FlipNum = originalFlipNum };                        
                        newMap[y].Add(x, curr);
                        if((curr.FlipNum % 2) == 0)
                        {
                            var bn = curr.BlackNeighbours();
                            if (bn == 2)
                            {
                                curr.FlipNum++;
                            }
                        }
                        else
                        {
                            var bn = curr.BlackNeighbours();
                            if (bn == 0 || bn > 2)
                            {
                                curr.FlipNum++;
                            }

                        }
                    }
                }

                map = newMap;
            }

            var ret2 = map.Values.SelectMany(y => y.Values).Where(h => (h.FlipNum % 2) == 1).Count();

            Console.WriteLine("Hello World!");
        }
    }
}
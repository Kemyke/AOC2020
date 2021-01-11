//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace AOC202020
//{
//    class Program
//    {
//        public const int SIZE = 3;
//        public class Tile
//        {
//            public int Id { get; set; }
//            public List<Tuple<int, int, char>> Points = new List<Tuple<int, int, char>>();

//            public int Rotation { get; set; } = 0;
//            public bool Flipped { get; set; } = false;

//            public void Rotate()
//            {
//                Rotation = (Rotation + 90) % 360;

//                for (int i = 0; i < SIZE; i++)
//                {
//                    for (int j = i + 1; j < SIZE; j++)
//                    {
//                        var temp = Points.Single(p=>p.Item1 == i && p.Item2 == j);
//                        arr[i][j] = arr[j][i];
//                        arr[j][i] = temp;
//                    }
//                }

//                for (int i = 0; i < SIZE; i++)
//                {
//                    for (int j = 0; j < SIZE / 2; j++)
//                    {
//                        temp = arr[i][j];
//                        arr[i][j] = arr[i][col - 1 - j];
//                        arr[i][col - 1 - j] = temp;
//                    }

//                }
//            }

//            public string Top
//            {
//                get
//                {
//                    return string.Join("", Points.Where(p => p.Item2 == 0).Select(p=>p.Item3));
//                }
//            }

//            public string Bottom
//            {
//                get
//                {
//                    return string.Join("", Points.Where(p => p.Item2 == SIZE - 1).Select(p => p.Item3));
//                }
//            }

//            public string Left
//            {
//                get
//                {
//                    return string.Join("", Points.Where(p => p.Item1 == 0).Select(p => p.Item3));
//                }
//            }

//            public string Right
//            {
//                get
//                {
//                    return string.Join("", Points.Where(p => p.Item1 == SIZE - 1).Select(p => p.Item3));
//                }
//            }

//            public bool CanMatch(string border)
//            {
//                var revBorder = string.Join("", border.Reverse());
//                if(border == Top || revBorder == Top ||
//                    border == Bottom || revBorder == Bottom ||
//                    border == Left || revBorder == Left ||
//                    border == Right || revBorder == Right)
//                {
//                    return true;
//                }
//                return false;
//            }
            
//            public List<Tile> PossibleMatches(char border)
//            {
//                var b = GetCurrentBorder(border);
//                var ts = tiles.Except(new List<Tile> { this }).ToList();
//                var ret = ts.Where(t => t.CanMatch(b)).ToList();
//                return ret;
//            }

//            public int GetNonMatchingSides()
//            {
//                int ret = 0;
//                var ts = tiles.Except(new List<Tile> { this }).ToList();
//                ret += ts.Where(t => t.CanMatch(this.Top)).Count() == 0 ? 1 : 0;
//                ret += ts.Where(t => t.CanMatch(this.Bottom)).Count() == 0 ? 1 : 0;
//                ret += ts.Where(t => t.CanMatch(this.Left)).Count() == 0 ? 1 : 0;
//                ret += ts.Where(t => t.CanMatch(this.Right)).Count() == 0 ? 1 : 0;
//                return ret;
//            }
//        }

//        public static List<Tile> tiles = new List<Tile>();
//        public static Dictionary<int, Dictionary<int, Tile>> picture = new Dictionary<int, Dictionary<int, Tile>>();

//        public static Tile tile = new Tile
//        {
//            Id = 0,
//            Points = new List<Tuple<int, int, char>>
//            {
//                new Tuple<int, int, char>(0, 0, '1'),
//                new Tuple<int, int, char>(1, 0, '2'),
//                new Tuple<int, int, char>(2, 0, '3'),
//                new Tuple<int, int, char>(0, 1, '4'),
//                new Tuple<int, int, char>(1, 1, '5'),
//                new Tuple<int, int, char>(2, 1, '6'),
//                new Tuple<int, int, char>(0, 2, '7'),
//                new Tuple<int, int, char>(1, 2, '8'),
//                new Tuple<int, int, char>(2, 2, '9'),
//            }
//        };


//        static void Main(string[] args)
//        {
//            var lines = File.ReadAllLines("input20.txt");
//            Tile t = null;
//            int y = 0;
//            foreach(var line in lines)
//            {
//                if(line == "")
//                {
//                    continue;
//                }
//                if(line.StartsWith("Tile"))
//                {
//                    if(t!=null)
//                    {
//                        tiles.Add(t);
//                    }
//                    t = new Tile { Id = int.Parse(line.Replace("Tile ", "").Replace(":", "")) };
//                    y = 0;
//                    continue;
//                }

//                int x = 0;
//                foreach(var ch in line)
//                {
//                    t.Points.Add(new Tuple<int, int, char>(x, y, ch));
//                    x++;
//                }
//                y++;
//            }
//            tiles.Add(t);

//            //var ret1 = tiles.Where(tt => tt.GetNonMatchingSides() == 2).Select(tt => (long)tt.Id).Aggregate((a, b) => a * b);

//            var corners = tiles.Where(tt => tt.GetNonMatchingSides() == 2).ToList();
//            var size = (int)Math.Sqrt(tiles.Count);

//            for (int dy = 0; dy < size; dy++)
//            {
//                picture[dy] = new Dictionary<int, Tile>();
//                for (int dx = 0; dx < size; dx++)
//                {
//                    if(dx == 0 && dy == 0)
//                    {
//                        var init = tile;//corners.First();
//                        init.Flipped = true;
//                        picture[dy][dx] = init;
//                        while(true)//init.PossibleMatches('L').Count != 0 || init.PossibleMatches('T').Count != 0)
//                        {
//                            init.Rotation += 90;
//                        }
//                    }


//                }
//            }
//            Console.WriteLine("Hello World!");
//        }
//    }
//}

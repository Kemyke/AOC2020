using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC202020
{
    class Program
    {
        public const int SIZE = 10;
        public class Tile
        {
            public int Id { get; set; }
            public char[,] Points;

            public int Rotation { get; set; } = 0;
            public bool Flipped { get; set; } = false;

            private void ReverseColumns()
            {
                for (int i = 0; i < SIZE; i++)
                    for (int j = 0, k = SIZE - 1;
                         j < k; j++, k--)
                    {
                        var temp = Points[j, i];
                        Points[j, i] = Points[k, i];
                        Points[k, i] = temp;
                    }
            }

            private void Transpose()
            {
                for (int i = 0; i < SIZE; i++)
                    for (int j = i; j < SIZE; j++)
                    {
                        var temp = Points[j, i];
                        Points[j, i] = Points[i, j];
                        Points[i, j] = temp;
                    }
            }

            public void Rotate()
            {
                Rotation = (Rotation + 270) % 360;

                Transpose();
                ReverseColumns();
            }

            public void Flip()
            {
                Flipped = !Flipped;
                Transpose();
                Rotate();
                //Rotate();
                //Rotate();
            }

            public void Print()
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        Console.Write(Points[i, j] + " ");
                        if ((i + 1) % SIZE == 0)
                        {
                        }
                    }
                    Console.WriteLine();
                }
            }

            public string Top
            {
                get
                {
                    string ret = "";
                    for (int i = 0; i < SIZE; i++)
                    {
                        ret += Points[0, i];
                    }
                    return ret;
                }
            }

            public string Bottom
            {
                get
                {
                    string ret = "";
                    for (int i = 0; i < SIZE; i++)
                    {
                        ret += Points[SIZE - 1, i];
                    }
                    return ret;
                }
            }

            public string Left
            {
                get
                {
                    string ret = "";
                    for (int i = 0; i < SIZE; i++)
                    {
                        ret += Points[i, 0];
                    }
                    return ret;
                }
            }

            public string Right
            {
                get
                {
                    string ret = "";
                    for (int i = 0; i < SIZE; i++)
                    {
                        ret += Points[i, SIZE - 1];
                    }
                    return ret;
                }
            }

            public bool CanMatch(string border)
            {
                var revBorder = string.Join("", border.Reverse());
                if (border == Top || revBorder == Top ||
                    border == Bottom || revBorder == Bottom ||
                    border == Left || revBorder == Left ||
                    border == Right || revBorder == Right)
                {
                    return true;
                }
                return false;
            }

            public int GetNonMatchingSides()
            {
                int ret = 0;
                var ts = tiles.Except(new List<Tile> { this }).ToList();
                ret += ts.Where(t => t.CanMatch(this.Top)).Count() == 0 ? 1 : 0;
                ret += ts.Where(t => t.CanMatch(this.Bottom)).Count() == 0 ? 1 : 0;
                ret += ts.Where(t => t.CanMatch(this.Left)).Count() == 0 ? 1 : 0;
                ret += ts.Where(t => t.CanMatch(this.Right)).Count() == 0 ? 1 : 0;
                return ret;
            }

            public Tile TopNeighbour
            {
                get
                {
                    return tiles.Where(t => t != this && t.CanMatch(Top)).SingleOrDefault();
                }
            }

            public Tile BottomNeighbour
            {
                get
                {
                    return tiles.Where(t => t != this && t.CanMatch(Bottom)).SingleOrDefault();
                }
            }

            public Tile LeftNeighbour
            {
                get
                {
                    return tiles.Where(t => t != this && t.CanMatch(Left)).SingleOrDefault();
                }
            }

            public Tile RightNeighbour
            {
                get
                {
                    return tiles.Where(t => t != this && t.CanMatch(Right)).SingleOrDefault();
                }
            }
        }

        public static List<Tile> tiles = new List<Tile>();
        public static Dictionary<int, Dictionary<int, Tile>> picture = new Dictionary<int, Dictionary<int, Tile>>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input20.txt");
            Tile t = null;
            int y = 0;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                if (line.StartsWith("Tile"))
                {
                    if (t != null)
                    {
                        tiles.Add(t);
                    }
                    t = new Tile { Id = int.Parse(line.Replace("Tile ", "").Replace(":", "")), Points = new char[SIZE, SIZE] };
                    y = 0;
                    continue;
                }

                int x = 0;
                foreach (var ch in line)
                {
                    t.Points[y, x] = ch;
                    x++;
                }
                y++;
            }
            tiles.Add(t);

            Tile[,] orderedTileIds = new Tile[PICSIZE, PICSIZE];

            var topLeft = tiles.Where(ti => ti.GetNonMatchingSides() == 2).ToList().First();
            while (topLeft.TopNeighbour != null || topLeft.LeftNeighbour != null)
            {
                topLeft.Rotate();
            }
            orderedTileIds[0, 0] = topLeft;
            var c = topLeft;
            for (int i = 1; i < PICSIZE; i++)
            {
                var n = c.RightNeighbour;
                while (n.LeftNeighbour != c)
                {
                    n.Rotate();
                }
                if (n.Left != n.LeftNeighbour.Right)
                {
                    n.Flip();
                    if (n.Left != n.LeftNeighbour.Right)
                    {

                    }
                }
                orderedTileIds[i, 0] = n;
                c = n;
            }

            for (int j = 1; j < PICSIZE; j++)
            {
                for (int i = 0; i < PICSIZE; i++)
                {
                    var n = orderedTileIds[i, j - 1].BottomNeighbour;
                    while (n.TopNeighbour == null || n.TopNeighbour != orderedTileIds[i, j - 1])
                    {
                        n.Rotate();
                    }
                    if (n.Top != n.TopNeighbour.Bottom)
                    {
                        n.Flip();
                        n.Rotate();
                        n.Rotate();
                        if (n.Top != n.TopNeighbour.Bottom)
                        {

                        }
                    }
                    orderedTileIds[i, j] = n;
                }
            }

            char[,] pic = new char[IMGSIZE, IMGSIZE];
            for (int j = 0; j < PICSIZE; j++)
            {
                for (int i = 0; i < PICSIZE; i++)
                {
                    var ct = orderedTileIds[j, i];

                    for (int l = 1; l <= 8; l++)
                    {
                        for (int k = 1; k <= 8; k++)
                        {
                            pic[i * 8 + k - 1, j * 8 + l - 1] = ct.Points[k, l];
                        }
                    }
                }
            }

            Transpose(pic);

            //Transpose(pic);
            //ReverseColumns(pic);

            //Transpose(pic);
            //ReverseColumns(pic);

            //Transpose(pic);
            //ReverseColumns(pic);

            StringBuilder sb = new StringBuilder();
            List<string> picLines = new List<string>();
            for (int j = 0; j < IMGSIZE; j++)
            {
                for (int i = 0; i < IMGSIZE; i++)
                {
                    sb.Append(pic[i, j]);
                }
                picLines.Add(sb.ToString());
                sb = new StringBuilder();
            }

            Regex regex1 = new Regex(".{18}#");
            Regex regex2 = new Regex("(?=#.{4}##.{4}##.{4}###)");
            Regex regex3 = new Regex(".{1}#.{2}#.{2}#.{2}#.{2}#.{2}#");
            var mn = 0;
            for (int i = 1; i < picLines.Count - 1; i++)
            {
                if(i==90)
                {

                }
                foreach (Match m in regex2.Matches(picLines[i]))
                {
                    var mi = m.Index;
                    if (regex1.Match(picLines[i - 1], mi, 20).Success)
                    {
                        if (regex3.Match(picLines[i + 1], mi, 20).Success)
                        {

                            mn += 15;
                        }
                    }
                    
                }
            }

            for (int i = 0; i < picLines.Count; i++)
            {
                Console.WriteLine(picLines[i]);
            }

            var ret2 = picLines.SelectMany(s => s.ToCharArray()).Where(ch => ch == '#').Count() - mn;


            Console.ReadLine();
        }
        const int PICSIZE = 12;
        const int IMGSIZE = PICSIZE * 8;

        static void ReverseColumns(char[,] arr)
        {
            for (int i = 0; i < IMGSIZE; i++)
                for (int j = 0, k = IMGSIZE - 1;
                     j < k; j++, k--)
                {
                    var temp = arr[j, i];
                    arr[j, i] = arr[k, i];
                    arr[k, i] = temp;
                }
        }

        static void Transpose(char[,] arr)
        {
            for (int i = 0; i < IMGSIZE; i++)
                for (int j = i; j < IMGSIZE; j++)
                {
                    var temp = arr[j, i];
                    arr[j, i] = arr[i, j];
                    arr[i, j] = temp;
                }
        }

    }
}
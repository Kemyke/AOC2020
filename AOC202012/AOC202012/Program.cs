using System;
using System.IO;
using System.Linq;

namespace AOC2020Day12
{
    class Program
    {
        static (int x, int y, int h) ship = (0, 0, 90);
        static (int x, int y) waypoint = (10, -1);

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input12.txt");
            //foreach(var line in lines)
            //{
            //    var c = line.First();
            //    var v = int.Parse(line.Substring(1));

            //    switch(c)
            //    {
            //        case 'N':
            //            ship = (ship.x, ship.y - v, ship.h);
            //            break;
            //        case 'S':
            //            ship = (ship.x, ship.y + v, ship.h);
            //            break;
            //        case 'E':
            //            ship = (ship.x + v, ship.y, ship.h);
            //            break;
            //        case 'W':
            //            ship = (ship.x - v, ship.y, ship.h);
            //            break;
            //        case 'R':
            //            ship = (ship.x, ship.y, (ship.h + v) % 360);
            //            break;
            //        case 'L':
            //            ship = (ship.x, ship.y, (ship.h - v + 360) % 360);
            //            break;
            //        case 'F':
            //            switch(ship.h)
            //            {
            //                case 0:
            //                    ship = (ship.x, ship.y - v, ship.h);
            //                    break;
            //                case 90:
            //                    ship = (ship.x + v, ship.y, ship.h);
            //                    break;
            //                case 180:
            //                    ship = (ship.x, ship.y + v, ship.h);
            //                    break;
            //                case 270:
            //                    ship = (ship.x - v, ship.y, ship.h);
            //                    break;
            //                default:
            //                    throw new Exception();
            //            }
            //            break;
            //        default:
            //            throw new Exception();
            //    }
            //}

            foreach (var line in lines)
            {
                var c = line.First();
                var v = int.Parse(line.Substring(1));

                switch (c)
                {
                    case 'N':
                        waypoint = (waypoint.x, waypoint.y - v);
                        break;
                    case 'S':
                        waypoint = (waypoint.x, waypoint.y + v);
                        break;
                    case 'E':
                        waypoint = (waypoint.x + v, waypoint.y);
                        break;
                    case 'W':
                        waypoint = (waypoint.x - v, waypoint.y);
                        break;
                    case 'R':
                    case 'L':
                        if (c == 'L')
                        {
                            v *= -1;
                            v += 360;
                            v %= 360;
                        }
                        v /= 90;
                        for(int i=0;i<v;i++)
                        {
                            waypoint = (-1 * waypoint.y,  waypoint.x);
                        }
                        break;
                    case 'F':
                        ship = (ship.x + v * waypoint.x, ship.y + v * waypoint.y, ship.h);
                        break;
                    default:
                        throw new Exception();
                }
            }

            int ret = Math.Abs(ship.x) + Math.Abs(ship.y);
            Console.WriteLine("Hello World!");
        }
    }
}

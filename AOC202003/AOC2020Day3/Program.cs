using System;
using System.IO;

namespace AOC2020Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input3.txt");
            int x = 0;
            int y = 0;
            int trees0 = 0;
            while (y < lines.Length)
            {
                if (lines[y][x % lines[0].Length] == '#')
                    trees0++;
                x += 1;
                y += 1;
            }


            x = 0;
            y = 0;
            int trees1 = 0;
            while (y < lines.Length)
            {
                if (lines[y][x % lines[0].Length] == '#')
                    trees1++;
                x += 3;
                y += 1;
            }

            x = 0;
            y = 0;
            int trees2 = 0;
            while (y < lines.Length)
            {
                if (lines[y][x % lines[0].Length] == '#')
                    trees2++;
                x += 5;
                y += 1;
            }

            x = 0;
            y = 0;
            int trees3 = 0;
            while (y < lines.Length)
            {
                if (lines[y][x % lines[0].Length] == '#')
                    trees3++;
                x += 7;
                y += 1;
            }

            x = 0;
            y = 0;
            int trees4 = 0;
            while (y < lines.Length)
            {
                if (lines[y][x % lines[0].Length] == '#')
                    trees4++;
                x += 1;
                y += 2;
            }


            Console.WriteLine(trees0 * trees1 * trees2 * trees3 * trees4);
            Console.ReadKey();
        }
    }
}

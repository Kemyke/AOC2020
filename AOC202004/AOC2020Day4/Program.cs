using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2020Day4
{
    class Program
    {
        static Regex colorPattern = new Regex("#([0-9abcdef]){6}");
        static List<string> eyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        static bool IsByrValid(string byr)
        {
            return int.Parse(byr) >= 1920 && int.Parse(byr) <= 2002;
        }

        static bool IsIyrValid(string iyr)
        {
            return int.Parse(iyr) >= 2010 && int.Parse(iyr) <= 2020;
        }

        static bool IsEyrValid(string eyr)
        {
            return int.Parse(eyr) >= 2020 && int.Parse(eyr) <= 2030;
        }

        static bool IsHclValid(string hcl)
        {
            return colorPattern.IsMatch(hcl);
        }

        static bool IsEclValid(string ecl)
        {
            return eyeColors.Contains(ecl);
        }

        static bool IsPidValid(string pid)
        {
            return pid.Length == 9;
        }

        static bool IsHgtValid(string hgt)
        {
            if (hgt[hgt.Length - 1] == 'n')
            {
                var i = int.Parse(hgt.Substring(0, hgt.Length - 2));
                return i >= 59 && i <= 76;
            }
            else if (hgt[hgt.Length - 1] == 'm')
            {
                var c = int.Parse(hgt.Substring(0, hgt.Length - 2));
                return c >= 150 && c <= 193;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            int valid = 0;
            var lines = File.ReadAllLines("input4.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var passport = new List<string>();
                while(i < lines.Length && lines[i]!="")
                {
                    passport.Add(lines[i++]);
                }

                Dictionary<string, string> fields = new Dictionary<string, string>();

                foreach (var pl in passport)
                {
                    var kvs = pl.Split(' ');
                    foreach (var kv in kvs)
                    {
                        var x = kv.Split(':');
                        fields.Add(x[0], x[1]);
                    }
                }

                if(fields.ContainsKey("byr") && IsByrValid(fields["byr"]) &&
                    fields.ContainsKey("iyr") && IsIyrValid(fields["iyr"]) &&
                    fields.ContainsKey("eyr") && IsEyrValid(fields["eyr"]) &&
                    fields.ContainsKey("hgt") && IsHgtValid(fields["hgt"]) &&
                    fields.ContainsKey("hcl") && IsHclValid(fields["hcl"]) &&
                    fields.ContainsKey("ecl") && IsEclValid(fields["ecl"]) &&
                    fields.ContainsKey("pid") && IsPidValid(fields["pid"])
                    )
                {
                    valid++;
                }
            }



            Console.WriteLine("Hello World!");
        }
    }
}

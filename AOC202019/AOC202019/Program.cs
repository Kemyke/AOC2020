using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202019
{
    class Program
    {
        static Dictionary<int, string> rules = new Dictionary<int, string>();

        static List<string> valid42s = new List<string>();
        static List<string> valid31s = new List<string>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input19.txt");
            foreach (var rule in lines.TakeWhile(l => l != ""))
            {
                var idx = rule.IndexOf(':');
                int rn = int.Parse(rule.Substring(0, idx));
                rules.Add(rn, rule.Substring(idx + 2).Replace("\"", ""));
            }

            //rules[8] = "42 | 42 8";
            //rules[11] = "42 31 | 42 11 31";

            HashSet<string> substitutedRules = new HashSet<string>() { "42" };
            while(substitutedRules.Any())
            {
                var rule = substitutedRules.First();
                substitutedRules.Remove(rule);
                var fs = rule.Split(" ").First(s => s != "a" && s != "b");
                var sr = rules[int.Parse(fs)];
                var ssr = sr.Split(" | ");
                var idx = rule.IndexOf(fs);
                rule = rule.Remove(idx, fs.Length);
                foreach(var s in ssr)
                {
                    var nr = rule.Insert(idx, s);
                    if(nr.Any(c=>c != 'a' && c != 'b' && c != ' '))
                    {
                        if (!substitutedRules.Contains(nr))
                        {
                            substitutedRules.Add(nr);
                        }
                    }
                    else
                    {
                        var w = nr.Replace(" ", "");
                        if (!valid42s.Contains(w))
                        {
                            valid42s.Add(w);
                        }
                    }
                }
            }

            substitutedRules = new HashSet<string>() { "31" };
            while (substitutedRules.Any())
            {
                var rule = substitutedRules.First();
                substitutedRules.Remove(rule);
                var fs = rule.Split(" ").First(s => s != "a" && s != "b");
                var sr = rules[int.Parse(fs)];
                var ssr = sr.Split(" | ");
                var idx = rule.IndexOf(fs);
                rule = rule.Remove(idx, fs.Length);
                foreach (var s in ssr)
                {
                    var nr = rule.Insert(idx, s);
                    if (nr.Any(c => c != 'a' && c != 'b' && c != ' '))
                    {
                        if (!substitutedRules.Contains(nr))
                        {
                            substitutedRules.Add(nr);
                        }
                    }
                    else
                    {
                        var w = nr.Replace(" ", "");
                        if (!valid31s.Contains(w))
                        {
                            valid31s.Add(w);
                        }
                    }
                }
            }

            var pws = lines.SkipWhile(l => l != "");
            int ret2 = 0;
            foreach(var pw in pws)
            {
                if(IsValid(pw, 2, 1))
                {
                    Console.WriteLine(pw);
                    ret2++;
                }
            }
            
            Console.WriteLine("Hello World!");
        }

        public static bool IsValid(string word, int remaining42s, int remaining31s)
        {
            if(word == "")
            {
                return remaining31s <= 0 && -1 * remaining31s < -1 * remaining42s + 1;
            }
            if (remaining31s > 0)
            {
                foreach (var pw in valid42s)
                {
                    if (word.StartsWith(pw))
                    {
                        if (IsValid(word.Substring(pw.Length), remaining42s - 1, remaining31s))
                        {
                            return true;
                        }
                    }
                }
            }

            if (remaining42s <= 0)
            {
                foreach (var pw in valid31s)
                {
                    if (word.StartsWith(pw))
                    {
                        if(IsValid(word.Substring(pw.Length), remaining42s, remaining31s - 1))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}

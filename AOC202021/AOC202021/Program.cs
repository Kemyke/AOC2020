using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202021
{
    class Program
    {
        class Food
        {
            public List<string> Ingredients { get; set; } = new List<string>();
            public List<string> Allergenes { get; set; } = new List<string>();
        }

        static List<Food> foods = new List<Food>();
        static Dictionary<string, List<string>> possibleAllergenes = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input21.txt");
            Food food;
            foreach(var line in lines)
            {
                food = new Food();
                var ls = line.Split(" (");
                food.Ingredients.AddRange(ls[0].Split(" "));
                if (ls.Length > 1)
                {
                    food.Allergenes.AddRange(ls[1].Substring(0, ls[1].Length - 1).Replace("contains ", "").Split(", "));
                }
                foods.Add(food);
            }

            foreach(var a in foods.SelectMany(f=>f.Allergenes).Distinct())
            {
                possibleAllergenes.Add(a, new List<string>());
            }

            foreach(var a in possibleAllergenes)
            {
                var afoods = foods.Where(f => f.Allergenes.Contains(a.Key)).ToList();
                a.Value.AddRange(afoods.First().Ingredients);
                foreach(var af in afoods.Skip(1))
                {
                    var c = a.Value.Intersect(af.Ingredients).ToList();
                    a.Value.Clear();
                    a.Value.AddRange(c);
                }
            }

            int ret1 = 0;
            foreach(var pna in foods.SelectMany(f => f.Ingredients).Except(possibleAllergenes.SelectMany(a => a.Value)))
            {
                ret1 += foods.Where(f => f.Ingredients.Contains(pna)).Count();
            }

            while(possibleAllergenes.Any(a=>a.Value.Count > 1))
            {
                //List<string> fixs = possibleAllergenes.Where(a => a.Value.Count == 1).Select(a => a.Key).ToList();
                foreach (var pa in possibleAllergenes.Where(a=>a.Value.Count > 1))
                {
                    foreach(var f in possibleAllergenes.Where(a => a.Value.Count == 1).Select(a => a.Value.Single()).ToList())  //fixs)
                    {
                        pa.Value.Remove(f);
                    }
                }
            }

            var ret2 = string.Join(",", possibleAllergenes.Keys.OrderBy(i => i).Select(a => possibleAllergenes[a].Single()));

            Console.WriteLine("Hello World!");
        }
    }
}

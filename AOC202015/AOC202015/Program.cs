using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC202015
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> numbers = "9,3,1,0,8,4".Split(",").Select(n => long.Parse(n)).ToList();
            while(true)
            {
                var idx = numbers.Take(numbers.Count - 1).ToList().LastIndexOf(numbers.Last());
                if (idx == -1)
                {
                    numbers.Add(0);
                }
                else
                {
                    numbers.Add(numbers.Count - idx - 1);  
                }

                if(numbers.Count == 2020)
                {
                    break;
                }
            }
            var ret = numbers.Last();

            var initNums = "9,3,1,0,8,4".Split(",").ToList();
            Dictionary<long, int> numbersDict = initNums.Take(initNums.Count - 1).ToDictionary(n => long.Parse(n), n => initNums.IndexOf(n) + 1);
            long last = long.Parse(initNums.Last());
            int turn = initNums.Count;
            while (true)
            {
                int nlast;
                if(numbersDict.TryGetValue(last, out int v))
                {
                    nlast = turn - v;
                }
                else
                {
                    nlast = 0;
                }

                if(turn == 30000000)
                {

                }

                numbersDict[last] = turn;

                last = nlast;
                turn++;
            }



                Console.WriteLine("Hello World!");
        }
    }
}

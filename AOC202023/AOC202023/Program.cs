using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC202023
{
    class Program
    {
        class Item
        {
            public int Value { get; set; }
            public Item Next { get; set; }
        }

        static Dictionary<int, Item> ItemIndexes = new Dictionary<int, Item>();

        static void Main(string[] args)
        {
            string input = "624397158";
            var numbers = input.Select(c => int.Parse(c.ToString())).ToList().Union(Enumerable.Range(10, 999991)).ToList();
            for(int i = 0; i < numbers.Count; i++)
            {
                ItemIndexes.Add(numbers[i], new Item { Value = numbers[i] });
            }

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                ItemIndexes[numbers[i]].Next = ItemIndexes[numbers[i + 1]];
            }

            ItemIndexes[numbers.Last()].Next = ItemIndexes[numbers.First()];

            var curr = ItemIndexes[numbers.First()];
            for (int i = 0; i < 10000000; i++)
            {
                var destCandidate = curr.Value - 1;
                var tc = curr;
                var pick1 = curr.Next;
                var pick2 = pick1.Next;
                var pick3 = pick2.Next;

                var nextCurr = pick3.Next;
                curr.Next = nextCurr;

                for (int j = 0; j < 4; j++)
                {
                    if (destCandidate == 0)
                    {
                        destCandidate = ItemIndexes.Count;
                    }
                    if (destCandidate == pick1.Value || destCandidate == pick2.Value || destCandidate == pick3.Value)
                    {
                        destCandidate = (ItemIndexes.Count + destCandidate - 1) % ItemIndexes.Count;
                    }
                    else
                    {
                        break;
                    }
                }

                var dest = ItemIndexes[destCandidate];
                var td = dest.Next;
                dest.Next = pick1;
                pick3.Next = td;
                curr = nextCurr;
            }

            var ret1 = ItemIndexes[1].Next;
            for(int i = 0; i < 9; i++)
            {
                Console.Write(ret1.Value);
                ret1 = ret1.Next;
            }

            var ret2 = (long)ItemIndexes[1].Next.Value * ItemIndexes[1].Next.Next.Value;

            Console.ReadLine();
        }        
    }
}

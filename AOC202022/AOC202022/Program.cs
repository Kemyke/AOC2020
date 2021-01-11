using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC202022
{
    class Program
    {
        static List<int> winnerHand;

        static bool RecursivePlay(List<int> deck1, List<int> deck2)
        {
            HashSet<string> oldHands = new HashSet<string>();

            while (deck1.Any() && deck2.Any())
            {
                if(oldHands.Contains(string.Join(",", deck1)) || oldHands.Contains(string.Join(",", deck2)))
                {
                    return true;
                }
                oldHands.Add(string.Join(",", deck1));
                oldHands.Add(string.Join(",", deck2));

                var t1 = deck1.First();
                var t2 = deck2.First();

                bool t1Wins;

                if (t1 < deck1.Count && t2 < deck2.Count)
                {
                    t1Wins = RecursivePlay(deck1.Skip(1).Take(t1).ToList(), deck2.Skip(1).Take(t2).ToList());
                }
                else
                {
                    t1Wins = t1 > t2;
                }

                if (t1Wins)
                {
                    deck1 = deck1.Skip(1).Append(t1).Append(t2).ToList();
                    deck2 = deck2.Skip(1).ToList();
                }
                else
                {
                    deck2 = deck2.Skip(1).Append(t2).Append(t1).ToList();
                    deck1 = deck1.Skip(1).ToList();
                }
            }
            winnerHand = deck1.Any() ? deck1.ToList() : deck2.ToList();
            return deck1.Any();
        }


        static void Main(string[] args)
        {
            List<int> deck1 = new List<int>();
            List<int> deck2 = new List<int>();

            var lines = File.ReadAllLines("input22.txt");
            bool nextDeck = false;
            foreach(var line in lines.Skip(1))
            {
                if (line == "" || line.StartsWith("Player"))
                {
                    nextDeck = true;
                    continue;
                }
                if (!nextDeck)
                {
                    deck1.Add(int.Parse(line));
                }
                else
                {
                    deck2.Add(int.Parse(line));
                }
            }

            //while(deck1.Any() && deck2.Any())
            //{
            //    var t1 = deck1.First();
            //    var t2 = deck2.First();

            //    if(t1 > t2)
            //    {
            //        deck1 = deck1.Skip(1).Append(t1).Append(t2).ToList();
            //        deck2 = deck2.Skip(1).ToList();
            //    }
            //    else
            //    {
            //        deck2 = deck2.Skip(1).Append(t2).Append(t1).ToList();
            //        deck1 = deck1.Skip(1).ToList();
            //    }
            //}

            //int ret1;
            //if(deck1.Any())
            //{
            //    ret1 = deck1.Select(c => (deck1.Count - deck1.IndexOf(c)) * c).Sum();
            //}
            //else
            //{
            //    ret1 = deck2.Select(c => (deck2.Count - deck2.IndexOf(c)) * c).Sum();
            //}


            RecursivePlay(deck1, deck2);

            int ret2 = winnerHand.Select(c => (winnerHand.Count - winnerHand.IndexOf(c)) * c).Sum();

            Console.WriteLine("Hello World!");
        }
    }
}

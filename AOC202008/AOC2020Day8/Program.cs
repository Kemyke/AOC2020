using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input8.txt");
            int maxJmps = lines.Where(l => l.StartsWith("jmp")).Count();
            for (int aj = 0; aj < maxJmps; aj++)
            {
                int alterJmp = aj;

                var newLines = lines.ToList();
                int jc = 0;
                int i = 0;
                foreach (var l in newLines)
                {
                    if (l.StartsWith("jmp"))
                    {
                        if (jc == alterJmp)
                        {
                            newLines[i] = newLines[i].Replace("jmp", "nop");
                            break;
                        }
                        jc++;
                    }
                    i++;

                }

                HashSet<int> oldPtrs = new HashSet<int>();
                int acc = 0;
                int ptr = 0;
                while (true)
                {
                    if (ptr >= lines.Length)
                    {
                        
                    }
                    if (oldPtrs.Contains(ptr))
                    {
                        break;
                    }
                    oldPtrs.Add(ptr);
                    var line = newLines[ptr];
                    var cmds = line.Split(" ");
                    switch (cmds[0])
                    {
                        case "nop":
                            ptr++;
                            break;
                        case "acc":
                            acc += int.Parse(cmds[1]);
                            ptr++;
                            break;
                        case "jmp":
                            ptr += int.Parse(cmds[1]);
                            break;
                        default:
                            throw new Exception();

                    }

                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}

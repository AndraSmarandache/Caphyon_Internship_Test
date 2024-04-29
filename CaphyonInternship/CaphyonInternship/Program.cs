using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        using (StreamReader file = new StreamReader(@"resources\input.txt"))
        {
            List<LinkedList<char>> crateStack = new List<LinkedList<char>>();
            int lineCount = 1;
            if (file != null)
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    int current = 0;
                    while (current < line.Length && line[current] == ' ')
                    {
                        current++;
                    }

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line[current] == '[')
                        {
                            lineCount = 1;
                            while (lineCount < line.Length)
                            {
                                if (line[lineCount] >= 'A' && line[lineCount] <= 'Z')
                                {
                                    int n = (lineCount - 1) / 4;
                                    if (n >= crateStack.Count)
                                    {
                                        while (n >= crateStack.Count)
                                        {
                                            crateStack.Add(new LinkedList<char>());
                                        }
                                    }
                                    crateStack[n].AddLast(line[lineCount]);
                                }
                                lineCount += 4;
                            }
                        }
                        else
                        {
                            if (line[current] == 'm')
                            {
                                string[] parts = line.Split(' ');
                                int quantity, destination, source;
                                int.TryParse(parts[1], out quantity);
                                int.TryParse(parts[3], out source);
                                int.TryParse(parts[5], out destination);
                                for (int i = 0; i < quantity; i++)
                                {
                                    if (crateStack[source - 1].Count > 0)
                                    {
                                        char crate = crateStack[source - 1].First.Value;
                                        crateStack[source - 1].RemoveFirst();
                                        crateStack[destination - 1].AddFirst(crate);
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < crateStack.Count; i++)
                {
                    if (crateStack[i].Count > 0)
                        Console.Write(crateStack[i].First.Value);
                }
                file.Close();
            }
        }
    }
}

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
            //Considering the input, we have to read the crates from the top of the stack to the bottom and then operate as usual with the stack,
            // so I used a linked list to be able to work with both the back and the front of the crate structure 
            List<LinkedList<char>> crateStack = new List<LinkedList<char>>();
            int lineCount = 1;
            if (file != null)
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    //On every line we see what the first character is to determine what part of the input needs to be tackled
                    int current = 0;
                    while (current < line.Length && line[current] == ' ')
                    {
                        current++;
                    }

                    if (!string.IsNullOrEmpty(line))
                    {
                        //If the first character is '[', then we are reading the initial stacks of crates
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
                                //Considering the other charachters("[] ") and the letters before, we have to increment by 4 to reach another crate(or an empty space"
                                lineCount += 4;
                            }
                        }
                        else
                        {
                            //If the first character is '[', then we are moving the crates from one stack to another
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

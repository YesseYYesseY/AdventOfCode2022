namespace Day5
{
    internal class CargoCrane
    {
        List<List<char>> Containers;
        public int IndexLine;
        public CargoCrane(string[] Data)
        {
            Containers = new List<List<char>>();
            IndexLine = -1;
            for (int i = 0; i < Data.Length; i++)
            {
                var Line = Data[i];
                if (Line == string.Empty) break;
                if (Line.StartsWith(" 1 ")) { IndexLine = i; break; }
            }

            var ContainerIndexes = Data[IndexLine];
            int SearchedChar = 2;
            int AmountOfContainers = 0;

            while (SearchedChar < ContainerIndexes.Length && ContainerIndexes[SearchedChar] == ' ')
            {
                AmountOfContainers++;
                SearchedChar += 4;
            }

            for (int i = 0; i < AmountOfContainers; i++)
            {
                Containers.Add(new List<char>());
                for (int j = IndexLine - 1; j >= 0; j--)
                {
                    var ContainerChar = Data[j][(4 * i) + 1];
                    if (ContainerChar != ' ')
                    {
                        Containers[i].Add(ContainerChar);
                    }
                }
            }
        }

        public void ParseMovementString(string Instructions, bool CrateMover9001 = false)
        {
            if (Instructions == string.Empty) return;
            //Console.WriteLine(Instructions);
            // "move 11 from 1  to 7"
            //    0  1   2   3  4  5
            var SplitInstructions = Instructions.Split(' ');
            var Amount = int.Parse(SplitInstructions[1]);
            var From =   int.Parse(SplitInstructions[3]) - 1;
            var To =     int.Parse(SplitInstructions[5]) - 1;

            List<char> TempContainers = new List<char>();
            for (int i = 0; i < Amount; i++)
            {
                TempContainers.Add(Containers[From].Last());
                Containers[From].RemoveAt(Containers[From].Count() - 1);
            }

            if (CrateMover9001)
            {
                TempContainers.Reverse();
            }

            foreach (var Container in TempContainers)
            {
                Containers[To].Add(Container);
            }
        }
        // HFJGSJSDQ
        // 
        public override string ToString()
        {
            string ret = "";

            for (int i = 0; i < Containers.Count; i++)
            {
                var ContainerSpot = Containers[i];
                ret += $" {i + 1} => ";
                foreach (var Container in ContainerSpot)
                {
                    ret += $"[{Container}] ";
                }
                ret += '\n';
            }

            return ret;
        }

        public string AllLastChars()
        {
            string ret = "";
            foreach (var ContainerSlot in Containers)
            {
                ret += ContainerSlot.Last();
            }
            return ret;
        }

        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");
            var crane = new CargoCrane(Input);
            for (int i = crane.IndexLine + 1; i < Input.Length; i++)
            {
                crane.ParseMovementString(Input[i]);
            }
            Console.WriteLine($"Part 1: {crane.AllLastChars()}");

            crane = new CargoCrane(Input);

            for (int i = crane.IndexLine + 1; i < Input.Length; i++)
            {
                crane.ParseMovementString(Input[i], true);
            }
            Console.WriteLine($"Part 2: {crane.AllLastChars()}");
        }
    }
}
/*
 --- Day 5: Supply Stacks ---
The expedition can depart as soon as the final supplies have been unloaded from the ships. Supplies are stored in stacks of marked crates, but because the needed supplies are buried under many other crates, the crates need to be rearranged.

The ship has a giant cargo crane capable of moving crates between stacks. To ensure none of the crates get crushed or fall over, the crane operator will rearrange them in a series of carefully-planned steps. After the crates are rearranged, the desired crates will be at the top of each stack.

The Elves don't want to interrupt the crane operator during this delicate procedure, but they forgot to ask her which crate will end up where, and they want to be ready to unload them as soon as possible so they can embark.

They do, however, have a drawing of the starting stacks of crates and the rearrangement procedure (your puzzle input). For example:

    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
In this example, there are three stacks of crates. Stack 1 contains two crates: crate Z is on the bottom, and crate N is on top. Stack 2 contains three crates; from bottom to top, they are crates M, C, and D. Finally, stack 3 contains a single crate, P.

Then, the rearrangement procedure is given. In each step of the procedure, a quantity of crates is moved from one stack to a different stack. In the first step of the above rearrangement procedure, one crate is moved from stack 2 to stack 1, resulting in this configuration:

[D]        
[N] [C]    
[Z] [M] [P]
 1   2   3 
In the second step, three crates are moved from stack 1 to stack 3. Crates are moved one at a time, so the first crate to be moved (D) ends up below the second and third crates:

        [Z]
        [N]
    [C] [D]
    [M] [P]
 1   2   3
Then, both crates are moved from stack 2 to stack 1. Again, because crates are moved one at a time, crate C ends up below crate M:

        [Z]
        [N]
[M]     [D]
[C]     [P]
 1   2   3
Finally, one crate is moved from stack 1 to stack 2:

        [Z]
        [N]
        [D]
[C] [M] [P]
 1   2   3
The Elves just need to know which crate will end up on top of each stack; in this example, the top crates are C in stack 1, M in stack 2, and Z in stack 3, so you should combine these together and give the Elves the message CMZ.

After the rearrangement procedure completes, what crate ends up on top of each stack?
*/

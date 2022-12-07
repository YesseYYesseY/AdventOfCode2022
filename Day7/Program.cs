namespace Day7
{
    internal class Program
    {
        static string FixPath(string path)
        {
            if(path == "..")
            {
                return "../";
            }

            if (path == "/")
            {
                return "./Root/";
            }
            return path + "/";
        }

        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");

            string CurrentPath = ""; // this is super scuffed LOL, i wanted to simulate a file system but it was too hard. I may try again later

            for (int i = 0; i < Input.Length; i++)
            {
                var Line = Input[i];

                if (Line[0] == '$')
                {
                    if (Line[2] == 'c' && Line[3] == 'd')
                    {
                        CurrentPath += FixPath(Line.Substring(5));
                    }
                }
                else
                {
                    var Out = Line.Split(' ');
                    if (Out[0] == "dir")
                    {
                        string path = CurrentPath + Out[1] + "/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    }
                    else
                    {
                        var path = CurrentPath + Out[0];
                        if(!File.Exists(path))
                        {
                            File.Create(path).Close();
                        }
                    }
                }
            }

            Console.WriteLine(CurrentPath);
            Console.WriteLine();

            List<int> results = new List<int>();

            foreach (var path in Directory.GetDirectories("./Root/", "*", SearchOption.AllDirectories))
            {
                int result = 0;
                foreach (var file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    result += int.Parse(Path.GetFileName(file));
                }
                Console.WriteLine($"{path} = {result}");
                results.Add(result);
            }

            int final = 0;

            foreach (var result in results)
            {
                if(result <= 100000)
                {
                    final += result;
                }
            }

            Console.WriteLine($"Part 1: {final}");

            List<int> Part2Info = new List<int>();

            foreach (var path in Directory.GetDirectories("./Root/", "*", SearchOption.AllDirectories))
            {
                int result = 0;
                foreach (var file in Directory.GetFiles(path, "*"))
                {
                    result += int.Parse(Path.GetFileName(file));
                }
                Console.WriteLine($"{path} = {result}");
                Part2Info.Add(result);
            }

            var SpaceLeft = 70000000 - Part2Info.Sum();
            var SpaceNeeded = 30000000 - SpaceLeft;
            Console.WriteLine($"SpaceLeft {SpaceLeft}");
            Console.WriteLine($"SpaceNeeded {SpaceNeeded}");

            int Closest = int.MinValue;
            int ClosestDisplay = int.MinValue;

            foreach (var result in results)
            {
                var NewSpaceNeeded = SpaceNeeded - result;
                Console.WriteLine(NewSpaceNeeded);
                if(NewSpaceNeeded <= 0)
                {
                    if(NewSpaceNeeded > Closest)
                    {
                        Closest = NewSpaceNeeded;
                        ClosestDisplay = result;
                    }
                }
            }

            Console.WriteLine($"Part 2: {ClosestDisplay}");
        }
    }
}
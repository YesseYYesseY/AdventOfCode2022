using System.Diagnostics.CodeAnalysis;

namespace Day8
{
    internal class Program
    {
        struct Vector2
        {
            public int x, y;
        }

        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");
            List<List<int>> Trees = new List<List<int>>();
            foreach (var Line in Input)
            {
                List<int> NewLine = new List<int>();
                foreach (var Character in Line.ToCharArray())
                {
                    var Num = int.Parse(Character.ToString());

                    NewLine.Add(Num);
                }
                Trees.Add(NewLine);
            }

            foreach (var TreeLine in Trees)
            {
                foreach (var Tree in TreeLine)
                {
                    Console.Write($"{Tree} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            List<Vector2> Filter = new List<Vector2>();
            int Part1Result = 0;

            Part1Result += SearchNorth(Trees, Filter); // 10 Expected
            Part1Result +=  SearchWest(Trees, Filter); // 11 Expected 
            Part1Result += SearchSouth(Trees, Filter); //  8 Expected 
            Part1Result +=  SearchEast(Trees, Filter); // 11 Expected 

            // Combined = 21 Expected

            Console.WriteLine();
            Console.WriteLine($"Part 1: {Part1Result}");

            List<int> Part2Results = new List<int>();
            for (int x = 0; x < Trees[0].Count; x++)
            {
                for (int y = 0; y < Trees.Count; y++)
                {
                    Part2Results.Add(CalcScenicScore(Trees, x, y));
                }
            }

            Part2Results.Sort();
            Part2Results.Reverse();

            Console.WriteLine($"Part 2: {Part2Results[0]}");

        }

        static int CalcScenicScore(List<List<int>> Trees, int x, int y)
        {
            var Tree = Trees[y][x];

            int Right = 0;
            for (int x2 = x + 1; x2 < Trees[0].Count; x2++)
            {
                var TempTree = Trees[y][x2];

                Right += 1;
                if(TempTree >= Tree)
                {
                    break;
                }
            }

            int Left = 0;
            for (int x2 = x - 1; x2 >= 0; x2--)
            {
                var TempTree = Trees[y][x2];

                Left += 1;
                if (TempTree >= Tree)
                {
                    break;
                }
            }

            int Up = 0;
            for (int y2 = y - 1; y2 >= 0; y2--)
            {
                var TempTree = Trees[y2][x];

                Up += 1;
                if (TempTree >= Tree)
                {
                    break;
                }
            }

            int Down = 0;
            for (int y2 = y + 1; y2 < Trees.Count; y2++)
            {
                var TempTree = Trees[y2][x];

                Down += 1;
                if (TempTree >= Tree)
                {
                    break;
                }
            }

            return Up * Down * Right * Left;
        }

        static int SearchNorth(List<List<int>> Trees, List<Vector2> Filter)
        {
            int result = 0;

            for (int x = 0; x < Trees[0].Count; x++)
            {
                int HighestTree = -1;

                for (int y = 0; y < Trees.Count; y++)
                {
                    var Tree = Trees[y][x];

                    if(HighestTree < Tree)
                    {
                        bool PassedFiler = true;
                        foreach (var Filt in Filter)
                        {
                            if (Filt.x == x && Filt.y == y)
                            {
                                PassedFiler = false;
                                break;
                            }
                        }
                        if (PassedFiler)
                        {
                            result += 1;
                            Filter.Add(new Vector2()
                            {
                                x = x,
                                y = y
                            });
                        }
                        HighestTree = Tree;
                    }
                }
            }
            
            return result;
        }

        static int SearchWest(List<List<int>> Trees, List<Vector2> Filter)
        {
            int result = 0;

            for (int y = 0; y < Trees.Count; y++)
            {
                int HighestTree = -1;

                for (int x = 0; x < Trees[y].Count; x++)
                {
                    var Tree = Trees[y][x];

                    if (HighestTree < Tree)
                    {
                        bool PassedFiler = true;
                        foreach (var Filt in Filter)
                        {
                            if (Filt.x == x && Filt.y == y)
                            {
                                PassedFiler = false;
                                break;
                            }
                        }
                        if (PassedFiler)
                        {
                            result += 1;
                            Filter.Add(new Vector2()
                            {
                                x = x,
                                y = y
                            });
                        }
                        HighestTree = Tree;
                    }
                }
            }

            return result;
        }

        static int SearchSouth(List<List<int>> Trees, List<Vector2> Filter)
        {
            int result = 0;

            for (int x = 0; x < Trees[0].Count; x++)
            {
                int HighestTree = -1;

                for (int y = Trees.Count - 1; y >= 0; y--)
                {
                    var Tree = Trees[y][x];

                    if(HighestTree < Tree)
                    {
                        bool PassedFiler = true;
                        foreach (var Filt in Filter)
                        {
                            if (Filt.x == x && Filt.y == y)
                            {
                                PassedFiler = false;
                                break;
                            }
                        }
                        if (PassedFiler)
                        {
                            result += 1;
                            Filter.Add(new Vector2()
                            {
                                x = x,
                                y = y
                            });
                        }
                        HighestTree = Tree;
                    }
                }
            }

            return result;
        }

        static int SearchEast(List<List<int>> Trees, List<Vector2> Filter)
        {
            int result = 0;

            for (int y = 0; y < Trees.Count; y++)
            {
                int HighestTree = -1;

                for (int x = Trees[y].Count - 1; x >= 0; x--)
                {
                    var Tree = Trees[y][x];

                    if (HighestTree < Tree)
                    {
                        bool PassedFiler = true;
                        foreach (var Filt in Filter)
                        {
                            if (Filt.x == x && Filt.y == y)
                            {
                                PassedFiler = false;
                                break;
                            }
                        }
                        if (PassedFiler)
                        {
                            result += 1;
                            Filter.Add(new Vector2()
                            {
                                x = x,
                                y = y
                            });
                        }
                        HighestTree = Tree;
                    }
                }
            }

            return result;
        }
    }
}
/*
            3 0 3 7 3 
            2 5 5 1 2 
            6 5 3 3 2 
            3 3 5 4 9 
            3 5 3 9 0 
 */
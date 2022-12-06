namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");
            List<int> CaloriesList = new List<int>();
            int CurrentCalories = 0;

            foreach (var Line in Input)
            {
                if (Line == string.Empty)
                {
                    CaloriesList.Add(CurrentCalories);
                    CurrentCalories = 0;
                }

                if (int.TryParse(Line, out var NewCalories))
                {
                    CurrentCalories += NewCalories;
                }
            }

            CaloriesList.Sort();
            CaloriesList.Reverse();

            Console.WriteLine($"Part 1: {CaloriesList[0]}");
            Console.WriteLine($"Part 2: {CaloriesList[0] + CaloriesList[1] + CaloriesList[2]}");
        }
    }
}
namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");
            int MaxCalories = 0;
            int CurrentCalories = 0;

            foreach (var Line in Input)
            {
                if(Line == string.Empty)
                {
                    if(CurrentCalories > MaxCalories)
                    {
                        MaxCalories = CurrentCalories;
                    }
                    CurrentCalories = 0;
                }

                if(int.TryParse(Line, out var NewCalories))
                {
                    CurrentCalories += NewCalories;
                }
            }
            Console.WriteLine(MaxCalories);
        }
    }
}
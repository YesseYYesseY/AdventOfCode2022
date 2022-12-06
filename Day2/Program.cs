using Microsoft.VisualBasic;

namespace Day2
{
    internal class Program
    {
        const char OpponentRock = 'A';
        const char OpponentPaper = 'B';
        const char OpponentScissors = 'C';

        const char PlayerRock = 'X';
        const char PlayerPaper = 'Y';
        const char PlayerScissors = 'Z';

        const char RequireLost = 'X';
        const char RequireDraw = 'Y';
        const char RequireWin = 'Z';

        const int ScoreRock = 1;
        const int ScorePaper = 2;
        const int ScoreScissors = 3;

        const int ScoreLost = 0;
        const int ScoreDraw = 3;
        const int ScoreWin = 6;

        static void Main(string[] args)
        {
            var Input = File.ReadAllLines("./Input.txt");
            int TotalScore = 0;

            foreach (var Round in Input)
            {
                TotalScore += CalculateRoundScore(Round, false);
            }

            Console.WriteLine($"Part 1: {TotalScore}");

            TotalScore = 0;

            foreach (var Round in Input)
            {
                TotalScore += CalculateRoundScore(Round, true);
            }

            Console.WriteLine($"Part 2: {TotalScore}");
        }

        static string MoveToString(char Input)
        {
            switch (Input)
            {
                case OpponentRock:
                case PlayerRock:
                    return "Rock";
                case OpponentPaper: 
                case PlayerPaper: 
                    return "Paper";
                case OpponentScissors: 
                case PlayerScissors: 
                    return "Scissors";
            }
            return string.Empty;
        }

        static int CalculateRoundOutcome(string Input)
        {
            char Opponent = Input[0];
            char Player = Input[2];

            if(Player == PlayerRock)
            {
                if (Opponent == OpponentRock) return ScoreDraw;
                if (Opponent == OpponentPaper) return ScoreLost;
                if (Opponent == OpponentScissors) return ScoreWin;
            }

            if (Player == PlayerPaper)
            {
                if (Opponent == OpponentRock) return ScoreWin;
                if (Opponent == OpponentPaper) return ScoreDraw;
                if (Opponent == OpponentScissors) return ScoreLost;
            }

            if (Player == PlayerScissors)
            {
                if (Opponent == OpponentRock) return ScoreLost;
                if (Opponent == OpponentPaper) return ScoreWin;
                if (Opponent == OpponentScissors) return ScoreDraw;
            }

            return 0;
        }

        static int CalculateRoundScore(string Input, bool Part2)
        {
            int MoveScore = 0;
            switch (GetPlayerMove(Input, Part2))
            {
                case PlayerRock: MoveScore = ScoreRock; break;
                case PlayerPaper: MoveScore = ScorePaper; break;
                case PlayerScissors: MoveScore = ScoreScissors; break;
            }
            if (!Part2)
            {
                return MoveScore + CalculateRoundOutcome(Input);
            }
            else
            {
                int OutcomeScore = 0;
                switch (Input[2])
                {
                    case RequireWin: OutcomeScore = ScoreWin; break;
                    case RequireDraw: OutcomeScore = ScoreDraw; break;
                    case RequireLost: OutcomeScore = ScoreLost; break;
                }
                return MoveScore + OutcomeScore;
            }
        }
       
        static char GetPlayerMove(string Input, bool Part2)
        {
            if(!Part2)
            {
                return Input[2];
            }
            else
            {
                char Opponent = Input[0];
                char Outcome = Input[2];

                if (Opponent == OpponentRock)
                {
                    if (Outcome == RequireWin) return PlayerPaper;
                    if (Outcome == RequireDraw) return PlayerRock;
                    if (Outcome == RequireLost) return PlayerScissors;
                }

                if (Opponent == OpponentPaper)
                {
                    if (Outcome == RequireWin) return PlayerScissors;
                    if (Outcome == RequireDraw) return PlayerPaper;
                    if (Outcome == RequireLost) return PlayerRock;
                }

                if (Opponent == OpponentScissors)
                {
                    if (Outcome == RequireWin) return PlayerRock;
                    if (Outcome == RequireDraw) return PlayerScissors;
                    if (Outcome == RequireLost) return PlayerPaper;
                }
            }
            return '?';
        }
    }
}

/*
--- Day 2: Rock Paper Scissors ---
The Elves begin to set up camp on the beach. To decide whose tent gets to be closest to the snack storage, a giant Rock Paper Scissors tournament is already in progress.

Rock Paper Scissors is a game between two players. Each game contains many rounds; in each round, the players each simultaneously choose one of Rock, Paper, or Scissors using a hand shape. Then, a winner for that round is selected: Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock. If both players choose the same shape, the round instead ends in a draw.

Appreciative of your help yesterday, one Elf gives you an encrypted strategy guide (your puzzle input) that they say will be sure to help you win. "The first column is what your opponent is going to play: A for Rock, B for Paper, and C for Scissors. The second column--" Suddenly, the Elf is called away to help with someone's tent.

The second column, you reason, must be what you should play in response: X for Rock, Y for Paper, and Z for Scissors. Winning every time would be suspicious, so the responses must have been carefully chosen.

The winner of the whole tournament is the player with the highest score. Your total score is the sum of your scores for each round. The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors) plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).

Since you can't be sure if the Elf is trying to help you or trick you, you should calculate the score you would get if you were to follow the strategy guide.

For example, suppose you were given the following strategy guide:

A Y
B X
C Z
This strategy guide predicts and recommends the following:

In the first round, your opponent will choose Rock (A), and you should choose Paper (Y). This ends in a win for you with a score of 8 (2 because you chose Paper + 6 because you won).
In the second round, your opponent will choose Paper (B), and you should choose Rock (X). This ends in a loss for you with a score of 1 (1 + 0).
The third round is a draw with both players choosing Scissors, giving you a score of 3 + 3 = 6.
In this example, if you were to follow the strategy guide, you would get a total score of 15 (8 + 1 + 6).

What would your total score be if everything goes exactly according to your strategy guide?
 */
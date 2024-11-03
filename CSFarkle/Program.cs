using BetterConsoleTables;

namespace CSFarkle
{
    internal class Program
    {
        private static readonly Dice dice = new Dice();
        private static readonly List<Player> playerList = [];

        private static Table? scoreTable;
        private static readonly TableConfiguration tableConfiguration = TableConfiguration.Unicode();
        private static readonly ColumnHeader[] columnHeaders = [new ColumnHeader("Player #"), new ColumnHeader("Points"), new ColumnHeader("# of Turns")];

        static void Main(string[] args)
        {
            Console.WriteLine("How many players are playing?");
            int numberOfPlayers;
            while (!int.TryParse(Console.ReadLine(), out numberOfPlayers))
            {
                Console.WriteLine("How many players are playing?");
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerList.Add(new Player(i + 1));
            }

            Console.Clear();

            while (true)
            {
                foreach (Player player in playerList)
                {
                    if (player.Points >= 10_000)
                    {
                        Console.WriteLine($"Player {player.PlayerNum} wins! With {player.Points:N0} points!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        Exit();
                    }

                    int numOfDice = 6;
                    bool firstRoll = true;

                    while (true)
                    {
                        scoreTable = new Table(tableConfiguration, columnHeaders);

                        for (int i = 0; i < playerList.Count; i++)
                        {
                            if (playerList[i].PlayerNum != player.PlayerNum)
                            {
                                scoreTable.AddRow($"{playerList[i].PlayerNum}", playerList[i].Points.ToString("N0"), playerList[i].NumOfTurns);
                            }
                        }

                        Console.Write(scoreTable);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nExit Game (e)");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine($"\nPlayer {player.PlayerNum}:");
                        Console.WriteLine($"Turn: {player.NumOfTurns + 1}");
                        Console.WriteLine($"Points: {player.Points:N0}");
                        Console.WriteLine($"Running Total: {player.RunningTotal:N0}");

                        Console.WriteLine($"\nDice can be rolled: {numOfDice}");
                        Console.WriteLine("Roll Dice? Y/N");
                        string userInput = Console.ReadKey().KeyChar.ToString().ToLower();

                        if (userInput == "y")
                        {
                            dice.RollDice(numOfDice);
                            Console.WriteLine("\nRolled Dice:");
                            Console.WriteLine(dice.GetDiceValues());
                            firstRoll = false;

                            Dictionary<string, (int score, List<int> diceToSave)> options = PointChecker.CheckDice(dice.GetDiceValues());
                            if (options.Count != 0 && !options.Keys.ElementAt(0).Contains("No Matches"))
                            {
                                foreach (string option in options.Keys)
                                {
                                    Console.Write(option);
                                }

                                Console.Write("Enter option number: ");
                                int optionNumber;
                                while (!int.TryParse(Console.ReadLine(), out optionNumber) || optionNumber < 1 || optionNumber > options.Count)
                                {
                                    Console.Write("Invalid input. Please enter a valid option number: ");
                                }

                                (int score, List<int> diceToSave) = options.ElementAt(optionNumber - 1).Value;
                                player.RunningTotal += score;
                                numOfDice -= diceToSave.Count;

                                if (numOfDice <= 0)
                                {
                                    Console.WriteLine("You can roll all 6 dice again!");
                                    Console.ReadKey();
                                    numOfDice = 6;

                                }
                                dice.ClearDiceValues();
                                Console.Clear();
                            }
                            else if (options.Keys.ElementAt(0).Contains("No Matches"))
                            {
                                Reset(player);
                                Console.WriteLine("Farkle!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        else if (!firstRoll && userInput == "n")
                        {
                            if (player.RunningTotal < 500 && player.Points < 500)
                            {
                                Console.WriteLine("\nYou must have 500 points to be on the score board!");
                                Console.ReadKey();
                            }
                            else
                            {
                                player.Points += player.RunningTotal;
                            }
                            Console.Clear();
                            Reset(player);
                            break;
                        }
                        else if (userInput == "e")
                        {
                            Exit();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter Y, N, or E.");
                        }
                    }
                }
            }
        }


        private static void Reset(Player player)
        {
            player.RunningTotal = 0;
            dice.ClearDiceValues();
            player.NumOfTurns++;
        }

        private static void Exit(int statusCode = 0)
        {
            Environment.Exit(statusCode);
        }
    }
}

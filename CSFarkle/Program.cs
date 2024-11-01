using BetterConsoleTables;

namespace CSFarkle
{
#pragma warning disable CS8604
#pragma warning disable CS8601
    internal class Program
    {
        private static string userInput = string.Empty;

        private static readonly Dice dice = new Dice();
        private static readonly List<Player> playerList = [];

        private static Table? scoreTable;
        private static readonly TableConfiguration tableConfiguration = TableConfiguration.Unicode();
        private static readonly ColumnHeader[] columnHeaders = [new ColumnHeader("Player #"), new ColumnHeader("Points"), new ColumnHeader("# of Turns")];

        static void Main(string[] args)
        {
        numberOfPlayers:
            Console.WriteLine("How many players are playing?");
            try
            {
                int numberOfPlayers = int.Parse(Console.ReadLine());
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    playerList.Add(new Player(i + 1));
                }
            }
            catch
            {
                goto numberOfPlayers;
            }

            Console.Clear();

            while (true)
            {
                foreach (Player player in playerList)
                {
                    int numOfDice = 6;
                    bool firstRoll = true;

                    if (player.Points >= 10_000)
                    {
                        Console.WriteLine($"Player {player.PlayerNum} wins! With {player.Points:N0} points!");
                        goto exit;
                    }

                begin:

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

                rollDice:
                    try
                    {
                        Console.WriteLine($"\nDice can be rolled: {numOfDice}");
                        Console.WriteLine("Roll Dice? Y/N");
                        userInput = Console.ReadKey().KeyChar.ToString().ToLower();

                        if (userInput == "y")
                        {
                            dice.RollDice(numOfDice);
                            Console.WriteLine("\nRolled Dice:");
                            Console.WriteLine(dice.GetDiceValues());
                            firstRoll = false;
                            goto checkDice;
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
                            continue;
                        }
                        else if (userInput == "e")
                        {
                            Exit();
                        }
                        else
                        {
                            goto rollDice;
                        }
                    }
                    catch
                    {
                        goto rollDice;
                    }

                checkDice:
                    try
                    {
                        Dictionary<string, (int score, List<int> diceToSave)> options = PointChecker.CheckDice(dice.GetDiceValues());
                        foreach (string option in options.Keys)
                        {
                            if (option.Contains("No Matches"))
                            {
                                Reset(player);
                                Console.WriteLine("Farkle!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write(option);
                        }

                        if (options.ElementAt(0).Value.score == 0)
                        {
                            continue;
                        }

                        userInput = Console.ReadLine();

                        (int score, List<int> diceToSave) = options.ElementAt(int.Parse(userInput) - 1).Value;

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

                        goto begin;

                    }
                    catch
                    {
                        goto checkDice;
                    }
                }
            }

        exit:
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Exit();
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

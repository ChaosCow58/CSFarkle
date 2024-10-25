namespace CSFarkle
{
#pragma warning disable CS8604
#pragma warning disable CS8601
    internal class Program
    {
        private static Dice dice = new Dice();
        private static string userInput = string.Empty;
        private static int numberOfPlayers = 1;
        private static readonly List<Player> playerList = [];

        static void Main(string[] args)
        {
            numberOfPlayers:
            Console.WriteLine("How many players are playing?");
            try
            {
                numberOfPlayers = int.Parse(Console.ReadLine());
                for (int i = 0;i < numberOfPlayers; i++)
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

                    for (int i = 0; i < playerList.Count; i++)
                    {
                        if (playerList[i].PlayerNum != player.PlayerNum)
                        {
                            Console.WriteLine($"Player {playerList[i].PlayerNum}: {playerList[i].Points:N0}");
                        }
                    }

                    Console.WriteLine($"\nPlayer {player.PlayerNum}:");
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
                        else if (!firstRoll)
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
                            player.RunningTotal = 0;
                            Console.Clear();
                            continue;
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
                        Dictionary<string, (int points, List<int> diceToSave)> options = PointChecker.CheckDice(dice.GetDiceValues());
                        foreach (string option in options.Keys)
                        {
                            Console.Write(option);
                        }
                        userInput = Console.ReadLine();
                        (int score, List<int> diceToSave) = options.ElementAt(int.Parse(userInput) - 1).Value;
                        if (score == 0)
                        {
                            player.RunningTotal = 0;
                            dice.ClearDiceValues();
                            Console.WriteLine("Farkle!");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        player.RunningTotal += score;

                        numOfDice -= diceToSave.Count;
                        if (numOfDice <= 0)
                        {
                            Console.WriteLine("You can roll all 6 dice again!");
                            numOfDice = 6;
                        }
                        dice.ClearDiceValues();

                        Console.WriteLine($"Running Total: {player.RunningTotal:N0}");
                        Console.ReadKey();
                        Console.Clear();

                        for (int i = 0; i < playerList.Count; i++)
                        {
                            if (playerList[i].PlayerNum != player.PlayerNum)
                            {
                                Console.WriteLine($"Player {playerList[i].PlayerNum}: {playerList[i].Points:N0}");
                            }
                        }
                        Console.WriteLine($"\nPlayer {player.PlayerNum}:");
                        Console.WriteLine($"Points: {player.Points:N0}");
                        Console.WriteLine($"Running Total: {player.RunningTotal:N0}");
                        goto rollDice;

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
           
        }
    }
}

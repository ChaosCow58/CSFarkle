namespace CSFarkle
{
    internal class Program
    {
        private static Dice dice = new Dice();
        private static PointChecker pointChecker = new PointChecker();
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
                    Console.WriteLine($"Player {player.PlayerNum}:");
                    Console.WriteLine($"Points: {player.Points}");
                    Console.WriteLine($"Running Total: {player.RunningTotal}\n");

                    rollDice:
                    try
                    {
                        Console.WriteLine("Roll Dice? Y/N");
                        userInput = Console.ReadKey().KeyChar.ToString().ToLower();
                        
                        if (userInput == "y")
                        {
                            dice.RollDice();
                            Console.WriteLine("\nRolled Dice:");
                            Console.WriteLine(dice.GetDiceValues());
                            goto checkDice;
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
                        Dictionary<string, int> options = pointChecker.CheckDice(dice.GetDiceValues());
                        foreach (string option in options.Keys)
                        {
                            Console.Write(option);
                        }
                        userInput = Console.ReadLine();
                        string selectedOptionKey = options.Keys.FirstOrDefault(option => option.StartsWith(userInput + ")"));
                        player.RunningTotal += options[selectedOptionKey];
                        Console.WriteLine($"Running Total: {player.RunningTotal}");
                        Console.ReadKey();

                    }
                    catch 
                    { 
                        goto checkDice; 
                    }
                }
                break;
            }


           //dice.RollDice();
           //Console.WriteLine(dice.GetDiceValues());
        }
    }
}

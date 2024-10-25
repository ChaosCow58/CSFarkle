using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFarkle
{
    public class PointChecker
    {
        public PointChecker() { }

        // Option, Points, Dice to save
        public static Dictionary<string, (int points, List<int> diceToSave)> CheckDice(string diceValues)
        {
            Dictionary<string, (int points, List<int> diceToSave)> result = [];
            List<int> numbers = diceValues
            .Replace("[", "")   
            .Replace("]", "")    
            .Split(',')
            .Select(s => int.Parse(s.Trim()))
            .ToList();

            // numbers = [1, 1, 1, 4, 4, 4];

            // Number then count of numbers
            Dictionary<int, int> numberCounts = numbers.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            int option = 1;
            int numOfTriplets = 0;
            bool isStraight = true;

            for (int i = 1; i <= 6; i++)
            {
                if (!numberCounts.ContainsKey(i))
                {
                    isStraight = false;
                    break;
                }
            }

            if (isStraight)
            {
                return new Dictionary<string, (int points, List<int> diceToSave)>{ { "1) Save the 1-6 straight\n", (1500, numbers) } };
            }

            int pairCount = numberCounts.Count(n => n.Value == 2);
            if (pairCount == 3)
            {
                result.Add($"{option}) Save the three pairs\n", (1500, numbers));
                option++;
            }

            foreach (KeyValuePair<int, int> kvp in numberCounts)
            {
                int number = kvp.Key;
                int count = kvp.Value;
                List<int> diceToSave = numbers.Where(x => x == number).ToList();
            
                if (number == 1 && count == 2)
                {
                    result.Add($"{option}) Save both of the ones the 1s\n", (200, diceToSave));
                    option++;
                }
                if (number == 1 && (count >= 1 && count <= 2)) 
                {
                    result.Add($"{option}) Save a 1\n", (100, diceToSave.Take(1).ToList()));
                    option++;  
                }
                if (number == 5 && count == 2)
                {
                    result.Add($"{option}) Save both of the fives the 5s\n", (100, diceToSave));
                    option++;
                }
                if (number == 5 && (count >= 1 && count <= 2)) 
                {  
                    result.Add($"{option}) Save a 5\n", (50, diceToSave.Take(1).ToList()));
                    option++;   
                }
                if (count == 3)
                {
                    int points = number * 100; // Triple 1 gives 300, triple 2 gives 200, etc.
                    if (number == 1)
                        points = 300; // Special case for triple 1s

                    result.Add($"{option}) Save the triple {number}s\n", (points, diceToSave));
                    option++;
                    numOfTriplets++;
                }
                if (count == 6)
                {
                    return new Dictionary<string, (int points, List<int> diceToSave)>{ { "1) Save the 6 of a kind\n", (3000, diceToSave)} };
                }
                else if (count == 5)
                {
                    result.Add($"{option}) Save the 5 of a kind\n", (2000, diceToSave));
                    option++;
                }
                else if (count == 4)
                {
                    // Check for a pair with remaining numbers
                    bool pairExists = numberCounts.Any(n => n.Value == 2);

                    if (pairExists)
                    {
                        result.Add($"{option}) Save the 4 of a kind and the pair\n", (1500, diceToSave));
                    }
                    else
                    {
                        result.Add($"{option}) Save the 4 of a kind\n", (1000, diceToSave));
                    }
                    option++;
                }
                if (numOfTriplets == 2)
                {
                    return new Dictionary<string, (int points, List<int> diceToSave)>{ {"1) Save the two triplets\n", (2500, numbers) } };
                }
            }

            if (result.Count == 0)
            {
                return new Dictionary<string, (int points, List<int> diceToSave)> { { "1) No Matches\n", (0, new List<int>()) } };
            }

            return result;
        }

    }
}

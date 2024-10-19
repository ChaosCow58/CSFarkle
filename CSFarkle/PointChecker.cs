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

        public Dictionary<string, int> CheckDice(string diceValues)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            List<int> numbers = diceValues
            .Replace("[", "")   
            .Replace("]", "")    
            .Split(',')
            .Select(s => int.Parse(s.Trim()))
            .ToList();

            // Number then count of numbers
            Dictionary<int, int> numberCounts = numbers.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            int option = 1;
            int numOfTriplets = 0;
            bool isStraight = true;

            for (int i = 1;i <= 6;i++)
            {
                if (!numberCounts.ContainsKey(i))
                {
                    isStraight = false;
                    break;
                }
            }

            if (isStraight)
            {
                return new Dictionary<string, int>{ { "1) Save the 1-6 straight\n", 1500 } };
            }

            int pairCount = numberCounts.Count(n => n.Value == 2);
            if (pairCount == 3)
            {
                result.Add($"{option}) Save the three pairs\n", 1500);
                option++;
            }

            foreach (KeyValuePair<int, int> kvp in numberCounts)
            {
                int number = kvp.Key;
                int count = kvp.Value;
            
                if (number == 1 && count == 2)
                {
                    result.Add($"{option}) Save both of the ones the 1s\n", 200);
                    option++;
                }
                if (number == 1 && (count >= 1 && count <= 2)) 
                {
                    result.Add($"{option}) Save an 1\n", 100);
                    option++;  
                }
                if (number == 5 && count == 2)
                {
                    result.Add($"{option}) Save both of the fives the 5s\n", 100);
                    option++;
                }
                if (number == 5 && (count >= 1 && count <= 2)) 
                {  
                    result.Add($"{option}) Save an 5\n", 50);
                    option++;   
                }
                if (number == 1 && count == 3)
                {
                    result.Add($"{option}) Save the triple 1s\n", 300);
                    option++;
                    numOfTriplets++;
                }
                if (number == 2 && count == 3)
                {
                    result.Add($"{option}) Save the triple 2s\n", 200);
                    option++;
                    numOfTriplets++;
                }
                if (number == 3 && count == 3)
                {
                    result.Add($"{option}) Save the triple 3s\n", 300);
                    option++;
                    numOfTriplets++;
                }
                if (number == 4 && count == 3)
                {
                    result.Add($"{option}) Save the triple 4s\n", 400);
                    option++;
                    numOfTriplets++;
                }
                if (number == 5 && count == 3)
                {
                    result.Add($"{option}) Save the triple 5s\n", 500);
                    option++;
                    numOfTriplets++;
                }
                if (number == 6 && count == 3)
                {
                    result.Add($"{option}) Save the triple 6s\n", 600);
                    option++;
                    numOfTriplets++;
                }
                if (count == 6)
                {
                    return new Dictionary<string, int>{ { "1) Save the 6 of a kind\n", 3000} };
                }
                else if (count == 5)
                {
                    result.Add($"{option}) Save the 5 of a kind\n", 2000);
                    option++;
                }
                else if (count == 4)
                {
                    // Check for a pair with remaining numbers
                    bool pairExists = numberCounts.Any(n => n.Value == 2);

                    if (pairExists)
                    {
                        result.Add($"{option}) Save the 4 of a kind and the pair\n", 1500);
                    }
                    else
                    {
                        result.Add($"{option}) Save the 4 of a kind\n", 1000);
                    }
                    option++;
                }
                if (numOfTriplets == 2)
                {
                    return new Dictionary<string, int>{ {"1) Save the two triplets\n", 2500 } };
                }
            }

            if (result.Count == 0)
            {
                return new Dictionary<string, int> { { "1) No Matches\n", 0 } };
            }

            return result;
        }

    }
}

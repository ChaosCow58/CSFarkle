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

        public List<string> CheckDice(string diceValues)
        {
            List<string> result = new List<string>();
            List<int> numbers = diceValues
            .Replace("[", "")   
            .Replace("]", "")    
            .Split(',')
            .Select(s => int.Parse(s.Trim()))
            .ToList();
            foreach (int s in numbers)
            {
                Console.Write(s);
            }

            return result;
        }
    }
}

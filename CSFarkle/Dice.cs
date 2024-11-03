using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFarkle
{
    public class Dice
    {
       private List<int> DiceValues { get; set; } = [];
       private readonly Random random = new Random();

       public Dice() { }

        public void RollDice(int numberOfDice = 6) 
        {
            DiceValues.Capacity = numberOfDice;
            for (int i = 0; i < numberOfDice; i++)
            {
                DiceValues.Add(random.Next(1, 7));
            }
        }

        public string GetDiceValues() 
        {
            return $"[{string.Join(", ", DiceValues)}]";
        }

        public void AddDiceValue(int value)
        {
            DiceValues.Add(value);
        }

        public void DeleteDiceValue(int value)
        {
            DiceValues.Remove(value);
        }

        public void ClearDiceValues()
        {
            DiceValues.Clear();
        }
    }
}

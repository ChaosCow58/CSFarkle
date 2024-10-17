using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFarkle
{
    internal class Dice
    {
       private List<int> DiceValues { get; set; } = [];
       private Random random = new Random();

       public Dice() { }

        public void RollDice() 
        {
            for (int i = 0;i < 6;i++)
            {
                DiceValues.Add(random.Next(1, 7));
            }
        }

        public string GetDiceValues() 
        {
            string val = "[";
            for (int i = 0;i < DiceValues.Count;i++) 
            { 
                if (i != DiceValues.Count - 1) 
                { 
                    val += DiceValues[i] + ", ";
                }
                else
                {
                    val += DiceValues[i] + "]";
                }
            }
            return val;
        }

        public void AddDiceValue(int value)
        {
            DiceValues.Add(value);
        }

        public void DeleteDiceValue(int value)
        {
            DiceValues.Remove(value);
        }
    }
}

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
       private Random random = new Random();

       public Dice() { }

        public void RollDice(int numberOfDice = 6) 
        {
            for (int i = 0;i < numberOfDice;i++)
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

        public void ClearDiceValues()
        {
            DiceValues.Clear();
        }
    }
}

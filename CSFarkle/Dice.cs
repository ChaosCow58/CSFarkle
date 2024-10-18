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
       private List<int> StoredValues { get; set; } = [];
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

        public string GetStoredValues()
        {
            string val = "[";
            for (int i = 0; i < StoredValues.Count; i++)
            {
                if (i != StoredValues.Count - 1)
                {
                    val += StoredValues[i] + ", ";
                }
                else
                {
                    val += StoredValues[i] + "]";
                }
            }
            return val;
        }

        public void AddStoredValue(int value)
        {
            StoredValues.Add(value);
        }

        public void DeleteStoredValue(int value)
        {
            StoredValues.Remove(value);
        }

        public void ClearStoredValues()
        {
            StoredValues.Clear();
        }
    }
}

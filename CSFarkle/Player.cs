using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFarkle
{
    public class Player(int playerNum)
    {
        public int Points { get; set; }
        public int RunningTotal { get; set; }
        public bool HasOneChance { get; set; } = false;
        public int PlayerNum { get; } = playerNum;
        public int NumOfTurns { get; set; } = 0;
    }
}

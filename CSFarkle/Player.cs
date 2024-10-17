using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFarkle
{
    internal class Player
    {
        public int Points { get; set; }
        public bool  HasFarkled { get; set; }
        public bool HasOneChance { get; set; } = false;
        public int PlayerNum { get; }

        public Player(int playerNum) 
        { 
            this.PlayerNum = playerNum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DiceGame.MultiInstance
{
    internal class Die(int sidesCon = 0)
    {
        internal int sides = sidesCon;
        internal int rolledNum = 0;
        Random random = new Random();
        public int Roll()
        {
            rolledNum = random.Next(1, sides + 1);
            return rolledNum;
        }
    }
}

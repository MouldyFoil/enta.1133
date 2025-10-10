using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.MultiInstance
{
    internal class Player(bool CPU = false, string name = "unnamed") //The class for the player and opponent.
    {
        internal string name = name;
        internal List<Die> dice = new List<Die>();
        internal List<int> winningRolls = new List<int>();
        internal int activeDieIndex;
        internal int score = 0;
        internal bool isCPU = CPU;
        internal int ReturnActiveDieRoll()
        {
            return dice[activeDieIndex].rolledNum;
        }
        internal int ReturnActiveDieSides()
        {
            return dice[activeDieIndex].sides;
        }
        internal int SumOfWinningRolls()
        {
            int sum = 0;
            foreach(int roll in winningRolls)
            {
                sum += roll;
            }
            return sum;
        }
    }
}

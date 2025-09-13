using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts
{
    internal class GameController
    {
        internal Character player = new();
        Character opponent = new();
        List<int> diceOptions;
        
    }
    internal class Character
    {
        internal string name;
        internal int score;
        internal List<int> storedRolls;
        internal List<int> diceOptionsLeft;
    }
}

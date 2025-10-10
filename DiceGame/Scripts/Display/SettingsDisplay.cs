using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiceGame.Scripts.Display
{
    internal class SettingsDisplay : GeneralDisplay
    {
        internal void DisplayDiceOptions(List<int> dice)
        {
            string diceList = "Current dice selection:\n";
            foreach(int die in dice)
            {
                diceList += die + " ";
            }
            Console.WriteLine(diceList);
        }
    }
}

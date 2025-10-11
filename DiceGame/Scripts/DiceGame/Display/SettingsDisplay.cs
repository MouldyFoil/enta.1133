using DiceGame.Scripts.DiceGame.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiceGame.Scripts.DiceGame.Display
{
    internal class SettingsDisplay : GeneralDisplay
    {
        internal void DisplayDiceOptions(List<int> dice)
        {
            string diceList = "Current dice selection:\n";
            foreach(int die in dice)
            {
                diceList += "D" + die + " ";
            }
            Console.WriteLine(diceList);
        }
        internal void DisplayPlayerCPUStatus(Player playerSelected)
        {
            string playerCPUStatus = "no longer";
            if (playerSelected.isCPU)
            {
                playerCPUStatus = "now";
            }
            Console.WriteLine(playerSelected.name + " is " + playerCPUStatus + " a CPU");
        }
    }
}

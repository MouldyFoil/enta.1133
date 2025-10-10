using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Display
{
    internal class GeneralDisplay
    {
        internal string ListPlayers(List<Player> players, string spacing = "", string lastEntryFlavor = "", bool displayBotStatus = false, bool addNumbers = false)
        {
            string listedPlayers = "";
            int index = 0;
            foreach (Player player in players)
            {
                if (index == players.Count - 1 && lastEntryFlavor != "")
                {
                    listedPlayers += lastEntryFlavor;
                }
                else if (index > 0)
                {
                    listedPlayers += spacing;
                }
                if (addNumbers)
                {
                    listedPlayers += +(index + 1) + ". ";
                }
                listedPlayers += player.name;
                if (displayBotStatus && player.isCPU)
                {
                    listedPlayers += "(CPU)";
                }
                index++;
            }
            return listedPlayers;
        }
        internal void InvalidInputDisplay()
        {
            Console.WriteLine("INVALID INPUT");
        }
    }
}

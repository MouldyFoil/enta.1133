using DiceGame.Scripts.Play;
using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Display
{
    internal class DisplayRound : GeneralDisplay
    {
        internal void DisplayPlayerIntroduction(List<Player> players)
        {
            string introText = ListPlayers(players, ", ", " and ") + " sit at the table.";
            Console.WriteLine(introText);
        }
        internal void DisplayDiceOptions(Player player)
        {
            string diceOptions = "Options:";
            foreach (Die d in player.dice)
            {
                if(d.rolledNum == 0)
                {
                    diceOptions += " " + d.sides;
                }
            }
            Console.WriteLine(diceOptions);
        }
        internal void DisplayTurn(Player player) //Displays the most recent actions taken by specified character.
        {
            Console.WriteLine(player.name + " decides to roll a D" + player.ReturnActiveDieSides() + ".");
            DisplayRolledNumber(player);
        }
        internal void DisplayRolledNumber(Player character) //Displays the most recent roll of a specified character and adds flare if certain conditions are met.
        {
            Console.WriteLine(character.name + " rolled a " + character.ReturnActiveDieRoll() + ".");
            if (character.ReturnActiveDieRoll() == character.ReturnActiveDieSides())
            {
                if (character.ReturnActiveDieRoll() == 20)
                {
                    Console.WriteLine("NAT 20!");
                }
                else
                {
                    Console.WriteLine("MAXIMUM!");
                }
            }
            else if (character.ReturnActiveDieRoll() > (float)(character.ReturnActiveDieSides() + 1) / 2)
            {
                Console.WriteLine("Above average!");
            }
            else if (character.ReturnActiveDieRoll() == 1)
            {
                Console.WriteLine("Nat 1 :(");
            }
        }
        internal void DisplayTieText(List<Player> tiedPlayers)
        {
            string tieText = "There was a tie between ";
            int index = 0;
            foreach (Player player in tiedPlayers)
            {
                if (index == tiedPlayers.Count - 1)
                {
                    tieText += " and " + player.name + ".";
                }
                else if (index == 0)
                {
                    tieText += player.name;
                }
                else
                {
                    tieText += ", " + player.name;
                }
                index++;
            }
            Console.WriteLine(tieText);
        }
    }
}

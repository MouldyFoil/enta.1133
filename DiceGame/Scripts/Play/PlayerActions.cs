using DiceGame.Scripts.MultiInstance;
using DiceGame.Scripts.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Play
{
    internal class PlayerActions(DisplayRound displayConstructor, Random randomConstructor)
    {
        DisplayRound display = displayConstructor;
        Random random = randomConstructor;
        internal void ComputerTurn(Player player) //Handles the computer turn, randomizing the die chosen by the computer.
        {
            RollDie(player, random.Next(player.dice.Count - 1));
        }
        internal void UserTurn(Player player) //Handles a player turn and takes input.
        {
            Console.WriteLine("Input amount of sides of the dice you want to roll.");
            bool inputValid = false;
            while (!inputValid)
            {
                display.DisplayDiceOptions(player);
                if (int.TryParse(Console.ReadLine(), out int parsedNum) && CheckDiceInputValidity(parsedNum, out int selectedDie, player))
                {
                    RollDie(player, selectedDie);
                    inputValid = true;
                }
                else
                {
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }
        private bool CheckDiceInputValidity(int inputSides, out int dieIndex, Player player) //Checks if player input is valid and outputs the index of the selected die.
        {
            int index = 0;
            foreach (Die die in player.dice)
            {
                if (die.sides == inputSides && die.rolledNum == 0)
                {
                    dieIndex = index;
                    return true;
                }
                index++;
            }
            dieIndex = 0; //if this happens, it means that the player input is invalid and dieIndex will not be used. The only reason it is zero is because i didnt want the computer to yell at me.
            return false;
        }
        internal void RerollDie(Player player) //Rerolls active die of specified character.
        {
            player.dice[player.activeDieIndex].Roll();
        }
        private void RollDie(Player character, int dieIndex) //Rolls a die of specified character and sets the active die index to specified amount.
        {
            character.dice[dieIndex].Roll();
            character.activeDieIndex = dieIndex;
        }
    }
}

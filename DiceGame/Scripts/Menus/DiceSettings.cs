using DiceGame.Scripts.Display;
using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Menus
{
    internal class DiceSettings(SettingsDisplay display)
    {
        SettingsDisplay display = display;
        List<int> diceOptions = new List<int>();
        int[] defaultDiceOptions = [6, 8, 12, 20];
        bool inDiceSettings;
        internal List<int> RunDiceSettings(List<int> diceOptionsIn)
        {
            diceOptions = diceOptionsIn;
            display.DisplayDiceOptions(diceOptions);
            inDiceSettings = true;
            while (inDiceSettings)
            {
                Console.WriteLine("\n\nDICE SETTINGS\n(NOT RECCOMENDED TO EDIT ON FIRST PLAYTHROUGH)\n");
                display.DisplayDiceOptions(diceOptions);
                Console.WriteLine("1. New dice list");
                Console.WriteLine("2. Add dice");
                Console.WriteLine("3. Remove dice");
                Console.WriteLine("4. Use default");
                Console.WriteLine("5. Back");
                DiceSettingsInput();
            }
            return diceOptions;
        }
        private void DiceSettingsInput()
        {
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                if (int.TryParse(Console.ReadLine(), out int parsedNum))
                {
                    switch (parsedNum)
                    {
                        case 1:
                            NewDiceList();
                            break;
                        case 2:
                            AddDice();
                            break;
                        case 3:
                            RemoveDice();
                            break;
                        case 4:
                            SetDiceAsDefault();
                            break;
                        case 5:
                            inDiceSettings = false;
                            break;
                        default:
                            validInput = false;
                            Console.WriteLine("INVALID INPUT");
                            break;
                    }
                }
                else
                {
                    validInput = false;
                    display.InvalidInputDisplay();
                }
            }
        }
        private void NewDiceList()
        {
            List<int> dice = HandleDiceListInput("for your new list");
            if(dice.Count > 0)
            {
                diceOptions = dice;
            }
            else
            {
                display.InvalidInputDisplay();
            }
        }
        private void AddDice()
        {
            List<int> dice = HandleDiceListInput("you want to add");
            if (dice.Count > 0)
            {
                foreach (int die in dice)
                {
                    diceOptions.Add(die);
                }
            }
            else
            {
                display.InvalidInputDisplay();
            }
        }
        private void RemoveDice()
        {
            List<int> dice = HandleDiceListInput("you want to remove");
            if (dice.Count > 0)
            {
                foreach(int die in dice)
                {
                    if (diceOptions.Contains(die))
                    {
                        diceOptions.Remove(die);
                    }
                    else
                    {
                        Console.WriteLine("There is no D" + die.ToString());
                    }
                }
                if(diceOptions.Count == 0)
                {
                    Console.WriteLine("REMOVED ALL DICE\nRESETING TO DEFAULTS");
                    SetDiceAsDefault();
                }
            }
            else
            {
                display.InvalidInputDisplay();
            }
        }
        private void SetDiceAsDefault()
        {
            diceOptions = defaultDiceOptions.ToList();
        }
        private List<int> HandleDiceListInput(string clarificationText)
        {
            Console.WriteLine("Input the number of sides of the dice " + clarificationText + "\nIf you want to add multiple, insert the numbers with non-number characters in-between");
            Console.WriteLine("Ex. 6, 8, 12, 20");
            string input = Console.ReadLine();
            List<int> dice = new List<int>(0);
            int currentDiceIndex = 0;
            int addToCurrent;
            foreach(Char c in input.ToCharArray())
            {
                if(int.TryParse(c.ToString(), out addToCurrent))
                {
                    if(dice.Count() - 1 == currentDiceIndex)
                    {
                        dice[currentDiceIndex] = (dice[currentDiceIndex] * 10) + addToCurrent;
                    }
                    else
                    {
                        dice.Add(addToCurrent);
                    }
                }
                else if (dice.Count() > 0 && currentDiceIndex == dice.Count() - 1)
                {
                    currentDiceIndex++;
                }
            }
            while (dice.Contains(0))
            {
                dice.Remove(0);
            }
            return dice;
        }
    }
}

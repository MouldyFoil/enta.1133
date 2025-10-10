using DiceGame.Scripts.Display;
using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Menus
{
    internal class Settings
    {
        internal List<Player> players { get; private set; } = [new Player(false, "Person"), new Player(true, "Computer")];
        internal List<int> diceOptions { get; private set; } = [6, 8, 12, 20];
        SettingsDisplay display = new SettingsDisplay();
        PlayerSettings playerSettings;
        int playerMax = 6;
        bool inSettings;
        internal void StartupThings()
        {
            playerSettings = new PlayerSettings(display);
        }
        internal void SettingsMenu()
        {
            inSettings = true;
            while (inSettings)
            {
                Console.WriteLine("SETTINGS:\n\n");
                Console.WriteLine("1. Players");
                Console.WriteLine("2. Dice");
                Console.WriteLine("3. Back");
                MainSettingsInputs();
            }
        }
        private void MainSettingsInputs()
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
                            playerSettings.RunPlayersSettings(players, playerMax);
                            break;
                        case 2:
                            DiceSettings();
                            break;
                        case 3:
                            inSettings = false;
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
        
        private void DiceSettings()
        {
            display.DisplayDiceOptions(diceOptions);
        }
        
    }
}

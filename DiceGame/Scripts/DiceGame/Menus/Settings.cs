using DiceGame.Scripts.DiceGame.Display;
using DiceGame.Scripts.DiceGame.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DiceGame.Menus
{
    internal class Settings
    {
        internal List<Player> players { get; private set; } = [new Player(false, "Person"), new Player(true, "Computer")];
        internal List<int> diceOptions { get; private set; } = [6, 8, 12, 20];
        SettingsDisplay display = new SettingsDisplay();
        PlayerSettings playerSettings;
        DiceSettings diceSettings;
        int playerMax = 4;
        bool inSettings;
        internal void StartupThings()
        {
            playerSettings = new PlayerSettings(display);
            diceSettings = new DiceSettings(display);
        }
        internal void RunSettingsMenu()
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
                            players = playerSettings.RunPlayersSettings(players, playerMax);
                            break;
                        case 2:
                            diceOptions = diceSettings.RunDiceSettings(diceOptions);
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
        
        
        
    }
}

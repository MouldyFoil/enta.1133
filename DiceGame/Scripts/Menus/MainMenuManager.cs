using DiceGame.Scripts.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Menus
{
    internal class MainMenuManager(GameManager gameManagerConst, Settings settingsConst)
    {
        GameManager gameManager = gameManagerConst;
        Settings settings = settingsConst;
        bool quit = false;
        internal void Introduction()
        {
            Console.WriteLine("Hello and welcome to");
            Console.WriteLine("THE DICE GAME THAT YOU CAN CUSTOMIZE HOWEVER YOU LIKE AND IS REALLY COOL AND STUFF (working title)\n");
            Console.WriteLine("I would reccomend you check out the tutorial, then mess around with some settings.\n\n");
            Console.WriteLine("-------------------------------------------------------\n");
            MainMenu();
            //MainMenu();
        }
        internal void MainMenu()
        {
            while (!quit)
            {
                Console.WriteLine("MAIN MENU\n(Type the number of the option you want to select)\n\n");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Tutorial");
                Console.WriteLine("3. Settings");
                Console.WriteLine("4. Quit");
                MainMenuInputs();
            }

        }
        private void MainMenuInputs()
        {
            bool validInput = false;
            while(!validInput)
            {
                validInput = true;
                if (int.TryParse(Console.ReadLine(), out int parsedNum))
                {
                    switch (parsedNum)
                    {
                        case 1:
                            gameManager.Play();
                            break;
                        case 2:
                            Tutorial();
                            break;
                        case 3:
                            settings.SettingsMenu();
                            break;
                        case 4:
                            quit = true;
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
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }
        private void Tutorial()
        {

        }
        
        
    }
}

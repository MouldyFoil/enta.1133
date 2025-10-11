using DiceGame.Scripts.DiceGame.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DiceGame.Menus
{
    internal class MainMenuManager(GameManager gameManagerConst, Settings settingsConst)
    {
        GameManager gameManager = gameManagerConst;
        Settings settings = settingsConst;
        bool quit = false;
        bool inTutorial;
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
                            settings.RunSettingsMenu();
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
            inTutorial = true;
            List<string> pages;
            pages = new List<string>();
            pages.Add
            ("So you're wondering how you play..." +
            "\nIt's simple!"+
            "\n..."+
            "\nI think?"
            );
            pages.Add
            (
             "ROUNDS:" +
             "\nEach player gets a set of dice, by default they get a D6, D8, D12 and D20" +
             "\nAll players must roll one of these on their turn" +
             "\nWhen a player rolls a die, it gets used up and they cant use it for the rest of the match" +
             "\nThe player with the highest roll scores a point" +
             "\nIf players tie on a roll, they reroll their die untill a winner is decided" +
             "\nThis continues until all dice have been used up"
            );
            pages.Add
            (
             "MATCHES:" +
             "\nWhen all dice have been used up, player scores are compared" +
             "\nIf players tie here, the player with the highest sum of winning rolls wins"
            );
            pages.Add
            (
             "BYE BYE:"+
             "\nThats pretty much how it works"+
             "\nContinuing will exit the tutorial"
            );
            DisplaytTutorial(pages);
        }
        private void DisplaytTutorial(List<string> pages)
        {
            int pageIndex = 0;
            while (inTutorial)
            {
                Console.WriteLine("\n\n" + pages[pageIndex]);
                Console.WriteLine("\n1. Next\n2. Previous\n3. Exit");
                pageIndex = TutorialInputs(pageIndex);
                if(pageIndex >= pages.Count())
                {
                    inTutorial = false;
                }
                if(pageIndex < 0)
                {
                    pageIndex = 0;
                    Console.WriteLine("YOURE ON THE FIRST PAGE, EXIT TO LEAVE TUTORIAL");
                }
            }
        }
        private int TutorialInputs(int indexToAddTo)
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
                            indexToAddTo++;
                            break;
                        case 2:
                            indexToAddTo--;
                            break;
                        case 3:
                            inTutorial = false;
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
            return indexToAddTo;
        }
    }
}

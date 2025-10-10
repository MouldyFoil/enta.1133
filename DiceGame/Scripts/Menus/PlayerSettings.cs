using DiceGame.Scripts.Display;
using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Menus
{
    internal class PlayerSettings(SettingsDisplay displayIn)
    {
        List<Player> players = new List<Player>();
        int playerMax;
        SettingsDisplay display = displayIn;
        bool inPlayerSettings;
        internal void RunPlayersSettings(List<Player> playersIn, int playerMax)
        {
            players = playersIn;
            inPlayerSettings = true;
            while (inPlayerSettings)
            {
                Console.WriteLine("PLAYER SETTINGS\n\n");
                Console.WriteLine(players.Count() + "/" + playerMax + " players");
                Console.WriteLine("Current players:\n" + display.ListPlayers(players, " ", "", true, true));
                Console.WriteLine("1. Edit player");
                Console.WriteLine("2. Add player");
                Console.WriteLine("3. Remove player");
                Console.WriteLine("4. Back");
                PlayerSettingsInput();
            }
        }
        private void PlayerSettingsInput()
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
                            EditPlayer();
                            break;
                        case 2:
                            AddPlayer(playerMax);
                            break;
                        case 3:
                            RemovePlayer();
                            break;
                        case 4:
                            inPlayerSettings = false;
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
        private void EditPlayer()
        {
            int numberInput = SelectPlayer("edit");
            Player playerSelected = players[numberInput - 1];
            Console.WriteLine("Would you like to.. \n 1. Edit name \n 2. Flip CPU status \n 3. Both \n 4. Nevermind...");
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                if (int.TryParse(Console.ReadLine(), out int parsedNum))
                {
                    switch (parsedNum)
                    {
                        case 1:
                            SetNewName(playerSelected);
                            break;
                        case 2:
                            FlipCPUStatus(playerSelected);
                            break;
                        case 3:
                            FlipCPUStatus(playerSelected);
                            SetNewName(playerSelected);
                            break;
                        case 4:

                            break;
                        default:
                            validInput = false;
                            display.InvalidInputDisplay();
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

        private void FlipCPUStatus(Player playerSelected)
        {
            playerSelected.isCPU = !playerSelected.isCPU;
            string playerCPUStatus = "no longer";
            if (playerSelected.isCPU)
            {
                playerCPUStatus = "now";
            }
            Console.WriteLine(playerSelected.name + " is " + playerCPUStatus + " a CPU");
        }

        private void SetNewName(Player playerSelected)
        {
            Console.WriteLine("What is their new name?");
            AssignPlayerName(playerSelected);
        }

        private void AddPlayer(int playerMax)
        {
            if (players.Count <= playerMax)
            {
                Player newPlayer = new Player();
                SetPlayerValues(newPlayer);
                players.Add(newPlayer);
            }
            else
            {
                Console.WriteLine("Too many players!");
            }
        }

        private void SetPlayerValues(Player newPlayer)
        {
            Console.WriteLine("What is this player's name?");
            AssignPlayerName(newPlayer);
            Console.WriteLine("Is this player a CPU?\nY/N");
            SetPlayerIsCPU(newPlayer);
        }

        private void RemovePlayer()
        {
            int numberInput = SelectPlayer("remove");
            Console.WriteLine("Removing " + players[numberInput - 1]);
            players.RemoveAt(numberInput - 1);
        }
        private int SelectPlayer(string action)
        {
            Console.WriteLine("Input the number of the player you want to " + action);
            bool validInput = false;
            int validatedInput = 0;
            while (!validInput)
            {
                validInput = true;
                if (int.TryParse(Console.ReadLine(), out int numberInput) && 0 < numberInput && numberInput <= players.Count())
                {
                    validatedInput = numberInput;
                }
                else
                {
                    validInput = false;
                    display.InvalidInputDisplay();
                }
            }
            return validatedInput;
        }
        private void AssignPlayerName(Player player) //Sets name of specified character to console input.
        {
            string newName = Console.ReadLine();
            while (newName == "" || newName == null)
            {
                Console.WriteLine("Thats an empty line! please input a name.");
                newName = Console.ReadLine();
            }
            player.name = newName;
        }
        private void SetPlayerIsCPU(Player player)
        {
            bool validInput = false;
            string input = Console.ReadLine().ToLower();
            while (!validInput)
            {
                validInput = true;
                if (input == "y" || input == "yes")
                {
                    player.isCPU = true;
                }
                else if (input == "n" || input == "no")
                {
                    player.isCPU = false;
                }
                else
                {
                    display.InvalidInputDisplay();
                    validInput = false;
                }
            }

        }
    }
}

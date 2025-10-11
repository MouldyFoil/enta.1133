using DiceGame.Scripts.DungeonThing.PlayerStuff;
using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing
{
    
    internal class DungeonGameLoop
    {
        Player player = new Player();
        RoomsGrid grid = new RoomsGrid();
        Random random = new Random();
        bool gameLooping = true;
        internal void BeginDungeonStuff()
        {
            int startingPosX = random.Next(grid.sizeX);
            int startingPosY = random.Next(grid.sizeY);
            grid.CreateGrid();
            player.currentRoom = grid.ReturnRoomBasedOnCoords(startingPosX, startingPosY);
            if (player.currentRoom is EncounterRoom)
            {
                grid.ChangeRoomType(startingPosX, startingPosY);
            }
            Introduction();
            while (gameLooping)
            {
                GameLoop();
            }
        }
        private void Introduction()
        {
            Console.WriteLine("You wake up with in a dungeon with no memory of how you got here.\n\n");
            player.currentRoom.OnRoomEntered();
        }
        private void GameLoop()
        {
            PromptPlayer();
            HandleMainInputs();
        }

        private void PromptPlayer()
        {
            Console.WriteLine("\nWhat would you like to do?\n1. Move\n2. Search room\n3. Check inventory");
        }
        private void HandleMainInputs()
        {
            bool inputValid = false;
            while (!inputValid)
            {
                inputValid = true;
                string input = Console.ReadLine();
                if(int.TryParse(input, out int intInput))
                {
                    switch (intInput)
                    {
                        case 1:
                            MovePlayer();
                            break;
                        case 2:
                            SearchRoom();
                            break;
                        case 3:

                            break;
                        default:
                            inputValid = false;
                            Console.WriteLine("INVALID INPUT");
                            break;
                    }
                }
                else
                {
                    inputValid = false;
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }
        private void MovePlayer()
        {
            PromptPlayerMove();
            HandleMoveInputs();
        }
        private void PromptPlayerMove()
        {
            Console.WriteLine("\nWhat direction do you want to move?");
            string options = "";
            if(player.currentRoom.north != null)
            {
                options += "N/";
            }
            if (player.currentRoom.east != null)
            {
                options += "E/";
            }
            if (player.currentRoom.south != null)
            {
                options += "S/";
            }
            if (player.currentRoom.west != null)
            {
                options += "W";
            }
            Console.WriteLine(options);
        }
        private void HandleMoveInputs()
        {
            bool inputValid = false;
            while (!inputValid)
            {
                inputValid = true;
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "n":
                        player.Move(false, true);
                        break;
                    case "e":
                        player.Move(true, true);
                        break;
                    case "s":
                        player.Move(false, false);
                        break;
                    case "w":
                        player.Move(true, false);
                        break;
                    default:
                        inputValid = false;
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }
        }
        private void SearchRoom()
        {
            player.currentRoom.OnRoomSearched();
        }
    }
}

using DiceGame.Scripts.DungeonThing.PlayerStuff;
using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing
{
    
    internal static class DungeonGameLoop
    {
        internal static Player player { get; private set; } = new Player();
        internal static Random random = new Random();
        internal static bool gameLooping = true;
        static bool restartGame = true;
        internal static void BeginDungeonStuff()
        {
            while (restartGame)
            {
                player = new Player();
                int startingPosX = random.Next(RoomsGrid.sizeX);
                int startingPosY = random.Next(RoomsGrid.sizeY);
                RoomsGrid.CreateGrid();
                player.currentRoom = RoomsGrid.ReturnRoomBasedOnCoords(startingPosX, startingPosY);
                if (player.currentRoom is EncounterRoom)
                {
                    RoomsGrid.ChangeRoomType(startingPosX, startingPosY);
                    player.currentRoom = RoomsGrid.ReturnRoomBasedOnCoords(startingPosX, startingPosY);
                }
                Introduction();
                while (gameLooping)
                {
                    GameLoop();
                }
                PromptReplay();
            }
        }
        private static void Introduction()
        {
            Console.WriteLine("You wake up with in a dungeon with no memory of how you got here.");
            NamePlayer();
            player.currentRoom.OnRoomEntered();
        }

        private static void NamePlayer()
        {
            Console.WriteLine("What do you call yourself?\n\n");
            bool validInput = false;
            while (!validInput)
            {
                string input = Console.ReadLine();
                if (input != null && input != "" && input.ToCharArray()[0].ToString() != " ")
                {
                    player.combat.name = input;
                    validInput = true;
                }
            }
        }

        private static void GameLoop()
        {
            Console.WriteLine("\n\nCurrent coordinates: " + RoomsGrid.ReturnSpecificRoomCoords(player.currentRoom, out int y).ToString() + ", " + y.ToString());
            Console.WriteLine(player.combat.name + "'s health: " + player.combat.health + "/" + player.combat.maxHealth);
            PromptPlayer();
            HandleMainInputs();
        }

        private static void PromptPlayer()
        {
            Console.WriteLine("\nWhat would you like to do?\n1. Move\n2. Search room\n3. Check inventory");
        }
        private static void HandleMainInputs()
        {
            bool inputValid = false;
            while (!inputValid)
            {
                inputValid = true;
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        MovePlayer();
                        break;
                    case "2":
                        player.SearchRoom();
                        break;
                    case "3":
                        player.OpenInventory(false);
                        break;
                    default:
                        inputValid = false;
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }
        }
        private static void MovePlayer()
        {
            PromptPlayerMove();
            HandleMoveInputs();
        }
        private static void PromptPlayerMove()
        {
            Console.WriteLine("\nWhat direction do you want to move?");
            string options = "";
            if(RoomsGrid.ReturnRoomBasedOnCoords(player.currentRoom.coordsY + 1, player.currentRoom.coordsX) != null)
            {
                options += "N/";
            }
            if (RoomsGrid.ReturnRoomBasedOnCoords(player.currentRoom.coordsY, player.currentRoom.coordsX + 1) != null)
            {
                options += "E/";
            }
            if (RoomsGrid.ReturnRoomBasedOnCoords(player.currentRoom.coordsY - 1, player.currentRoom.coordsX) != null)
            {
                options += "S/";
            }
            if (RoomsGrid.ReturnRoomBasedOnCoords(player.currentRoom.coordsY, player.currentRoom.coordsX - 1) != null)
            {
                options += "W";
            }
            Console.WriteLine(options);
        }
        private static void HandleMoveInputs()
        {
            bool inputValid = false;
            while (!inputValid)
            {
                inputValid = true;
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "n":
                        player.MovePlayer(false, true);
                        break;
                    case "e":
                        player.MovePlayer(true, true);
                        break;
                    case "s":
                        player.MovePlayer(false, false);
                        break;
                    case "w":
                        player.MovePlayer(true, false);
                        break;
                    default:
                        inputValid = false;
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }

        }
        private static void PromptReplay()
        {
            Console.Clear();
            Console.WriteLine("Game over\n\nWould you like to play again?\nY/N");
            bool inputValid = false;
            while (!inputValid)
            {
                inputValid = true;
                string input = Console.ReadLine().ToLower();
                if (input == "y" || input == "yes")
                {
                    restartGame = true;
                }
                else if(input == "n" || input == "no")
                {
                    restartGame = false;
                }
                else
                {
                    inputValid = false;
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }

    }
}

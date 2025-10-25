using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.PlayerStuff
{
    internal static class PlayerMovement
    {
        internal static void Move(bool horizontalMovement, bool leftOrUp, Player player)
        {
            int coordsX = 1;
            int coordsY = 1;
            if (horizontalMovement)
            {
                coordsY = 0;
            }
            else
            {
                coordsX = 0;
            }
            if (!leftOrUp)
            {
                coordsX *= -1;
                coordsY *= -1;
            }
            AttemptMoveToRoom(player.currentRoom.coordsX + coordsX, player.currentRoom.coordsY + coordsY, player);
        }
        private static void AttemptMoveToRoom(int coordsX, int coordsY, Player player)
        {
            Room nextRoom = RoomsGrid.ReturnRoomBasedOnCoords(coordsX, coordsY);
            if (nextRoom != null)
            {
                player.currentRoom = nextRoom;
                player.currentRoom.OnRoomEntered();
            }
            else
            {
                Console.WriteLine("That room doesn't exist!");
            }
        }
    }
}

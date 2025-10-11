using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.PlayerStuff
{
    internal class Player
    {
        internal Room currentRoom;
        Inventory inventory = new Inventory();
        internal void Move(bool horizontalMovement, bool leftOrUp)
        {
            if(horizontalMovement && leftOrUp)
            {
                currentRoom = currentRoom.east;
            }
            switch(horizontalMovement, leftOrUp)
            {
                case (false, true):
                    AttemptMoveToRoom(currentRoom.north);
                    break;
                case (true, true):
                    AttemptMoveToRoom(currentRoom.east);
                    break;
                case (false, false):
                    AttemptMoveToRoom(currentRoom.south);
                    break;
                case (true, false):
                    AttemptMoveToRoom(currentRoom.west);
                    break;
            }
        }
        private void AttemptMoveToRoom(Room nextRoom)
        {
            if (nextRoom != null)
            {
                currentRoom = nextRoom;
                currentRoom.OnRoomEntered();
            }
            else
            {
                Console.WriteLine("That room doesn't exist!");
            }
        }
    }
}

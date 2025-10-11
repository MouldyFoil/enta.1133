using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal abstract class Room
    {
        internal abstract string roomDesc { get; }
        internal Room north;
        internal Room east;
        internal Room south;
        internal Room west;
        internal bool searched = false;
        internal bool explored = false;
        internal string RoomDescription()
        {
            return roomDesc;
        }
        internal void OnRoomEntered()
        {
            string descriptionAndBeenHere = "";
            if(explored)
            {
                descriptionAndBeenHere += "You've been here before...\n";
            }
            descriptionAndBeenHere += roomDesc;
            Console.WriteLine(descriptionAndBeenHere);
            explored = true;
            ExtraEnterBehavior();
        }
        internal abstract void ExtraEnterBehavior();
        internal abstract void OnRoomSearched();
        internal void OnRoomExit()
        {
            Console.WriteLine("\nYou left the room...\n");
        }
    }
}

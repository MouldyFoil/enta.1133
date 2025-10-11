using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal class RoomsGrid
    {
        Room[,] rooms = new Room[3,3];
        Random random = new Random();
        internal int sizeX = 3;
        internal int sizeY = 3;
        internal void CreateGrid()
        {
            rooms = new Room[sizeX, sizeY];
            //Console.WriteLine("Creating grid");
            for(int y = 0; y < rooms.GetLength(1); y++)
            {
                //Console.WriteLine("Starting collum " + y);
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    //Console.WriteLine("Starting row " + x);
                    int randomNum = random.Next(2);
                    switch (randomNum)
                    {
                        case 0:
                            rooms[x, y] = new TreasureRoom();
                            break;
                        case 1:
                            rooms[x, y] = new EncounterRoom();
                            break;
                    }
                    Room currentRoom = rooms[x, y];
                    if (x - 1 >= 0)
                    {
                        Room westRoom = rooms[x - 1, y];
                        westRoom.east = currentRoom;
                        currentRoom.west = westRoom;
                    }
                    if(y - 1 >= 0)
                    {
                        Room southRoom = rooms[x, y - 1];
                        southRoom.north = currentRoom;
                        currentRoom.south = southRoom;
                    }
                    //Console.WriteLine("Created room " + x + ", " + y + " it is a type " + randomNum + " room");
                }
            }
        }
        internal Room ReturnRoomBasedOnCoords(int x, int y)
        {
            return rooms[x,y];
        }
        internal void ChangeRoomType(int x, int y)
        {
            rooms[x, y] = new TreasureRoom();
        }
    }
}

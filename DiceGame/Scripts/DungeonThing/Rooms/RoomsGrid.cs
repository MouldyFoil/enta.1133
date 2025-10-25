using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal static class RoomsGrid
    {
        static Room[,] rooms = new Room[3,3];
        static Random random = new Random();
        static internal int sizeX = 10;
        static internal int sizeY = 10;
        internal static void CreateGrid()
        {
            rooms = new Room[sizeX, sizeY];
            //Console.WriteLine("Creating grid");
            for(int y = 0; y < rooms.GetLength(1); y++)
            {
                //Console.WriteLine("Starting collum " + y);
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    //Console.WriteLine("Starting row " + x);
                    int randomNum = random.Next(4);
                    switch (randomNum)
                    {
                        case 0:
                            rooms[x, y] = new TreasureRoom();
                            rooms[x, y].OnRoomCreatedGeneric();
                            break;
                        case 1:
                            rooms[x, y] = new EncounterRoom();
                            rooms[x, y].OnRoomCreatedGeneric();
                            break;
                        case 2:
                            rooms[x, y] = new FountainRoom();
                            rooms[x, y].OnRoomCreatedGeneric();
                            break;
                        case 3:
                            rooms[x, y] = new BasicRoom();
                            rooms[x, y].OnRoomCreatedGeneric();
                            break;
                    }
                    /*Room currentRoom = rooms[x, y];
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
                    }*/
                    //Console.WriteLine("Created room " + x + ", " + y + " it is a type " + randomNum + " room");
                }
            }
        }
        internal static int ReturnSpecificRoomCoords(Room roomIn, out int outY)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    if(rooms[x, y] == roomIn)
                    {
                        outY = y;
                        return x;
                    }
                }
            }
            Console.WriteLine("Can't find room");
            outY = 0;
            return 0;
        }
        internal static Room ReturnRoomBasedOnCoords(int x, int y)
        {
            if (x < rooms.GetLength(0) && y < rooms.GetLength(1) && x >= 0 && y >= 0)
            {
                return rooms[x, y];
            }
            return null;
        }
        internal static void ChangeRoomType(int x, int y)
        {
            rooms[x, y] = new TreasureRoom();
            rooms[x, y].OnRoomCreatedGeneric();
        }
    }
}

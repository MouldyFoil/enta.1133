using DiceGame.Scripts.DungeonThing.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal abstract class Room
    {
        internal float itemCapacity = 100;
        internal int coordsX { get; private set; }
        internal int coordsY { get; private set; }
        internal abstract string roomDesc { get; }
        internal List<Item> items = new List<Item>();
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
            ExtraEnterBehavior();
            explored = true;
        }
        internal void OnRoomSearchedGeneric()
        {
            OnRoomSearched();
        }
        internal abstract void ExtraEnterBehavior();
        internal abstract void OnRoomSearched();
        internal void OnRoomExit()
        {
            Console.WriteLine("\nYou left the room...\n");
        }
        internal void OnRoomCreatedGeneric()
        {
            coordsX = RoomsGrid.ReturnSpecificRoomCoords(this, out int coordsYOut);
            coordsY = coordsYOut;
            OnRoomCreated();
        }
        internal abstract void OnRoomCreated();
        internal void AddItem(Item itemAdded)
        {
            if(SumItemSizes() + itemAdded.size < itemCapacity)
            {
                items.Add(itemAdded);
            }
            else
            {
                Console.WriteLine("An invisible hand destroys the item");
            }
        }
        internal float SumItemSizes()
        {
            float sumOfItemSizes = 0;
            foreach(Item item in items)
            {
                sumOfItemSizes += item.size;
            }
            return sumOfItemSizes;
        }
        internal void PopulateItems()
        {
            if (!PopulateItemsSpecal())
            {
                while (DungeonGameLoop.random.Next(10) == 0)
                {
                    items.Add(ItemRepository.DrawFromItemPool());
                }
            }
        }
        internal abstract bool PopulateItemsSpecal();
    }
}

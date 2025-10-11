using DiceGame.Scripts.DungeonThing.ItemThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.PlayerStuff
{
    internal class Inventory
    {
        internal List<Item> items { get; } = new List<Item>();
        internal float maxInventorySize;
        internal void AddItem(Item itemToAdd)
        {
            if (ReturnSumOfItemsSize() + itemToAdd.size < maxInventorySize)
            {
                items.Add(itemToAdd);
            }
            else
            {
                Console.WriteLine("Inventory is full!");
            }
        }
        internal void RemoveItem(Item itemToRemove)
        {
            items.Remove(itemToRemove);
        }
        internal void RemoveItemByName(string name)
        {
            foreach (Item item in items)
            {
                if(item.name == name)
                {
                    RemoveItem(item);
                }
            }
        }
        internal float ReturnSumOfItemsSize()
        {
            float sumOfItemsSize = 0;
            foreach (Item item in items)
            {
                sumOfItemsSize += item.size;
            }
            return sumOfItemsSize;
        }
    }
}

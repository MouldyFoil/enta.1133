using DiceGame.Scripts.DungeonThing.Items;
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
        Inventory inventory;
        internal int fightsWon;
        internal PlayerCombat combat { get; private set; }
        internal Room currentRoom;
        bool searching = false;
        internal Player()
        {
            combat = new PlayerCombat(this);
            inventory = new(combat);
        }
        internal void MovePlayer(bool horizontalMovement, bool leftOrUp)
        {
            PlayerMovement.Move(horizontalMovement, leftOrUp, this);
        }
        internal bool OpenInventory(bool inCombat)
        {
            return inventory.EnterInventory(inCombat);
        }
        internal void SearchRoom()
        {
            currentRoom.OnRoomSearched();
            searching = true;
            while (searching)
            {
                if (currentRoom.items.Count() > 0)
                {
                    Console.WriteLine("You found stuff in here!");
                    Console.WriteLine("Inventory space: " + inventory.ReturnSumOfItemsSize() + "/" + inventory.inventorySize);
                    string foundText = "Items in room:\n";
                    int itemNum = 1;
                    foreach (Item item in currentRoom.items)
                    {
                        foundText += itemNum.ToString() + ": " + item.ReturnItemNameWithInfo() + "\n";
                        itemNum++;
                    }
                    foundText += "Input the number of the item you want to collect, input 'stop' to stop search";
                    Console.WriteLine(foundText);
                }
                else
                {
                    Console.WriteLine("There isn't anything here... input 'stop' to stop the search");
                }
                HandleSearchInputs();
            }
        }
        internal void HandleSearchInputs()
        {
            string input = Console.ReadLine();
            if(input.ToLower() == "stop")
            {
                searching = false;
            }
            else if(int.TryParse(input, out int intInput) && intInput <= currentRoom.items.Count() && intInput > 0)
            {
                CollectItem(currentRoom.items[intInput - 1]);
            }
        }
        internal void CollectItem(Item input)
        {
            if (inventory.AddItem(input))
            {
                currentRoom.items.Remove(input);
            }
        }
    }
}

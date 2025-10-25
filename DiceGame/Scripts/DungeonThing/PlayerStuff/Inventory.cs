using DiceGame.Scripts.DiceGame.MultiInstance;
using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.PlayerStuff
{
    internal class Inventory(PlayerCombat combat)
    {
        internal List<Item> items { get; } = [new Dagger()];
        internal float inventorySize = 10;
        PlayerCombat combat = combat;
        bool inCombat;
        bool endTurn = true;
        bool exiting = false;
        internal bool AddItem(Item itemToAdd)
        {
            if (ReturnSumOfItemsSize() + itemToAdd.size < inventorySize)
            {
                items.Add(itemToAdd);
                return true;
            }
            else
            {
                Console.WriteLine("Inventory is full!");
                return false;
            }
        }
        internal bool EnterInventory(bool combatStatus)
        {
            inCombat = combatStatus;
            exiting = false;
            endTurn = false;
            while (!exiting)
            {
                DisplayInventory();
                endTurn = true;
                HandleInventoryInputs();
            }
            return endTurn;
        }
        private void DisplayInventory()
        {
            combat.DisplayCombatInfo();
            string inputText = "";
            if (items.Count > 0)
            {
                inputText = "Input the number of the item you want to inspect\n";
                Console.WriteLine("Inventory space: " + ReturnSumOfItemsSize() + "/" + inventorySize);
                string inventoryText = "Items in inventory:\n";
                int itemNum = 1;
                foreach (Item item in items)
                {
                    inventoryText += itemNum.ToString() + " " + item.ReturnItemNameWithInfo() + "\n";
                    itemNum++;
                }
                Console.WriteLine(inventoryText);
            }
            else
            {
                Console.WriteLine("Inventory empty... 0/" + inventorySize);
            }
            inputText += "Type 'back' to exit inventory\n" +
                "Type 'unequip' to unequip a weapon\n" +
                "Type 'drop' to drop a weapon";
            Console.WriteLine(inputText);
        }

        private void HandleInventoryInputs()
        {
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                string input = Console.ReadLine();
                if (input.ToLower() == "back")
                {
                    exiting = true;
                    endTurn = false;
                }
                else if (input.ToLower() == "unequip")
                {
                    EquipedItemActions(false);
                }
                else if (input.ToLower() == "drop")
                {
                    EquipedItemActions(true);
                }
                else if (int.TryParse(input, out int intInput) && intInput <= items.Count())
                {
                    ItemOptions(items[intInput - 1]);
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }
        private void EquipedItemActions(bool drop)
        {
            endTurn = false;
            combat.DisplayEquipedWeapons();
            string display = "Select an item to ";
            if (drop) { display += "drop"; }
            else { display += "unequip"; }
            Console.WriteLine(display);
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                string input = Console.ReadLine();
                if (input.ToLower() == "back")
                {
                    Console.WriteLine("Returning...");
                }
                else if (int.TryParse(input, out int intInput) && intInput <= combat.equipedWeapons.Count() && intInput > 0)
                {
                    
                    if (drop)
                    {
                        DungeonGameLoop.player.currentRoom.AddItem(combat.equipedWeapons[intInput - 1]);
                        combat.equipedWeapons.Remove(combat.equipedWeapons[intInput - 1]);
                    }
                    else if (AddItem(combat.equipedWeapons[intInput - 1]))
                    {
                        combat.equipedWeapons.Remove(combat.equipedWeapons[intInput - 1]);
                    }
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("INVALID INPUT");
                    
                }
            }
        }
        
        private void ItemOptions(Item item)
        {
            DisplayItemOptions(item);
            HandleItemInput(item);
        }
        private void DisplayItemOptions(Item item)
        {
            Console.WriteLine("\n\nItem: " + item.ReturnItemNameWithInfo());
            string optionsText = "Options:\n1. Inspect\n2. Use\n3. Drop\n";
            Console.WriteLine(optionsText);
        }
        private void HandleItemInput(Item item)
        {
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                string input = Console.ReadLine();
                if (inCombat)
                {
                    exiting = true;
                }
                switch (input.ToLower())
                {
                    case "1":
                        Console.WriteLine(item.description);
                        break;
                    case "2":
                        if (item is Weapon)
                        {
                            exiting = false;
                            endTurn = false;
                            if (!combat.EquipWeapon((Weapon?)item))
                            {
                                Console.WriteLine("Your hands are full!");
                            }
                            else
                            {
                                RemoveItem(item);
                            }
                        }
                        else if(item.UseAndReturnConsumable(combat))
                        {
                            RemoveItem(item);
                        }
                        break;
                    case "3":
                        DungeonGameLoop.player.currentRoom.items.Add(item);
                        RemoveItem(item);
                        break;
                    case "back":
                        break;
                    default:
                        validInput = false;
                        Console.WriteLine("INVALID INPUT");
                        break;
                }

            }
        }
        internal void RemoveItem(Item itemToRemove)
        {
            if (items.Contains(itemToRemove))
            {
                items.Remove(itemToRemove);
            }
            else
            {
                Console.WriteLine("ERROR ITEM DOES NOT EXIST");
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

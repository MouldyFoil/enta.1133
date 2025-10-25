using DiceGame.Scripts.DungeonThing.Items.Consumables;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items
{
    internal static class ItemRepository
    {
        static Weapon[] weapons =
        [
            new Dagger("Knife", "A knife that seems more like a kitchen appliance than a weapon.", 1, 5, RarityEnum.common),
            new Dagger("Glass dagger", "A dagger that is too brittle to use more than a few times.", 5, 3, RarityEnum.uncommon),
            new Dagger(),
            new Mace(),
            new Mace("Skull Crusher", "The hammer of a jolly fellow.", 10, 30, RarityEnum.legendary),
            new Mace("Club", "A wooden club. Doesn't seem very durable", 4, 5, RarityEnum.common),
            new Longsword(),
            new Longsword("The Sword of Jeremy",
            "A legendary weapon said to have been wielded by an ancient hero... an ancient hero that 30% of the population is named after.", 
            10, 30, RarityEnum.legendary
            )



        ];
        static Consumable[] consumables = 
        [
            new HealingPotion("Makeshift bandage kit", 2, "A bandage kit with 5 uses, healing 2 health each use.", RarityEnum.common, 5),
            new HealingPotion("Distilled healing potion", 5, "A weak healing potion that heals 5 health", RarityEnum.common, 1, 1),
            new HealingPotion(),
            new HealingPotion("Healthy healing potion", 20, "A strong healing potion that heals 20 health", RarityEnum.rare, 1, 1),
            new HealingPotion("Extra healthy healing potion", 20, "A stronger healing potion that heals 20 health", RarityEnum.epic, 1, 1),
            new HealingPotion("Panacea", 9999, "A cure all for any ailments", RarityEnum.legendary, 1, 1),
        ];
        internal static Item GetItem(string itemName)
        {
            Item[] items = weapons;
            items.Concat(consumables);
            foreach (Item item in items)
            {
                if (item.name == itemName)
                {
                    return item;
                }
            }
            return null;
        }
        internal static Item DrawFromItemPool()
        {
            List<Item> itemsToCheck = new List<Item>();
            foreach(Item item in weapons)
            {
                itemsToCheck.Add(item);
            }
            foreach (Item item in consumables)
            {
                itemsToCheck.Add(item);
            }
            return DrawFromItemList(itemsToCheck);
        }

        private static Item DrawFromItemList(List<Item> itemsToCheck)
        {
            List<Item> items = ItemListOfRarity(itemsToCheck);
            int randomDraw = DungeonGameLoop.random.Next(items.Count());
            return items[randomDraw];
        }

        internal static List<Item> ItemListOfRarity(List<Item> itemsIn)
        {
            List<Item> items = itemsIn.ToList();
            RarityEnum rarity = DrawRarityLevel();
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].rarity != rarity)
                {
                    items.RemoveAt(i);
                    i = 0;
                }
            }
            return items;
        }
        internal static RarityEnum DrawRarityLevel()
        {
            RarityEnum rarity = RarityEnum.common;
            int sum = 0;
            foreach (int value in Enum.GetValues(typeof(RarityEnum)))
            {
                sum += value;
            }
            int roll = DungeonGameLoop.random.Next(1, sum + 1);
            int min = 0;
            int i = 0;
            foreach (int max in Enum.GetValues(typeof(RarityEnum)))
            {
                if(roll > min && roll <= max + min)
                {
                    rarity = (RarityEnum)max;
                }
                min = max;
            }
            return rarity;
        }
    }
}

using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Consumables;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies
{
    internal static class EnemyRepository
    {
        static Combatant[] enemies =
        [
            new Goblin(),
            new Ogre(),
            new Zombie(),
        ];
        /*internal static Item GetItem(string itemName)
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
        }*/
        internal static Combatant DrawFromEnemyPool()
        {
            /*List<Item> itemsToCheck = new List<Item>();
            foreach (Item item in weapons)
            {
                itemsToCheck.Add(item);
            }
            foreach (Item item in consumables)
            {
                itemsToCheck.Add(item);
            }
            return DrawFromItemList(enemies);*/
            return enemies[DungeonGameLoop.random.Next(enemies.Length)];
        }

        /*private static Item DrawFromItemList(List<Combatant> enemiesToCheck)
        {
            List<Item> items = EnemyListOfRarity(enemiesToCheck);
            int randomDraw = DungeonGameLoop.random.Next(items.Count());
            return items[randomDraw];
        }

        internal static List<Item> EnemyListOfRarity(List<Combatant> enemiesIn)
        {
            List<Combatant> items = enemiesIn.ToList();
            RarityEnum rarity = DrawRarityLevel();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].rarity != rarity)
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
            foreach (int max in Enum.GetValues(typeof(RarityEnum)))
            {
                if (roll > min && roll <= max)
                {
                    rarity = (RarityEnum)max;
                }
            }
            return rarity;
        }*/
    }
}

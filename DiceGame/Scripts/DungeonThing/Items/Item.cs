using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items
{
    internal abstract class Item
    {
        internal abstract string name { get; }
        internal abstract string description { get; }
        internal abstract float size { get; }
        internal abstract int uses { get; set; }
        internal abstract string AdditionalItemInfo(string info);
        internal abstract bool UseAndReturnConsumable(Combatant user);
        internal abstract RarityEnum rarity { get; set; }
        internal string ReturnItemNameWithInfo()
        {
            string allInfo = name;
            allInfo += ". Size: " + size + " Uses: " + uses;
            allInfo += AdditionalItemInfo(allInfo);
            return allInfo;
        }
    }
    internal enum RarityEnum
    {
        common = 50,
        uncommon = 45,
        rare = 20,
        epic = 10,
        legendary = 5,
    }
}

using DiceGame.Scripts.DungeonThing.Combat;
using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items.Weapons
{
    internal abstract class Weapon : Item
    {
        internal abstract int damage { get; set; }
        internal abstract DamageTypeEnum type { get; }
        internal abstract int handsRequired { get; }
        internal abstract void UseEvent();
        internal override string AdditionalItemInfo(string info)
        {
            return " DMG: " + damage + " Type: " + Enum.GetName(type);
        }
        internal override bool UseAndReturnConsumable(Combatant user)
        {
            UseEvent();
            return false;
        }
        internal int ReturnDamageAndDecreaseUses(out DamageTypeEnum damageType)
        {
            uses--;
            damageType = type;
            return damage;
        }
    }
    
}

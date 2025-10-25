using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items.Consumables
{
    internal abstract class Consumable : Item
    {
        internal abstract void UseEvent(Combatant user);
        internal override bool UseAndReturnConsumable(Combatant user)
        {
            uses--;
            UseEvent(user);
            return uses <= 0;
        }
    }
}

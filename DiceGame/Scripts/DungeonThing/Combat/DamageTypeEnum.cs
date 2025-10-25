using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat
{
    internal enum DamageTypeEnum
    {
        None = 0,
        Slashing = 1,
        Piercing = 2,
        Bludgeoning = 4,
        Burning = 8,
        Poison = 16,

    }
}

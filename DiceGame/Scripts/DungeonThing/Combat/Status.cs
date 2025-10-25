using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat
{
    internal abstract class Status
    {
        internal abstract void OnTurnEvent();
        internal abstract void OnAttackEvent();
        internal abstract void OnDamageEvent();
    }
}

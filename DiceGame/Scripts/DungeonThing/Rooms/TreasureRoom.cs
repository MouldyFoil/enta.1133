using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal class TreasureRoom : Room
    {
        internal override string roomDesc { get; } = "The room shimmers with the light of it's treasure.";
        internal override void OnRoomSearched()
        {
            searched = true;
        }
        internal override void ExtraEnterBehavior()
        {
            
        }
    }
}

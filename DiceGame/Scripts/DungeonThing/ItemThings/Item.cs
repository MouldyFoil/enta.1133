using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.ItemThings
{
    internal abstract class Item
    {
        internal abstract string name {  get; }
        internal abstract float size { get; }
    }
}

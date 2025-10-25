using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
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
        internal override string roomDesc { get; } = "This room seems luckier.";
        internal override void OnRoomSearched()
        {
            
        }
        internal override void ExtraEnterBehavior()
        {
            
        }
        internal override bool PopulateItemsSpecal()
        {
            Item dagger = new Dagger();
            items.Add(ItemRepository.DrawFromItemPool());
            while (DungeonGameLoop.random.Next(2) == 0)
            {
                items.Add(ItemRepository.DrawFromItemPool());
            }
            return true;
        }
        internal override void OnRoomCreated()
        {
            PopulateItems();
        }
    }
}

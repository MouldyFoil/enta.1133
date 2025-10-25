using DiceGame.Scripts.DiceGame.Menus;
using DiceGame.Scripts.DiceGame.Play;
using DiceGame.Scripts.DungeonThing.Combat;
using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies;
using DiceGame.Scripts.DungeonThing.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal class EncounterRoom : Room
    {
        internal override string roomDesc { get; } = "There are signs of a battle...";
        internal List<Combatant> creatures = new List<Combatant>();
        internal override void OnRoomSearched()
        {
            
        }
        internal override void ExtraEnterBehavior()
        {
            if (!explored)
            {
                PopulateRoom();
            }
            foreach(Combatant combatant in creatures)
            {
                combatant.room = this;
            }
            
            CombatHandler combatHandler = new CombatHandler();
            combatHandler.AttemptBeginCombat(creatures);
        }
        internal void PopulateRoom()
        {
            for (int i = 0; i <= DungeonGameLoop.player.fightsWon; i++)
            {
                creatures.Add(EnemyRepository.DrawFromEnemyPool());
            }

        }
        internal override void OnRoomCreated()
        {
            
        }

        internal override bool PopulateItemsSpecal()
        {
            return false;
        }
    }
}

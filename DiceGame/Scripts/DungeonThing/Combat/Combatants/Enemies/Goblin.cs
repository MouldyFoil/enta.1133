using DiceGame.Scripts.DiceGame.MultiInstance;
using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies
{
    internal class Goblin : AIWielder
    {
        internal override string name { get; set; } = "Goblin";
        internal override int maxHealth { get; set; } = 10;
        internal override int health { get; set; } = 10;
        internal override int hands { get; set; } = 2;
        internal override int baseDamage { get; set; } = 1;
        internal override int repDecreaseOnHit { get; } = 1;
        internal override DamageTypeEnum damageType { get; } = 0;
        internal override List<Weapon> equipedWeapons { get; set; } = [new Dagger("Goblin Shiv", "A shiv made by a goblin.", 1, 5)];
        internal override List<Item> drops { get; set; }

        internal override RarityEnum rarity => throw new NotImplementedException();

        internal Goblin()
        {
            Dictionary<FactionEnum, int> goblinPriorities = new Dictionary<FactionEnum, int>();
            goblinPriorities.Add(FactionEnum.Player, -2);
            goblinPriorities.Add(FactionEnum.Humans, -1);
            goblinPriorities.Add(FactionEnum.Goblins, 1);
            List<FactionEnum> joinedFactions = [FactionEnum.Goblins];
            factions = new CombatantFactions(goblinPriorities, joinedFactions, repChangedCombatants);
        }
    }
}

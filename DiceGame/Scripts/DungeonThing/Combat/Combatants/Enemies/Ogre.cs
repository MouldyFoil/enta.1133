using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies
{
    internal class Ogre : AIWielder
    {
        internal override string name { get; set; } = "Ogre";
        internal override int maxHealth { get; set; } = 15;
        internal override int health { get; set; } = 15;
        internal override int hands { get; set; } = 2;
        internal override int baseDamage { get; set; } = 3;
        internal override int repDecreaseOnHit { get; } = 1;
        internal override DamageTypeEnum damageType { get; } = 0;
        internal override List<Weapon> equipedWeapons { get; set; } = [new Mace("Ogre club", "A club fashioned by an ogre. More deadly in the hands of one.", 3, 5)];
        internal override List<Item> drops { get; set; }

        internal override RarityEnum rarity => throw new NotImplementedException();

        internal Ogre()
        {
            Dictionary<FactionEnum, int> goblinPriorities = new Dictionary<FactionEnum, int>();
            goblinPriorities.Add(FactionEnum.Player, -2);
            goblinPriorities.Add(FactionEnum.Humans, -2);
            goblinPriorities.Add(FactionEnum.Goblins, -2);
            goblinPriorities.Add(FactionEnum.Undead, -3);
            List<FactionEnum> joinedFactions = [FactionEnum.Goblins];
            factions = new CombatantFactions(goblinPriorities, joinedFactions, repChangedCombatants);
        }
    }
}

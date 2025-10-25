using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies
{
    internal class Zombie : AIWielder
    {
        internal override string name { get; set; } = "Zombie";
        internal override int maxHealth { get; set; } = 5;
        internal override int health { get; set; } = 3;
        internal override int hands { get; set; } = 2;
        internal override int baseDamage { get; set; } = 1;
        internal override int repDecreaseOnHit { get; } = 1;
        internal override DamageTypeEnum damageType { get; } = 0;
        internal override List<Weapon> equipedWeapons { get; set; } = [new Longsword("Aged sword", "A sword that seems as worn as its original wielder.", 2, 5)];
        internal override List<Item> drops { get; set; }

        internal override RarityEnum rarity => throw new NotImplementedException();
        internal override void OnTurn()
        {
            if (health > 0)
            {
                Attack(factions.ReturnMostHatedCombatant(othersInBattle));
            }
            Console.WriteLine(name + " heals 1 health");
            Heal(1);
        }
        internal Zombie()
        {
            Dictionary<FactionEnum, int> goblinPriorities = new Dictionary<FactionEnum, int>();
            goblinPriorities.Add(FactionEnum.Player, -2);
            goblinPriorities.Add(FactionEnum.Humans, -1);
            goblinPriorities.Add(FactionEnum.Undead, 1);
            List<FactionEnum> joinedFactions = [FactionEnum.Undead];
            factions = new CombatantFactions(goblinPriorities, joinedFactions, repChangedCombatants);
        }
    }
}

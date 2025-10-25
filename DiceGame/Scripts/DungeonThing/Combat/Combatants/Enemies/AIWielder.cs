using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants.Enemies
{
    internal abstract class AIWielder : WeaponWielding
    {
        internal abstract int repDecreaseOnHit {  get; }
        internal abstract List<Item> drops { get; set; }
        internal abstract RarityEnum rarity { get; }
        internal override int HandleDamageModifiers(int damageToEdit, DamageTypeEnum type)
        {
            return damageToEdit;
        }
        internal override void OnTurn()
        {
            if (health > 0)
            {
                Attack(factions.ReturnMostHatedCombatant(othersInBattle));
            }
        }
        internal override void ExtraDeathThings()
        {
            Console.WriteLine(name + " died!");
            string dropText = name + " dropped:\n";
            for (int i = 0; i < equipedWeapons.Count; i++)
            {
                Item item = equipedWeapons[i];
                dropText += (i + 1).ToString() + ". " + item.name + "\n";
                room.items.Add(item);
                equipedWeapons.RemoveAt(i);
                i = 0;
            }
            if(drops != null)
            {
                for (int i = 0; i < drops.Count; i++)
                {
                    Item item = drops[i];
                    dropText += (i + 1).ToString() + item.name + "\n";
                    room.items.Add(item);
                    drops.RemoveAt(i);
                    i = 0;
                }
            }
            Console.WriteLine(dropText);
        }
        internal override bool ExtraCleanOthers(Combatant combatant)
        {
            if (combatant.health <= 0)
            {
                othersInBattle.Remove(combatant);
                return true;
            }
            return false;
        }
        internal override void HandleAttackerRepChange(Combatant combatantIn)
        {
            if (repChangedCombatants.ContainsKey(combatantIn))
            {
                repChangedCombatants[combatantIn] -= repDecreaseOnHit;
            }
            else
            {
                repChangedCombatants.Add(combatantIn, -repDecreaseOnHit);
            } 
        }
    }
}

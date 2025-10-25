using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants
{
    internal abstract class Combatant
    {
        internal CombatantFactions factions { get; set; }
        internal abstract string name { get; set; }
        internal abstract int health { get; set; }
        internal abstract int maxHealth { get; set; }
        internal abstract int baseDamage { get; set; }
        internal abstract DamageTypeEnum damageType { get; }
        internal List<Combatant> othersInBattle;
        internal abstract int HandleDamageModifiers(int damageToEdit, DamageTypeEnum type);
        internal abstract void OnTurn();
        internal abstract void ExtraDeathThings();
        internal abstract void HandleAttackerRepChange(Combatant combatantIn);
        internal abstract void Attack(Combatant target);
        internal abstract bool ExtraCleanOthers(Combatant combatant);
        internal Dictionary<Combatant, int> repChangedCombatants { get; private set; } = new Dictionary<Combatant, int>();
        internal Room room;
        internal void TakeDamage(int damage, DamageTypeEnum type, Combatant attacker)
        {
            TakeDamage(damage, type);
            if(health > 0)
            {
                HandleAttackerRepChange(attacker);
            }
        }
        internal void TakeDamage(int damage, DamageTypeEnum type)
        {
            bool aliveBefore = false;
            if (health > 0)
            {
                aliveBefore = true;
            }
            int modifiedDamage = HandleDamageModifiers(damage, type);
            health -= modifiedDamage;
            if (aliveBefore && health <= 0)
            {
                Die();
            }
        }
        internal void Heal(int amount)
        {
            if(health < maxHealth)
            {
                health += amount;
                if(health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }
        internal void Die()
        {
            ExtraDeathThings();
        }
        internal bool StillEnemyFactions()
        {
            return factions.StillEnemies(othersInBattle);
        }
        internal void AddOthersInBattle(List<Combatant> others)
        {
            othersInBattle = others.ToList();
            othersInBattle.Remove(this);
            for (int i = 0; i < othersInBattle.Count; i++)
            {
                Combatant other = othersInBattle[i];
                if (ExtraCleanOthers(other))
                {
                    i = 0;
                }
            }
        }
    }
}

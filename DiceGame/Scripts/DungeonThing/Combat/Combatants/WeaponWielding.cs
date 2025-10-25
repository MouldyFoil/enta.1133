using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat.Combatants
{
    internal abstract class WeaponWielding : Combatant
    {
        internal abstract List<Weapon> equipedWeapons { get; set; }
        internal abstract int hands { get; set; }
        internal override void Attack(Combatant target)
        {
            string displayText = "\n" + name + " attacks " + target.name + " with ";
            if (equipedWeapons != null && equipedWeapons.Count() > 0)
            {
                int index = 0;
                foreach(Weapon weapon in equipedWeapons)
                {
                    if (index == equipedWeapons.Count() - 1)
                    {
                        displayText += " and ";
                    }
                    else if(index > 0)
                    {
                        displayText += ", ";
                    }
                    displayText += weapon.name;
                    target.TakeDamage(weapon.ReturnDamageAndDecreaseUses(out DamageTypeEnum type) + baseDamage, type, this);
                    index++;
                }
                for (int i = 0; i < equipedWeapons.Count; i++)
                {
                    Weapon weapon = equipedWeapons[i];
                    if (weapon.uses <= 0)
                    {
                        equipedWeapons.Remove(weapon);
                        i = 0;
                    }
                }
            }
            else
            {
                displayText += "their bare hands!";
                target.TakeDamage(baseDamage, damageType, this);
            }
            Console.WriteLine(displayText);
        }
        internal bool EquipWeapon(Weapon weaponToEquip)
        {
            if (weaponToEquip.handsRequired + RetrunHandsUsed() <= hands)
            {
                equipedWeapons.Add(weaponToEquip);
                return true;
            }
            return false;
        }
        internal void DisplayEquipedWeapons()
        {
            int weaponNumber = 1;
            foreach(Weapon weapon in equipedWeapons)
            {
                Console.WriteLine(weaponNumber + ". " + weapon.name);
                weaponNumber++;
            }
        }
        internal int RetrunHandsUsed()
        {
            int sum = 0;
            foreach (Weapon weapon in equipedWeapons)
            {
                sum += weapon.handsRequired;
            }
            return sum;
        }
        internal int ReturnAttackDamage()
        {
            int damage = baseDamage;
            foreach(Weapon weapon in equipedWeapons)
            {
                damage += weapon.damage;
            }
            return damage;
        }
    }
}

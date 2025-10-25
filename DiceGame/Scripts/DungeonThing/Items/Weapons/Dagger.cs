using DiceGame.Scripts.DungeonThing.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items.Weapons
{
    internal class Dagger(string name = "Simple Dagger", string description = "A simple dagger that does 1 piercing damage", int damage = 2, int uses = 10, RarityEnum rarity = RarityEnum.uncommon) : Weapon
    {
        internal override string name { get; } = name;
        internal override string description { get; } = description;
        internal override float size { get; } = 1;
        internal override int damage { get; set; } = damage;
        internal override int uses { get; set; } = uses;
        internal override int handsRequired { get; } = 1;
        internal override DamageTypeEnum type { get; } = DamageTypeEnum.Piercing;
        internal override RarityEnum rarity { get; set; } = rarity;

        internal override void UseEvent()
        {
            Console.WriteLine(description);
        }
    }
}

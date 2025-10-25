using DiceGame.Scripts.DungeonThing.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Items.Weapons
{
    internal class Longsword(string name = "Basic longsword", string description = "A basic longsowrd that does 5 damage", int damage = 5, int uses = 10, RarityEnum rarity = RarityEnum.uncommon) : Weapon
    {
        internal override string name { get; } = name;
        internal override string description { get; } = description;
        internal override float size { get; } = 4;
        internal override int damage { get; set; } = damage;
        internal override int uses { get; set; } = uses;
        internal override int handsRequired { get; } = 2;
        internal override DamageTypeEnum type { get; } = DamageTypeEnum.Slashing;
        internal override RarityEnum rarity { get; set; } = rarity;

        internal override void UseEvent()
        {
            Console.WriteLine(description);
        }
    }
}

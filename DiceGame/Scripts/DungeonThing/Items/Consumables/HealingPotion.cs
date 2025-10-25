using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DiceGame.Scripts.DungeonThing.Items.Consumables
{
    internal class HealingPotion(string nameIn = "Healing potion", int healAmountIn = 10, string descriptionIn = "A standard healing potion that heals 10 health", RarityEnum rarityIn = RarityEnum.uncommon, int usesIn = 1, int sizeIn = 1) : Consumable
    {
        internal int healAmount = healAmountIn;
        internal override string name => nameIn;
        internal override string description => descriptionIn;

        internal override float size => sizeIn;

        internal override int uses { get; set; } = usesIn;
        internal override RarityEnum rarity { get; set; } = rarityIn;

        internal override string AdditionalItemInfo(string info)
        {
            return " HP ON USE: " + healAmount;
        }
        internal override void UseEvent(Combatant user)
        {
            user.Heal(healAmount);
        }
    }
}

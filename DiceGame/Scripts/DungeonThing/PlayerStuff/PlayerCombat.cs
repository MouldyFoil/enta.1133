using DiceGame.Scripts.DungeonThing.Combat;
using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using DiceGame.Scripts.DungeonThing.Items;
using DiceGame.Scripts.DungeonThing.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.PlayerStuff
{
    internal class PlayerCombat : WeaponWielding
    {
        bool endTurn = false;
        Player player = DungeonGameLoop.player;
        List<KeyValuePair<int, string>> extraDeadFlavors =
        [
            new KeyValuePair<int, string>(0, "Dead"),
            new KeyValuePair<int, string>(-5, "Brutalized"),
            new KeyValuePair<int, string>(-10, "Mangled"),
            new KeyValuePair<int, string>(-30, "Unrecognizable")
        ];
        internal override string name { get; set; } = "Player";
        internal override int maxHealth { get; set; } = 50;
        internal override int hands { get; set; } = 2;
        internal override int health { get; set; } = 50;
        internal override int baseDamage { get; set; } = 1;
        internal override DamageTypeEnum damageType { get; } = DamageTypeEnum.Bludgeoning;
        internal override List<Weapon> equipedWeapons { get; set; } = new List<Weapon>();
        internal PlayerCombat(Player playerIn)
        {
            player = playerIn;
            Dictionary<FactionEnum, int> priorities = new Dictionary<FactionEnum, int>();
            List<FactionEnum> joinedFactions = [FactionEnum.Player, FactionEnum.Humans];
            factions = new CombatantFactions(priorities, joinedFactions, repChangedCombatants);
        }
        internal override int HandleDamageModifiers(int damageToEdit, DamageTypeEnum type)
        {
            return damageToEdit;
        }
        internal override void OnTurn()
        {
            endTurn = false;
            while (!endTurn)
            {
                endTurn = true;
                Console.WriteLine();
                DisplayCombatInfo();
                int num = 1;
                foreach (Combatant combatant in othersInBattle)
                {
                    string combatantText = num + " ";
                    if (combatant.health <= 0)
                    {
                        combatantText += "(";
                        string deathText = "";
                        foreach (KeyValuePair<int, string> flavor in extraDeadFlavors)
                        {
                            if (combatant.health <= flavor.Key)
                            {
                                deathText = flavor.Value;
                            }
                        }
                        combatantText += deathText;
                        combatantText += ") ";
                    }
                    combatantText += combatant.name + "(HP: " + combatant.health + "/" + combatant.maxHealth + ")";
                    combatantText += " FRIENDLINESS: " + combatant.factions.DetermineCreatureRep(this);
                    Console.WriteLine(combatantText);
                    if (combatant is WeaponWielding)
                    {
                        WeaponWielding weaponCombatant = (WeaponWielding)combatant;
                        weaponCombatant.DisplayEquipedWeapons();
                    }
                    Console.WriteLine();
                    num++;
                }
                ChooseOption();
            }
        }

        internal void DisplayCombatInfo()
        {
            Console.WriteLine("Current HP: " + health + "/" + maxHealth);
            if (equipedWeapons.Count() > 0)
            {
                string equipedWeaponsText = "Current equiped weapons: ";
                foreach (Weapon weapon in equipedWeapons)
                {
                    equipedWeaponsText += weapon.name + " | ";
                }
                Console.WriteLine(equipedWeaponsText);
            }
            else
            {
                Console.WriteLine("No weapons equiped.");
            }
            Console.WriteLine("Damage: " + ReturnAttackDamage() + " Base damage: " + baseDamage);
            Console.WriteLine("Hands used: " + RetrunHandsUsed() + "/" + hands);
        }

        internal void ChooseOption()
        {
            Console.WriteLine("What would you like to do?\n1. Attack\n2. Inventory\n3. Search room");
            bool actionChosen = false;
            while (!actionChosen)
            {
                actionChosen = true;
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if (othersInBattle.Count() == 0)
                        {
                            Attack(othersInBattle[0]);
                        }
                        else
                        {
                            DisplayEnemies();
                            actionChosen = SelectEnemy();
                        }
                        break;
                    case "2":
                        endTurn = player.OpenInventory(true);
                        break;
                    case "3":
                        player.SearchRoom();
                        break;
                    default:
                        actionChosen = false;
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }
        }
        internal bool SelectEnemy()
        {
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                string input = Console.ReadLine();
                if (input.ToLower() == "back")
                {
                    Console.WriteLine("Returning...");
                }
                else if (int.TryParse(input, out int intInput) && intInput <= othersInBattle.Count() && intInput > 0)
                {
                    Attack(othersInBattle[intInput - 1]);
                    return true;
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("INVALID INPUT");

                }
            }
            return false;
        }
        internal void DisplayEnemies()
        {
            Console.WriteLine("\nInput the number of the enemy you want to attack");
            int enemyNumber = 1;
            foreach (Combatant combatant in othersInBattle)
            {
                Console.WriteLine(enemyNumber + ". " + combatant.name);
                enemyNumber++;
            }
        }
        internal override void ExtraDeathThings()
        {
            DungeonGameLoop.gameLooping = false;
        }
        internal override bool ExtraCleanOthers(Combatant combatant)
        {
            return false;
        }
        internal override void HandleAttackerRepChange(Combatant combatantIn)
        {
            
        }
        internal void DecreaseArms()
        {
            hands--;
            if (hands < 0)
            {
                hands = 0;
            }
        }
    }
}

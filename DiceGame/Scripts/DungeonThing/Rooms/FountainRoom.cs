using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal class FountainRoom : Room
    {
        internal override string roomDesc => "There is a fountain in the center of the room.";
        bool positiveEffect;
        bool displayOpposite;
        bool drank = false;

        internal override void ExtraEnterBehavior()
        {
            if (!explored)
            {
                OnRoomSearched();
            }
        }

        internal override void OnRoomCreated()
        {
            positiveEffect = DungeonGameLoop.random.Next(3) != 0;
            displayOpposite = DungeonGameLoop.random.Next(4) == 0;
        }

        internal override void OnRoomSearched()
        {
            if (!drank)
            {
                string positiveText = "You have a good feeling about this fountain.";
                string negativeText = "The fountain looks fine.";
                if (positiveEffect && !displayOpposite || !positiveEffect && displayOpposite)
                {
                    Console.WriteLine(negativeText);
                }
                else if (positiveEffect && displayOpposite || !positiveEffect && !displayOpposite)
                {
                    Console.WriteLine(positiveText);
                }
                Console.WriteLine("Do you drink from the fountain?\nY/N\n");
                bool inputValid = false;
                while (!inputValid)
                {
                    inputValid = true;
                    string input = Console.ReadLine().ToLower();
                    if (input == "y" || input == "yes")
                    {
                        drank = true;
                        if (positiveEffect)
                        {
                            PositiveEffect();
                        }
                        else
                        {
                            NegativeEffect();
                        }
                    }
                    else if (input == "n" || input == "no")
                    {
                        
                    }
                    else
                    {
                        inputValid = false;
                        Console.WriteLine("INVALID INPUT");
                    }
                }
            }
        }
        private void PositiveEffect()
        {
            switch (DungeonGameLoop.random.Next(6))
            {
                case 0:
                    DungeonGameLoop.player.combat.hands++;
                    Console.WriteLine("You grow an additional arm!");
                    break;
                case 1:
                case 2:
                    DungeonGameLoop.player.combat.baseDamage++;
                    Console.WriteLine("You feel your strength increase.");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("Your vigor increases!");
                    DungeonGameLoop.player.combat.maxHealth += 5;
                    DungeonGameLoop.player.combat.Heal(5);
                    break;
            }
        }
        private void NegativeEffect()
        {
            switch (DungeonGameLoop.random.Next(8))
            {
                case 0:
                    DungeonGameLoop.player.combat.DecreaseArms();
                    Console.WriteLine("An arm falls off...");
                    break;
                case 1:
                case 2:
                    DungeonGameLoop.player.combat.maxHealth--;
                    Console.WriteLine("You feel less vigor coursing through you...");
                    break;
                case 3:
                case 4:
                    DungeonGameLoop.player.combat.baseDamage--;
                    Console.WriteLine("You feel weaker...");
                    break;
                case 5:
                case 6:
                case 7:
                    DungeonGameLoop.player.combat.health -= 5;
                    Console.WriteLine("The water singes you!");
                    break;
            }
        }
        internal override bool PopulateItemsSpecal()
        {
            return true;
        }
    }
}

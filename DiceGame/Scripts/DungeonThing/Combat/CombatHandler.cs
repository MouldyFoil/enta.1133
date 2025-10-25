using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using DiceGame.Scripts.DungeonThing.PlayerStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat
{
    internal class CombatHandler
    {
        internal List<Combatant> combatants = new List<Combatant>();

        internal void AttemptBeginCombat(List<Combatant> combatantsIn)
        {
            SetCombatantsList(combatantsIn);
            if (AssureFight() && CheckForPlayer())
            {
                HandleCombat();
            }
        }

        private void SetCombatantsList(List<Combatant> combatantsIn)
        {
            combatants = [DungeonGameLoop.player.combat];
            foreach (Combatant combatant in combatantsIn)
            {
                if (combatant.health > 0)
                {
                    combatants.Add(combatant);
                }
            }
            HandleOtherCombatantLists();
        }

        private void HandleOtherCombatantLists()
        {
            foreach (Combatant combatant in combatants)
            {
                combatant.AddOthersInBattle(combatants);
            }
        }

        private void HandleCombat()
        {
            int turnNum = 0;
            while (TwoOrMoreCombatantsAlive() && CheckForPlayer() && AssureFight())
            {
                if(turnNum >= combatants.Count())
                {
                    turnNum = 0;
                }
                else
                {
                    combatants[turnNum].OnTurn();
                    turnNum++;
                    HandleOtherCombatantLists();
                    /*if (combatants[turnNum].health > 0)
                    {
                        combatants[turnNum].OnTurn();
                        turnNum++;
                    }
                    else
                    {
                        combatants.RemoveAt(turnNum);
                        HandleOtherCombatantLists();
                    }*/
                }
                if (!TwoOrMoreCombatantsAlive() || !AssureFight())
                {
                    DungeonGameLoop.player.fightsWon++;
                }
            }
        }
        private bool TwoOrMoreCombatantsAlive()
        {
            List<Combatant> aliveCombatants = new List<Combatant>();
            foreach(Combatant combatant in combatants)
            {
                if (combatant.health > 0)
                {
                    aliveCombatants.Add(combatant);
                    if(aliveCombatants.Count() > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckForPlayer()
        {
            foreach (Combatant combatant in combatants)
            {
                if(combatant is PlayerCombat && combatant.health > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private bool AssureFight()
        {
            foreach(Combatant combatant in combatants)
            {
                if (combatant.StillEnemyFactions())
                {
                    return true;
                }
            }
            return false;
        }
    }
}

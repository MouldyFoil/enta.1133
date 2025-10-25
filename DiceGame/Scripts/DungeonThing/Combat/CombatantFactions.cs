using DiceGame.Scripts.DungeonThing.Combat.Combatants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Combat
{
    internal class CombatantFactions()
    {
        internal Dictionary<FactionEnum, int> factionOpinions = new Dictionary<FactionEnum, int>();
        internal List<FactionEnum> joinedFactions = new List<FactionEnum>();
        internal Dictionary<Combatant, int> repChangedCombatants = new Dictionary<Combatant, int>();
        internal CombatantFactions(Dictionary<FactionEnum, int> factionPrioritiesCons, List<FactionEnum> joinedFactionsCons, Dictionary<Combatant, int> reputationAlteredCombatants) : this()
        {
            factionOpinions = factionPrioritiesCons;
            joinedFactions = joinedFactionsCons;
            repChangedCombatants = reputationAlteredCombatants;
        }
        internal bool StillEnemies(List<Combatant> combatants)
        {
            if (ReturnLowestRep(combatants) < 0)
            {
                return true;
            }
            return false;
        }
        internal Combatant ReturnMostHatedCombatant(List<Combatant> combatants)
        {
            return FindHatedOrLovedCombatant(combatants, out int noUse, true);
        }
        internal int ReturnLowestRep(List<Combatant> combatants)
        {
            FindHatedOrLovedCombatant(combatants, out int lowestRep, true);
            return lowestRep;
        }
        internal Combatant ReturnMostLovedCombatant(List<Combatant> combatants)
        {
            return FindHatedOrLovedCombatant(combatants, out int noUse, false);
        }
        internal int ReturnHighestRep(List<Combatant> combatants)
        {
            FindHatedOrLovedCombatant(combatants, out int lowestRep, false);
            return lowestRep;
        }
        private Combatant FindHatedOrLovedCombatant(List<Combatant> combatants, out int bestOrWorstRep, bool hated)
        {
            bestOrWorstRep = 0;
            List<Combatant> selectedCombatants = new List<Combatant>();
            Combatant hatedOrLovedCombatant = null;
            foreach (Combatant combatant in combatants)
            {
                int determinedRep = DetermineCreatureRep(combatant);
                if (hated && determinedRep < bestOrWorstRep)
                {
                    bestOrWorstRep = determinedRep;
                    selectedCombatants.Add(combatant);
                }
                else if(!hated && determinedRep > bestOrWorstRep)
                {
                    bestOrWorstRep = determinedRep;
                    selectedCombatants.Add(combatant);
                }
            }
            if(selectedCombatants.Count > 0)
            {
                hatedOrLovedCombatant = selectedCombatants[DungeonGameLoop.random.Next(selectedCombatants.Count())];
            }
            return hatedOrLovedCombatant;
        }
        internal int DetermineCreatureRep(Combatant combatant)
        {
            int rep = 0;
            foreach (FactionEnum faction in combatant.factions.joinedFactions)
            {
                if (factionOpinions.ContainsKey(faction))
                {
                    rep += factionOpinions[faction];
                }
            }
            if (repChangedCombatants.ContainsKey(combatant))
            {
                rep += repChangedCombatants[combatant];
            }
            return rep;
        }
    }
    internal enum FactionEnum
    {
        None = 0,
        Player = 1,
        Goblins = 2,
        Humans = 3,
        Beast = 4,
        Undead = 5,
    }
}

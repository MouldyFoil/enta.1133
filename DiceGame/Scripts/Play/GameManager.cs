using DiceGame.Scripts.Display;
using DiceGame.Scripts.Menus;
using DiceGame.Scripts.MultiInstance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.Play
{
    internal class GameManager(Settings settingsConst)
    {
        Random random = new Random();
        Settings settings = settingsConst;
        DisplayRound display = new DisplayRound();
        PlayerActions actions;
        internal List<Player> players = [new Player(false, "Person"), new Player(true, "Computer")];
        internal List<int> diceOptions = [6, 8, 12, 20];
        bool userReplays = true;
        internal void StartingActions()
        {
            actions = new PlayerActions(display, random);
        }
        internal void Play()
        {
            userReplays = true;
            while (userReplays)
            {
                MatchStartupActions();
                for (int roundsLeft = diceOptions.Count; roundsLeft > 0; roundsLeft--)
                {
                    HandleRound();
                }
                DetermineMatchWinner();
                Console.WriteLine("\n\n1. Restart same settings");
                Console.WriteLine("2. Main menu");
                ReplayMenuInputs();
            }
        }
        private void ReplayMenuInputs()
        {
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                if (int.TryParse(Console.ReadLine(), out int parsedNum))
                {
                    switch (parsedNum)
                    {
                        case 1:
                            
                            break;
                        case 2:
                            userReplays = false;
                            break;
                    }
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("INVALID INPUT");
                }
            }
        }
        private void MatchStartupActions()
        {
            foreach (Player player in players)
            {
                player.score = 0;
            }
            players = settings.players;
            diceOptions = settings.diceOptions;
            SetPlayersDice(diceOptions, players);
            display.DisplayPlayerIntroduction(players);
            Console.WriteLine("Determining turn order.");
            players = RandomizePlayerList(players);
            Console.WriteLine(players[0].name + " goes first.");
        }
        private List<Player> RandomizePlayerList(List<Player> playersInput)
        {
            List<Player> playersToOrder = playersInput.ToList();
            List<Player> playersOrdered = new List<Player>();
            while(playersOrdered.Count < playersInput.Count)
            {
                int index = random.Next(playersToOrder.Count);
                playersOrdered.Add(playersToOrder[index]);
                playersToOrder.RemoveAt(index);
            }
            return playersOrdered;
        }
        private void HandleRound() //Runs through one round of the game.
        {
            foreach(Player player in players)
            {
                Console.WriteLine("\n\n" + player.name + "'s turn.\n");
                if (player.isCPU)
                {
                    actions.ComputerTurn(player);
                }
                else
                {
                    actions.UserTurn(player);
                }
                display.DisplayTurn(player);
            }
            DetermineRoundWinner(players);
        }

        private void SetPlayersDice(List<int> sides, List<Player> playersInput) //Sets the dice list to a set of dice with int sides amount of sides.
        {
            foreach(Player player in playersInput) { player.dice = new List<Die>(); }
            foreach (int side in sides)
            {
                foreach (Player player in playersInput) { player.dice.Add(new Die(side)); }
            }
        }
        private void DetermineRoundWinner(List<Player> playersInput) //Determines round winner and rerolls if it is a tie.
        {
            List<Player> topPlayers = playersInput.ToList();
            int highestRoll = FindHighestRoll(topPlayers);
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (topPlayers[i].ReturnActiveDieRoll() < highestRoll)
                {
                    topPlayers.RemoveAt(i);
                    i = 0;
                }
            }
            if (topPlayers.Count > 1)
            {
                display.DisplayTieText(topPlayers);
                Console.WriteLine("Rerolling...");
                HandleRollTie(topPlayers);
            }
            else
            {
                Player winningPlayer = topPlayers[0];
                winningPlayer.score++;
                winningPlayer.winningRolls.Add(winningPlayer.ReturnActiveDieRoll());
                Console.WriteLine("\n\nWINNNER------\n" + winningPlayer.name + " won the round!");
                Console.WriteLine(winningPlayer.name + " now has " + winningPlayer.score.ToString() + " score.");
            }
        }
        private int FindHighestRoll(List<Player> playersInput)
        {
            int highestRoll = 0;
            foreach (Player player in playersInput)
            {
                if (player.ReturnActiveDieRoll() > highestRoll)
                {
                    highestRoll = player.ReturnActiveDieRoll();
                }
            }
            Console.WriteLine("\nThe highest roll was " +  highestRoll);
            return highestRoll;
        }
        private void HandleRollTie(List<Player> playersInput)
        {
            foreach (Player player in playersInput)
            {
                actions.RerollDie(player);
                display.DisplayRolledNumber(player);
            }
            DetermineRoundWinner(playersInput);
        }
        private void DetermineMatchWinner()
        {
            Console.WriteLine("\n\nMATCH RESULTS------\n");
            List<Player> topPlayers = players.ToList();
            int highestScore = FindHighestScore(topPlayers);
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (topPlayers[i].score < highestScore)
                {
                    topPlayers.RemoveAt(i);
                    i = 0;
                }
            }
            if (topPlayers.Count > 1)
            {
                HandleMatchTie(topPlayers);
            }
            else
            {
                Player winningPlayer = topPlayers[0];
                Console.WriteLine(winningPlayer.name + " won the match with a score of " + winningPlayer.score.ToString() + "!");
            }
        }

        private void HandleMatchTie(List<Player> topPlayers)
        {
            int topSum = 0;
            display.DisplayTieText(topPlayers);
            Console.WriteLine("Lets see who has a bigger sum of winning rolls.");
            foreach (Player player in topPlayers)
            {
                string winningRollText = "";
                winningRollText = player.name + "'s winning rolls were ";
                int index = 0;
                foreach (int roll in player.winningRolls)
                {
                    if (index == players.Count - 1)
                    {
                        winningRollText += " and ";
                    }
                    else if (index > 0)
                    {
                        winningRollText += ", ";
                    }
                    winningRollText += roll;
                    index++;
                }
                Console.WriteLine(winningRollText);
                Console.WriteLine("with a sum of " + player.SumOfWinningRolls());
                if(player.SumOfWinningRolls() > topSum)
                {
                    topSum = player.SumOfWinningRolls();
                }
            }
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (topPlayers[i].SumOfWinningRolls() < topSum)
                {
                    topPlayers.RemoveAt(i);
                }
            }
            if (topPlayers.Count() > 1)
            {
                string unbreakableTieText = "UNBREAKABLE TIE BETWEEN ";
                int index = 0;
                foreach (Player player in topPlayers)
                {
                    if(index > 0)
                    {
                        unbreakableTieText += ", ";
                    }
                    else if(index == topPlayers.Count() - 1)
                    {
                        unbreakableTieText += "AND ";
                    }
                    unbreakableTieText += player.name.ToUpper();
                    index++;
                }
                Console.WriteLine(unbreakableTieText + "!");
            }
            else
            {
                Console.WriteLine(topPlayers[0].name + " won the tie breaker");
            }
        }

        private int FindHighestScore(List<Player> playersInput)
        {
            int highestScore = 0;
            foreach (Player player in playersInput)
            {
                if (player.score > highestScore)
                {
                    highestScore = player.score;
                }
            }
            return highestScore;
        }
    }
}

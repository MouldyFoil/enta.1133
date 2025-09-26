using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts
{
    internal class GameController
    {
        Random random = new Random();
        internal Character player = new();
        internal Character computer = new();
        int[] defaultDiceOptions = [6, 8, 12, 20];
        internal void Play()
        {
            SetCharacterDice(defaultDiceOptions);
            Console.WriteLine("Welcome to my game - Zoey Sep 25th 2025");
            Console.WriteLine("What is your name?");
            AssignCharatcerName(player);
            Console.WriteLine("Nice name, " + player.name + ".");
            Console.WriteLine("but, what is the name of me, your opponent?");
            AssignCharatcerName(computer);
            Console.WriteLine(computer.name + "... I like that name!");
            Console.WriteLine("Lets see who goes first.");
            bool playerFirst = random.Next(2) == 0;
            if(playerFirst)
            {
                Console.WriteLine(player.name + " goes first.");
            }
            else
            {
                Console.WriteLine(computer.name + " goes first.");
            }
            HandleRound(playerFirst);
            Console.WriteLine("Thats all there is for now, goodbye!");
        }

        private void HandleRound(bool playerGoesFirst) //Runs through one round of the game.
        {
            if (playerGoesFirst)
            {
                PlayerTurn();
                ComputerTurn();
            }
            else
            {
                ComputerTurn();
                PlayerTurn();
            }
            DetermineRoundWinner();
        }

        private void SetCharacterDice(int[] sides) //Sets the dice list to a set of dice with int sides amount of sides.
        {
            player.dice = new List<Die>();
            computer.dice = new List<Die>();
            foreach (int side in sides)
            {
                player.dice.Add(new Die(side));
                computer.dice.Add(new Die(side));
            }
        }

        private void AssignCharatcerName(Character character) //Sets name of specified character to console input.
        {
            character.name = Console.ReadLine();
            if (character.name == null || character.name == "")
            {
                Console.WriteLine("Thats an empty line! please input a name.");
                AssignCharatcerName(character);
            }
        }
        private void PlayerTurn() //Handles a player turn and takes input.
        {
            Console.WriteLine(player.name + "'s turn.");
            Console.WriteLine("Input amount of sides of the dice you want to roll.");
            Console.WriteLine("Options: 6, 8, 12, 20");
            if (int.TryParse(Console.ReadLine(), out int parsedNum) && CheckDiceInputValidity(parsedNum, out int selectedDie))
            {
                RollDie(player, selectedDie);
                DisplayTurn(player);
            }
            else
            {
                Console.WriteLine("INVALID INPUT");
                PlayerTurn();
            }
        }
        private void ComputerTurn() //Handles the computer turn, randomizing the die chosen by the computer.
        {
            RollDie(computer, random.Next(computer.dice.Count - 1));
            DisplayTurn(computer);
        }

        private void DisplayTurn(Character character) //Displays the most recent actions taken by specified character.
        {
            Console.WriteLine(character.name + "'s turn.");
            Console.WriteLine(character.name + " decides to roll a D" + character.ReturnActiveDieSides() + ".");
            DisplayRolledNumber(character);
        }

        private static void DisplayRolledNumber(Character character) //Displays the most recent roll of a specified character and adds flare if certain conditions are met.
        {
            Console.WriteLine(character.name + " rolled a " + character.ReturnActiveDieRoll() + ".");
            if(character.ReturnActiveDieRoll() == character.ReturnActiveDieSides())
            {
                if(character.ReturnActiveDieRoll() == 20)
                {
                    Console.WriteLine("NAT 20!");
                }
                else
                {
                    Console.WriteLine("MAXIMUM!");
                }
            }
            else if(character.ReturnActiveDieRoll() > (float)(character.ReturnActiveDieSides() + 1) / 2)
            {
                Console.WriteLine("Above average!");
            }
            else if(character.ReturnActiveDieRoll() == 1)
            {
                Console.WriteLine("Nat 1 :(");
            }
        }
        private void RerollDie(Character character) //Rerolls active die of specified character and determines the round winner.
        {
            RollDie(character, character.activeDieIndex);
            DisplayRolledNumber(character);
            DetermineRoundWinner();
        }
        private void RollDie(Character character, int dieIndex) //Rolls a die of specified character and sets the active die index to specified amount.
        {
            character.dice[dieIndex].Roll();
            character.activeDieIndex = dieIndex;
        }
        private bool CheckDiceInputValidity(int inputSides, out int dieIndex) //Checks if player input is valid and outputs the index of the selected die.
        {
            int index = 0;
            foreach(Die die in player.dice)
            {
                if(die.sides == inputSides)
                {
                    dieIndex = index;
                    return true;
                }
                index++;
            }
            dieIndex = 0; //if this happens, it means that the player input is invalid and dieIndex will not be used. The only reason it is zero is because i didnt want the computer to yell at me.
            return false;
        }
        private void DetermineRoundWinner() //Determines round winner and rerolls if it is a tie.
        {
            int playersLastRoll = player.ReturnActiveDieRoll();
            int computersLastRoll = computer.ReturnActiveDieRoll();
            if (playersLastRoll > computersLastRoll)
            {
                player.score++;
                Console.WriteLine(player.name + " wins!");
            }
            else if(playersLastRoll < computersLastRoll)
            {
                computer.score++;
                Console.WriteLine(computer.name + " wins!");
            }
            else
            {
                Console.WriteLine("Tie...");
                Console.WriteLine("Rerolling dice...");
                RerollDie(player);
                RerollDie(computer);
            }
        }
        /*internal string DisplayRandomExample(int operationIndex) //Creates a random example using the operator of index param
{
   int maxNumExample = 11; //the max random number (exclusive)
   string[] operations = ["+", "-", "/", "*", "%"]; //The possible operations in string format
   if (operationIndex >= operations.Length)
   {
       return "that operation is not accounted for";
   }
   Random random = new Random();
   int number1 = random.Next(1, maxNumExample); //num left of operator
   int number2 = random.Next(1, maxNumExample); //num right of operator
   string finalText; //the text to be returned
   float operationResult = 0;
   finalText = "Ex. " + number1.ToString() + " " + operations[operationIndex] + " " + number2.ToString(); //sets finalText to a random equation with the operators
   switch(operationIndex) //finds operation based on index and sets the operation result to the result of the equation where number1 is on the left side of the operator
                          //and number2 is on the right side of the operator
   {
       case 0:
           operationResult = number1 + number2;
           break;
       case 1:
           operationResult = number1 - number2;
           break;
       case 2:
           operationResult = (float)number1 / (float)number2;
           break;
       case 3:
           operationResult = number1 * number2;
           break;
       case 4:
           operationResult = number1 % number2;
           break;
   }
   finalText += " = " + operationResult.ToString(); //adds the result of the equation to finalText
   return finalText;
}*/
    }
    
    internal class Character //The class for the player and opponent. I didnt make it in a seperate file because its only used in this script, hope thats okay.
    {
        internal string name;
        internal List<Die> dice = new List<Die>();
        internal int activeDieIndex;
        internal int score = 0;
        internal int ReturnActiveDieRoll()
        {
            return dice[activeDieIndex].rolledNum;
        }
        internal int ReturnActiveDieSides()
        {
            return dice[activeDieIndex].sides;
        }
        /*
        internal List<int> storedRolls;
        internal List<int> diceOptionsLeft;*/
    }
}

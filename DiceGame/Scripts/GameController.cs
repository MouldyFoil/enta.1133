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
        /*internal Character player = new();
        internal Character opponent = new();
        List<int> diceStartingOptions;*/
        int[] rolls = [6, 8, 12, 20];
        internal void Play()
        {
            Console.WriteLine("Welcome - Zoey Sep 17th 2025");
            DieRoller roller = new DieRoller();
            Console.WriteLine("Sum of rolls is " + roller.Roll(rolls));

            Console.WriteLine("+ is an operator that adds the number on either side together");
            Console.WriteLine(DisplayRandomExample(0));

            Console.WriteLine("- is an operator that subtracts the value on the right from the one on the left");
            Console.WriteLine(DisplayRandomExample(1));

            Console.WriteLine("/ is an operator that divides the value on the left by the one on the right");
            Console.WriteLine(DisplayRandomExample(2));

            Console.WriteLine("* is an operator that multiplies the the numbers on either side together");
            Console.WriteLine(DisplayRandomExample(3));

            Console.WriteLine("++ is a shorthand for += 1");
            Console.WriteLine("Ex. int num = 3 so num++ would make num equal to 4");

            Console.WriteLine("-- is a shorthand for -= 1");
            Console.WriteLine("Ex. int num = 7 so num-- would make num equal to 6");

            Console.WriteLine("%, the modulo operation returns the remainder of a division");
            Console.WriteLine(DisplayRandomExample(4));

            Console.WriteLine("Thanks for reading, goodbye");
        }
        internal string DisplayRandomExample(int operationIndex) //Creates a random example using the operator of index param
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
        }
    }
    /*
    internal class Character
    {
        internal string name;
        internal int score;
        internal List<int> storedRolls;
        internal List<int> diceOptionsLeft;
    }*/
}

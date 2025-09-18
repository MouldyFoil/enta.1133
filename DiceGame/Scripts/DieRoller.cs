using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts
{
    internal class DieRoller
    {
        internal int Roll(int[] sides)
        {
            Random random = new Random();
            int sum = 0; //int that contains the sum of all rolls
            string diceRolledText = "Rolling a "; //string that contains which dice were rolled
            string resultText = "The results are "; //string that contains the result of each roll
            int index = 0; //stores the current index of the side in the foreach loop
            foreach (int side in sides)
            {
                int rollResult = random.Next(1, side + 1); //rolls a dice with given sides + 1 is there to make maximum inclusive
                if(index + 1 < sides.Length) //Adds a comma if not the last number
                {
                    diceRolledText += "D" + side.ToString() + ", ";
                    resultText += rollResult.ToString() + ", ";
                }
                else //Adds extra text to the last number
                {
                    diceRolledText += "and a D" + side.ToString();
                    resultText += "and " + rollResult.ToString();
                }
                sum += rollResult; //Adds each rollresult
                index++;
            }
            Console.WriteLine(diceRolledText);
            Console.WriteLine(resultText);
            return sum;
        }
    }
}

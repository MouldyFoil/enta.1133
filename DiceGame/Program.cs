using DiceGame.Scripts.Menus;
using DiceGame.Scripts.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class Program
    {
        static void Main()
        {
            Settings settings = new Settings();
            GameManager gameManager = new GameManager(settings);
            MainMenuManager menuManager = new MainMenuManager(gameManager, settings);
            gameManager.StartingActions();
            menuManager.Introduction();
        }
    }
}

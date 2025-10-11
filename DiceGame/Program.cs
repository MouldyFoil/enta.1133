using DiceGame.Scripts.DiceGame.Menus;
using DiceGame.Scripts.DiceGame.Play;
using DiceGame.Scripts.DungeonThing;
using DiceGame.Scripts.DungeonThing.Rooms;
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
            DungeonGameLoop gameLoop = new DungeonGameLoop();
            gameLoop.BeginDungeonStuff();
            /*Settings settings = new Settings();
            GameManager gameManager = new GameManager(settings);
            MainMenuManager menuManager = new MainMenuManager(gameManager, settings);
            gameManager.StartingActions();
            settings.StartupThings();
            menuManager.Introduction();*/
        }
    }
}

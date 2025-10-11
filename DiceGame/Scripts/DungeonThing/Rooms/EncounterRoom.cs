using DiceGame.Scripts.DiceGame.Menus;
using DiceGame.Scripts.DiceGame.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Scripts.DungeonThing.Rooms
{
    internal class EncounterRoom : Room
    {
        internal override string roomDesc { get; } = "An enemy aproaches...";
        internal override void OnRoomSearched()
        {
            searched = true;
        }
        internal override void ExtraEnterBehavior()
        {
            Settings settings = new Settings();
            GameManager gameManager = new GameManager(settings);
            MainMenuManager menuManager = new MainMenuManager(gameManager, settings);
            gameManager.StartingActions();
            settings.StartupThings();
            menuManager.Introduction();
        }
    }
}

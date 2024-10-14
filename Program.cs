using System;
using SFML.Audio;
using SFML.Window;
using SFML.Graphics;
using PAS.Engine;

namespace PAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Game gameInstance = Game.GetInstance();
            gameInstance.InitWindow(1920, 1080, "PAS", true);

            Scene mainMenu = new Content.Scenes.MainMenu(gameInstance);

            gameInstance.SetScene(mainMenu);
            gameInstance.StartGame();

        }
    }
}
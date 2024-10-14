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
            AssetLoader.GetInstance().LoadTexture("button.png", "button");
            AssetLoader.GetInstance().LoadFont("upheavtt.ttf", "main");

            Game gameInstance = Game.GetInstance();
            gameInstance.InitWindow(1920, 1080, "PAS", true);

            Scene mainMenu = new Content.Scenes.MainMenu();

            gameInstance.SetScene(mainMenu);
            gameInstance.StartGame();

        }
    }
}
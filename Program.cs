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
            AssetLoader.GetInstance().LoadFont("Picopixel.ttf", "pico");

            AssetLoader.GetInstance().LoadTexture("class_select_bg.png", "class_selection_bg");
            AssetLoader.GetInstance().LoadTexture("class_frame.png", "class_frame");
            AssetLoader.GetInstance().LoadTexture("class_selector_button.png", "selector_button");
            
            AssetLoader.GetInstance().LoadTexture("healer_test.png", "healer");

            Game gameInstance = Game.GetInstance();
            gameInstance.InitWindow(1920, 1080, "PAS", true);

            Scene mainMenu = new Content.Scenes.MainMenu();

            gameInstance.SetScene(mainMenu);
            gameInstance.StartGame();

        }
    }
}
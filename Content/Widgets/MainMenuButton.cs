using PAS.Engine;
using SFML.Graphics;
using PAS.Content.Scenes;
using SFML.System;

namespace PAS.Content.Widgets
{
    internal class MainMenuButton : Engine.Button
    {
        public MainMenuButton() : base() 
        {
            sprite = new Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("MAIN MENU", AssetLoader.GetInstance().GetFont("main"), new Vector2f(3f,4));
            _text.Color = new Color(5,5,5);
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            Game.GetInstance().SetScene(new MainMenu());
            base.OnClick(eventArgs);
        }
    }
}

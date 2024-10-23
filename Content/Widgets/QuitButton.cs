using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Widgets
{
    internal class QuitButton : Engine.Button
    {
        public QuitButton() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("QUIT", AssetLoader.GetInstance().GetFont("main"), new Vector2f(18f, 4f));
            _text.Color = new Color(50, 50, 50);
        }
        public override void OnClick(System.EventArgs eventArgs)
        {
            
            Game.GetInstance().Quit();
            base.OnClick(eventArgs);
        }
    }
}

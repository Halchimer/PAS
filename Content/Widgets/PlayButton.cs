using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using EventArgs = PAS.Engine.EventArgs;

namespace PAS.Content.Widgets
{
    internal class TestEvent : Engine.Event
    {

    }

    internal class PlayButton : Engine.Button
    {
        public PlayButton() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("PLAY", AssetLoader.GetInstance().GetFont("main"), new Vector2f(18.0f, 4f));
            _text.Color = new Color(50, 50, 50);
        }
        
        public override void OnClick(System.EventArgs eventArgs)
        {
            Game.GetInstance().SetScene(new Scenes.ClassSelectionScene(parentScene));

            base.OnClick(eventArgs);
        }
    }
}

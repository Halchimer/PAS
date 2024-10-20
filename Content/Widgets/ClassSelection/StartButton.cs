using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Widgets
{
    internal class ConfirmCharacterEvent : Engine.Event { }
    internal class StartButton : Engine.Button
    {
        public StartButton() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));
            AddText("CONFIRM", AssetLoader.GetInstance().GetFont("main"), new Vector2f(9f, 4f));
            _text.Color = new Color(50, 50, 50);
        }
        public override void OnClick(System.EventArgs eventArgs)
        {

            PASEventHandler.GetInstance().TriggerEvent(new ConfirmCharacterEvent());
            base.OnClick(eventArgs);
        }
    }
}

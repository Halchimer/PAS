using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using EventArgs = PAS.Engine.EventArgs;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class ClassScrollLeftEvent : Engine.Event {}

    internal class ClassScrollButtonLeft : Engine.Button
    {
        public ClassScrollButtonLeft() : base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("selector_button"));
            sprite.Origin = new Vector2f(0, -16);
            sprite.TextureRect = new IntRect(0, 16, 16, 16);
            
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            PASEventHandler.GetInstance().TriggerEvent(new ClassScrollLeftEvent());
            base.OnClick(eventArgs);
        }

        public override void SetLocation(Vector2f location, bool snapSprite = true)
        {
            base.SetLocation(location + new Vector2f(0, -16f), snapSprite);
        }
    }
}

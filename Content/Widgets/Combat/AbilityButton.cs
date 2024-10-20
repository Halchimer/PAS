using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using EventArgs = PAS.Engine.EventArgs;

namespace PAS.Content.Widgets.Combat
{
    internal class AbilityButton : Engine.Button
    {
        PlayerActionSendEventArgs buttonEventArgs;
        public AbilityButton() : base()
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));

            AddText("SPELL", AssetLoader.GetInstance().GetFont("main"), new Vector2f(15f, 4f));
            _text.Color = new Color(50, 50, 50);
            
            buttonEventArgs = new PlayerActionSendEventArgs();
            buttonEventArgs.Action = CharacterActions.Ability;
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            base.OnClick(buttonEventArgs);
        }
    }
}

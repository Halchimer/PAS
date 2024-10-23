using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.Widgets.Combat
{
    internal class ParryButton : Engine.Button
    {
        PlayerActionSendEventArgs buttonEventArgs;
        public ParryButton() : base()
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("button"));

            AddText("PARRY", AssetLoader.GetInstance().GetFont("main"), new Vector2f(14f, 4f));
            _text.Color = new Color(50, 50, 50);
            
            buttonEventArgs = new PlayerActionSendEventArgs();
            buttonEventArgs.Action = CharacterActions.Parry;
        }

        public override void OnClick(System.EventArgs eventArgs)
        {
            base.OnClick(buttonEventArgs);
        }
    }
}
